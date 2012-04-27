using HtmlParser;
using System.Collections.Generic;
namespace HtmlParser
{
    public class AdFilter : PreciseParseFilter
    {
        private string toString;
        public string Body { get; set; }
        public AdFilter()
        {
            htmlParser.AddOmitTags(new List<string>() { "<br>", "</br>" });
            Body = string.Empty;
            toString = string.Empty;
        }
        public override void Populate()
        {
            try
            {
                toString = htmlParser.ToString();
                HtmlTag parent = FilterBySequence(new int[] { 1, 1 });
                if (parent == null)
                    return;
                List<HtmlTag> tags;
                parent.FilterForChildrenByNameAndAttribute("div", new KeyValuePair<string, string>("id", "userbody"), out tags);
                if (tags != null && tags.Count > 0)
                    Body = tags[0].Value;
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
