using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLWFramework
{
    public interface BasePollEventHandler
    {
        BaseBackgroundPoller.NumberOfEntriesFoundHandler numberOfEntriesFoundHandler {get;}
        BaseBackgroundPoller.EntryFoundHandler entryFoundHandler { get; }
        BaseBackgroundPoller.EntryParsedHandler entryParsedHandler { get; }
        BaseBackgroundPoller.PollDoneHandler pollDoneHandler { get; }
        BaseBackgroundPoller.PollErrorHandler pollErrorHandler { get; }

        /*public BasePollEventHandler()
        {
            numberOfEntriesFoundHandler = new BaseBackgroundPoller.NumberOfEntriesFoundHandler(this.OnNumberOfEntriesFound);
            entryFoundHandler = new BaseBackgroundPoller.EntryFoundHandler(this.OnEntryFound);
            entryParsedHandler = new BaseBackgroundPoller.EntryParsedHandler(this.OnEntryParsed);
            pollDoneHandler = new BaseBackgroundPoller.PollDoneHandler(this.OnPollDone);
            pollErrorHandler = new BaseBackgroundPoller.PollErrorHandler(this.OnPollError);
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
        }*/
    }
}
