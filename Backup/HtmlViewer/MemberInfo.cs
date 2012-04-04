using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace HtmlViewer
{
    class MemberInfo : ListViewItem
    {
        public List<TreeNode> Nodes;
        public MemberInfo()
        {
            Nodes = new List<TreeNode>();
        }
        public void UpdateSubInfo()
        {
            foreach(TreeNode node in Nodes)
                SubItems.Add(node.Text);
        }
    }
}
