using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlParser
{
    public class PreciseParseFilter
    {
        protected HtmlParser htmlParser;
        public PreciseParseFilter()
        {
            htmlParser = new HtmlParser();
        }
        public bool ParseURL(string url)
        {
            return htmlParser.ParseURL(url, true);
        }
        public bool ParseRawHTML(string html)
        {
            return htmlParser.ParseRawHTML(html, true);
        }
        protected HtmlTag FilterBySequence(int[] sequence)
        {
            try
            {
                HtmlTag cur_tag = htmlParser._nodes[sequence[0]];
                for (int index = 1; index < sequence.Count(); index++)
                    cur_tag = cur_tag.Children[sequence[index]];

                return cur_tag;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    };
}
