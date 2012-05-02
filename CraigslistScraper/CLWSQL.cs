using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using CLWFramework;
namespace CraigslistScraper
{
    public class CLWSQL
    {
        private SqlConnection connection;
        private PollHandler pollHandler;
        public CLWSQL()
        {
            connection = new SqlConnection();
            Logger.Initiate("");
            pollHandler = null;
        }

        public bool Open(string dbName)
        {
            try
            {
                connection.ConnectionString = dbName;
                connection.Open();

                //There should be something here that checks to see if the current database exists
                //if so, it should check to see if we have any prior entries and load them and
                //pass them to the poll handler.
                //That way we don't get any duplicates.

                //Make a Handler for each subscribed user in the users.db

                Locations.Instance.DownloadLocations();
                Categories.Instance.DownloadCategories();
                Dictionary<string, Dictionary<string, SubsectionDetails>> sections;
                Categories.Instance.FormatForCityDetails(out sections);
                Dictionary<string, Dictionary<string, Dictionary<string, CityDetails>>> areas;
                Locations.Instance.FormatForPollHandler(sections, out areas);
                pollHandler = new PollHandler(areas, "Craigslist Scraper");
                pollHandler.EntryFound += new PollHandler.EntryFoundHandler(this.OnEntryFound);
                pollHandler.Start(new TimeSpan(0, 15, 0));
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        private void OnEntryFound(EntryInfo info)
        {

        }

        public void Close()
        {
            connection.Close();
        }
    }
}
