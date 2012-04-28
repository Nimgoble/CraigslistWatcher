using System;
using System.Collections.Generic;
using HtmlParser;

namespace CLWFramework.CLWFilters
{
    public class EntryFilter : CLWParseFilter
    {
        public List<EntryInfo> EntryList;
        public string NextHundred;
        public EntryFilter()
        {
            htmlParser.AddOmitTags(new List<string>() { "<br>", "</br>" });
            EntryList = new List<EntryInfo>();
            NextHundred = null;
        }
        public override void Populate()
        {
            EntryList.Clear();
            NextHundred = null;
            HtmlTag parent = FilterBySequence(new int[] { 1, 1, 5 });
            Dictionary<string, KeyValuePair<string, string>> classAndAttributes = new Dictionary<string, KeyValuePair<string, string>>();
            List<HtmlTag> parentList = new List<HtmlTag>();
            parent.FilterForChildrenByName("p", out parentList);
            foreach (HtmlTag child in parentList)
            {
                EntryInfo info = EntryInfo.CreateEntryInfo(child);
                if (info != null)
                    EntryList.Add(info);
            }
            parentList.Clear();
            parent.FilterForChildrenByNameAndAttribute("font", new KeyValuePair<string, string>("size", "4"), out parentList);
            foreach (HtmlTag child in parentList)
            {
                if (child.Children.Count == 0)
                    continue;
                HtmlTag refChild = child.Children[0];
                if (refChild.Attributes.ContainsKey("href"))
                    NextHundred = refChild.Attributes["href"];
            }
        }
    };
}
