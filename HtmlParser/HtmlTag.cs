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
            TrailingSlash = false;
            Value = null;
            Name = null;
            Parent = null;
            OpenTag_Start = -1;
            OpenTag_Close = -1;
            CloseTag_Start = -1;
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

        /// <summary>
        /// JH: Used for tracking parent tags
        /// </summary>
        public int TagNumber { get; set; }

        public HtmlTag Parent { get; set; }

        public int OpenTag_Start { get; set; }
        public int OpenTag_Close { get; set; }
        public int CloseTag_Start { get; set; }
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

        public void FilterForChildrenByName(string name, out HtmlTag parent)
        {
            parent = this.VanillaCopy(null);
            this._FilterForChildrenByName(name, ref parent);
        }

        private void _FilterForChildrenByName(string name, ref HtmlTag parent)
        {
            if (parent == null)
                parent = this.VanillaCopy(null);

            if (this.Children.Count == 0)
                return;

            for (int i = 0; i < this.Children.Count; i++)
            {
                if (this.Children[i].Name == name)
                    parent.Children.Add(this.Children[i]);
                this.Children[i]._FilterForChildrenByName(name, ref parent);
            }
        }
        public void FilterForChildrenByName(List<string> names, ref HtmlTag parent)
        {
            if (Children.Count == 0 || parent == null)
                return;

            for (int i = 0; i < Children.Count; i++)
            {
                if (names.Contains(Children[i].Name))
                    parent.Children.Add(Children[i]);

                Children[i].FilterForChildrenByName(names, ref parent);
            }
        }
        public void FilterForChildrenByNameAndAttribute(Dictionary<string, KeyValuePair<string, string>> tag_list, ref HtmlTag parent)
        {
            if (Children.Count == 0 || parent == null)
                return;

            for (int i = 0; i < Children.Count; i++)
            {
                KeyValuePair<string, string> valid_attribute = new KeyValuePair<string, string>();
                if (tag_list.TryGetValue(Children[i].Name, out valid_attribute))
                {
                    if (valid_attribute.Key == "*")
                    {
                        if (Children[i].Attributes.ContainsValue(valid_attribute.Value))
                            parent.Children.Add(Children[i]);
                    }
                    else if (valid_attribute.Value == "*")
                    {
                        if (Children[i].Attributes.ContainsKey(valid_attribute.Key))
                            parent.Children.Add(Children[i]);
                    }
                    else if (Children[i].Attributes.Contains(valid_attribute))
                        parent.Children.Add(Children[i]);
                }

                Children[i].FilterForChildrenByNameAndAttribute(tag_list, ref parent);
            }
        }
        public void FilterForChildrenByNameAndAttribute(Dictionary<string, KeyValuePair<string, string>> tag_list, out List<HtmlTag> values)
        {
            values = null;
            if (Children.Count == 0)
                return;

            values = new List<HtmlTag>();
            foreach(HtmlTag child in Children)
            {
                KeyValuePair<string, string> valid_attribute = new KeyValuePair<string, string>();
                if (tag_list.TryGetValue(child.Name, out valid_attribute))
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
        public void FilterOutChildrenByAttribute(Dictionary<string, List<string>> valid_attributes)
        {
            List<HtmlTag> new_children = new List<HtmlTag>();

            for (int i = 0; i < Children.Count; i++)
            {
                for (int a = 0; a < valid_attributes.Count; a++)
                {
                    KeyValuePair<string, List<string>> valid_attribute_values = valid_attributes.ElementAt(a);
                    for (int b = 0; b < valid_attribute_values.Value.Count; b++)
                    {
                        KeyValuePair<string, string> valid_attribute = new KeyValuePair<string, string>(valid_attribute_values.Key, valid_attribute_values.Value[b]);

                        for (int z = 0; z < Children[i].Attributes.Count; z++)
                        {
                            if (Children[i].Attributes.Contains(valid_attribute))
                                new_children.Add(Children[i]);
                        }
                    }
                }
            }
            Children = new_children;
            return;
        }
        public void FilterOutChildrenByAttribute(Dictionary<string, string> valid_attributes)
        {
            List<HtmlTag> new_children = new List<HtmlTag>();

            for (int i = 0; i < Children.Count; i++)
            {
                for (int a = 0; a < valid_attributes.Count; a++)
                {
                    KeyValuePair<string, string> valid_attribute = valid_attributes.ElementAt(a);
                    for (int z = 0; z < Children[i].Attributes.Count; z++)
                    {
                        if (Children[i].Attributes.Contains(valid_attribute))
                            new_children.Add(Children[i]);
                    }
                }
            }
            Children = new_children;
            return;
        }
        public void FilterOutChildrenByAttribute(string attribute, string value)
        {
            List<HtmlTag> new_children = new List<HtmlTag>();
            KeyValuePair<string, string> key_value = new KeyValuePair<string, string>(attribute, value);

            for (int i = 0; i < Children.Count; i++)
            {
                /*for (int z = 0; z < Children[i].Attributes.Count; z++)
                {
                    if (Children[i].Attributes.ElementAt(z).Key == attribute &&
                        Children[i].Attributes.ElementAt(z).Value == value)*/
                if (Children[i].Attributes.Contains(key_value))
                    new_children.Add(Children[i]);
                //}
            }
            Children = new_children;
            return;
        }

        public override string ToString()
        {
            string padding = String.Empty;
            HtmlTag _parent = this.Parent;
            while (_parent != null)
            {
                padding += " ";
                _parent = _parent.Parent;
            }

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
