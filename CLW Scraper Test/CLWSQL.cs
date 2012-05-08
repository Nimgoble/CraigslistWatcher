using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using CLWFramework;

namespace CraigslistScraper
{
    public class CLWSQL : BasePollEventHandler
    {
        private Dictionary<AreaDetails, ArrayList> areaLastFiveSearched;
        private List<User> users;
        private const int listSize = 5;
        private SqlConnection connection;
        private AreaPollHandler areaPollHandler;
        public CLWSQL()
        {
            connection = new SqlConnection();
            Logger.Initiate("");
            areaPollHandler = null;
            areaLastFiveSearched = new Dictionary<AreaDetails, ArrayList>();
            users = new List<User>();
        }

        public bool Open(string dbName)
        {
            //EAGLE-D8C30EC3C\JHAR
            try
            {
                //connection.ConnectionString = "user id=username;password=password;server=localhost;Trusted_Connection=yes;database=CLWDB;connection timeout=30";
                connection.ConnectionString = string.Format("user id=username;password=password;server={0};Trusted_Connection=yes;database=master;connection timeout=30", dbName);
                connection.Open();

                //There should be something here that checks to see if the current database exists
                //if so, it should check to see if we have any prior entries and load them and
                //pass them to the poll handler.
                //That way we don't get any duplicates.

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Users";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                    users.Add(new User(reader[0].ToString()));

                reader.Close();

                foreach (User user in users)
                    user.Populate(connection);

                //Make a Handler for each subscribed user in the users.db
                Areas.Instance.Initialize();
                areaPollHandler = new AreaPollHandler();
                //Subscribe to all with this.
                areaPollHandler.Subscribe(Areas.Instance.AreasList, this);
                areaPollHandler.Start(new TimeSpan(0, 15, 0));
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        protected void OnEntryFound(BaseBackgroundPoller poller, EntryInfo info)
        {

        }

        public void Close()
        {
            connection.Close();
        }
    }
}
