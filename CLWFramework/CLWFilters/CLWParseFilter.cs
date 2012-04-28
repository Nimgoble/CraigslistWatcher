using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlParser;

namespace CLWFramework.CLWFilters
{
    public class CLWParseFilter : PreciseParseFilter
    {
        private CLWParseURLWorkerHandler clwWorker;
        private AsyncCallback clwCallback;
        public CLWParseFilter()
        {
            clwWorker = new CLWParseURLWorkerHandler(this.ParseURLWorker);
            clwCallback = new AsyncCallback(this.OnParseURLCompleted);
        }
        //Async stuff
        public void ParseURLAsync(EntryInfo info, CLWParseURLCompletedHandler handler)
        {
            CLWParseURLCompleted += handler;
            clwWorker.BeginInvoke(info, clwCallback, info);
        }
        private delegate void CLWParseURLWorkerHandler(EntryInfo info);
        private void ParseURLWorker(EntryInfo info)
        {
            htmlParser.ParseURL(info.URL, true);
            Populate();
        }
        public delegate void CLWParseURLCompletedHandler(EntryInfo info, CLWParseFilter filter);
        public event CLWParseURLCompletedHandler CLWParseURLCompleted;
        private void OnParseURLCompleted(IAsyncResult e)
        {
            if (CLWParseURLCompleted != null)
            {
                EntryInfo info = (EntryInfo)e.AsyncState;
                CLWParseURLCompleted(info, this);
            }
        }
    }
}
