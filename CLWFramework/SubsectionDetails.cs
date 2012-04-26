using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLWFramework
{
    public class SubsectionDetails
    {
        public SubsectionDetails()
        {
            Suffix = "";
            TopFiveEntriesFromLastSearch = new List<string>();
        }
        public SubsectionDetails(string suffix)
        {
            Suffix = suffix;
            TopFiveEntriesFromLastSearch = new List<string>();
        }
        public string Suffix;
        public List<string> TopFiveEntriesFromLastSearch;
    }
}
