using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using HtmlParser;

namespace CLWFramework
{
    //public partial class CLWTabPage : System.Windows.Forms.TabPage
    public partial class CLWTabPage : /*System.Windows.Forms.UserControl,*/ System.Windows.Forms.TabPage, BasePollEventHandler
    {
        private List<string> keywords {get;set;}
        private TimeSpan tickInterval;
        private TimeSpan refreshInterval;
        private Int32 previousRefreshMin1;
        private Int32 Min1, Min2, Sec1, Sec2;
        private AreaPollHandler pollHandler;
        //This is never really implemented...
        private String tabName;
        private Int32 totalFound;
        private Int32 totalSearched;
        private Int32 totalEntries;
        private static string entryFormat = "<font style=\"font-size:12px;text-align:left\">{0}</font>";

        private Dictionary<AreaDetails, List<string>> areaLastFiveSearched;

        BaseBackgroundPoller.NumberOfEntriesFoundHandler _numberOfEntriesFoundHandler;
        BaseBackgroundPoller.EntryFoundHandler _entryFoundHandler;
        BaseBackgroundPoller.EntryParsedHandler _entryParsedHandler;
        BaseBackgroundPoller.PollDoneHandler _pollDoneHandler;
        BaseBackgroundPoller.PollErrorHandler _pollErrorHandler;

