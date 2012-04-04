using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlParser;
using System.Windows.Forms;


public sealed class Locations
{
    private static readonly Locations instance = new Locations();

    public static Locations Instance
    {
        get { return instance; }
    }

    public Dictionary<string/*country*/,
            Dictionary<string/*state*/,
                Dictionary<string/*city*/, string /*website*/>>> LocationDictionary { get; set; }

    private Locations()
    {
        LocationDictionary = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
    }

    public override string ToString()
    {
        string rtn = "";
        for (int areas = 0; areas < LocationDictionary.Count; areas++)
        {
            rtn += LocationDictionary.ElementAt(areas).Key + '\n';
            Dictionary<string, Dictionary<string, string>> states = LocationDictionary.ElementAt(areas).Value;
            for (int state_counter = 0; state_counter < states.Count; state_counter++)
            {
                rtn += " " + states.ElementAt(state_counter).Key + '\n';
                Dictionary<string, string> cities = states.ElementAt(state_counter).Value;
                for (int city_counter = 0; city_counter < cities.Count; city_counter++)
                {
                    rtn += "  " + cities.ElementAt(city_counter).Key + " : " + cities.ElementAt(city_counter).Value + '\n';
                }
            }
        }
        rtn += "\n";
        return rtn;
    }

    public void PopulateTreeView( ref TreeView tree_view )
    {
        TreeView new_view = new TreeView();
        tree_view.CheckBoxes = true;

        for (int areas = 0; areas < LocationDictionary.Count; areas++)
        {
            string current_area = LocationDictionary.ElementAt(areas).Key;
            tree_view.Nodes.Add(current_area);
            Dictionary<string, Dictionary<string, string>> states = LocationDictionary.ElementAt(areas).Value;
            for (int state_counter = 0; state_counter < states.Count; state_counter++)
            {
                string current_state = states.ElementAt(state_counter).Key;
                tree_view.Nodes[areas].Nodes.Add(current_state);
                Dictionary<string, string> cities = states.ElementAt(state_counter).Value;
                for (int city_counter = 0; city_counter < cities.Count; city_counter++)
                {
                    tree_view.Nodes[areas].Nodes[state_counter].Nodes.Add(cities.ElementAt(city_counter).Key);
                }
            }
        }
    }

    public void DownloadLocations()
    {
        string site = "http://www.craigslist.org/about/sites";
        
        HtmlParser.HtmlParser parser = new HtmlParser.HtmlParser();

        if (!parser.ParseURL(site, false, new string[] {}))
            return;

        List<HtmlParser.HtmlTag> nodes = parser._nodes;
        HtmlParser.HtmlTag new_parent = null;

        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].Name == "html")
            {
                nodes[i].FilterForChildrenByName("div", out new_parent);
                List<string> class_names = new List<string>();
                class_names.Add("jump_to_continents");
                class_names.Add("colmask");
                Dictionary<string, List<string>> filter_for = new Dictionary<string, List<string>>();
                filter_for.Add("class", class_names);
                new_parent.FilterOutChildrenByAttribute(filter_for);

                Dictionary<string, string> abbr_to_area = new Dictionary<string, string>();

                for (int z = 0; z < new_parent.Children.Count; z++)
                {
                    HtmlTag Child = new_parent.Children[z];
                    if(Child.Attributes.Contains(new KeyValuePair<string,string>("class", "jump_to_continents")))
                    {
                        for(int a = 0; a < Child.Children.Count; a++)
                        {
                            string abbreviation = Child.Children[a].Attributes.ElementAt(0).Value;
                            abbreviation = abbreviation.Substring(1, abbreviation.Length - 1);
                            abbr_to_area.Add(abbreviation, Child.Children[a].Value);
                        }
                    }
                    else if(Child.Attributes.Contains(new KeyValuePair<string,string>("class", "colmask")))
                    {
                        HtmlParser.HtmlTag NewerChild = new HtmlParser.HtmlTag();
                        Dictionary<string, KeyValuePair<string, string>> tag_list = new Dictionary<string, KeyValuePair<string, string>>();
                        tag_list.Add("h1", new KeyValuePair<string, string>("class", "continent_header"));
                        tag_list.Add("div", new KeyValuePair<string, string>("class", "state_delimiter"));
                        tag_list.Add("a", new KeyValuePair<string, string>("href", "*"));
                        Child.FilterForChildrenByNameAndAttribute(tag_list, ref NewerChild);

                        string current_area = "";
                        string current_state = "";
                        for (int count_newer_children = 0; count_newer_children < NewerChild.Children.Count; count_newer_children++ )
                        {
                            HtmlParser.HtmlTag temp_child = NewerChild.Children[count_newer_children];
                            bool val_found = true;
                            switch (temp_child.Name)
                            {
                                    /*area*/
                                case "h1":
                                    {
                                        string temp_area = temp_child.Children[0].Attributes.ElementAt(0).Value;
                                        if (!abbr_to_area.TryGetValue(temp_area, out current_area))
                                        {
                                            val_found = false;
                                            break;
                                        }

                                        if (!LocationDictionary.ContainsKey(current_area))
                                            LocationDictionary.Add(current_area, new Dictionary<string, Dictionary<string, string>>());
                                    }
                                    break;
                                    /*state*/
                                case "div":
                                    {
                                        current_state = temp_child.Value;
                                        if (current_state == null)
                                            current_state = "Unspecified";
                                        try
                                        {
                                            if (!LocationDictionary[current_area].ContainsKey(current_state))
                                                LocationDictionary[current_area].Add(current_state, new Dictionary<string, string>());
                                        }
                                        catch (System.Exception ex)
                                        {
                                            string error = ex.ToString();
                                        }
                                    }
                                    break;
                                    /*city*/
                                case "a":
                                    {
                                        string city = temp_child.Value;
                                        string website = temp_child.Attributes.ElementAt(0).Value;
                                        LocationDictionary[current_area][current_state].Add(city, website);
                                    }
                                    break;
                            }
                            if( !val_found )
                                break;
                        }
                        new_parent.Children[z] = NewerChild;
                    }
                }
            }
        }
    }
}

