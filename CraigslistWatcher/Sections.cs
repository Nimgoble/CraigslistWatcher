using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public sealed class Sections
{
    private static readonly Sections instance = new Sections();

    public static Sections Instance
    {
        get { return instance; }
    }
    public Dictionary<CategoriesFilter.CategoryInfo, List<CategoriesFilter.CategoryInfo>> CategoriesDictionary;
    private Sections()
    {
        CategoriesDictionary = new Dictionary<CategoriesFilter.CategoryInfo, List<CategoriesFilter.CategoryInfo>>();
        CategoriesFilter filter = new CategoriesFilter();
        filter.Populate("http://chicago.craigslist.org", ref CategoriesDictionary);
    }
    public string GetSuffix(string parentSection, string childSection)
    {
        foreach (KeyValuePair<CategoriesFilter.CategoryInfo, List<CategoriesFilter.CategoryInfo>> parent in CategoriesDictionary)
        {
            if (parent.Key.Name == parentSection)
            {
                if (childSection == null)
                {
                    if (parent.Key.Name == parentSection)
                        return parent.Key.Suffix;
                }
                else
                {
                    foreach (CategoriesFilter.CategoryInfo info in parent.Value)
                    {
                        if (info.Name == childSection)
                            return info.Suffix;
                    }
                }
            }
        }
        return "";
    }
    public void PopulateTreeView(ref TreeView tree_view)
    {
        TreeView new_view = new TreeView();
        tree_view.CheckBoxes = true;

        foreach (KeyValuePair<CategoriesFilter.CategoryInfo, List<CategoriesFilter.CategoryInfo>> parentSection in CategoriesDictionary)
        {
            TreeNode node = new TreeNode(parentSection.Key.Name);
            node.Tag = parentSection.Key;
            foreach (CategoriesFilter.CategoryInfo childInfo in parentSection.Value)
            {
                TreeNode childNode = new TreeNode(childInfo.Name);
                childNode.Tag = childNode;
                node.Nodes.Add(childNode);
            }
            tree_view.Nodes.Add(node);
        }
    }
}
