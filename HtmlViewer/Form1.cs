using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HtmlViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtURL.Text == String.Empty)
                return;

            HtmlParser.HtmlParser parser = new HtmlParser.HtmlParser();
            if (!parser.ParseURL(txtURL.Text, true, new string[] {"<br>"}))
                return;

            parser.PopulateTreeView(ref this.trParsedURL);
            trParsedURL.ExpandAll();
        }
    }
}
