using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLWFramework
{
    public class CityDetails
    {
        public CityDetails()
        {
            CityWebsite = "";
            Keywords = new List<string>();
            Sections = new Dictionary<string, Dictionary<string, SubsectionDetails>>();
        }
        public CityDetails(string city, string website, List<string> keywords, Dictionary<string, Dictionary<string, SubsectionDetails>> sections)
        {
            City = city;
            Keywords = keywords;
            Sections = sections;
            CityWebsite = website;
        }
        public string City;
        public string CityWebsite;
        public List<string> Keywords;
        public Dictionary<string, Dictionary<string, SubsectionDetails>> Sections;
    }
}
