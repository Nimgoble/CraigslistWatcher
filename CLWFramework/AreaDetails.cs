using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLWFramework
{
    public class AreaDetails : IComparable<AreaDetails>
    {
        //All of the stuff that makes an area unique
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Section { get; set; }
        public string Subsection { get; set; }
        
        //Technical details
        public string CityWebsite { get; set; }
        public string SectionSuffix { get; set; }
        public string Website { get; set; }

        public AreaDetails()
        {
            Country = String.Empty;
            State = String.Empty;
            City = String.Empty;
            Section = String.Empty;
            Subsection = String.Empty;
            Website = String.Empty;
            CityWebsite = String.Empty;
            SectionSuffix = String.Empty;
        }
        public AreaDetails(string country, 
                            string state, 
                            string city, 
                            string section, 
                            string subsection, 
                            string citywebsite,
                            string suffix)
        {
            Country = country;
            State = state;
            City = city;
            Section = section;
            Subsection = subsection;
            CityWebsite = citywebsite;
            SectionSuffix = suffix;
            Website = citywebsite + "/" + suffix;
        }
        //"United States - Illinois - Chicago - For Sale - Musc Instr"

        public override string ToString()
        {
            if (Subsection == String.Empty)
                return String.Format("{0} - {1} - {2} - {3}", Country, State, City, Section);

            return String.Format("{0} - {1} - {2} - {3} - {4}", Country, State, City, Section, Subsection);
        }

        public int CompareTo(AreaDetails other)
        {
            return this.ToString().CompareTo(other.ToString());
        }
    }
}