        public BaseBackgroundPoller.NumberOfEntriesFoundHandler numberOfEntriesFoundHandler 
        {
            get
            {
                return _numberOfEntriesFoundHandler;
            }
        }
        public BaseBackgroundPoller.EntryFoundHandler entryFoundHandler 
        {
            get
            {
                return _entryFoundHandler;
            }
        }
        public BaseBackgroundPoller.EntryParsedHandler entryParsedHandler 
        {
            get
            {
                return _entryParsedHandler;
            }
        }
        public BaseBackgroundPoller.PollDoneHandler pollDoneHandler 
        {
            get
            {
                return _pollDoneHandler;
            }
        }
        public BaseBackgroundPoller.PollErrorHandler pollErrorHandler 
        {
            get
            {
                return _pollErrorHandler;
            }
        }
        public CLWTabPage()
        {
            InitializeComponent();
            tickInterval = new TimeSpan(0, 0, 1);
            refreshInterval = new TimeSpan(0, 0, 0);
            keywords = new List<string>();
            previousRefreshMin1 = 0;
            Min1 = 0;
            Min2 = 0;
            Sec1 = 0;
            Sec2 = 0;
            totalFound = 0;
            totalSearched = 0;
            totalEntries = 0;
            pollHandler = new AreaPollHandler();

            areaLastFiveSearched = new Dictionary<AreaDetails, List<string>>();

            pollHandler.PollTimerTick += new AreaPollHandler.PollTimerTickHandler(this.UpdateRefreshTimeControl);
            pollHandler.PollStarted += new AreaPollHandler.PollStartedHandler(this.PollStarted);
            pollHandler.PollEnded += new AreaPollHandler.PollEndedHandler(this.PollEnded);

            _pollErrorHandler = new BaseBackgroundPoller.PollErrorHandler(this.OnPollError);
            _pollDoneHandler = new BaseBackgroundPoller.PollDoneHandler(this.OnPollDone);
            _numberOfEntriesFoundHandler = new BaseBackgroundPoller.NumberOfEntriesFoundHandler(this.OnNumberOfEntriesFound);
            _entryParsedHandler = new BaseBackgroundPoller.EntryParsedHandler(this.OnEntryParsed);
            _entryFoundHandler = new BaseBackgroundPoller.EntryFoundHandler(this.OnEntryFound);

            Locations.Instance.PopulateTreeView(ref this.trAreas);
            Categories.Instance.PopulateTreeView(ref this.trSections);
            this.wbEntries.Navigate("about:blank");
            this.wbEntries.Document.OpenNew(true);
            this.wbEntries.Refresh();
        }
        private void Reset()
        {
            if(this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate() { Reset(); });
            else
            {
                this.lblEntriesFound.Text = "Entries Found: " + (totalFound = 0).ToString();
                this.lblEntriesSearched.Text = "Entries Searched: " + (totalSearched = 0).ToString();
                this.lblTotalEntries.Text = "Total Entries: " + (totalEntries = 0).ToString();
            }
        }
        public void PollStarted()
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate() { PollStarted(); });
            Reset();
            txtKeywords.Enabled = false;
            lstKeywords.Enabled = false;
        }
        public void PollEnded()
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate() { PollEnded(); });
            txtKeywords.Enabled = true;
            lstKeywords.Enabled = true;
        }
        public void UpdateRefreshTimeControl(String timeLeft)
        {
            if (this.lblRefreshCountdown.InvokeRequired)
                this.lblRefreshCountdown.Invoke((MethodInvoker)delegate() { UpdateRefreshTimeControl(timeLeft); });
            else
            {
                try
                {
                    lblRefreshCountdown.Text = timeLeft;
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.Log(ex.ToString(), LogType.ltError);
                }
            }
           
        }

        private void trAreas_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
                HandleCheck(e.Node);
        }
        /*
         * -If the node is checked and has children, check them all
         * -If the node is unchecked and has a parent, uncheck parent
         * -
         * */
        private void HandleCheck(TreeNode node)
        {
            int i = 0;
            /*Update children to reflect parent*/
            for (i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].Checked = node.Checked;
                HandleCheck(node.Nodes[i]);
            }

            /*If all siblings are checked, including us, check parent*/
            if (node.Checked)
            {
                if (node.Parent != null)
                {
                    bool all_checked = true;
                    for (i = 0; i < node.Parent.Nodes.Count; i++)
                    {
                        if (!node.Parent.Nodes[i].Checked)
                        {
                            all_checked = false;
                            break;
                        }
                    }
                    if (all_checked)
                        node.Parent.Checked = true;
                }
            }
            else
            {
                TreeNode parent = node.Parent;
                while (parent != null)
                {
                    parent.Checked = false;
                    parent = parent.Parent;
                }
            }
        }

        private void trSections_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
                HandleCheck(e.Node);
        }

        private void txtKeywords_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //keywords.Add(txtKeywords.Text);
                lstKeywords.Items.Add(txtKeywords.Text);
                keywords.Add(txtKeywords.Text.ToLower());
                txtKeywords.Text = "";
            }
        }

        private void lstKeywords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete &&
                lstKeywords.SelectedIndex != -1)
            {
                int index = lstKeywords.SelectedIndex;
                lstKeywords.Items.RemoveAt(index);
                keywords.RemoveAt(index);
            }
        }

        private void btnForceRefresh_Click(object sender, EventArgs e)
        {
            //pollHandler.Stop();

            if (this.btnForceRefresh.Text == "Start Search")
                this.btnForceRefresh.Text = "Refresh";

            if (keywords.Count == 0)
            {
                MessageBox.Show("Please enter at least one keyword.", "Error", MessageBoxButtons.OK);
                return;
            }

            //Get each section.
            List<KeyValuePair<string, string>> sections = new List<KeyValuePair<string, string>>();
            foreach(TreeNode sectionNode in trSections.Nodes)
            {
                if(sectionNode.Checked)
                {
                    //We want this whole section
                    sections.Add(new KeyValuePair<string, string>(sectionNode.Text, String.Empty));
                    continue;
                }
                else
                {
                    foreach(TreeNode subsectionNode in sectionNode.Nodes)
                    {
                        if(subsectionNode.Checked)
                            sections.Add(new KeyValuePair<string,string>(sectionNode.Text, subsectionNode.Text));
                    }
                }
            }

            if (sections.Count == 0)
            {
                MessageBox.Show("Please choose at least one section.", "Error", MessageBoxButtons.OK);
                return;
            }

            List<AreaDetails> areaDetails = new List<AreaDetails>();
            foreach(TreeNode CountryNode in trAreas.Nodes)
            {
                //We want the whole country.
                if(CountryNode.Checked)
                {
                    foreach(KeyValuePair<string, string> section in sections)
                        areaDetails.AddRange(Areas.Instance.AreasList.FindAll(area => area.Country == CountryNode.Text && 
                                                                              area.Section == section.Key &&
                                                                              area.Subsection == section.Value));
                }
                else
                {
                    foreach(TreeNode StateNode in CountryNode.Nodes)
                    {
                        if(StateNode.Checked)
                        {
                            foreach(KeyValuePair<string, string> section in sections)
                                areaDetails.AddRange(Areas.Instance.AreasList.FindAll(area => area.Country == CountryNode.Text && 
                                                                              area.State == StateNode.Text &&
                                                                              area.Section == section.Key &&
                                                                              area.Subsection == section.Value));
                        }
                        else
                        {
                            foreach(TreeNode CityNode in StateNode.Nodes)
                            {
                                if(CityNode.Checked)
                                {
                                    foreach(KeyValuePair<string, string> section in sections)
                                        areaDetails.AddRange(Areas.Instance.AreasList.FindAll(area => area.Country == CountryNode.Text && 
                                                                              area.State == StateNode.Text &&
                                                                              area.City == CityNode.Text &&
                                                                              area.Section == section.Key &&
                                                                              area.Subsection == section.Value));
                                }
                            }
                        }
                    }
                }
            }

            if(areaDetails.Count == 0)
            {
                MessageBox.Show("Please choose at least one area.", "Error", MessageBoxButtons.OK);
                return;
            }

            areaLastFiveSearched = areaDetails.ToDictionary(v => v, v => new List<string>());

            pollHandler.Subscribe(areaDetails, this);
            if (!pollHandler.Start(refreshInterval))
            {
                areaLastFiveSearched.Clear();
                return;
            }
        }

        private void nudMin1_ValueChanged(object sender, EventArgs e)
        {
            if (sender != this.nudMin1)
                return;
            Min1 = Decimal.ToInt32(this.nudMin1.Value);
            UpdateRefreshTime();
        }

        private void nudMin2_ValueChanged(object sender, EventArgs e)
        {
            if (sender != this.nudMin2)
                return;
            Min2 = Decimal.ToInt32(this.nudMin2.Value);
            UpdateRefreshTime();
        }

        private void nudSec1_ValueChanged(object sender, EventArgs e)
        {
            if (sender != this.nudSec1)
                return;
            Sec1 = Decimal.ToInt32(this.nudSec1.Value);
            UpdateRefreshTime();
        }

        private void nudSec2_ValueChanged(object sender, EventArgs e)
        {
            if (sender != this.nudSec2)
                return;
            Sec2 = Decimal.ToInt32(this.nudSec2.Value);
            UpdateRefreshTime();
        }

        void UpdateRefreshTime()
        {
            if (Min1!= this.previousRefreshMin1)
            {
                if (Min1 == 6 && this.previousRefreshMin1 != 6)
                {
                    this.nudMin2.Maximum = this.nudMin2.Value = Min2 = 0;
                    this.nudSec1.Maximum = this.nudSec1.Value = Sec1 = 0;
                    this.nudSec2.Maximum = this.nudSec2.Value = Sec2 = 0;
                }
                else if (this.previousRefreshMin1 == 6)
                {
                    this.nudMin2.Maximum = Min2 = 9;
                    this.nudSec1.Maximum = Sec1 = 5;
                    this.nudSec2.Maximum = Sec2 = 9;
                }
                this.previousRefreshMin1 = Min1;
            }
            //Long and Ugly.
            refreshInterval = new TimeSpan(0, (10 * Min1) + Min2, (10 * Sec1) + Sec2);
        }

        private void wbEntries_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //Open anything from craigslist in the browser
            if (e.Url.ToString().Contains("craigslist"))
                Process.Start(e.Url.ToString());
            //anything else, besides a reset, gets the boot
            if (!e.Url.ToString().Contains("about:blank"))
                e.Cancel = true;
        }

        protected void OnNumberOfEntriesFound(BaseBackgroundPoller poller, Int32 numEntries)
        {
            if (this.lblTotalEntries.InvokeRequired)
                this.lblTotalEntries.Invoke(new MethodInvoker(delegate() { OnNumberOfEntriesFound(poller, numEntries); }));
            else
            {
                try
                {
                    if (!areaLastFiveSearched.Keys.Contains(poller.PollerAreaDetails))
                        return;
                    this.lblTotalEntries.Text = "Total Entries: " + (totalEntries += numEntries).ToString();
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.Log(ex.ToString(), LogType.ltError);
                }
            }
            
        }
        protected void OnEntryFound(BaseBackgroundPoller poller, EntryInfo info)
        {
            List<string> lastFiveSearched = null;
            if (!areaLastFiveSearched.TryGetValue(poller.PollerAreaDetails, out lastFiveSearched) || lastFiveSearched.Contains(info.URL))
            {
                poller.EntryFound -= this._entryFoundHandler;
                return;
            }

            lastFiveSearched.Insert(0, info.URL);
        }
        protected void OnEntryParsed(BaseBackgroundPoller poller, EntryInfo info)
        {
            if (this.wbEntries.InvokeRequired)
                this.wbEntries.Invoke(new MethodInvoker(delegate() { OnEntryParsed(poller, info); }));
            else
            {
                try
                {
                    string body = info.Body.ToLower();
                    string title = info.Title.ToLower();
                    foreach (string key in keywords)
                    {
                        if (body.Contains(key) || title.Contains(key))
                        {
                            string output = String.Format(CLWTabPage.entryFormat, info.ToString());
                            this.wbEntries.Document.Write(output);
                            if (this.wbEntries.Document.Body != null)
                                this.wbEntries.Document.Window.ScrollTo(0, this.wbEntries.Document.Body.ScrollRectangle.Bottom);
                            Application.DoEvents();
                            UpdateEntriesFound();
                            break;
                        }
                    }
                    UpdateEntriesSearched();
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.Log(ex.ToString() + ": " + info, LogType.ltError);
                }
            }
        }
        protected void OnPollDone(BaseBackgroundPoller poller, string message)
        {
            List<string> lastFiveSearched = null;
            if (!areaLastFiveSearched.TryGetValue(poller.PollerAreaDetails, out lastFiveSearched))
            {
                if(lastFiveSearched.Count > 5)
                    lastFiveSearched.RemoveRange(5, lastFiveSearched.Count - 5);
            }
            Logger.Instance.Log(message, poller.PollerAreaDetails.City, LogType.ltArea);
        }
        protected void OnPollError(BaseBackgroundPoller poller, string area, string message)
        {
            Logger.Instance.Log(message, area, LogType.ltError);
        }

        private void UpdateEntriesFound()
        {
            if (this.lblEntriesFound.InvokeRequired)
                this.lblEntriesFound.Invoke(new MethodInvoker(delegate() { UpdateEntriesFound(); }));
            else
            {
                try
                {
                    this.lblEntriesFound.Text = "Entries Found: " + (++totalFound).ToString();
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.Log(ex.ToString(), LogType.ltError);
                }
            }
        }
        public void UpdateEntriesSearched()
        {
            if (this.lblEntriesSearched.InvokeRequired)
                this.lblEntriesSearched.Invoke((MethodInvoker)delegate() { UpdateEntriesSearched(); });
            else
            {
                try
                {
                    this.lblEntriesSearched.Text = "Entries Searched: " + (++totalSearched).ToString();
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.Log(ex.ToString(), LogType.ltError);
                }
            }
        }
    }
}
