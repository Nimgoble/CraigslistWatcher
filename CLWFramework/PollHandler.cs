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

//msg: body->blockquote->p->a
//w4m: body->blockquote->p->a
//jobs: body->blockquote->p->a
//housing: body->blockquote->p->a
//gigs: body->blockquote->p->a
//for sale: body->blockquote->p->a
//community: body->blockquote->p->a

//once the website has been queried and parsed, search for this: <div id="userbody">

//If our last_poll_starting_entry is blank, search the entire repository for this city/(sub)section
//If not, set our last_poll_starting_entry to the first entry of this search, then continue searching until we hit the previous
//last_poll_starting_entry.
//What happens if our last_poll_starting_entry is deleted?  Keep a list of the last 5 and do a list.Contains(current_entry)?

namespace CLWFramework
{
    public class PollHandler
    {
        public Dictionary<string /*Country*/, Dictionary<string /*state name*/, Dictionary<string /*city name*/, CityDetails /*city website*/>>> Areas_ { get; set; }
        private System.Timers.Timer  timer { get; set; }
        private List<string/*entry website*/> entryList { get; set; }
        private TimeSpan refreshTime;
        private TimeSpan refreshTimeCounter;
        private TimeSpan timeout;

        private object lockObject { get; set; }
        private bool polling;
        public string toString;
        private System.Diagnostics.Stopwatch stopWatch;
        private int totalSearched;
        private int matchingEntriesFound;

        private BackgroundPoller.EntrySearchedHandler entrySearchedHandler;
        private BackgroundPoller.EntryFoundHandler entryFoundHandler;
        private BackgroundPoller.NumberOfEntriesFoundHandler numberOfEntriesFoundHandler;

        public PollHandler()
        {
            Areas_ = new Dictionary<string, Dictionary<string, Dictionary<string, CityDetails>>>();
            toString = "";
            entrySearchedHandler = new BackgroundPoller.EntrySearchedHandler(this.OnEntrySearched);
            entryFoundHandler = new BackgroundPoller.EntryFoundHandler(this.OnEntryFound);
            numberOfEntriesFoundHandler = new BackgroundPoller.NumberOfEntriesFoundHandler(this.OnNumberOfEntriesFound);
            InitializeMembers();
        }

        public PollHandler(Dictionary<string, Dictionary<string, Dictionary<string, CityDetails>>> Areas, 
                            string to_string)
        {
            Areas_ = Areas;
            toString = to_string;
            InitializeMembers();
        }

        ~PollHandler()
        {
        }

        void InitializeMembers()
        {
            lockObject = new object();
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(Tick);
            timeout = new TimeSpan(0, 0, 1);
            polling = false;
            polling = false;
            totalSearched = 0;
            matchingEntriesFound = 0;
            stopWatch = new System.Diagnostics.Stopwatch();
        }

        public override string ToString()
        {
            return this.toString;
        }

        public void Start(TimeSpan refreshTime)
        {
            try
            {
                this.refreshTime = new TimeSpan(refreshTime.Hours, refreshTime.Minutes, refreshTime.Seconds);
                if (polling)
                    Logger.Instance.Log("Already refreshing.");
                else
                {
                    this.refreshTimeCounter = new TimeSpan(refreshTime.Hours, refreshTime.Minutes, refreshTime.Seconds);
                    timer.AutoReset = false;
                    timer.Interval = 1;
                    timer.Start();
                }
               
            }
            catch (System.Exception ex)
            {
                Logger.Instance.Log(ex.ToString());
            }
        }

        public void Tick(object sender, ElapsedEventArgs e)
        {
            refreshTimeCounter = refreshTimeCounter.Subtract(timeout);
            if (!timer.AutoReset)
            {
                DoPoll();
                timer.AutoReset = true;
                timer.Interval = 1000;
            }
            else if (refreshTimeCounter == TimeSpan.Zero)
            {
                timer.Stop();
                DoPoll();
                refreshTimeCounter = refreshTimeCounter.Add(refreshTime);
            }
            else
            {
                OnPollTimerTick(refreshTimeCounter.Minutes.ToString() + ":" + refreshTimeCounter.Seconds.ToString());
            }
        }

