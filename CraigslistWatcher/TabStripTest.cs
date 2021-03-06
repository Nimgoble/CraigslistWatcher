﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CLWFramework;

namespace CraigslistWatcher
{
    public partial class TabStripTest : Form
    {
        public object mutex_ { get; set; }
        private Int32 tabCounter;
        public TabStripTest()
        {
            InitializeComponent();
            tabCounter = 0;
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

        private void TabStripTest_Load(object sender, EventArgs e)
        {
            mutex_ = new object();
            Logger.Initiate("");
            Logger.Instance.LogMessage += new Logger.LogMessageHandler(this.Log);
            this.wbLog.Navigate("about:blank");
            this.wbLog.Document.OpenNew(true);
            this.wbLog.Refresh();
            //Locations.Instance.DownloadLocations();
            //Categories.Instance.DownloadCategories();
            Areas.Instance.Initialize();
        }

        private void btnNewSearch_Click(object sender, EventArgs e)
        {
            CLWTabPage page = new CLWTabPage();
            page.Text = (++tabCounter).ToString();
            tbQueries.Controls.Add(page);
        }

        private void btnRemoveSearch_Click(object sender, EventArgs e)
        {
            if (tbQueries.SelectedIndex == -1)
                return;
            tbQueries.Controls.Remove(tbQueries.SelectedTab);
        }
    }
}
