﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CLWFramework;
using CLWFramework.CLWFilters;
namespace HtmlViewer
{
    public partial class Form1 : Form
    {
        private List<string> filteredTags;
        private string currentURL;
        public Form1()
        {
            InitializeComponent();
            filteredTags = new List<string>();
            currentURL = "";

            /*AdFilter adfilter = new AdFilter();
            adfilter.Populate("http://chicago.craigslist.org/sox/msg/2939038242.html");
            EntryFilter filter = new EntryFilter();
            string url = "http://chicago.craigslist.org/msg/";
            filter.Populate(url);
            while (filter.NextHundred != null)
                filter.Populate(url + filter.NextHundred);*/

            /*AdFilter filter = new AdFilter();
            filter.ParseURL("http://chicago.craigslist.org/wcl/msg/2988125335.html");
            filter.Populate();
            int i = 0;*/

            /*Locations.Instance.DownloadLocations();
            Categories.Instance.DownloadCategories();*/

            Areas.Instance.Initialize();

            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add("mechanicalanimal125@yahoo.com");
                message.Subject = "This is the Subject line";
                message.From = new System.Net.Mail.MailAddress("mechanicalanimal125@yahoo.com");
                message.Body = "This is the message body";
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("plus.smtp.mail.yahoo.com");
                smtp.EnableSsl = true;
                smtp.Port = 465;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (System.Exception ex)
            {
                string e = ex.ToString();
            }
            

            /*LocationsFilter locFilter = new LocationsFilter();
            locFilter.Populate("http://www.craigslist.org/about/sites");*/

            /*CategoriesFilter filter = new CategoriesFilter();
            Dictionary<CategoryInfo, List<CategoryInfo>> dict = new Dictionary<CategoryInfo, List<CategoryInfo>>();
            if(filter.ParseURL("http://chicago.craigslist.org"))
                filter.Populate(ref dict);*/
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            /*string file_name = "C:\\Users\\Pat\\Documents\\Programming\\Projects\\CraigslistWatcher\\CraigslistWatcher\\CraigslistWatcher\\test\\chicago-For Sale-music instr.xml";
            System.IO.StreamReader test_file = new System.IO.StreamReader(file_name, true);
            string test_data = test_file.ReadToEnd();*/
            filteredTags.Clear();
            if (txtURL.Text == String.Empty)
                return;

            currentURL = txtURL.Text;
            filteredTags.Add("<br>");
            filteredTags.Add("</br>");
            ParseCurrentURL(filteredTags.ToArray());
        }

        public void UncheckAll()
        {
            if (this.trParsedURL.InvokeRequired)
                this.trParsedURL.Invoke(new MethodInvoker(delegate() { this.UncheckAll(); }));
            else
            {
                foreach (TreeNode node in this.trParsedURL.Nodes)
                {
                    if (node.Checked)
                        node.Checked = false;

                    if (node.Nodes.Count > 0)
                    {
                        foreach (TreeNode child in node.Nodes)
                            _UncheckAll(child);
                    }
                }
            }
        }

        private void _UncheckAll(TreeNode parent)
        {
            if (parent.Checked)
                parent.Checked = false;

            if (parent.Nodes.Count > 0)
            {
                foreach (TreeNode child in parent.Nodes)
                    _UncheckAll(child);
            }
        }
        
        public void GetCheckedNodes(ref List<TreeNode> checkedList)
        {
            foreach (TreeNode node in trParsedURL.Nodes)
            {
                if (node.Checked)
                    checkedList.Add(node);

                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode child in node.Nodes)
                        _GetCheckedNodes(ref checkedList, child);
                }
            }
        }

        private void _GetCheckedNodes(ref List<TreeNode> checkedList, TreeNode parent)
        {
            if (parent.Checked)
                checkedList.Add(parent);

            if (parent.Nodes.Count > 0)
            {
                foreach (TreeNode child in parent.Nodes)
                    _GetCheckedNodes(ref checkedList, child);
            }
        }

        private void btnFilterOutTag_Click(object sender, EventArgs e)
        {
            List<TreeNode> checkedList = new List<TreeNode>();
            GetCheckedNodes(ref checkedList);

            if (checkedList.Count <= 0)
                return;

            foreach (TreeNode node in checkedList)
            {
                int tagOpen = node.Text.IndexOf('<') + 1;
                int tagClose = node.Text.IndexOf('>') + 1;
                string tag = node.Text.Substring(tagOpen, (tagClose - tagOpen));
                string openTag = "<" + tag, closeTag = "</" + tag;
                if (filteredTags.Contains(openTag) || filteredTags.Contains(closeTag))
                    continue;

                filteredTags.Add(openTag);
                filteredTags.Add(closeTag);
            }

            ParseCurrentURL(filteredTags.ToArray());
        }

        private void ParseCurrentURL(string [] omits)
        {
            trParsedURL.Nodes.Clear();
            HtmlParser.HtmlParser parser = new HtmlParser.HtmlParser();
            parser.AddOmitTags(filteredTags);
            if (!parser.ParseURL(currentURL, true))
                return;

            parser.PopulateTreeView(ref this.trParsedURL);
            trParsedURL.ExpandAll();
        }

        private void btnAddClass_Click(object sender, EventArgs e)
        {
            FilterPage page = new FilterPage(ref filteredTags, this);
            this.tbTabs.Controls.Add(page);
            this.tbTabs.SelectedTab = page;
        }

        private void btnRemoveClass_Click(object sender, EventArgs e)
        {
            if(tbTabs.SelectedIndex != -1 && tbTabs.SelectedIndex != 0)
                this.tbTabs.Controls.RemoveAt(tbTabs.SelectedIndex);
        }

        private void trParsedURL_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void txtNodeSequence_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void txtNodeSequence_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                TreeNode node = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                if (node == null)
                    return;

                List<string> sequence = new List<string>();
                sequence.Insert(0, node.Index.ToString());
                while (node.Parent != null)
                {
                    node = node.Parent;
                    sequence.Insert(0, node.Index.ToString());
                }
                txtNodeSequence.Text = String.Join(",", sequence.ToArray());
            }
        }
    }
}
