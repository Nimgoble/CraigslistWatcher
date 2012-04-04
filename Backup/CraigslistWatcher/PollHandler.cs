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
        public List<string> TopFiveEntriesFromLastSearch { get; set; }
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
        public string City_ { get; set; }
        public string CityWebsite_ { get; set; }
        public List<string> Keywords_ { get; set; }
        public Dictionary<string, Dictionary<string, SubsectionDetails>> Sections_ { get; set; }
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
        private HtmlParser.ParseFilter filter_;
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
            filter_ = HtmlParser.ParseFilter.Create("html(body(blockquote[parent](p(a),font(a))))");
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
                            city_workers.Add(new BackgroundPoller(this, ref details, filter_));
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

    public class BackgroundPoller : EventWaitHandle
    {
        public BackgroundWorker Worker_ { get; set; }
        public CityDetails Details_  { get; set; }
        public PollHandler Parent_ { get; set; }
        public int matchingEntriesFound { get; set; }
        private HtmlParser.ParseFilter filter_;
        private int entries_searched_;
        private System.Diagnostics.Stopwatch stop_watch_;
        public BackgroundPoller(PollHandler parent, ref CityDetails details, HtmlParser.ParseFilter filter)
            : base(false, EventResetMode.ManualReset)
        {
            Parent_ = parent;
            Details_ = details;
            matchingEntriesFound = 0;
            Worker_ = new BackgroundWorker();
            filter_ = filter;
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
            for (int section_counter = 0; section_counter < Details_.Sections_.Count; section_counter++)
            {
                Dictionary<string, SubsectionDetails> sub_sections = Details_.Sections_.ElementAt(section_counter).Value;
                for (int sub_section_counter = 0; sub_section_counter < sub_sections.Count; sub_section_counter++)
                {
                    try
                    {
                        SubsectionDetails sub_details = sub_sections.ElementAt(sub_section_counter).Value;
                        List<string> last_five = sub_details.TopFiveEntriesFromLastSearch;
                        string site = Details_.CityWebsite_ + "/" + sub_details.Suffix_;
                        SearchSection(site, "", ref IndividualEntries, ref last_five);
                    }
                    catch (Exception error)
                    {
                        Logger.Instance.Log(error.ToString(), Details_.City_, LogType.ltError);
                    }
                }
            }
        }

        private void SearchSection(string base_section_site, string index_suffix, ref List<string> IndividualEntries, ref List<string> LastFiveEntriesSearched)
        {
            HtmlParser.HtmlParser parser = new HtmlParser.HtmlParser();
            if (!parser.ParseURL(base_section_site + index_suffix, true, new string[] { "<br>" }))
                return;

            List<HtmlParser.HtmlTag> nodes = parser._nodes;
            HtmlParser.HtmlTag new_parent = null;
            if (parser.FilterNodes(filter_, out new_parent))
            {
                foreach (HtmlTag pa in new_parent.Children)
                {
                    if (pa.Name == "p")
                    {
                        foreach (HtmlTag a in pa.Children)
                        {
                            string site = null;
                            if (a.Attributes.TryGetValue("href", out site))
                            {
                                if (LastFiveEntriesSearched.Count != 0 && LastFiveEntriesSearched.Contains(site))
                                {
                                    FillLastFive(ref LastFiveEntriesSearched, new_parent);
                                    return;
                                }
                                else
                                    SearchEntry(site, pa);
                            }
                        }
                    }
                    else
                    {
                        string suffix = null;
                        if (pa.Children.ElementAt(0).Attributes.TryGetValue("href", out suffix))
                            SearchSection(base_section_site, "/" + suffix, ref IndividualEntries, ref LastFiveEntriesSearched);

                    }
                }
                if (index_suffix == "")
                    FillLastFive(ref LastFiveEntriesSearched, new_parent);
            }          
        }

        private void FillLastFive(ref List<string> LastFiveEntriesSearched, HtmlTag parent)
        {
            int max_count = (parent.Children.Count < 5) ? parent.Children.Count : 5;
            for (int i = 0; i < max_count; i++)
            {
                HtmlTag temp_p = parent.Children[i];
                if (temp_p.Children.Count > 0)
                {
                    HtmlTag temp_a = temp_p.Children[0];
                    if (temp_a.Attributes.Count > 0)
                    {
                        string temp_site = "";
                        if (temp_a.Attributes.TryGetValue("href", out temp_site))
                            LastFiveEntriesSearched.Insert(LastFiveEntriesSearched.Count, temp_site);
                    }
                }
            }
        }

        //title = <html><head><title>
        //body = <html><body><div id="userbody">
        private void SearchEntry(string entry_site, HtmlTag parent)
        {
            if (entry_site == String.Empty)
                return;
            //

            HtmlParser.HtmlParser parser = new HtmlParser.HtmlParser();
            if (!parser.ParseURL(entry_site, true, new string[] {"<br>"}))
                return;

            List<HtmlParser.HtmlTag> nodes = parser._nodes;
            HtmlTag new_parent = new HtmlTag();
            new_parent.Name = "Artificial Parent";

            HtmlParser.ParseFilter title_filter = HtmlParser.ParseFilter.Create("html(head(title[parent]))");
            HtmlParser.ParseFilter body_filter = HtmlParser.ParseFilter.Create("html(head(div[parent]))");

            HtmlParser.HtmlTag title_tag = null;
            parser.FilterNodes(title_filter, out title_tag);
            HtmlParser.HtmlTag body_tag = null;
            parser.FilterNodes(body_filter, out body_tag);

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Name == "html")
                {
                    try
                    {
                        HtmlTag header_tag = null;
                        nodes[i].FilterForChildrenByName("title", out header_tag);
                        string title = header_tag.Children[0].Value;

                        Dictionary<string, KeyValuePair<string, string>> tag_list = new Dictionary<string, KeyValuePair<string, string>>();
                        tag_list.Add("div", new KeyValuePair<string, string>("id", "userbody"));
                        nodes[i].FilterForChildrenByNameAndAttribute(tag_list, ref new_parent);

                        //Deleted by author, expired, etc.
                        if(new_parent.Children.Count == 0)
                            continue;

                        string body = new_parent.Children[0].Value;
                        if (body != null && body != String.Empty)
                        {
                            body = body.ToLower();
                            foreach (string keyword in Details_.Keywords_)
                            {
                                if (!body.Contains(keyword) && !title.Contains(keyword))
                                {
                                    int start = entry_site.LastIndexOf('/');
                                    string output_file = entry_site.Substring(start, entry_site.Length - start);
                                    output_file += ".xml";
                                    System.IO.StreamWriter test_xml = new System.IO.StreamWriter(output_file, false);
                                    test_xml.WriteLine("Couldn't find: '" + keyword + "' in body: '" + body + "'");
                                    test_xml.WriteLine(parser.ToString());
                                    test_xml.Close();
                                    return;
                                }
                            }
                            matchingEntriesFound++;
                            Parent_.UpdateEntries(parent.ToString());
                        }
                    }
                    catch (Exception error)
                    {
                        Logger.Instance.Log(error.ToString(), Details_.City_, LogType.ltError);
                    }
                }
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
