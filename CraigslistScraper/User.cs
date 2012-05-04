using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLWFramework;
namespace CraigslistScraper
{
    public class User : BasePollEventHandler
    {
        public User()
        {
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
