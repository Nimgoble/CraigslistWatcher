using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CLWFramework;
namespace CraigslistScraper
{
    public class User : BasePollEventHandler
    {
        public string Name;
        public User(string name)
        {
            Name = name;
        }
        public void Populate(SqlConnection connection)
        {
            //Check to see if the table for this user exists.
            SqlCommand userDbCommand = connection.CreateCommand();
            userDbCommand.CommandText = "SELECT * FROM " + Name;

            SqlDataReader userReader = null;
            try
            {
                userReader = userDbCommand.ExecuteReader();
            }
            catch (System.Exception ex)
            {
                //This really shouldn't be done here.  This should be done when a user first registers.
                userDbCommand.CommandText = String.Format("CREATE TABLE {0} (Subscriptions TEXT)", Name);
                userDbCommand.ExecuteNonQuery();
                userDbCommand.CommandText = "SELECT * FROM " + Name;
                userReader = userDbCommand.ExecuteReader();
            }
            userReader.Close();
        }
        protected virtual void OnNumberOfEntriesFound(BaseBackgroundPoller poller, Int32 numEntries)
        {
        }
        protected virtual void OnEntryFound(BaseBackgroundPoller poller, EntryInfo info)
        {
        }
        protected virtual void OnEntryParsed(BaseBackgroundPoller poller, EntryInfo info)
        {
        }
        protected virtual void OnPollDone(BaseBackgroundPoller poller, string message)
        {
        }
        protected virtual void OnPollError(BaseBackgroundPoller poller, string area, string message)
        {
        }
    }
}
