using System;
using System.Collections.Generic;
using HtmlParser;

public class EntryFilter : PreciseParseFilter
{
    public class EntryInfo
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string Area { get; set; }
        public string Price { get; set; }
        public EntryInfo(HtmlTag src)
        {
            Title = src.Children[1].Value;
            URL = src.Children[1].Attributes["href"];
            Area = src.Children[2].Value;
            Price = src.MiscellaneousItems[0];
            for (int i = 0; i < Price.Length; i++)
            {
                if (Price[i] == '$')
                {
                    Price = Price.Substring(i, Price.Length - i);
                    break;
                }
            }
            
        }
    };
    public List<EntryInfo> EntryList;
    public string NextHundred;
	public EntryFilter()
	{
		EntryList = new List<EntryInfo>();
        NextHundred = null;
	}
	public void Populate(string url) 
	{
        AddOmitTags(new List<string>() { "<br>", "</br>" });
        EntryList.Clear();
        NextHundred = null;
		Init(url);
        HtmlTag parent = FilterBySequence(new int[] {1,1,5});
        Dictionary<string, KeyValuePair<string, string>> classAndAttributes = new Dictionary<string, KeyValuePair<string, string>>();
        classAndAttributes.Add("p", new KeyValuePair<string, string>("class", "row"));
        classAndAttributes.Add("font", new KeyValuePair<string, string>("size", "4"));
        List<HtmlTag> parentList = new List<HtmlTag>();
        parent.FilterForChildrenByNameAndAttribute(classAndAttributes, out parentList);
        foreach (HtmlTag child in parentList)
        {
            if (child.Name == "p")
                EntryList.Add(new EntryInfo(child));
            else if (child.Name == "font")
            {
                if (child.Children.Count == 0)
                    continue;
                HtmlTag refChild = child.Children[0];
                if(refChild.Attributes.ContainsKey("href"))
                    NextHundred = refChild.Attributes["href"];
            }
        }
	}
};
