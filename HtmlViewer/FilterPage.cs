using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HtmlViewer
{
    public partial class FilterPage : /*UserControl,*/ System.Windows.Forms.TabPage
    {
        private List<string> filteredTags;
        private Form1 parent;
        public FilterPage(ref List<string> _filteredTags, Form1 _parent)
        {
            filteredTags = _filteredTags;
            parent = _parent;
            InitializeComponent();
            lstvwClassMembers.Columns.Add("Member Name", -2, HorizontalAlignment.Center);
            lstvwClassMembers.Columns.Add("Nodes", -2, HorizontalAlignment.Left);
        }

        //Add custom draw to list view to draw all subitems in a single column.
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            List<TreeNode> nodeList = new List<TreeNode>();
            parent.GetCheckedNodes(ref nodeList);
            if (nodeList.Count == 0)
            {
                MessageBox.Show("Please select a node from the Parsed URL tree");
                return;
            }
            MemberInfo info = new MemberInfo();
            MemberForm memberForm = new MemberForm(ref info, nodeList);

            if (memberForm.ShowDialog() == DialogResult.OK)
            {
                lstvwClassMembers.Items.Add(info);
                foreach (TreeNode node in nodeList)
                    info.Nodes.Add(node);
                info.UpdateSubInfo();
            }

            parent.UncheckAll();
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
                //Declare file and open
                string file_name = txtClassName.Text + ".cs";
                System.IO.StreamWriter class_file = new System.IO.StreamWriter(file_name, false);
                //Write the includes and the class declaration
                class_file.Write("using System;\nusing System.Collections.Generic;\nusing HtmlParser;\npublic class " + txtClassName.Text + " : PreciseParseFilter\n{\n");
                //Init populate string
                List<string> populate = new List<string>() {"\tpublic void Populate(string url) \n\t{"};
                //Add the omit tags, if we have any.
                if (filteredTags.Count > 0)
                    populate.Add("\n\t\thtmlParser.AddOmitTags(new List<string>() {" + string.Join(",", filteredTags.Select(x => "\"" + x.ToString() + "\"").ToArray()) + "});\n");
                populate.Add("\t\tInit(url);\n");
                List<string> members = new List<string>();
                List<string> constructor = new List<string> {"\tpublic " + txtClassName.Text + "()\n\t{\n"};
                foreach (MemberInfo member in lstvwClassMembers.Items)
                {
                    //For some reason, the HTML comment block screws up the file...
                    /*foreach(string line in member.HTML)
                        members.Add(line);*/
                    members.Add("\tpublic " + member.Type + " " + member.Name + ";\n");
                    constructor.Add(string.Format("\t\t{0} = new {1}();\n", member.Name, member.Type));
                    string format = "(FilterBySequence(new int[] {";
                    string format_tail = "}));\n";
                    if (member.Nodes.Count > 1)
                    {
                        foreach (TreeNode node in member.Nodes)
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
                            populate.Add("\t\t" + member.Name + ".Add" + format + string.Join(",", sequence.Select(x => x.ToString()).ToArray()) + format_tail);
                        }
                    }
                    else if (member.Nodes.Count == 1)
                    {
                        TreeNode node = member.Nodes[0];
                        List<int> list_sequence = new List<int>();
                        list_sequence.Insert(0, node.Index);
                        while (node.Parent != null)
                        {
                            node = node.Parent;
                            list_sequence.Insert(0, node.Index);
                        }
                        int[] sequence = list_sequence.ToArray();
                        populate.Add("\t\t" + member.Name + " = " + format + string.Join(",", sequence.Select(x => x.ToString()).ToArray()) + format_tail);
                    }
                }
                constructor.Add("\t}\n");
                populate.Add("\t}\n");
                foreach (string line in members)
                    class_file.Write(line);
                foreach (string line in constructor)
                    class_file.Write(line);
                foreach (string line in populate)
                    class_file.Write(line);
                class_file.Write("};\n");
                class_file.Close();
                MessageBox.Show(String.Format("{0} generated successfully in {1}", txtClassName.Text, file_name));
            }
        }

        private void txtClassName_TextChanged(object sender, EventArgs e)
        {
            this.Text = txtClassName.Text;
        }
    }
}
