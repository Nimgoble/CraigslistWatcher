using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmlParser
{
    public class PreciseParseFilter : HtmlParser
    {
        protected bool Init(string url)
        {
            return ParseURL(url, true);
        }

        protected HtmlTag FilterBySequence(int[] sequence)
        {
            try
            {
                HtmlTag cur_tag = _nodes[sequence[0]];
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
