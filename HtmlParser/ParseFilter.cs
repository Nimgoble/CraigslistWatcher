using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlParser
{
    //Filter Template: "Parent(child(grandchild,grandchild,grandchild),child(grandchild(greatgrandchild,greatgrandchild)),child)"
    //Note:  Only 1 parent allowed
    public class ParseFilter
    {
        private ParseFilter parent_;
        public static ParseFilter Create(string filter_pattern)
        {
            ParseFilter filter = new ParseFilter();
            int place = 0;
            filter.ParseName(filter_pattern, ref place);
            filter.ParseChildren(filter_pattern, ref place);

            return filter;
        }
        public ParseFilter()
        {
            AcceptableChildren = new Dictionary<string, ParseFilter>();
            AllChildren = false;
            parent_ = this;
        }
        public ParseFilter(ParseFilter parent)
        {
            AcceptableChildren = new Dictionary<string, ParseFilter>();
            AllChildren = false;
            parent_ = parent;
        }

        //Returns true if we have children
        private void ParseName(string format, ref int place)
        {
            int name_start = place;
            while (place != format.Length)
            {
                if (format[place] == '(' || format[place] == ',' || format[place] == ')' || format[place] == '[')
                {
                    this.Name = format.Substring(name_start, place - name_start);
                    return;
                }
                place++;
            }
        }
        private void ParseAttributes(string format, ref int place)
        {
            place++;
            int name_start = place;
            char current = format[place];
            while (place != format.Length)
            {
                place++;
                current = format[place];
                if (current == ',')
                {
                    string attribute = format.Substring(name_start, place - name_start);
                    if (attribute == "parent")
                        parent_ = this;
                    place++;
                    name_start = place;
                    current = format[place];
                    continue;
                }
                else if (current == ']')
                {
                    string attribute = format.Substring(name_start, place - name_start);
                    if (attribute == "parent")
                        parent_ = this;
                    place++;
                    return;
                }
            }
        }
        private void ParseChildren(string format, ref int place)
        {
            char current = format[place];
            while ((place < format.Length) && (current != ')'))
            {
                place++;
                ParseFilter child = new ParseFilter(parent_);
                child.ParseName(format, ref place);
                if (child.Name == "*")
                {
                    this.AllChildren = true;
                    place = format.IndexOf(')', place);
                    break;
                }
                current = format[place];
                if (current == '[')
                {
                    child.ParseAttributes(format, ref place);
                }
                current = format[place];
                if (current == '(')
                {
                    child.ParseChildren(format, ref place);
                }
                current = format[place];
                this.AcceptableChildren.Add(child.Name, child);
            }
            place++;
        }
        public Dictionary<string, ParseFilter> AcceptableChildren { get; set; }
        public string Name { get; set; }
        public bool AllChildren { get; set; }
        public bool isParent()
        {
            return (this == parent_);
        }
        public static bool operator ==(ParseFilter a, ParseFilter b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return ((a.Name == b.Name) && (a.AllChildren == b.AllChildren) && (a.AcceptableChildren == b.AcceptableChildren));
        }

        public static bool operator !=(ParseFilter a, ParseFilter b)
        {
            return !(a == b);
        }
    }
}
