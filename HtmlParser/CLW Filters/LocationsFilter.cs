using System;
using System.Collections.Generic;
using HtmlParser;

namespace HtmlParser
{
    public class LocationsFilter : PreciseParseFilter
    {
        public Dictionary<String, String> SectionToName;
        public LocationsFilter()
        {
            htmlParser.AddOmitTags(new List<string>() { "<br>", "</br>", "<ul>", "</ul>", "<li>", "</li>" });
            SectionToName = new Dictionary<String, String>();
        }
        public void Populate(ref Dictionary<string, Dictionary<string, Dictionary<string, string>>> LocationDictionary)
        {
            HtmlTag parent = (FilterBySequence(new int[] { 1, 1 }));
            ParseSectionNames(parent);
            ParseCountries(parent, ref LocationDictionary);
        }
        private void ParseSectionNames(HtmlTag parent)
        {
            List<HtmlTag> tagList = null;
            parent.FilterForChildrenByNameAndAttribute("div", new KeyValuePair<string, string>("class", "jump_to_continents"), out tagList);
            if (tagList != null)
            {
                HtmlTag locationsStuff = tagList[0];
                foreach (HtmlTag child in locationsStuff.Children)
                {
                    String key = String.Empty;
                    if (child.Attributes.TryGetValue("href", out key))
                    {
                        key = key.Substring(1, key.Length - 1);
                        SectionToName.Add(key, child.Value);
                    }
                }
            }
        }

        private void ParseCountries(HtmlTag parent, ref Dictionary<string, Dictionary<string, Dictionary<string, string>>> LocationDictionary)
        {
            List<HtmlTag> tagList = null;
            string currentCountry = "";
            string currentState = "";
            string currentCity = "";
            parent.FilterForChildrenByNameAndAttribute("div", new KeyValuePair<string, string>("class", "colmask"), out tagList);
            if (tagList != null)
            {
                Dictionary<string, List<KeyValuePair<string, string>>> searchTagList =
                       new Dictionary<string, List<KeyValuePair<string, string>>>();
                searchTagList["a"] = new List<KeyValuePair<string, string>>() 
                                             { new KeyValuePair<string, string>("name", "*"),
                                               new KeyValuePair<string, string>("href", "*") };
                searchTagList["div"] = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("class", "state_delimiter") };
                foreach (HtmlTag child in tagList)
                {
                    List<HtmlTag> stateList = null;
                    child.FilterForChildrenByNameAndAttribute(searchTagList, out stateList);
                    if (stateList != null)
                    {
                        foreach (HtmlTag stateChild in stateList)
                        {
                            if (stateChild.Name == "a")
                            {
                                if (stateChild.Attributes.ContainsKey("name"))
                                {
                                    //Country
                                    stateChild.Attributes.TryGetValue("name", out currentCountry);
                                    SectionToName.TryGetValue(currentCountry, out currentCountry);
                                    LocationDictionary[currentCountry] = new Dictionary<string, Dictionary<string, string>>();
                                }
                                else
                                {
                                    //City/Entry
                                    String entry = null;
                                    stateChild.Attributes.TryGetValue("href", out entry);
                                    LocationDictionary[currentCountry][currentState][stateChild.Value] = entry;
                                }
                            }
                            else if (stateChild.Name == "div")
                            {
                                //State
                                currentState = stateChild.Value;
                                LocationDictionary[currentCountry][currentState] = new Dictionary<string, string>();
                            }
                        }
                    }
                }
            }
        }
    };
}
