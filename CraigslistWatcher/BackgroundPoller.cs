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
namespace CraigslistWatcher
{
    public class BackgroundPoller : EventWaitHandle
    {
        public BackgroundWorker Worker_ { get; set; }
        public CityDetails Details_ { get; set; }
        public PollHandler Parent_ { get; set; }
        public int matchingEntriesFound { get; set; }
        private int entries_searched_;
        private System.Diagnostics.Stopwatch stop_watch_;
        public BackgroundPoller(PollHandler parent, ref CityDetails details)
            : base(false, EventResetMode.ManualReset)
        {
            Parent_ = parent;
            Details_ = details;
            matchingEntriesFound = 0;
            Worker_ = new BackgroundWorker();
            entries_searched_ = 0;
            stop_watch_ = new System.Diagnostics.Stopwatch();
            Worker_.DoWork += this.PollCity;
            Worker_.RunWorkerCompleted += this.PollDone;
            Worker_.RunWorkerAsync();
        }

        public void PollCity(object sender, DoWorkEventArgs e)
        {
            stop_watch_.Start();
            List<string> IndividualEntries = new List<string>();
            foreach (Dictionary<string, SubsectionDetails> subSections in Details_.Sections_.Values)
            {
                foreach (SubsectionDetails subSection in subSections.Values)
                {
                    try
                    {
                        string site = Details_.CityWebsite_ + "/" + subSection.Suffix_;
                        SearchSection(site, "", ref IndividualEntries, ref subSection.TopFiveEntriesFromLastSearch);
                    }
                    catch (Exception error)
                    {
                        Logger.Instance.Log(error.ToString(), Details_.City_, LogType.ltError);
                    }
                }
            }
        }

        private void SearchSection(string sectionSite, string indexSuffix, ref List<string> IndividualEntries, ref List<string> LastFiveEntriesSearched)
        {
            EntryFilter entryFilter = new EntryFilter();
            try
            {
                entryFilter.Populate(sectionSite + indexSuffix);
            }
            catch (Exception e)
            {
                Logger.Instance.Log(e.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, LogType.ltError);
            }

            foreach (EntryFilter.EntryInfo entryInfo in entryFilter.EntryList)
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
                SearchSection(sectionSite, "/" + entryFilter.NextHundred, ref IndividualEntries, ref LastFiveEntriesSearched);
            else
            {
                int i = 0;//Debug
            }
        }

        private void FillLastFive(ref List<string> LastFiveEntriesSearched, List<EntryFilter.EntryInfo> entries)
        {
            int max_count = (entries.Count < 5) ? entries.Count : 5;
            for (int i = 0; i < max_count; i++)
            {
                EntryFilter.EntryInfo entry = entries[i];
                LastFiveEntriesSearched.Insert(LastFiveEntriesSearched.Count, entry.URL);
            }
        }

        //title = <html><head><title>
        //body = <html><body><div id="userbody">
        private void SearchEntry(EntryFilter.EntryInfo entry)
        {
            try
            {
                AdFilter adFilter = new AdFilter();
                try { adFilter.Populate(entry.URL); }
                catch (Exception error) { Logger.Instance.Log(error.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, LogType.ltError); }
                
                if (adFilter.Body != null && adFilter.Body != String.Empty)
                {
                    string body = adFilter.Body.ToLower();
                    string title = entry.Title.ToLower();
                    foreach (string keyword in Details_.Keywords_)
                    {
                        if (body.Contains(keyword) || title.Contains(keyword))
                        {
                            matchingEntriesFound++;
                            Parent_.UpdateEntries(entry.ToString());
                            return;
                        }
                    }
                    /*string entrySite = entry.URL;
                    int start = entrySite.LastIndexOf('/');
                    string output_file = entrySite.Substring(start, entrySite.Length - start);
                    output_file += ".xml";
                    System.IO.StreamWriter test_xml = new System.IO.StreamWriter(output_file, false);
                    test_xml.WriteLine("Couldn't find: '" + string.Join(",", Details_.Keywords_.Select(x => x.ToString()).ToArray()) + "' in body: '" + body + "'");
                    test_xml.WriteLine(adFilter.ToString());
                    test_xml.Close();*/
                }
            }
            finally
            {
                entries_searched_++;
                Parent_.UpdateTotalSearched();
            }

        }

        public void PollDone(object sender, RunWorkerCompletedEventArgs e)
        {
            stop_watch_.Stop();

            string output = "Found " + matchingEntriesFound.ToString() + " new entries out of " + entries_searched_.ToString() + " entries searched in " + stop_watch_.Elapsed.ToString();
            Logger.Instance.Log(output, Details_.City_, LogType.ltArea);

            base.Set();
        }
    }
}

