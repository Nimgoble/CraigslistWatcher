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
    public partial class MemberForm : Form
    {
        private MemberInfo memberInfo;
        public MemberForm(ref MemberInfo info, List<TreeNode> nodes)
        {
            memberInfo = info;
            InitializeComponent();
            foreach(TreeNode node in nodes)
                this.trParentFamily.Nodes.Add((TreeNode)node.Clone());
            this.cmbType.Items.AddRange(new object[] { "Int16",
                                                       "Int32",
                                                       "Int64",
                                                       "UInt16",
                                                       "UInt32",
                                                       "UInt64",
                                                       "Double",
                                                       "String",
                                                       "List<T>",
                                                       "Dictionary<K, T>",
                                                       "HtmlTag"});

            this.cmbSource.Items.Add("Dummy Source");
                
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            string error = "";
            if (txtMemberName.Text == String.Empty)
                error = "Please enter a valid member name.";
            else if (this.cmbType.SelectedIndex == -1)
                error = "Please select a type.";
            else if (this.cmbSource.SelectedIndex == -1)
                error = "Please select a source.";

            if(error != String.Empty)
            {
                MessageBox.Show(error, "Error");
                return;
            }

            memberInfo.Text = memberInfo.Name = txtMemberName.Text;
            memberInfo.Type = this.cmbType.SelectedItem.ToString();
            memberInfo.HTML += ("\t#region " + memberInfo.Name + " HTML\n\t/*\n");
            foreach (TreeNode node in trParentFamily.Nodes)
                FillMemberHTML(node, 0);
            memberInfo.HTML += "\t*/\n\t#endregion\n";
            this.DialogResult = DialogResult.OK;
        }

        private void FillMemberHTML(TreeNode parent, int padding)
        {
            string modText = parent.Text;
            modText = modText.Replace('\n', (char)1);
            modText = modText.Replace('\t', (char)1);
            modText = modText.PadLeft(4 + padding + modText.Length, ' ');
            memberInfo.HTML += (modText + "\n");
            foreach (TreeNode node in parent.Nodes)
                FillMemberHTML(node, padding + 1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void MemberForm_Load(object sender, EventArgs e)
        {

        }
    }
}
