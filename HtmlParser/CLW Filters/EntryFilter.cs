using System;
using System.Collections.Generic;
using HtmlParser;

namespace HtmlParser
{
    public class EntryFilter : PreciseParseFilter
    {
        public class EntryInfo
        {
            public string Title { get; set; }
            public string URL { get; set; }
            public string Area { get; set; }
            public string Value { get; set; }
            private string toString;
            public EntryInfo(string rawHtml)
            {
                toString = rawHtml;
                Title = String.Empty;
                URL = String.Empty;
                Area = String.Empty;
                Value = String.Empty;
            }
            public static EntryInfo CreateEntryInfo(HtmlTag src)
            {
                try
                {
                    if (src.Children.Count == 0)
                        return null;

                    EntryInfo info = new EntryInfo(src.ToString());
                    List<HtmlTag> tags;
                    src.FilterForChildrenByAttribute("href", "*", out tags);
                    if (tags.Count == 0)
                        return null;

                    info.Title = tags[0].Value;
                    info.URL = tags[0].Attributes["href"];
                    tags.Clear();

                    src.FilterForChildrenByName("font", out tags);
                    if(tags.Count > 0)
                        info.Area = tags[0].Value;

                    info.Value = string.Join(",", src.MiscellaneousItems.ToArray());
                    for (int i = 0; i < info.Value.Length; i++)
                    {
                        if (info.Value[i] == '$')
                        {
                            info.Value = info.Value.Substring(i, info.Value.Length - i);
                            break;
                        }
                    }
                    return info;
                }
                catch(Exception e)
                {
                    return null;
                }
            }
            public override string ToString()
            {
                return toString;
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
            htmlParser.AddOmitTags(new List<string>() { "<br>", "</br>" });
            EntryList.Clear();
            NextHundred = null;
            Init(url);
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
