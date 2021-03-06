﻿using System;
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
    public class BackgroundPoller : EventWaitHandle
    {
        private BackgroundWorker worker;
        private CityDetails details;
        private int entriesSearched;
        private System.Diagnostics.Stopwatch stopWatch;
        private CLWParseFilter.CLWParseURLCompletedHandler clwParseURLCompletedHandler;
        public BackgroundPoller(ref CityDetails details)
            : base(false, EventResetMode.ManualReset)
        {
            this.details = details;
            worker = new BackgroundWorker();
            entriesSearched = 0;
            stopWatch = new System.Diagnostics.Stopwatch();
            clwParseURLCompletedHandler = new CLWParseFilter.CLWParseURLCompletedHandler(this.OnEntryFound);
            worker.DoWork += this.PollCity;
            worker.RunWorkerCompleted += this.OnPollDone;
            worker.RunWorkerAsync();
        }

        public void PollCity(object sender, DoWorkEventArgs e)
        {
            stopWatch.Start();
            List<List<EntryInfo>> EntryInfoSectionList = new List<List<EntryInfo>>();
            Int32 numEntriesToSearch = 0;
            foreach (Dictionary<string, SubsectionDetails> subSections in details.Sections.Values)
            {
                List<EntryInfo> entries = new List<EntryInfo>();
                foreach (SubsectionDetails subSection in subSections.Values)
                {
                    try
                    {
                        string site = details.CityWebsite + "/" + subSection.Suffix;
                        SearchSection(site, "", ref subSection.TopFiveEntriesFromLastSearch, ref entries);
                    }
                    catch (Exception error)
                    {
                        OnPollError(details.City, error.ToString());
                    }
                }
                numEntriesToSearch += entries.Count;
                EntryInfoSectionList.Add(entries);
            }

            OnNumberOfEntriesFound(numEntriesToSearch);

            foreach (List<EntryInfo> entryList in EntryInfoSectionList)
            {
                for (int i = 0; i < entryList.Count; i++ )
                {
                    EntryInfo entry = entryList[i];
                    AdFilter filter = new AdFilter();
                    filter.ParseURLAsync(ref entry, clwParseURLCompletedHandler);
                }
            }
        }

        private void SearchSection(string sectionSite, 
                                    string indexSuffix, 
                                    ref List<string> LastFiveEntriesSearched, 
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
                OnPollError(details.City , error.ToString());
            }

            foreach (EntryInfo entryInfo in entryFilter.EntryList)
            {
                if (LastFiveEntriesSearched.Count != 0 && LastFiveEntriesSearched.Contains(entryInfo.URL))
                {
                    FillLastFive(ref LastFiveEntriesSearched, entryFilter.EntryList);
                    return;
                }
                else
                    entries.Add(entryInfo);
            }
            if (entryFilter.NextHundred != null)
                SearchSection(sectionSite, entryFilter.NextHundred, ref LastFiveEntriesSearched, ref entries);
            else//No more.  Fill Last Five
                FillLastFive(ref LastFiveEntriesSearched, entries);
        }

        private void FillLastFive(ref List<string> LastFiveEntriesSearched, List<EntryInfo> entries)
        {
            int max_count = (entries.Count < 5) ? entries.Count : 5;
            for (int i = 0; i < max_count; i++)
            {
                EntryInfo entry = entries[i];
                LastFiveEntriesSearched.Insert(LastFiveEntriesSearched.Count, entry.URL);
            }
        }

        public delegate void NumberOfEntriesFoundHandler(Int32 numEntries);
        public event NumberOfEntriesFoundHandler NumberOfEntriesFound;
        private void OnNumberOfEntriesFound(Int32 numEntries)
        {
            if (NumberOfEntriesFound != null)
                NumberOfEntriesFound(numEntries);
        }

        public delegate void PollDoneHandler(string message);
        public event PollDoneHandler PollDone;
        public void OnPollDone(object sender, RunWorkerCompletedEventArgs e)
        {
            stopWatch.Stop();

            if (PollDone != null)
            {
                string output = entriesSearched.ToString() + " entries searched in " + stopWatch.Elapsed.ToString();
                PollDone(output);
            }

            base.Set();
        }

        public delegate void PollErrorHandler(string area, string message);
        public event PollErrorHandler PollError;
        private readonly object PollErrorLock = new object();
        public void OnPollError(string area, string message)
        {
            lock (PollErrorLock)
            {
                if (PollError != null)
                    PollError(area, message);
            }
        }

        public delegate void EntryFoundHandler(EntryInfo info);
        public event EntryFoundHandler EntryFound;
        private readonly object EntryFoundLock = new object();
        protected void OnEntryFound(EntryInfo info, ParseFilter filter)
        {
            lock (EntryFoundLock)
            {
                entriesSearched++;
                if (EntryFound != null)
                    EntryFound(info);
            }
        }
    }
}

