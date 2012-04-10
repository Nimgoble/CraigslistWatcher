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
        public MemberForm(ref MemberInfo info)
        {
            memberInfo = info;
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (txtMemberName.Text != String.Empty)
            {

                memberInfo.Text = memberInfo.Name = txtMemberName.Text;
                /*foreach (TreeNode node in trParsedURL.Nodes)
                    GetCheckedNodes(ref memberInfo.Nodes, node);*/
                memberInfo.UpdateSubInfo();

                txtMemberName.Text = "";
                foreach (TreeNode node in memberInfo.Nodes)
                    node.Checked = false;
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Please enter a valid member name.", "Error");
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
