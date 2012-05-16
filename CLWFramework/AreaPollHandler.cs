using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using System.ComponentModel;
using HtmlParser;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;

namespace CLWFramework
{
    public class AreaPollHandler
    {
        private System.Timers.Timer timer;
        private TimeSpan refreshTime;
        private TimeSpan refreshTimeCounter;
        private TimeSpan timeout;
        private object lockObject;
        private bool polling;
        private System.Diagnostics.Stopwatch stopWatch;
        private int totalSearched;
        private Dictionary<AreaDetails, List<BasePollEventHandler>> areaDetailsDictionary;
        private List<BackgroundPoller.EntryFoundHandler> entryFoundHandlerList;
        private List<EventWaitHandle> cityWorkers;
        public AreaPollHandler()
        {
            areaDetailsDictionary = new Dictionary<AreaDetails, List<BasePollEventHandler>>();
            entryFoundHandlerList = new List<BackgroundPoller.EntryFoundHandler>();
            InitializeMembers();
        }

        ~AreaPollHandler()
        {
        }

        void InitializeMembers()
        {
            lockObject = new object();
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(Tick);
            timeout = new TimeSpan(0, 0, 1);
            polling = false;
            polling = false;
            totalSearched = 0;
            stopWatch = new System.Diagnostics.Stopwatch();
            cityWorkers = new List<EventWaitHandle>();
        }

        public void Subscribe(List<AreaDetails> areaDetails, BasePollEventHandler handler)
        {
            foreach(AreaDetails details in areaDetails)
                Subscribe(details, handler);
        }
        public void Subscribe(AreaDetails areaDetails, BasePollEventHandler handler)
        {
            List<BasePollEventHandler> areaHandlers = null;
            if (areaDetailsDictionary.TryGetValue(areaDetails, out areaHandlers))
                areaHandlers.Add(handler);
            else
            {
                areaHandlers = new List<BasePollEventHandler>();
                areaHandlers.Add(handler);
                areaDetailsDictionary.Add(areaDetails, areaHandlers);
            }
        }
        public void Unsubscribe(List<AreaDetails> areaDetails, BasePollEventHandler handler)
        {
            foreach (AreaDetails details in areaDetails)
                Unsubscribe(details, handler);
        }
        public void Unsubscribe(AreaDetails areaDetails, BasePollEventHandler handler)
        {
            List<BasePollEventHandler> areaHandlers = null;
            if (areaDetailsDictionary.TryGetValue(areaDetails, out areaHandlers))
            {
                areaHandlers.Remove(handler);
                if (areaHandlers.Count == 0)
                    areaDetailsDictionary.Remove(areaDetails);
            }
        }
        public bool Start(TimeSpan refreshTime)
        {
            try
            {
                this.refreshTime = new TimeSpan(refreshTime.Hours, refreshTime.Minutes, refreshTime.Seconds);
                if (polling)
                {
                    Logger.Instance.Log("Already refreshing.");
                    return false;
                }
                else
                {
                    this.refreshTimeCounter = new TimeSpan(refreshTime.Hours, refreshTime.Minutes, refreshTime.Seconds);
                    timer.AutoReset = false;
                    timer.Interval = 1;
                    timer.Start();
                    return true;
                }
               
            }
            catch (System.Exception ex)
            {
                Logger.Instance.Log(ex.ToString());
                return false;
            }
        }

        public void Tick(object sender, ElapsedEventArgs e)
        {
            refreshTimeCounter = refreshTimeCounter.Subtract(timeout);
            if (!timer.AutoReset)
            {
                DoPoll();
                timer.AutoReset = true;
                timer.Interval = 1000;
            }
            else if (refreshTimeCounter == TimeSpan.Zero)
            {
                timer.Stop();
                DoPoll();
                refreshTimeCounter = refreshTimeCounter.Add(refreshTime);
            }
            else
            {
                OnPollTimerTick(refreshTimeCounter.Minutes.ToString() + ":" + refreshTimeCounter.Seconds.ToString());
            }
        }

        public bool Stop()
        {
            if (Monitor.TryEnter(lockObject))
            {
                try
                {
                    timer.Stop();
                }
                finally
                {
                    Monitor.Exit(lockObject);
                }

                return true;
            }
            return false;
        }

        public void DoPoll()
        {
            if (polling)
                return;
            OnPollStarted();

            foreach (KeyValuePair<AreaDetails, List<BasePollEventHandler>> areaPair in areaDetailsDictionary)
            {
                //New background poller
                BackgroundAreaPoller backgroundPoller = new BackgroundAreaPoller(areaPair.Key);
                //Add each of our subscribers to the background poller's callbacks
                foreach (BasePollEventHandler handler in areaPair.Value)
                {
                    backgroundPoller.EntryFound += handler.entryFoundHandler;
                    backgroundPoller.NumberOfEntriesFound += handler.numberOfEntriesFoundHandler;
                    backgroundPoller.PollDone += handler.pollDoneHandler;
                    backgroundPoller.PollError += handler.pollErrorHandler;
                    backgroundPoller.aggregatedEntryParsedHandlers += handler.entryParsedHandler;
                }
                //Add to list
                cityWorkers.Add(backgroundPoller);
                //Cap at 100.  Wait for one to quit and then add another.
                backgroundPoller.Start();
                if (cityWorkers.Count == 100)
                {
                    int index = EventWaitHandle.WaitAny(cityWorkers.ToArray());
                    cityWorkers.RemoveAt(index);
                }
            }
            //wait for the last of the to finish.
            EventWaitHandle.WaitAll(cityWorkers.ToArray());

            cityWorkers.Clear();
            
            OnPollEnded();
        }

        public delegate void PollTimerTickHandler(string timeleft);
        public event PollTimerTickHandler PollTimerTick;
        private readonly object onPollTimerTickLock = new object();
        private void OnPollTimerTick(string timeleft)
        {
            lock (onPollTimerTickLock)
            {
                if (PollTimerTick != null)
                    PollTimerTick(timeleft);
            }
        }

        public delegate void PollStartedHandler();
        public event PollStartedHandler PollStarted;
        private readonly object onPollStartedLock = new object();
        private void OnPollStarted()
        {
            lock (onPollStartedLock)
            {
                OnPollTimerTick("Refreshing...");
                stopWatch.Start();
                polling = true;
                Logger.Instance.Log("Poll started.", "");
                if (PollStarted != null)
                    PollStarted();
            }
        }

        public delegate void PollEndedHandler();
        public event PollEndedHandler PollEnded;
        private readonly object onPollEndedLock = new object();
        private void OnPollEnded()
        {
            lock (onPollEndedLock)
            {
                stopWatch.Stop();
                polling = false;
                totalSearched = 0;
                timer.Start();
                Logger.Instance.Log("Poll ended. Time elapsed: " + stopWatch.Elapsed.ToString());
                stopWatch.Reset();
                if (PollEnded != null)
                    PollEnded();
            }
        }
    }
}
