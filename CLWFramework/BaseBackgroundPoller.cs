using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using HtmlParser;
namespace CLWFramework
{
    public class BaseBackgroundPoller : EventWaitHandle
    {
        public virtual AreaDetails PollerAreaDetails 
        { 
            get
            {
                return null;
            }
        }
        public BaseBackgroundPoller() : base(false, EventResetMode.ManualReset) { }
        public delegate void NumberOfEntriesFoundHandler(BaseBackgroundPoller poller, Int32 numEntries);
        public event NumberOfEntriesFoundHandler NumberOfEntriesFound;
        public delegate void PollDoneHandler(BaseBackgroundPoller poller, string message);
        public event PollDoneHandler PollDone;
        public delegate void PollErrorHandler(BaseBackgroundPoller poller, string area, string message);
        public event PollErrorHandler PollError;
        public delegate void EntryFoundHandler(BaseBackgroundPoller poller, EntryInfo info);
        public event EntryFoundHandler EntryFound;
        public delegate void EntryParsedHandler(BaseBackgroundPoller poller, EntryInfo info);

        protected virtual void OnNumberOfEntriesFound(Int32 numEntries)
        {
            if (NumberOfEntriesFound != null)
                NumberOfEntriesFound(this, numEntries);
        }

        protected virtual void OnPollDone(string output)
        {
            if (PollDone != null)
                PollDone(this, output);
            base.Set();
        }
        protected virtual void OnPollError(string area, string message)
        {
            if (PollError != null)
                PollError(this, area, message);
        }

        protected virtual bool OnEntryFound(EntryInfo info)
        {
            if (EntryFound != null)
            {
                EntryFound(this, info);
                return true;
            }
            return false;
        }
    }
}
