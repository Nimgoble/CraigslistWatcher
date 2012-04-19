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
                string file_name = txtClassName.Text + ".cs";
                System.IO.StreamWriter class_file = new System.IO.StreamWriter(file_name, false);
                string populate = "\tpublic void Populate(string url) \n\t{";
                if (filteredTags.Count > 0)
                    populate += "\n\t\thtmlParser.AddOmitTags(new List<string>() {" + string.Join(",", filteredTags.Select(x => "\"" + x.ToString() + "\"").ToArray()) + "});\n";
                populate += "\t\tInit(url);\n";
                string members = "";
                string constructor = "\tpublic " + txtClassName.Text + "()\n\t{\n";
                foreach (MemberInfo member in lstvwClassMembers.Items)
                {
                    members += member.HTML;
                    members += "\tpublic ";
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
                            populate += "\t\t" + member.Name + ".Add" + format + string.Join(",", sequence.Select(x => x.ToString()).ToArray()) + format_tail;
                        }
                        members += (member.Type + " ");
                        constructor += string.Format("\t\t{0} = new {1}();\n", member.Name, member.Type);
                    }
                    else
                    {
                        members += (member.Type + " ");
                        if (member.Nodes.Count == 1)
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
                            populate += "\t\t" + member.Name + " = " + format + string.Join(",", sequence.Select(x => x.ToString()).ToArray()) + format_tail;
                        }
                        constructor += string.Format("\t\t{0} = new {1}();\n", member.Name, member.Type);
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

        private void txtClassName_TextChanged(object sender, EventArgs e)
        {
            this.Text = txtClassName.Text;
        }
    }
}
