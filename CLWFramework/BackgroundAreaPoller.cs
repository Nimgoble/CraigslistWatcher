using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using System.ComponentModel;
using HtmlParser;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;
using CLWFramework.CLWFilters;

namespace CLWFramework
{
    public class BackgroundAreaPoller : BaseBackgroundPoller
    {
        private BackgroundWorker worker;
        private AreaDetails areaDetails;
        private int entriesSearched;
        private System.Diagnostics.Stopwatch stopWatch;
        private CLWParseFilter.CLWParseURLCompletedHandler clwParseURLCompletedHandler;
        private Dictionary<string, BaseBackgroundPoller.EntryParsedHandler> entryCallbacks;
        public BaseBackgroundPoller.EntryParsedHandler aggregatedEntryParsedHandlers;
        public BackgroundAreaPoller(AreaDetails _areaDetails)
        {
            aggregatedEntryParsedHandlers = null;
            entryCallbacks = new Dictionary<string, BaseBackgroundPoller.EntryParsedHandler>();
            this.areaDetails = _areaDetails;
            worker = new BackgroundWorker();
            entriesSearched = 0;
            stopWatch = new System.Diagnostics.Stopwatch();
            clwParseURLCompletedHandler = new CLWParseFilter.CLWParseURLCompletedHandler(this.OnEntryParsed);
            worker.DoWork += this.PollCity;
            worker.RunWorkerCompleted += this.OnPollDone;
            worker.RunWorkerAsync();
        }

        public void PollCity(object sender, DoWorkEventArgs e)
        {
            stopWatch.Start();
            List<EntryInfo> entries = new List<EntryInfo>();
            Int32 numEntriesToSearch = 0;
            SearchSection(areaDetails.Website, "", ref entries);
            numEntriesToSearch += entries.Count;

            OnNumberOfEntriesFound(numEntriesToSearch);

            for (int i = 0; i < entries.Count; i++)
            {
                EntryInfo entry = entries[i];
                AdFilter filter = new AdFilter();
                filter.ParseURLAsync(ref entry, clwParseURLCompletedHandler);
            }
        }

        private void SearchSection(string sectionSite, 
                                    string indexSuffix, 
                                    ref List<EntryInfo> entries )
        {
            //Well, fuck.  This doesn't work for Personals because of that stupid "I'm aware that I could see nudie pics" page.
            EntryFilter entryFilter = new EntryFilter();
            try
            {
                if (entryFilter.ParseURL(sectionSite + indexSuffix))
                    entryFilter.Populate();
            }
            catch (Exception error)
            {
                OnPollError(areaDetails.City , error.ToString());
            }

            bool parseMore = true;
            foreach (EntryInfo entryInfo in entryFilter.EntryList)
            {
                if(!(parseMore = OnEntryFound(entryInfo)))
                    break;

                entryCallbacks.Add(entryInfo.URL, (BaseBackgroundPoller.EntryParsedHandler)this.aggregatedEntryParsedHandlers.Clone());
            }
            if (entryFilter.NextHundred != null & parseMore)
                SearchSection(sectionSite, entryFilter.NextHundred, ref entries);
        }

        public void OnPollDone(object sender, RunWorkerCompletedEventArgs e)
        {
            stopWatch.Stop();
            base.OnPollDone(entriesSearched.ToString() + " entries searched in " + stopWatch.Elapsed.ToString());
        }

        
        private readonly object PollErrorLock = new object();
        public void OnPollError(string area, string message)
        {
            lock (PollErrorLock)
            {
                base.OnPollError(area, message);
            }
        }

       
        private readonly object EntryFoundLock = new object();
        protected bool OnEntryFound(EntryInfo info)
        {
            lock (EntryFoundLock)
            {
                return base.OnEntryFound(info);
            }
        }

        private readonly object EntryParsedLock = new object();
        protected void OnEntryParsed(EntryInfo info, ParseFilter filter)
        {
            lock (EntryParsedLock)
            {
                entriesSearched++;
                BaseBackgroundPoller.EntryParsedHandler handler = null;
                if (entryCallbacks.TryGetValue(info.URL, out handler))
                    handler(this, info);
            }
        }
    }
}
