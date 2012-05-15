using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlParser;
using System.Windows.Forms;
using CLWFramework.CLWFilters;
namespace CLWFramework
{
    public sealed class Locations
    {
        private static readonly Locations instance = new Locations();

        public static Locations Instance
        {
            get { return instance; }
        }
        private Dictionary<string/*country*/,
                Dictionary<string/*state*/,
                    Dictionary<string/*city*/, string /*website*/>>> locationDictionary;

        public Dictionary<string/*country*/,
                Dictionary<string/*state*/,
                    Dictionary<string/*city*/, string /*website*/>>> LocationDictionary
        {
            get
            {
                return locationDictionary;
            }
        }

        private Locations()
        {
            locationDictionary = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
        }

        public override string ToString()
        {
            string rtn = "";
            for (int areas = 0; areas < locationDictionary.Count; areas++)
            {
                rtn += LocationDictionary.ElementAt(areas).Key + '\n';
                Dictionary<string, Dictionary<string, string>> states = locationDictionary.ElementAt(areas).Value;
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

        public void PopulateTreeView(ref TreeView tree_view)
        {
            TreeView new_view = new TreeView();
            tree_view.CheckBoxes = true;

            for (int areas = 0; areas < locationDictionary.Count; areas++)
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
            locationDictionary.Clear();
            string site = "http://www.craigslist.org/about/sites";
            LocationsFilter filter = new LocationsFilter(ref locationDictionary);
            if(filter.ParseURL(site))
                filter.Populate();
        }

        public void FormatForPollHandler(Dictionary<string, Dictionary<string, SubsectionDetails>> sections,
                                        out Dictionary<string/*country*/, Dictionary<string/*state*/, Dictionary<string/*city*/, CityDetails /*website*/>>> locations)
        {
            locations = new Dictionary<string, Dictionary<string, Dictionary<string, CityDetails>>>();
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, string>>> pair in locationDictionary)
            {
                //Initialize state map for each country
                Dictionary<string, Dictionary<string, CityDetails>> stateMap = new Dictionary<string, Dictionary<string, CityDetails>>();
                foreach (KeyValuePair<string, Dictionary<string, string>> statePair in pair.Value)
                {
                    //fill each state map with the cities
                    Dictionary<string, CityDetails> cityMap = new Dictionary<string, CityDetails>();
                    foreach (KeyValuePair<string, string> cityPair in statePair.Value)
                        cityMap.Add(cityPair.Key, new CityDetails(cityPair.Key, cityPair.Value, sections));
                    //Add the city map to the stateMap
                    stateMap.Add(statePair.Key, cityMap);
                }
                locations.Add(pair.Key, stateMap);
            }
        }
    }
}
