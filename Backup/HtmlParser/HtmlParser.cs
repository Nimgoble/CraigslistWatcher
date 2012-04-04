using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace HtmlParser
{
    public class HtmlParser
    {
        protected string _html;
        protected int _pos;
        protected bool _scriptBegin;
        public List<HtmlTag> _nodes { get; set; }
        protected int _tag_count;

        public ParseFilter Filter;

        public HtmlParser()
        {
            _html = "";
            Reset();          
        }

        public bool ParseURL(string URL, bool remove_comments, string[] remove_tags)
        {
            try
            {
                WebRequest request = WebRequest.Create(URL);
                request.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse htmlresponse = (HttpWebResponse)request.GetResponse();

                Stream dataStream = htmlresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                string response = reader.ReadToEnd();
                Reset(response, remove_comments, remove_tags);
                return ParseHTML();
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Resets the current position to the start of the current document
        /// </summary>
        public void Reset()
        {
            _pos = _html.Length;
            _tag_count = 0;
            if (_nodes != null)
                _nodes.Clear();
            else
                _nodes = new List<HtmlTag>();
        }

        /// <summary>
        /// Sets the current document and resets the current position to the
        /// start of it
        /// </summary>
        /// <param name="html"></param>
        public void Reset(string html, bool remove_comments, string[] remove_tags)
        {
            _html = html;

            //Remove <br> tags
            if (remove_tags != null)
            {
                foreach (string tag in remove_tags)
                    _html = _html.Replace(tag, "");
            }

            //Remove comments
            if (remove_comments)
            {
                int start = _html.IndexOf("<!--");
                while (start != -1)
                {
                    int end = _html.IndexOf("-->", start);
                    if (end != -1)
                    {
                        _html = _html.Remove(start, (end + 3) - start);
                        start = _html.IndexOf("<!--");
                    }
                }
            }

            _pos = _html.Length;
            _tag_count = 0;
            if (_nodes != null)
                _nodes.Clear();
            else
                _nodes = new List<HtmlTag>();
        }

        /// <summary>
        /// Indicates if the current position is at the end of the current
        /// document
        /// </summary>
        public bool EOF()
        {
            return ((_pos <= 0) && (_pos >=_html.Length));
        }

        public bool EOF(int place)
        {
            return ((place <= 0) && (place >= _html.Length));
        }

        /// <summary>
        /// JH: Is the next node a comment
        /// </summary>
        public bool NextTagIsComment()
        {
            return (_html[_pos + 1] == '/' && _html[_pos + 2] == '-' && _html[_pos + 3] == '-') ||
             (_html[_pos + 1] == '!' && _html[_pos + 2] == '-' && _html[_pos + 3] == '-');
        }

        public bool NextTagIsComment(int offset)
        {
            return (_html[offset + 1] == '/' && _html[offset + 2] == '-' && _html[offset + 3] == '-') ||
                 (_html[offset + 1] == '!' && _html[offset + 2] == '-' && _html[offset + 3] == '-');
        }

        /// <summary>
        /// JH: Parses entire html stream in to HtmlTag format
        /// </summary>
        public bool ParseHTML()
        {
            while (MoveToNextTag())
            {
                HtmlTag node = new HtmlTag();
                node.Parent = null;
                int index = _pos;
                if (_html[_pos + 1] == '/')
                {
                    node.CloseTag_Start = index;
                    index += 2;
                    node.Name = ParseTagName(ref index);
                    _pos = node.CloseTag_Start;
                    if (node.Name != null)
                        if (ParseTag(ref node))
                            _pos = node.OpenTag_Start;
                        
                }
                else
                {
                    node.OpenTag_Start = _pos;
                    index++;
                    node.Name = ParseTagName(ref index);
                    ParseTagAttributes(ref node, ref index);
                }

                _nodes.Insert(0, node);
                
            }
            return true;
        }

        /// <summary>
        /// Parses the contents of an HTML tag. The current position should
        /// be at the first character following the tag's opening less-than
        /// character.
        /// 
        /// Note: We parse to the end of the tag even if this tag was not
        /// requested by the caller. This ensures subsequent parsing takes
        /// place after this tag
        /// </summary>
        /// <param name="name">Name of the tag the caller is requesting,
        /// or "*" if caller is requesting all tags</param>
        /// <param name="tag">Returns information on this tag if it's one
        /// the caller is requesting</param>
        /// <returns>True if data is being returned for a tag requested by
        /// the caller or false otherwise</returns>

        protected bool ParseTag( ref HtmlTag parent )
        {
            // Special handling
            /*bool doctype = _scriptBegin = false;
            if (String.Compare(s, "!DOCTYPE", true) == 0)
                doctype = true;
            else if (String.Compare(s, "script", true) == 0)
                _scriptBegin = true;*/

            bool found = false;
            while (!found && !EOF())
            {
                if (!MoveToNextTag())
                    break;

                int index = _pos;
                SkipWhitespace(ref index);

                if (_html[_pos + 1] == '/' && !NextTagIsComment())
                {
                    //Nested tag
                    parent.HadChildren = true;
                    HtmlTag child_tag = new HtmlTag();
                    child_tag.CloseTag_Start = _pos;
                    child_tag.Parent = parent;
                    index += 2;
                    SkipWhitespace(ref index);
                    child_tag.Name = ParseTagName(ref index);

                    if (child_tag.Name == "script")
                        continue;

                    ParseTag(ref child_tag);
                    parent.Children.Insert(0, child_tag);
                }
                else if (!NextTagIsComment())
                {
                    index++;
                    string name = ParseTagName(ref index);

                    if(name == "script")
                        continue;

                    SkipWhitespace(ref index);
                    if (name == parent.Name)
                    {
                        //Found it
                        parent.OpenTag_Start = _pos;
                        ParseTagAttributes(ref parent, ref index);
                        return true;
                    }
                    else
                    {
                        parent.HadChildren = true;

                        HtmlTag child_tag = new HtmlTag();
                        child_tag.Name = name;
                        child_tag.OpenTag_Start = _pos;
                        child_tag.Parent = parent;
                        ParseTagAttributes(ref child_tag, ref index);
                        parent.Children.Insert(0, child_tag);
                    }
                }

                /*if (parent.Children.Count > 0)
                    _pos = parent.Children.First().OpenTag_Start;
                else
                    _pos = tag_position;*/
            }
            
            return false;
        }

        /// <summary>
        /// Parses a tag name. The current position should be the first
        /// character of the name
        /// </summary>
        /// <returns>Returns the parsed name string</returns>
        protected string ParseTagName(ref int index)
        {
            int start = index;
            while (!EOF(index) && !Char.IsWhiteSpace(_html[index]) && _html[index] != '>')
                index++;
            return _html.Substring(start, index - start);
        }

        /// <summary>
        /// Parses a tag name. The given position should be the first
        /// character of the name
        /// </summary>
        /// <returns>Returns the parsed name string</returns>
       /* protected string ParseTagName(int place)
        {
            int start = place;
            while (!((place <= 0) && (place >= _html.Length)) && !Char.IsWhiteSpace(_html[place]) && _html[place] != '>')
                place++;

            return _html.Substring(start, place - start);
        }*/

        protected void ParseTagAttributes(ref HtmlTag tag, ref int index)
        {
            while (_html[index] != '>')
            {
                if (_html[index] == '/')
                {
                    // Handle trailing forward slash
                    tag.TrailingSlash = true;
                    index++;
                    SkipWhitespace(ref index);
                }
                else
                {
                    // Parse attribute name
                    string attribute_name = ParseAttributeName(ref index);
                    SkipWhitespace(ref index);
                    // Parse attribute value
                    string value = String.Empty;
                    if (_html[index] == '=')
                    {
                        index++;
                        SkipWhitespace(ref index);
                        value = ParseAttributeValue(ref index);
                        SkipWhitespace(ref index);
                    }
                    // This tag replaces existing tags with same name
                    if (tag.Attributes.Keys.Contains(attribute_name))
                        tag.Attributes.Remove(attribute_name);

                    tag.Attributes.Add(attribute_name, value);
                }
            }

            tag.OpenTag_Close = index;
            index++;
            int value_end = _html.IndexOf("<", index);
            if( value_end != -1 )
                tag.Value = _html.Substring(index, value_end - index);  
        }

        /// <summary>
        /// Parses an attribute name. The current position should be the
        /// first character of the name
        /// </summary>
        /// <returns>Returns the parsed name string</returns>
        protected string ParseAttributeName(ref int index)
        {
            int start = index;
            char c = _html[index];
            while (!EOF(index) && !Char.IsWhiteSpace(c) && c != '>' && c != '=')
                c = _html[++index];
            return _html.Substring(start, index - start);
        }

        /// <summary>
        /// Parses an attribute value. The current position should be the
        /// first non-whitespace character following the equal sign.
        /// 
        /// Note: We terminate the name or value if we encounter a new line.
        /// This seems to be the best way of handling errors such as values
        /// missing closing quotes, etc.
        /// </summary>
        /// <returns>Returns the parsed value string</returns>
        protected string ParseAttributeValue(ref int index)
        {
            int start, end;
            char c = _html[index];
            if (c == '"' || c == '\'')
            {
                // Move past opening quote
                index++;
                // Parse quoted value
                start = index;
                index = _html.IndexOfAny(new char[] { c, '\r', '\n' }, start);
                NormalizePosition(ref index);
                end = index;
                // Move past closing quote
                if (_html[index] == c)
                    index++;
            }
            else
            {
                // Parse unquoted value
                start = index;
                while (!EOF(index) && !Char.IsWhiteSpace(c) && c != '>')
                {
                    index++;
                    c = _html[index];
                }
                end = index;
            }
            return _html.Substring(start, end - start);
        }

        /// <summary>
        /// Moves to the start of the next tag
        /// </summary>
        /// <returns>True if another tag was found, false otherwise</returns>

        protected bool MoveToNextTag()
        {
            int pos = _pos;
            pos--;
            while (pos >= 0 )
            {
                if( _html[pos] == '<' && !NextTagIsComment(pos) )
                    break;
                pos--;
            }

            //Didn't find the next tag
            if (pos < 0)
                return false;

            _pos = pos;
            return true;
        }

        /// <summary>
        /// Moves the current position to the next character that is
        /// not whitespace
        /// </summary>
        protected void SkipWhitespace(ref int index)
        {
            while (!EOF(index) && Char.IsWhiteSpace(_html[index]))
                index++;
        }

        /// <summary>
        /// Normalizes the current position. This is primarily for handling
        /// conditions where IndexOf(), etc. return negative values when
        /// the item being sought was not found
        /// </summary>
        protected void NormalizePosition(ref int index)
        {
            if (index < 0)
                index = 0;
        }

        public override string ToString()
        {
            string rtn = String.Empty;
            for (int i = 0; i < _nodes.Count; i++)
                rtn += _nodes[i].ToString();

            return rtn;
        }

        public void PopulateTreeView(ref TreeView tree_view)
        {
            tree_view.CheckBoxes = true;

            foreach(HtmlTag tag in _nodes)
            {
                TreeNodeCollection nodes = tree_view.Nodes;
                tag.PopulateTreeView(ref nodes);
            }
        }

        public List<HtmlTag> FilterForChildrenByName(string name)
        {
            if (_nodes.Count == 0)
                return null;

            List<HtmlTag> rtn = new List<HtmlTag>();

            for (int i = 0; i < _nodes.Count; i++)
            {
                if (_nodes[i].Name == name)
                    rtn.Add(_nodes[i]);
            }

            if (rtn.Count != 0)
                return rtn;
            else
                return null;
        }

        public bool FilterNodes(ParseFilter filter, out HtmlTag parent)
        {
            parent = null;
            foreach (HtmlTag tag in _nodes)
            {
                if (tag.Name == filter.Name)
                {
                    if (filter.isParent())
                        parent = tag.VanillaCopy(null);
                    else
                        parent = new HtmlTag();

                    return _FilterNodes(ref filter, ref parent, tag);
                }
            }
            return false;
        }

        private bool _FilterNodes(ref ParseFilter filter, ref HtmlTag new_parent, HtmlTag current_parent)
        {
            if (!filter.AllChildren)
            {
                if (filter.AcceptableChildren.Count == 0)
                {
                    if (current_parent.Children.Count == 0)
                        return true;

                    return false;
                }
            }

            foreach (HtmlTag tag in current_parent.Children)
            {
                ParseFilter child_filter = null;
                if (filter.AllChildren || filter.AcceptableChildren.TryGetValue(tag.Name, out child_filter))
                {
                    if (child_filter.isParent())
                    {
                        new_parent = tag.VanillaCopy(null);
                        if (_FilterNodes(ref child_filter, ref new_parent, tag))
                        {
                            filter = child_filter;
                            return true;
                        }
                    }
                    else
                    {
                        HtmlTag new_child = tag.VanillaCopy(new_parent);
                        if (_FilterNodes(ref child_filter, ref new_child, tag))
                        {
                            if (child_filter.isParent())
                            {
                                filter = child_filter;
                                new_parent = new_child;
                                return true;
                            }
                            new_parent.Children.Add(new_child);
                        }
                    }
                }
            }

            if (filter.AcceptableChildren.Count > 0 && new_parent.Children.Count == 0)
                return false;

            return true;
        }

    }
}