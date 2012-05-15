using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLWFramework
{
    public class Areas
    {
        private static readonly Areas instance = new Areas();

        public static Areas Instance
        {
            get { return instance; }
        }

        private List<AreaDetails> areasList;

        public List<AreaDetails> AreasList
        {
            get
            {
                return areasList;
            }
        }

        private Areas()
        {
            areasList = new List<AreaDetails>();
        }

        public void Initialize()
        {
            areasList.Clear();
            Locations.Instance.DownloadLocations();
            Categories.Instance.DownloadCategories();
            foreach (KeyValuePair<CategoryInfo, List<CategoryInfo>> catPair in Categories.Instance.CategoriesDictionary)
            {
                foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, string>>> countryPair in Locations.Instance.LocationDictionary)
                {
                    foreach (KeyValuePair<string, Dictionary<string, string>> statePair in countryPair.Value)
                    {
                        foreach (KeyValuePair<string, string> cityPair in statePair.Value)
                        {
                            this.areasList.Add(new AreaDetails(countryPair.Key,
                                statePair.Key,
                                cityPair.Key,
                                catPair.Key.Name,
                                String.Empty,
                                cityPair.Value,
                                catPair.Key.Suffix));
                            foreach (CategoryInfo subSection in catPair.Value)
                            {
                                this.areasList.Add(new AreaDetails(countryPair.Key,
                                statePair.Key,
                                cityPair.Key,
                                catPair.Key.Name,
                                subSection.Name,
                                cityPair.Value,
                                subSection.Suffix));
                            }//subSection
                        }//cityPair
                    }//statePair
                }//countryPair
            }//catPair
        }//Initialize

        public void GetAllByCountry(string country, out List<AreaDetails> outList)
        {
            outList = areasList.FindAll(area => area.Country == country);
        }

        public void GetAllByState(string country, string state, out List<AreaDetails> outList)
        {
            outList = areasList.FindAll(area => (area.Country == country) && (area.State == state));
        }

        public void GetAreaDetails(string country, string state, string city, out List<AreaDetails> areaDetails)
        {
            areaDetails = areasList.FindAll(area => (area.Country == country) && (area.State == state) && (area.City == city));
        }

        public void GetAreaDetails(string country, string state, string city, string section, out List<AreaDetails> areaDetails)
        {
            areaDetails = areasList.FindAll(area => (area.Country == country) &&
                                        area.State == state &&
                                        area.City == city &&
                                        area.Section == section);
        }

        public void GetAreaDetails(string country, string state, string city, string section, string subSection, out AreaDetails areaDetails)
        {
            areaDetails = areasList.Find(area => (area.Country == country) &&
                                        area.State == state &&
                                        area.City == city &&
                                        area.Section == section &&
                                        area.Subsection == subSection);
        }
    }
}
