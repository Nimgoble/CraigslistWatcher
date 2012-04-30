using System;
using System.Collections.Generic;
using System.Globalization;
using HtmlParser;
namespace CLWFramework.CLWFilters
{
    public class AdFilter : CLWParseFilter
    {
        private string toString;
        public string Body { get; set; }
        public DateTime Date;
        //private static string dateFormat = "yyyy-MM-dd,  h:mmtt K";
        private EntryInfo info;
        public AdFilter()
        {
            htmlParser.AddOmitTags(new List<string>() { "<br>", "</br>" });
            Body = string.Empty;
            toString = string.Empty;
            Date = new DateTime();
            info = null;
        }
        public void ParseURLAsync(ref EntryInfo info, CLWParseURLCompletedHandler handler)
        {
            this.info = info;
            ParseURLAsync(info, handler);
        }
        public override void Populate()
        {
            try
            {
                toString = htmlParser.ToString();
                HtmlTag parent = FilterBySequence(new int[] { 1, 1 });
                if (parent == null)
                    return;
                //Date
                HtmlTag dateNode = parent.Children[4];
                if (dateNode != null)
                {
                    int end = dateNode.Value.IndexOf('\n', 7);
                    string date = dateNode.Value.Substring(7, end - 10);
                    DateTime.TryParse(date, out Date);
                    if (info != null)
                        info.Date = Date;
                }
                //Body
                List<HtmlTag> tags;
                parent.FilterForChildrenByNameAndAttribute("div", new KeyValuePair<string, string>("id", "userbody"), out tags);
                if (tags != null && tags.Count > 0)
                {
                    Body = tags[0].Value;
                    if (info != null)
                        info.Body = Body;
                }
            }
            catch (System.Exception e)
            {
                Body = e.ToString();
            }
        }
        public override string ToString()
        {
            return toString;
        }
    };
}
