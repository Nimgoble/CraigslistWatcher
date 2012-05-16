using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Drawing;

namespace HtmlParser
{
    public class HtmlParser
    {
        protected string _html;
        protected int _pos;
        protected bool _scriptBegin;
        public List<HtmlTag> _nodes { get; set; }
        protected int _tag_count;
        protected List<string> omitTags;
        public HtmlParser()
        {
            _html = "";
            omitTags = new List<string>();
            Reset();        
        }

        public void AddOmitTag(string tag)
        {
            if (tag != null && !omitTags.Contains(tag))
                omitTags.Add(tag);
        }
        public void AddOmitTags(List<string> tags)
        {
            if(tags != null)
                omitTags.AddRange(tags);
        }
        public void RemoveOmitTag(string tag)
        {
            if (tag != null && omitTags.Contains(tag))
                omitTags.Remove(tag);
        }
        public void ClearOmitTags()
        {
            omitTags.Clear();
        }

        public bool ParseRawHTML(string html, bool remove_comments)
        {
            Reset(html, remove_comments);
            return ParseHTML();
        }
        public bool ParseURL(string URL, bool remove_comments)
        {
            try
            {
                WebRequest request = WebRequest.Create(URL);
                request.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse htmlresponse = (HttpWebResponse)request.GetResponse();

                Stream dataStream = htmlresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                string response = reader.ReadToEnd();
                Reset(response, remove_comments);
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
        public void Reset(string html, bool remove_comments)
        {
            _html = html;

            //Remove <br> tags
            foreach (string tag in omitTags)
                _html = _html.Replace(tag, "");

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
            //We traverse the stream in reverse.
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
            //Find our first tag
            while (MoveToNextTag())
            {
                //Create a new tag
                HtmlTag node = new HtmlTag();
                node.Parent = null;
                node.Level = 0;
                //Our temporary forward-traversal index
                int index = _pos;
                //This is a closing tag
                if (_html[_pos + 1] == '/')
                {
                    node.CloseTag_Start = index;
                    //Move passed the '</'
                    index += 2;
                    //Parse tag name from current index
                    node.Name = ParseTagName(ref index);
                    //Reset to the index of the closing '</'
                    _pos = node.CloseTag_Start;
                    //If this was a valid closing tag, parse until we find our opening tag
                    if (node.Name != null)
                        if (ParseTag(ref node))
                            _pos = node.OpenTag_Start;//Found our opening tag
                        
                }
                else
                {
                    //This is an opening tag.
                    node.OpenTag_Start = _pos;
                    //Move passed the '<'
                    index++;
                    //Get our name
                    node.Name = ParseTagName(ref index);
                    //Parse our attributes.
                    ParseTagAttributes(ref node, ref index);
                }
                //Add this tag to our list of top-level tags
                _nodes.Insert(0, node);
                
            }
            return true;
        }

        protected bool ParseTag( ref HtmlTag parent )
        {
            //Have we found our opening tag?
            bool found = false;
            //While we haven't found our corresponding opening tag and we still have room left:
            while (!found && !EOF())
            {
                //We didn't find it.  Damn.
                if (!MoveToNextTag())
                    break;

                //Temporary traversal index
                int index = _pos;
                SkipWhitespace(ref index);

                //The end-tag of a nested child
                if (_html[_pos + 1] == '/' && !NextTagIsComment())
                {
                    //Nested tag
                    parent.HadChildren = true;
                    HtmlTag child_tag = new HtmlTag();
                    //Closing tag start
                    child_tag.CloseTag_Start = _pos;
                    child_tag.Level = parent.Level + 1;
                    //This tag's parent is us.
                    child_tag.Parent = parent;
                    //Move passed the '</'
                    index += 2;
                    //Skip to first character of name
                    SkipWhitespace(ref index);
                    //Parse name
                    child_tag.Name = ParseTagName(ref index);
                    //Find the closing of this close tag
                    SkipWhitespace(ref index);
                    child_tag.CloseTag_Close = index;
                    //We don't want scripts
                    if (child_tag.Name == "script")
                        continue;
                    //Valid tag, parse.
                    ParseTag(ref child_tag);
                    //Check if there is something in between the end of this tag and our previous first child
                    index++;
                    SkipWhitespace(ref index);
                    //If our parent had children and the first child's tag doesn't begin at the end of this tag,
                    //then we have some residual stuff to add to our parent's body
                    if (parent.Children.Count > 0 && parent.Children[0].OpenTag_Start != index)
                        parent.Value = _html.Substring(index, (parent.Children[0].OpenTag_Start - index)) + " " + parent.Value;
                    //parent.MiscellaneousItems.Insert(0, _html.Substring(index, (parent.Children[0].OpenTag_Start - index)));
                    //Insert at head of children
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
                        child_tag.Level = parent.Level + 1;
                        child_tag.OpenTag_Start = _pos;
                        child_tag.Parent = parent;
                        ParseTagAttributes(ref child_tag, ref index);
                        //If we were a self-closing tag, we had a child, and the child's tag does not start immediately after our end-tag,
                        //then add the misc stuff to our parent's body.
                        if (child_tag.TrailingSlash && parent.Children.Count > 0 && parent.Children[0].OpenTag_Start != index)
                            parent.Value = _html.Substring(index, (parent.Children[0].OpenTag_Start - index)) + " " + parent.Value;
                        //parent.MiscellaneousItems.Insert(0, _html.Substring(index, (parent.Children[0].OpenTag_Start - index)));
                        parent.Children.Insert(0, child_tag);
                    }
                }
            }
            return false;
        }
        protected string ParseTagName(ref int index)
        {
            int start = index;
            while (!EOF(index) && !Char.IsWhiteSpace(_html[index]) && _html[index] != '>')
                index++;
            return _html.Substring(start, index - start);
        }
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
                    tag.CloseTag_Close = index;
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
                tag.Value = _html.Substring(index, value_end - index) + tag.Value;  
        }
        protected string ParseAttributeName(ref int index)
        {
            int start = index;
            char c = _html[index];
            while (!EOF(index) && !Char.IsWhiteSpace(c) && c != '>' && c != '=')
                c = _html[++index];
            return _html.Substring(start, index - start);
        }
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
            //Temporary traversal variable
            int pos = _pos;
            //Our current position might be a '<'.  Move.
            pos--;
            //Go backwards through the stream, checking for a tag
            while (pos >= 0 )
            {
                if( _html[pos] == '<' && !NextTagIsComment(pos) )
                    break;
                pos--;
            }

            //Didn't find the next tag
            if (pos < 0)
                return false;

            //Found one.  Set our current position to the right place.
            _pos = pos;
            return true;
        }

        protected void SkipWhitespace(ref int index)
        {
            while (!EOF(index) && Char.IsWhiteSpace(_html[index]))
                index++;
        }
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
        public void DrawTree(Graphics graphics)
        {

        }
    }
}