using System;
using System.Collections.Generic;
using HtmlParser;
public class CategoriesFilter : PreciseParseFilter
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
	public CategoriesFilter()
	{
	}

    public void Populate(string url, ref Dictionary<CategoryInfo, List<CategoryInfo>> sectionDictionary)
    {
        htmlParser.AddOmitTags(new List<string>() { "<br>", "</br>" });
        Init(url);
        HtmlTag ParentNode = (FilterBySequence(new int[] { 1, 1, 0, 0, 0, 2, 4 }));
        //Set up each section
        foreach (HtmlTag child in ParentNode.Children)
        {
            string sectionSuffix = "";
            child.Attributes.TryGetValue("value", out sectionSuffix);
            string name = child.Value;
            name = name.Trim(new char[] {'\n', '\t', ' '});
            sectionDictionary.Add(new CategoryInfo(name, sectionSuffix), new List<CategoryInfo>());
        }
        ParentNode = (FilterBySequence(new int[] { 1, 1, 0, 0, 1, 1, 0 }));
        ParentNode.RemoveTags(new List<string>() { "li", "ul", "div" });
        string currentSection = "";
        foreach (HtmlTag td in ParentNode.Children)
        {
            List<CategoryInfo> currentList = null;
            foreach (HtmlTag child in td.Children)
            {
                if (child.Name == "h4")
                {
                    HtmlTag aChild = child.Children.Find(a => a.Name == "a");

                    if (aChild != null)
                        currentSection = aChild.Value;
                    else
                        currentSection = child.Value;
                    //This is stupid.  Just let me search with the goddam string
                    foreach (KeyValuePair<CategoryInfo, List<CategoryInfo>> pair in sectionDictionary)
                    {
                        if(pair.Key.Name == currentSection)
                        {
                            currentList = pair.Value;
                            break;
                        }
                    }
                }
                else if (child.Name == "a")
                {
                    string sectionSuffix = "";
                    child.Attributes.TryGetValue("href", out sectionSuffix);
                    if(currentList != null)
                        currentList.Add(new CategoryInfo(child.Value, sectionSuffix));
                }
            }
        }
    }
};