        public bool Stop()
        {
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    timer.Stop();
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }

                return true;
            }

            return false;
        }

        public delegate void RefreshBeginHandler();
        public event RefreshBeginHandler RefreshBegin;
        public void DoPoll()
        {
            if (polling)
                return;
            OnPollStarted();
            //This is going to take for-fucking-ever.
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, CityDetails>>> statemap_keyvalue_pair in Areas_)
            {
                Dictionary<string, Dictionary<string, CityDetails>> state_map = statemap_keyvalue_pair.Value;
                foreach (KeyValuePair<string, Dictionary<string, CityDetails>> citymap_keyvalue_pair in state_map)
                {
                    Dictionary<string, CityDetails> city_map = citymap_keyvalue_pair.Value;
                    try
                    {
                        List<EventWaitHandle> city_workers = new List<EventWaitHandle>();
                        foreach (KeyValuePair<string, CityDetails> city_details in city_map)
                        {
                            CityDetails details = city_details.Value;
                            BackgroundPoller backgroundPoller = new BackgroundPoller(ref details);
                            backgroundPoller.EntrySearched += this.entrySearchedHandler;
                            backgroundPoller.EntryFound += this.entryFoundHandler;
                            backgroundPoller.NumberOfEntriesFound += this.numberOfEntriesFoundHandler;
                            city_workers.Add(backgroundPoller);
                        }
                        WaitHandle.WaitAll(city_workers.ToArray());
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                    }
                }
            }
            OnPollEnded();
        }

        public delegate void NumberOfEntriesFoundHandler(Int32 numEntries);
        public event NumberOfEntriesFoundHandler NumberOfEntriesFound;
        private readonly object onNumberOfEntriesFoundLock = new object();
        public void OnNumberOfEntriesFound(Int32 numEntries)
        {
            lock (onNumberOfEntriesFoundLock)
            {
                if (NumberOfEntriesFound != null)
                    NumberOfEntriesFound(numEntries);
            }
        }

        public delegate void EntrySearchedHandler();
        public event EntrySearchedHandler EntrySearched;
        private readonly object onEntrySearchedLock = new object();
        public void OnEntrySearched()
        {
            lock (onEntrySearchedLock)
            {
                if (EntrySearched != null)
                    EntrySearched();
            }
        }

        public delegate void EntryFoundHandler(EntryInfo entry);
        public event EntryFoundHandler EntryFound;
        private readonly object onEntryFoundLock = new object();
        public void OnEntryFound(EntryInfo entry)
        {
            lock (onEntryFoundLock)
            {
                if (EntryFound != null)
                    EntryFound(entry);
            }
        }

        public delegate void PollTimerTickHandler(string timeleft);
        public event PollTimerTickHandler PollTimerTick;
        private readonly object onPollTimerTickLock = new object();
        private void OnPollTimerTick(string timeleft)
        {
            lock (onPollTimerTickLock)
            {
                if (PollTimerTick != null)
                    PollTimerTick(timeleft);
            }
        }

        public delegate void PollStartedHandler();
        public event PollStartedHandler PollStarted;
        private readonly object onPollStartedLock = new object();
        public void OnPollStarted()
        {
            lock (onPollStartedLock)
            {
                OnPollTimerTick("Refreshing...");
                stopWatch.Start();
                polling = true;
                Logger.Instance.Log("Poll started.", toString);
                if (PollStarted != null)
                    PollStarted();
            }
        }

        public delegate void PollEndedHandler();
        public event PollEndedHandler PollEnded;
        private readonly object onPollEndedLock = new object();
        public void OnPollEnded()
        {
            lock (onPollEndedLock)
            {
                stopWatch.Stop();
                polling = false;
                totalSearched = 0;
                timer.Start();
                Logger.Instance.Log("Poll ended. Entries searched: " + totalSearched.ToString() + ". Time elapsed: " + stopWatch.Elapsed.ToString(), toString);
                if (PollEnded != null)
                    PollEnded();
            }
        }
    }
}
