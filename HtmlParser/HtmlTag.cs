using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HtmlParser
{
    public class HtmlTag
    {
        public HtmlTag()
        {
            Attributes = new Dictionary<string, string>();
            Children = new List<HtmlTag>();
            //MiscellaneousItems = new List<string>();
            TrailingSlash = false;
            Value = null;
            Name = null;
            Parent = null;
            Level = -1;
            OpenTag_Start = -1;
            OpenTag_Close = -1;
            CloseTag_Start = -1;
            CloseTag_Close = -1;
            HadChildren = false;
        }
        /// <summary>
        /// Name of this tag
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// JH: Value in between opening tag and closing tag
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Collection of attribute names and values for this tag
        /// </summary>
        public Dictionary<string, string> Attributes { get; set; }

        /// <summary>
        /// True if this tag contained a trailing forward slash
        /// </summary>
        public bool TrailingSlash { get; set; }

        /// <summary>
        /// JH: Children that the filter allowed.
        /// </summary>
        public List<HtmlTag> Children { get; set; }

        //public List<string> MiscellaneousItems { get; set; }

        /// <summary>
        /// JH: Used for tracking parent tags
        /// </summary>
        public int Level { get; set; }

        public HtmlTag Parent { get; set; }

        public int OpenTag_Start { get; set; }
        public int OpenTag_Close { get; set; }
        public int CloseTag_Start { get; set; }
        public int CloseTag_Close { get; set; }
        public bool HadChildren { get; set; }

        public HtmlTag VanillaCopy(HtmlTag NewParent)
        {
            HtmlTag copy = new HtmlTag();
            copy.Attributes = this.Attributes;
            copy.Name = this.Name;
            copy.Parent = NewParent;
            copy.TrailingSlash = this.TrailingSlash;
            copy.Value = this.Value;
            copy.OpenTag_Start = this.OpenTag_Start;
            copy.OpenTag_Close = this.OpenTag_Close;
            copy.CloseTag_Start = this.CloseTag_Start;

            return copy;
        }
        /// <summary>
        /// Get all children with a specified name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="validChildren"></param>
        public void FilterForChildrenByName(string name, out List<HtmlTag> validChildren)
        {
            validChildren = null;
            if (this.Children.Count == 0)
                return;

            validChildren = new List<HtmlTag>();
            foreach (HtmlTag child in Children)
            {
                if (child.Name == name)
                    validChildren.Add(child);
            }
        }
        /// <summary>
        /// Get all children of the specified names
        /// </summary>
        /// <param name="names"></param>
        /// <param name="validChildren"></param>
        public void FilterForChildrenByName(List<string> names, out List<HtmlTag> validChildren)
        {
            validChildren = null;
            if (Children.Count == 0)
                return;

            validChildren = new List<HtmlTag>();
            foreach (HtmlTag child in Children)
            {
                if (names.Contains(child.Name))
                    validChildren.Add(child);
            }
        }
        /// <summary>
        /// Returns all of the children with a specified class name and attribute set.
        /// i.e. <div class="lolololol"></div>
        /// </summary>
        /// <param name="tagName">div</param>
        /// <param name="validAtrributeAndValue">("class","lolololol")</param>
        /// <param name="validChildren">list of children matching</param>
        public void FilterForChildrenByNameAndAttribute(string tagName, KeyValuePair<String, String> validAtrributeAndValue, out List<HtmlTag> validChildren)
        {
            validChildren = null;
            if (Children.Count == 0)
                return;

            validChildren = new List<HtmlTag>();
            foreach(HtmlTag child in Children)
            {
                if (tagName == child.Name)
                {
                    if (validAtrributeAndValue.Key == "*")
                    {
                        if (child.Attributes.ContainsValue(validAtrributeAndValue.Value))
                            validChildren.Add(child);
                    }
                    else if (validAtrributeAndValue.Value == "*")
                    {
                        if (child.Attributes.ContainsKey(validAtrributeAndValue.Key))
                            validChildren.Add(child);
                    }
                    else if (child.Attributes.Contains(validAtrributeAndValue))
                        validChildren.Add(child);
                }
            }
        }
        /// <summary>
        /// Returns all of the children with a relevant to each entry in the dictionary of specified class name and attribute sets.
        /// i.e. <a href="Hmmmmmm">,<div class="lolololol">, <h1 class="Damn">
        /// </summary>
        /// <param name="tagList">Dictionary of tag names and their relevant pairs</param>
        /// <param name="values">return values</param>
        public void FilterForChildrenByNameAndAttribute(Dictionary<string, KeyValuePair<string, string>> tagList, out List<HtmlTag> values)
        {
            values = null;
            if (Children.Count == 0)
                return;

            values = new List<HtmlTag>();
            foreach(HtmlTag child in Children)
            {
                KeyValuePair<string, string> valid_attribute = new KeyValuePair<string, string>();
                if (tagList.TryGetValue(child.Name, out valid_attribute))
                {
                    if (valid_attribute.Key == "*")
                    {
                        if (child.Attributes.ContainsValue(valid_attribute.Value))
                            values.Add(child);
                    }
                    else if (valid_attribute.Value == "*")
                    {
                        if (child.Attributes.ContainsKey(valid_attribute.Key))
                            values.Add(child);
                    }
                    else if (child.Attributes.Contains(valid_attribute))
                        values.Add(child);
                }
            }
        }
        /// <summary>
        /// Just like the above, except now you can have more than one valid attribute for a class.
        /// </summary>
        /// <param name="tagList"></param>
        /// <param name="values"></param>
        public void FilterForChildrenByNameAndAttribute(Dictionary<string, List<KeyValuePair<string, string>>> tagList, out List<HtmlTag> values)
        {
            values = null;
            if (Children.Count == 0)
                return;
            values = new List<HtmlTag>();
            foreach (HtmlTag child in Children)
            {
                List<KeyValuePair<string, string>> validAttributes = new List<KeyValuePair<string, string>>();
                if (tagList.TryGetValue(child.Name, out validAttributes))
                {
                    foreach (KeyValuePair<string, string> validAttribute in validAttributes)
                    {
                        if (validAttribute.Key == "*")
                        {
                            if (child.Attributes.ContainsValue(validAttribute.Value))
                                values.Add(child);
                        }
                        else if (validAttribute.Value == "*")
                        {
                            if (child.Attributes.ContainsKey(validAttribute.Key))
                                values.Add(child);
                        }
                        else if (child.Attributes.Contains(validAttribute))
                            values.Add(child);
                    }
                }
                if (child.Children.Count != 0)
                    child._RFilterForChildrenByNameAndAttribute(tagList, ref values);
            }
        }
        private void _RFilterForChildrenByNameAndAttribute(Dictionary<string, List<KeyValuePair<string, string>>> tagList, ref List<HtmlTag> values)
        {
            if (Children.Count == 0)
                return;
            foreach (HtmlTag child in Children)
            {
                List<KeyValuePair<string, string>> validAttributes = new List<KeyValuePair<string, string>>();
                if (tagList.TryGetValue(child.Name, out validAttributes))
                {
                    foreach (KeyValuePair<string, string> validAttribute in validAttributes)
                    {
                        if (validAttribute.Key == "*")
                        {
                            if (child.Attributes.ContainsValue(validAttribute.Value))
                                values.Add(child);
                        }
                        else if (validAttribute.Value == "*")
                        {
                            if (child.Attributes.ContainsKey(validAttribute.Key))
                                values.Add(child);
                        }
                        else if (child.Attributes.Contains(validAttribute))
                            values.Add(child);
                    }
                }
                if (child.Children.Count != 0)
                    child._RFilterForChildrenByNameAndAttribute(tagList, ref values);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validAttributesAndValues">Dictionary of<Attribute Name, ListOf<Attribute Values>></param>
        /// <param name="validChildren">return list</param>
        public void FilterForChildrenByAttribute(Dictionary<string, List<string>> validAttributesAndValues, out List<HtmlTag> validChildren)
        {
            validChildren = null;
            if (Children.Count == 0)
                return;
            validChildren = new List<HtmlTag>();

            for (int i = 0; i < Children.Count; i++)
            {
                for (int a = 0; a < validAttributesAndValues.Count; a++)
                {
                    KeyValuePair<string, List<string>> valid_attribute_values = validAttributesAndValues.ElementAt(a);
                    for (int b = 0; b < valid_attribute_values.Value.Count; b++)
                    {
                        KeyValuePair<string, string> valid_attribute = new KeyValuePair<string, string>(valid_attribute_values.Key, valid_attribute_values.Value[b]);

                        for (int z = 0; z < Children[i].Attributes.Count; z++)
                        {
                            if (Children[i].Attributes.Contains(valid_attribute))
                                validChildren.Add(Children[i]);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Just searching for one attribute and its value.  Tag name doesn't matter
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <param name="validChildren"></param>
        public void FilterForChildrenByAttribute(string attribute, string value, out List<HtmlTag> validChildren)
        {
            validChildren = new List<HtmlTag>();
            KeyValuePair<string, string> validAttribute = new KeyValuePair<string, string>(attribute, value);

            foreach (HtmlTag child in Children)
            {
                if (validAttribute.Key == "*")
                {
                    if (child.Attributes.ContainsValue(validAttribute.Value))
                        validChildren.Add(child);
                }
                else if (validAttribute.Value == "*")
                {
                    if (child.Attributes.ContainsKey(validAttribute.Key))
                        validChildren.Add(child);
                }
                else if (child.Attributes.Contains(validAttribute))
                    validChildren.Add(child);
            }
        }
        public void RemoveTags(List<string> tags)
        {
            for (int i = 0; i < Children.Count; i++ )
            {
                HtmlTag child = Children[i];
                child.RemoveTags(tags);
                //We're keeping this tag.  Check if it has any of the remove-tags.
                if (!tags.Contains(child.Name))
                    continue;

                Children.RemoveAt(i);
                int z = 0;
                for (; z < child.Children.Count; z++)
                {
                    HtmlTag grandChild = child.Children[z];
                    Children.Insert(i + z, grandChild);
                    grandChild.Parent = this;
                    grandChild.RemoveTags(tags);
                }
                i += (z - 1);
            }
        }
        private void _RRemoveTags(List<string> tags)
        {

        }
        public override string ToString()
        {
            string padding = String.Empty;
            for (int i = 0; i < Level; i++)
                padding += " ";

            string rtn = padding + "<";
            rtn += this.Name;
            for (int i = 0; i < this.Attributes.Count; i++)
            {
                rtn += ' ' + this.Attributes.ElementAt(i).Key + "=\"" + this.Attributes.ElementAt(i).Value + "\"";
            }

            if (this.TrailingSlash == true)
                rtn += "/>";
            else
            {
                rtn += '>';
                bool pad_end = false;

                if (this.Value != String.Empty)
                    rtn += this.Value;

                if (this.Children.Count > 0)
                {
                    rtn += "\n";
                    for (int i = 0; i < this.Children.Count; i++)
                        rtn += this.Children[i].ToString();

                    pad_end = true;
                }

                if (this.CloseTag_Start != -1)
                {
                    if (pad_end)
                        rtn += padding;
                    rtn += "</" + this.Name + ">";
                }
            }
            rtn += "\n";
            return rtn;
        }
        public string ToStringSansChildren()
        {
            string rtn = "<";
            rtn += this.Name;
            for (int i = 0; i < this.Attributes.Count; i++)
            {
                rtn += ' ' + this.Attributes.ElementAt(i).Key + "=\"" + this.Attributes.ElementAt(i).Value + "\"";
            }

            if (this.TrailingSlash == true)
                rtn += "/>";
            else
            {
                rtn += '>';

                if (this.Value != String.Empty)
                    rtn += this.Value;

                if (this.CloseTag_Start != -1)
                    rtn += "</" + this.Name + ">";
            }
            rtn += "\n";
            return rtn;
        }
        public void PopulateTreeView(ref TreeNodeCollection nodes)
        {
            TreeNode node = nodes.Add(this.ToStringSansChildren());
            foreach (HtmlTag child in Children)
            {
                TreeNodeCollection child_nodes = node.Nodes;
                child.PopulateTreeView(ref child_nodes);
            }
        }
    };
}
