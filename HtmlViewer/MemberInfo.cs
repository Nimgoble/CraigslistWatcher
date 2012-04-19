using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace HtmlViewer
{
    public class MemberInfo : ListViewItem
    {
        public List<TreeNode> Nodes;
        public string Type;
        public string HTML;
        public MemberInfo()
        {
            Nodes = new List<TreeNode>();
            Type = "";
            HTML = "";
        }
        public void UpdateSubInfo()
        {
            foreach(TreeNode node in Nodes)
                SubItems.Add(node.Text);
        }
    }
}
