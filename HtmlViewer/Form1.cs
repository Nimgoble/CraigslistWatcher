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
            lstvwClassMembers.Columns.Add("Member Name", -2, HorizontalAlignment.Center);
            lstvwClassMembers.Columns.Add("Nodes", -2, HorizontalAlignment.Left);

            AdFilter adfilter = new AdFilter();
            adfilter.Populate("http://chicago.craigslist.org/sox/msg/2939038242.html");
            EntryFilter filter = new EntryFilter();
            filter.Populate("http://chicago.craigslist.org/msg");
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            /*string file_name = "C:\\Users\\Pat\\Documents\\Programming\\Projects\\CraigslistWatcher\\CraigslistWatcher\\CraigslistWatcher\\test\\chicago-For Sale-music instr.xml";
            System.IO.StreamReader test_file = new System.IO.StreamReader(file_name, true);
            string test_data = test_file.ReadToEnd();*/
            trParsedURL.Nodes.Clear();
            if (txtURL.Text == String.Empty)
                return;

            HtmlParser.HtmlParser parser = new HtmlParser.HtmlParser();
            //if(!parser.ParseRawHTML(test_data, true, new string[] {"<br>"}))
                //return;
            if (!parser.ParseURL(txtURL.Text, true, new string[] {"<br>"}))
                return;


            parser.PopulateTreeView(ref this.trParsedURL);
            trParsedURL.ExpandAll();
        }
        //Add custom draw to list view to draw all subitems in a single column.
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            if (txtMemberName.Text != String.Empty &&
                !lstvwClassMembers.Items.ContainsKey(txtMemberName.Text))
            {
                MemberInfo info = new MemberInfo();
                info.Text = info.Name = txtMemberName.Text;
                foreach (TreeNode node in trParsedURL.Nodes)
                    GetCheckedNodes(ref info.Nodes, node);
                info.UpdateSubInfo();
                lstvwClassMembers.Items.Add(info);
                txtMemberName.Text = "";
                foreach (TreeNode node in info.Nodes)
                    node.Checked = false;
            }
        }
        private void GetCheckedNodes(ref List<TreeNode> checkedList, TreeNode parent)
        {
            if (parent.Checked)
                checkedList.Add(parent);

            if (parent.Nodes.Count > 0)
            {
                foreach (TreeNode child in parent.Nodes)
                    GetCheckedNodes(ref checkedList, child);
            }
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = lstvwClassMembers.SelectedIndices;
            if (indices.Count > 0)
            {
                foreach (int index in indices)
                    lstvwClassMembers.Items.RemoveAt(index);
            }
        }

        private void btnGenerateClass_Click(object sender, EventArgs e)
        {
            if (txtClassName.Text != String.Empty)
            {
                string file_name = txtClassName.Text + ".cs";
                System.IO.StreamWriter class_file = new System.IO.StreamWriter(file_name, false);
                string populate = "\tpublic void Populate(string url) \n\t{\n\t\tInit(url);\n";
                string members = "";
                string constructor = "\tpublic " + txtClassName.Text + "()\n\t{\n";
                foreach (MemberInfo member in lstvwClassMembers.Items)
                {
                    members += "\tpublic ";
                    string format = "(FilterBySequence(new int[] {";
                    string format_tail = "}));\n";
                    if (member.Nodes.Count > 1)
                    {
                        foreach(TreeNode node in member.Nodes)
                        {
                            List<int> list_sequence = new List<int>();
                            TreeNode temp = node;
                            list_sequence.Insert(0, temp.Index);
                            while (temp.Parent != null)
                            {
                                temp = temp.Parent;
                                list_sequence.Insert(0, temp.Index);
                            }
                            int[] sequence = list_sequence.ToArray();
                            populate += "\t\t" + member.Name + ".Add" + format + string.Join(",", sequence.Select(x => x.ToString()).ToArray()) + format_tail; 
                        }
                        members += "List<string> ";
                        constructor += string.Format("\t\t{0} = new List<string>();\n", member.Name);
                    }
                    else 
                    {
                        members += "string ";
                        if(member.Nodes.Count == 1)
                        {
                            TreeNode node = member.Nodes[0];
                            List<int> list_sequence = new List<int>();
                            list_sequence.Insert(0, node.Index);
                            while(node.Parent != null)
                            {
                                node = node.Parent;
                                list_sequence.Insert(0, node.Index);
                            }
                            int[] sequence = list_sequence.ToArray();
                            populate += "\t\t" + member.Name + " = " + format + string.Join(",", sequence.Select(x => x.ToString()).ToArray()) + format_tail; 
                        }
                        constructor += string.Format("\t\t{0} = \"\";\n", member.Name);
                    }
                    members += member.Name + ";\n";
                }
                constructor += "\t}\n";
                populate += "\t}\n";
                string class_string = "using System;\nusing System.Collections.Generic;\nusing HtmlParser;\npublic class " + txtClassName.Text + " : PreciseParseFilter\n{\n" + members + constructor + populate + "};\n";
                class_file.Write(class_string);
                class_file.Close();
            }
        }
    }
}
