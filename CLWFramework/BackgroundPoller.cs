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

namespace CLWFramework
{
    public class BackgroundPoller : EventWaitHandle
    {
        private BackgroundWorker worker;
        private CityDetails details;
        private int matchingEntriesFound;
        private int entriesSearched;
        private System.Diagnostics.Stopwatch stopWatch;
        public BackgroundPoller(ref CityDetails details)
            : base(false, EventResetMode.ManualReset)
        {
            this.details = details;
            matchingEntriesFound = 0;
            worker = new BackgroundWorker();
            entriesSearched = 0;
            stopWatch = new System.Diagnostics.Stopwatch();
            worker.DoWork += this.PollCity;
            worker.RunWorkerCompleted += this.OnPollDone;
            worker.RunWorkerAsync();
        }

        public void PollCity(object sender, DoWorkEventArgs e)
        {
            stopWatch.Start();
            foreach (Dictionary<string, SubsectionDetails> subSections in details.Sections.Values)
            {
                foreach (SubsectionDetails subSection in subSections.Values)
                {
                    try
                    {
                        string site = details.CityWebsite + "/" + subSection.Suffix;
                        SearchSection(site, "", ref subSection.TopFiveEntriesFromLastSearch);
                    }
                    catch (Exception error)
                    {
                        OnPollError(details.City, error.ToString());
                    }
                }
            }
        }

        private void SearchSection(string sectionSite, string indexSuffix, ref List<string> LastFiveEntriesSearched)
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
                    SearchEntry(entryInfo);
            }
            if (entryFilter.NextHundred != null)
                SearchSection(sectionSite, entryFilter.NextHundred, ref LastFiveEntriesSearched);
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

        //title = <html><head><title>
        //body = <html><body><div id="userbody">
        private void SearchEntry(EntryInfo entry)
        {
            try
            {
                AdFilter adFilter = new AdFilter();
                try 
                { 
                    if(adFilter.ParseURL(entry.URL))
                        adFilter.Populate(); 
                }
                catch (Exception error) { OnPollError(details.City, error.ToString()); }
                
                if (adFilter.Body != null && adFilter.Body != String.Empty)
                {
                    string body = adFilter.Body.ToLower();
                    string title = entry.Title.ToLower();
                    foreach (string keyword in details.Keywords)
                    {
                        if (body.Contains(keyword) || title.Contains(keyword))
                        {
                            OnEntryFound(entry);
                            return;
                        }
                    }
                }
            }
            finally
            {
                OnEntrySearched();
            }

        }

        public delegate void PollDoneHandler(string message);
        public event PollDoneHandler PollDone;
        public void OnPollDone(object sender, RunWorkerCompletedEventArgs e)
        {
            stopWatch.Stop();

            if (PollDone != null)
            {
                string output = "Found " + matchingEntriesFound.ToString() + " new entries out of " + entriesSearched.ToString() + " entries searched in " + stopWatch.Elapsed.ToString();
                PollDone(output);
            }

            base.Set();
        }

        public delegate void PollErrorHandler(string area, string message);
        public event PollErrorHandler PollError;
        public void OnPollError(string area, string message)
        {
            if (PollError != null)
                PollError(area, message);
        }

        public delegate void EntryFoundHandler(EntryInfo info);
        public event EntryFoundHandler EntryFound;
        protected void OnEntryFound(EntryInfo info)
        {
            matchingEntriesFound++;
            if(EntryFound != null)
                EntryFound(info);
        }

        public delegate void EntrySearchedHandler();
        public event EntrySearchedHandler EntrySearched;
        protected void OnEntrySearched()
        {
            entriesSearched++;
            if (EntrySearched != null)
                EntrySearched();
        }
    }
}

