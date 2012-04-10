using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace HtmlParser
{
    public partial class HtmlTree : UserControl
    {
        protected List<HtmlTag> nodes;
        public HtmlTree()
        {
            InitializeComponent();
            nodes = null;
        }

        public List<HtmlTag> Nodes
        {
            get
            {
                return nodes;
            }
            set
            {
                nodes = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // Calling the base class OnPaint
            base.OnPaint(pe);

            if (nodes == null)
                return;

            Graphics graphics = pe.Graphics;
        }
    }
}
