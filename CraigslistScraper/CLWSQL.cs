using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using CLWFramework;

namespace CraigslistScraper
{
    public class CLWSQL : BasePollEventHandler
    {
        private ArrayList list;
        private const int listSize = 5;
        private SqlConnection connection;
        private AreaPollHandler areaPollHandler;
        public CLWSQL()
        {
            connection = new SqlConnection();
            Logger.Initiate("");
            areaPollHandler = null;
            list = new ArrayList(5);
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
