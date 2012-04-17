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

namespace CraigslistWatcher
{
    public class SubsectionDetails
    {
        public SubsectionDetails()
        {
            Suffix_ = "";
            TopFiveEntriesFromLastSearch = new List<string>();
        }
        public SubsectionDetails(string suffix)
        {
            Suffix_ = suffix;
            TopFiveEntriesFromLastSearch = new List<string>();
        }
        public string Suffix_;
        public List<string> TopFiveEntriesFromLastSearch;
    }
    public class CityDetails
    {
        public CityDetails()
        {
            CityWebsite_ = "";
            Keywords_ = new List<string>();
            Sections_ = new Dictionary<string, Dictionary<string, SubsectionDetails>>();
        }
        public CityDetails(string city, string website, List<string> keywords, Dictionary<string, Dictionary<string, SubsectionDetails>> sections)
        {
            City_ = city;
            Keywords_ = keywords;
            Sections_ = sections;
            CityWebsite_ = website;
        }
        public string City_;
        public string CityWebsite_;
        public List<string> Keywords_;
        public Dictionary<string, Dictionary<string, SubsectionDetails>> Sections_;
    }
    public class PollHandler
    {
        public Dictionary<string /*Country*/, Dictionary<string /*state name*/, Dictionary<string /*city name*/, CityDetails /*city website*/>>> Areas_ { get; set; }
        private System.Timers.Timer  timer_ { get; set; }
        private List<string/*entry website*/> entry_list_ { get; set; }
        private TimeSpan refreshTime;
        private TimeSpan refreshTimeCounter;
        private TimeSpan timeout;

        private CLWTabPage main_form_;
        private object lock_object_ { get; set; }
        private bool polling_;
        public string to_string_;
        private System.Diagnostics.Stopwatch stop_watch_;
        private int total_searched_;
        private int matchingEntriesFound;

        public PollHandler(CLWTabPage main_form)
        {
            main_form_ = main_form;
            Areas_ = new Dictionary<string, Dictionary<string, Dictionary<string, CityDetails>>>();
            to_string_ = "";
            InitializeMembers();
        }

        public PollHandler(CLWTabPage main_form,
            Dictionary<string, Dictionary<string, Dictionary<string, CityDetails>>> Areas, 
            string to_string)
        {
            main_form_ = main_form;
            Areas_ = Areas;
            to_string_ = to_string;
            InitializeMembers();
        }

        ~PollHandler()
        {
        }

        void InitializeMembers()
        {
            lock_object_ = new object();
            timer_ = new System.Timers.Timer();
            timer_.Elapsed += new ElapsedEventHandler(Tick);
            timeout = new TimeSpan(0, 0, 1);
            polling_ = false;
            polling_ = false;
            total_searched_ = 0;
            matchingEntriesFound = 0;
            stop_watch_ = new System.Diagnostics.Stopwatch();
        }

        public override string ToString()
        {
            return this.to_string_;
        }

        public void Start(TimeSpan refreshTime)
        {
            try
            {
                this.refreshTime = new TimeSpan(refreshTime.Hours, refreshTime.Minutes, refreshTime.Seconds);
                if (polling_)
                    Logger.Instance.Log("Already refreshing.");
                else
                {
                    this.refreshTimeCounter = new TimeSpan(refreshTime.Hours, refreshTime.Minutes, refreshTime.Seconds);
                    timer_.AutoReset = false;
                    timer_.Interval = 1;
                    timer_.Start();
                    //DoPoll();
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
            if (!timer_.AutoReset)
            {
                DoPoll();
                timer_.AutoReset = true;
                timer_.Interval = 1000;
            }
            else if (refreshTimeCounter == TimeSpan.Zero)
            {
                timer_.Stop();
                DoPoll();
                refreshTimeCounter = refreshTimeCounter.Add(refreshTime);
            }
            else
            {
                main_form_.UpdateRefreshTimeControl(refreshTimeCounter.Minutes.ToString() + ":" + refreshTimeCounter.Seconds.ToString());
            }
        }

        public bool Stop()
        {
            if (Monitor.TryEnter(lock_object_))
            {
                try
                {
                    timer_.Stop();
                }
                finally
                {
                    Monitor.Exit(lock_object_);
                }

                return true;
            }

            return false;
        }

        public void DoPoll()
        {
            if (polling_)
                return;
            main_form_.UpdateRefreshTimeControl("Refreshing...");
            stop_watch_.Start();
            //This is going to take for-fucking-ever.
            polling_ = true;
            Logger.Instance.Log("Poll started.", to_string_);

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
                            city_workers.Add(new BackgroundPoller(this, ref details));
                        }
                        WaitHandle.WaitAll(city_workers.ToArray());
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                    }
                }
            }
            stop_watch_.Stop();
            polling_ = false;
            
            Logger.Instance.Log("Poll ended. Entries searched: " + total_searched_.ToString() +". Time elapsed: " + stop_watch_.Elapsed.ToString(), to_string_);
            total_searched_ = 0;
            timer_.Start();
        }

        public void UpdateEntries(string entry)
        {
            lock (lock_object_)
            {
                main_form_.UpdateEntriesFound(matchingEntriesFound++);
                main_form_.UpdateEntries(entry);
            }
        }

        public void UpdateTotalSearched()
        {
            lock (lock_object_)
            {
                main_form_.UpdateEntriesSearched(total_searched_++);
            }
        }
    }
}
