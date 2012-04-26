using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLWFramework
{
    public class CategoryInfo : IComparable<object>
    {
        public string Name;
        public string Suffix;
        public CategoryInfo()
        {
            Name = "";
            Suffix = "";
        }
        public CategoryInfo(string name, string suffix)
        {
            Name = name;
            Suffix = suffix;
        }
        public override string ToString()
        {
            return Name;
        }
        public int CompareTo(object obj)
        {
            if (obj is CategoryInfo)
                return this.Name.CompareTo(((CategoryInfo)obj).Name);
            else if (obj is string)
                return this.Name.CompareTo(((string)obj));
            throw new ArgumentException("object is not a valid comparer");
        }
    }
}
