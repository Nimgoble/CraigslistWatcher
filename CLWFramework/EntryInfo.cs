using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlParser;

namespace CLWFramework
{
    public class EntryInfo : IComparable<EntryInfo>
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string Area { get; set; }
        public string Value { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        private string toString;
        public EntryInfo(string rawHtml)
        {
            toString = rawHtml;
            Title = String.Empty;
            URL = String.Empty;
            Area = String.Empty;
            Value = String.Empty;
            Body = String.Empty;
            Date = new DateTime();
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
                if (tags.Count > 0)
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
            catch (Exception e)
            {
                return null;
            }
        }
        public override string ToString()
        {
            return toString;
        }
        public int CompareTo(EntryInfo other)
        {
            return URL.CompareTo(other.URL);
        }
    };
}
