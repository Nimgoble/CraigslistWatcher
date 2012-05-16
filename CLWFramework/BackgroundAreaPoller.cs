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
        public override AreaDetails PollerAreaDetails 
        {
            get
            {
                return areaDetails;
            }
        }
        private int entriesSearched;
        private System.Diagnostics.Stopwatch stopWatch;
        private CLWParseFilter.CLWParseURLCompletedHandler clwParseURLCompletedHandler;
        private Dictionary<string, BaseBackgroundPoller.EntryParsedHandler> entryCallbacks;
        public BaseBackgroundPoller.EntryParsedHandler aggregatedEntryParsedHandlers;
        private EventWaitHandle waitHandle;
        private Int32 numEntriesToSearch;
        public BackgroundAreaPoller(AreaDetails _areaDetails)
        {
            aggregatedEntryParsedHandlers = null;
            entryCallbacks = new Dictionary<string, BaseBackgroundPoller.EntryParsedHandler>();
            this.areaDetails = _areaDetails;
            worker = new BackgroundWorker();
            waitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            entriesSearched = 0;
            numEntriesToSearch = 0;
            stopWatch = new System.Diagnostics.Stopwatch();
            clwParseURLCompletedHandler = new CLWParseFilter.CLWParseURLCompletedHandler(this.OnEntryParsed);
            worker.DoWork += this.PollCity;
            worker.RunWorkerCompleted += this.OnPollDone;
        }

        public void Start()
        {
            worker.RunWorkerAsync();
        }

        public void PollCity(object sender, DoWorkEventArgs e)
        {
            stopWatch.Start();
            List<EntryInfo> entries = new List<EntryInfo>();
            SearchSection(areaDetails.Website, "", ref entries);
            numEntriesToSearch += entries.Count;

            OnNumberOfEntriesFound(numEntriesToSearch);

            if (numEntriesToSearch == 0)
                return;

            for (int i = 0; i < entries.Count; i++)
            {
                EntryInfo entry = entries[i];
                AdFilter filter = new AdFilter();
                filter.ParseURLAsync(ref entry, clwParseURLCompletedHandler);
            }
            
            waitHandle.WaitOne();
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
                if (entryCallbacks.Keys.Contains(entryInfo.URL))
                    continue;
                if(!(parseMore = OnEntryFound(entryInfo)))
                    break;

                entries.Add(entryInfo);
                BaseBackgroundPoller.EntryParsedHandler eph = null;
                foreach (BaseBackgroundPoller.EntryParsedHandler _eph in this.aggregatedEntryParsedHandlers.GetInvocationList())
                    eph += _eph;

                entryCallbacks.Add(entryInfo.URL, eph);
            }
            if (entryFilter.NextHundred != null & parseMore)
                SearchSection(sectionSite, entryFilter.NextHundred, ref entries);
        }

        public new void OnPollDone(object sender, RunWorkerCompletedEventArgs e)
        {
            stopWatch.Stop();
            base.OnPollDone(entriesSearched.ToString() + " entries searched in " + stopWatch.Elapsed.ToString());
        }

        
        private readonly object PollErrorLock = new object();
        public new void OnPollError(string area, string message)
        {
            lock (PollErrorLock)
            {
                base.OnPollError(area, message);
            }
        }

       
        private readonly object EntryFoundLock = new object();
        protected new bool OnEntryFound(EntryInfo info)
        {
            lock (EntryFoundLock)
            {
                return base.OnEntryFound(info);
            }
        }

        private readonly object EntryParsedLock = new object();
        protected new void OnEntryParsed(EntryInfo info, ParseFilter filter)
        {
            lock (EntryParsedLock)
            {
                BaseBackgroundPoller.EntryParsedHandler handler = null;
                if (entryCallbacks.TryGetValue(info.URL, out handler))
                    handler(this, info);
                if (++entriesSearched == numEntriesToSearch)
                    waitHandle.Set();
            }
        }
    }
}
