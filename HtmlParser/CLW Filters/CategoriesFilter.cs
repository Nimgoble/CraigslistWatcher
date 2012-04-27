using System;
using System.Collections.Generic;
using HtmlParser;
using CLWFramework;
public class CategoriesFilter : PreciseParseFilter
{
    private Dictionary<CategoryInfo, List<CategoryInfo>> sectionDictionary;
    public CategoriesFilter(ref Dictionary<CategoryInfo, List<CategoryInfo>> _sectionDictionary)
	{
        sectionDictionary = _sectionDictionary;
        htmlParser.AddOmitTags(new List<string>() { "<br>", "</br>" });
	}
    public override void Populate()
    {
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
