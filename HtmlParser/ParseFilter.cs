using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using CLWFramework;
namespace HtmlParser
{
    public class ParseFilter
    {
        //Make this a member instead of inheriting, so as to not expose all the HtmlParser methods to this class.
        protected HtmlParser htmlParser;
        private ParseURLWorkerHandler worker;
        private AsyncCallback callback;
        public ParseFilter()
        {
            htmlParser = new HtmlParser();
            worker = new ParseURLWorkerHandler(this.ParseURLWorker);
            callback = new AsyncCallback(this.OnParseURLCompleted);
        }
        public bool ParseURL(string url)
        {
            return htmlParser.ParseURL(url, true);
        }
        public bool ParseRawHTML(string html)
        {
            return htmlParser.ParseRawHTML(html, true);
        }
        //Children should override this
        public virtual void Populate()
        {
        }
        //Async stuff
        public void ParseURLAsync(EntryInfo info, ParseURLCompletedHandler handler)
        {
            ParseURLCompleted += handler;
            worker.BeginInvoke(info, callback, info);
        }
        private delegate void ParseURLWorkerHandler(EntryInfo info);
        private void ParseURLWorker(EntryInfo info)
        {
            htmlParser.ParseURL(info.URL, true);
            Populate();
        }
        public delegate void ParseURLCompletedHandler(EntryInfo info, ParseFilter filter);
        public event ParseURLCompletedHandler ParseURLCompleted;
        private void OnParseURLCompleted(IAsyncResult e)
        {
            if (ParseURLCompleted != null)
            {
                EntryInfo info = (EntryInfo)e.AsyncState;
                ParseURLCompleted(info, this);
            }
        }
    }
}
