using System;
using System.Diagnostics;
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
    public partial class CLWTabPage : /*System.Windows.Forms.UserControl,*/ System.Windows.Forms.TabPage
    {
        private List<string> keywords {get;set;}
        private TimeSpan tickInterval;
        private TimeSpan refreshInterval;
        private Int32 previousRefreshMin1;
        private Int32 Min1, Min2, Sec1, Sec2;
        private PollHandler pollHandler;
        //This is never really implemented...
        private String tabName;
        private Int32 totalFound;
        private Int32 totalSearched;
        private Int32 totalEntries;
        private static string entryFormat = "<font style=\"font-size:12px;text-align:left\">{0}</font>";
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
            pollHandler = new PollHandler();
            pollHandler.PollTimerTick += new PollHandler.PollTimerTickHandler(this.UpdateRefreshTimeControl);
            pollHandler.EntryFound += new PollHandler.EntryFoundHandler(this.UpdateEntries);
            pollHandler.PollStarted +=new PollHandler.PollStartedHandler(this.PollStarted);
            pollHandler.PollEnded += new PollHandler.PollEndedHandler(this.PollEnded);
            pollHandler.NumberOfEntriesFound += new PollHandler.NumberOfEntriesFoundHandler(this.UpdateTotalEntries);
            Locations.Instance.PopulateTreeView(ref this.trAreas);
            Categories.Instance.PopulateTreeView(ref this.trSections);
            this.wbEntries.Navigate("about:blank");
            this.wbEntries.Document.OpenNew(true);
            this.wbEntries.Refresh();
        }
        public void PollStarted()
        {
            totalFound = 0;
            totalSearched = 0;
            UpdateEntriesFound();
            UpdateEntriesSearched(null);
        }
        public void PollEnded()
        {
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
        private void UpdateEntriesFound()
        {
            if(this.lblEntriesFound.InvokeRequired)
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
        private void UpdateTotalEntries(Int32 numEntries)
        {
            if (this.lblTotalEntries.InvokeRequired)
                this.lblTotalEntries.Invoke(new MethodInvoker(delegate() { UpdateTotalEntries(numEntries); }));
            else
            {
                try
                {
                    this.lblTotalEntries.Text = "Entries Found: " + (totalEntries += numEntries).ToString();
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.Log(ex.ToString(), LogType.ltError);
                }
            }
        }
        public void UpdateEntries(EntryInfo entry)
        {
            if(this.wbEntries.InvokeRequired)
                this.wbEntries.Invoke(new MethodInvoker(delegate() { UpdateEntries(entry); }));
            else
            {
                try
                {
                    string body = entry.Body.ToLower();
                    string title = entry.Title.ToLower();
                    foreach (string key in keywords)
                    {
                        if (!body.Contains(key) && !title.Contains(key))
                            continue;
                        
                        string output = String.Format(CLWTabPage.entryFormat, entry.ToString());
                        this.wbEntries.Document.Write(output);
                        UpdateEntriesFound();
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Instance.Log(ex.ToString() + ": " + entry, LogType.ltError);
                }
                if (this.wbEntries.Document.Body != null)
                    this.wbEntries.Document.Window.ScrollTo(0, this.wbEntries.Document.Body.ScrollRectangle.Bottom);

                Application.DoEvents();
            }
        }
        public void UpdateEntriesSearched(EntryInfo entry)
        {
            if(this.lblEntriesSearched.InvokeRequired)
                this.lblEntriesSearched.Invoke((MethodInvoker)delegate() { UpdateEntriesSearched(entry); });
            else
            {
                try
                {
                    this.lblEntriesSearched.Text = "Entries Searched: " + (++totalSearched).ToString();
                    if(entry != null)
                        UpdateEntries(entry);
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

            totalFound = -1;
            totalSearched = -1;
            UpdateEntriesFound();
            UpdateEntriesSearched(null);
            totalEntries = 0;
            UpdateTotalEntries(0);

            Dictionary<string, Dictionary<string, Dictionary<string, CityDetails>>> Areas = new Dictionary<string, Dictionary<string, Dictionary<string, CityDetails>>>();
            Dictionary<string, Dictionary<string, SubsectionDetails>> sections = new Dictionary<string, Dictionary<string, SubsectionDetails>>();

            //Iterate through sections
            for (int sections_counter = 0; sections_counter < trSections.Nodes.Count; sections_counter++)
            {
                TreeNode section_node = trSections.Nodes[sections_counter];
                string section_name = section_node.Text;
                Dictionary<string, SubsectionDetails> subsections = new Dictionary<string, SubsectionDetails>();
                if (section_node.Checked)
                {
                    string section_suffix = Categories.Instance.GetSuffix(section_name, null);
                    subsections.Add(section_name, new SubsectionDetails(section_suffix));
                    sections.Add(section_name, subsections);
                }
                else
                {
                    for (int subsection_counter = 0; subsection_counter < section_node.Nodes.Count; subsection_counter++)
                    {
                        TreeNode subsection_node = section_node.Nodes[subsection_counter];
                        if (subsection_node.Checked)
                        {
                            string subsection_name = subsection_node.Text;
                            string section_suffix = Categories.Instance.GetSuffix(section_name, subsection_name);
                            subsections.Add(subsection_name, new SubsectionDetails(section_suffix));
                        }
                    }
                    if (subsections.Count != 0)
                        sections.Add(section_name, subsections);
                }
            }


            //This is wrong.  It just copies the whole thing, whether they are checked or not.
            //Iterate through countries
            for (int parent_area_counter = 0; parent_area_counter < trAreas.Nodes.Count; parent_area_counter++)
            {
                //Current country in tree
                TreeNode country_node = trAreas.Nodes[parent_area_counter];
                string country_name = country_node.Text;
                //Add country and its new state_map to our new dictionary
                //Areas.Add(country_name, new Dictionary<string, Dictionary<string, string>>());
                //Get the above state_map
                //Dictionary<string, Dictionary<string, string>> state_map = Areas[country_name];
                Dictionary<string, Dictionary<string, CityDetails>> state_map = new Dictionary<string, Dictionary<string, CityDetails>>();
                //Iterate through states in this country
                for (int state_counter = 0; state_counter < country_node.Nodes.Count; state_counter++)
                {
                    //Current state in tree
                    TreeNode state_node = country_node.Nodes[state_counter];
                    string state_name = state_node.Text;
                    //Add state and its new city_map to our new dictionary
                    //state_map.Add(state_name, new Dictionary<string, string>());
                    //Get the above city_map
                    //Dictionary<string, string> city_map = Areas[country_name][state_name];
                    Dictionary<string, CityDetails> city_map = new Dictionary<string, CityDetails>();
                    //Iterate through cities in this state
                    for (int city_counter = 0; city_counter < state_node.Nodes.Count; city_counter++)
                    {
                        //Current state in tree
                        TreeNode city_node = state_node.Nodes[city_counter];
                        if (city_node.Checked)
                        {
                            string city_name = city_node.Text;
                            string website = Locations.Instance.LocationDictionary[country_name][state_name][city_name];
                            city_map.Add(city_name, new CityDetails(city_name, website, sections));
                        }
                    }
                    if (city_map.Count != 0)
                        state_map.Add(state_name, city_map);
                }
                if (state_map.Count != 0)
                    Areas.Add(country_name, state_map);
            }

            if (Areas.Count == 0)
            {
                MessageBox.Show("Please choose at least one area.", "Error", MessageBoxButtons.OK);
                return;
            }

            if (sections.Count == 0)
            {
                MessageBox.Show("Please choose at least one section.", "Error", MessageBoxButtons.OK);
                return;
            }

            if (keywords.Count == 0)
            {
                MessageBox.Show("Please enter at least one keyword.", "Error", MessageBoxButtons.OK);
                return;
            }

            pollHandler.Areas = Areas;
            pollHandler.toString = tabName;
            pollHandler.Start(refreshInterval);
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
    }
}
