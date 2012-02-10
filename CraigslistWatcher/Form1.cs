using System;
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

namespace CraigslistWatcher
{
    public partial class CraigslistWatcher : Form
    {
        public List<PollHandler> poll_handlers_ { get; set; }
        public object mutex_ { get; set; }

        public CraigslistWatcher()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mutex_ = new object();
            poll_handlers_ = new List<PollHandler>();
            //Logger.Instance.log_form_ = this;
            this.wbLog.Navigate("about:blank");
            this.wbLog.Document.OpenNew(true);
            this.wbLog.Refresh();

            this.wbEntries.Navigate("about:blank");
            this.wbEntries.Document.OpenNew(true);
            this.wbEntries.Refresh();

            try
            {
                Locations.Instance.DownloadLocations();
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex.ToString(), LogType.ltError);
            }
        }

        public void AddNewPollHandler(Dictionary<string, Dictionary<string, Dictionary<string, CityDetails>>> Areas,
            Dictionary<string, Dictionary<string, SubsectionDetails>> sections,
            List<string> keywords)
        {
            string to_string = "";
            for (int i = 0; i < keywords.Count; i++)
            {
                to_string += keywords[i];
                if (i + 1 != keywords.Count)
                    to_string += ", ";
            }
            int index = poll_handlers_.Count;
            //poll_handlers_.Insert(index, new PollHandler(this, Areas, keywords, sections, to_string));
            //poll_handlers_[index].Start();

            TreeNode keyword_node = this.trFilters.Nodes.Add(to_string);

            //Add the sections.
            TreeNode sections_node = keyword_node.Nodes.Add("Sections");
            for (int sections_counter = 0; sections_counter < sections.Count; sections_counter++)
            {
                string section_name = sections.ElementAt(sections_counter).Key;
                Dictionary<string, SubsectionDetails> subsections = sections.ElementAt(sections_counter).Value;
                TreeNode current_section_node = sections_node.Nodes.Add(section_name);
                for (int subsections_counter = 0; subsections_counter < subsections.Count; subsections_counter++)
                {
                    string subsection_name = subsections.ElementAt(subsections_counter).Key;
                    current_section_node.Nodes.Add(subsection_name);
                }
            }
            //Add the areas
            TreeNode areas_node = keyword_node.Nodes.Add("Areas");
            for (int country_counter = 0; country_counter < Areas.Count; country_counter++)
            {
                Dictionary<string, Dictionary<string, CityDetails>> state_map = Areas.ElementAt(country_counter).Value;
                string country_name = Areas.ElementAt(country_counter).Key;
                TreeNode country_node = areas_node.Nodes.Add(country_name);
                for (int state_counter = 0; state_counter < state_map.Count; state_counter++)
                {
                    Dictionary<string, CityDetails> city_map = state_map.ElementAt(state_counter).Value;
                    string state_name = state_map.ElementAt(state_counter).Key;
                    TreeNode state_node = country_node.Nodes.Add(state_name);
                    for (int city_counter = 0; city_counter < city_map.Count; city_counter++)
                    {
                        string city_name = city_map.ElementAt(city_counter).Key;
                        state_node.Nodes.Add(city_name);
                    }
                }
            }
        }
        private void btnNewFilter_Click(object sender, EventArgs e)
        {
            EntryForm entry_form = new EntryForm(this);
            if (entry_form.ShowDialog() == DialogResult.OK)
            {
                trFilters.Invalidate();
            }
        }

        public void UpdateEntries(List<string> entry_list)
        {
            if (this.wbEntries.InvokeRequired)
            {
                this.wbEntries.Invoke(new MethodInvoker(delegate() { UpdateEntries(entry_list); }));
            }
            else
            {
                lock (mutex_)
                {
                    foreach (string entry in entry_list)
                    {
                        try
                        {
                            this.wbEntries.Document.Write(entry);
                        }
                        catch (System.Exception ex)
                        {
                            Logger.Instance.Log(entry, LogType.ltError);
                            this.wbLog.Navigate("about:blank");
                            this.wbLog.Document.OpenNew(true);
                            this.wbLog.Refresh();
                        }
                        
                    }

                    if (this.wbEntries.Document.Body != null)
                        this.wbEntries.Document.Window.ScrollTo(0, this.wbEntries.Document.Body.ScrollRectangle.Bottom);

                    Application.DoEvents();
                }
            }
        }

        public void Log(string text)
        {
            if (this.wbLog.InvokeRequired)
            {
                this.wbLog.Invoke(new MethodInvoker(delegate() { Log(text); }));
            }
            else
            {
                try
                {
                    this.wbLog.Document.Write(text);
                    if (this.wbLog.Document.Body != null)
                        this.wbLog.Document.Window.ScrollTo(0, this.wbLog.Document.Body.ScrollRectangle.Bottom);

                    Application.DoEvents();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK);
                    this.wbLog.Navigate("about:blank");
                    this.wbLog.Document.OpenNew(true);
                    this.wbLog.Refresh();
                }

            }
        }

        private void btnDeleteFilter_Click(object sender, EventArgs e)
        {
            if (trFilters.SelectedNode != null && trFilters.SelectedNode.Parent == null)
            {
                int index = trFilters.SelectedNode.Index;
                PollHandler handler = poll_handlers_[index];
                if (handler.Stop())
                {
                    Logger.Instance.Log("Filter deleted: " + handler.ToString());
                    poll_handlers_.RemoveAt(index);
                    trFilters.Nodes.RemoveAt(index);
                    trFilters.Invalidate();
                }
                else
                {
                    Logger.Instance.Log("Unable to remove filter because it's currently polling.", LogType.ltError);
                }
            }
        }
    }
}

public class EntryInfo
{
    public EntryInfo()
    {
        Keywords_ = new List<string>();
    }
    /*Title of link*/
    public string Name_ { get; set; }
    /*Website of entry*/
    public string Website_ { get; set; }

    /*Origin of the entry*/
    public string Origin_ { get; set; }

    /*Keyword triggered*/
    public List<string> Keywords_ { get; set; }

    public string KeywordsAsString()
    {
        string rtn = "";

        for(int index = 0; index < Keywords_.Count; index++)
        {
            rtn += Keywords_[index];
            if (index + 1 != Keywords_.Count)
                rtn += ", ";
        }
        return rtn;
    }
    public static bool operator ==(EntryInfo a, EntryInfo b)
    {
        // If both are null, or both are same instance, return true.
        if (System.Object.ReferenceEquals(a, b))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }

        // Return true if the fields match:
        return a.Website_ == b.Website_;
    }

    public static bool operator !=(EntryInfo a, EntryInfo b)
    {
        return !(a == b);
    }
}
