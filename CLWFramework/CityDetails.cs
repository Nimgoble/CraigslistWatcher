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
            Sections = new Dictionary<string, Dictionary<string, SubsectionDetails>>();
        }
        public CityDetails(string city, string website, Dictionary<string, Dictionary<string, SubsectionDetails>> sections)
        {
            City = city;
            Sections = sections;
            CityWebsite = website;
        }
        public string City;
        public string CityWebsite;
        public Dictionary<string, Dictionary<string, SubsectionDetails>> Sections;
    }
}
