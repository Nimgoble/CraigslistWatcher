using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace CLWFramework
{
    public enum LogSeverity
    {
        lsDebug = 0,
        lsDefault,
        lsHigh
    }
    public enum LogType
    {
        ltGeneral = 0,
        ltArea,
        ltError
    }

    public sealed class Logger
    {
        private static string[] LogTypeColors = { "green", "blue", "red" };
        private static Logger instance = null;
        private static object lock_object_ = new object();
        private System.IO.StreamWriter logFile;
        private LogSeverity logSeverity;
        public static Logger Instance
        {
            get
            {
                if (instance == null)
                    throw new Exception("Logger.Initialize has not been called.");
                return instance;
            }
        }

        public static void Initiate(string fileName)
        {
            if (instance == null)
            {
                instance = new Logger(fileName);
            }
        }

        private Logger(string fileName)
        {
            string fullFileName = Directory.GetCurrentDirectory() + "\\logs";
            if (!Directory.Exists(fullFileName))
                Directory.CreateDirectory(fullFileName);

            if (fileName == null || fileName == String.Empty)
                fileName = "log";
            fullFileName += fileName + "_" + DateTime.Now.ToString("ddMMyyyy") + ".html";
            logFile = new System.IO.StreamWriter(fullFileName, true);
            logSeverity = LogSeverity.lsDefault;
        }

        public void Finish()
        {
            logFile.Close();
        }

        public void Log(string message)
        {
            this.Log(message, "General", LogType.ltGeneral);
        }

        public void Log(string message, string area)
        {
            this.Log(message, area, LogType.ltArea);
        }

        public void Log(string message, string area, LogType type)
        {
            this.Log(message, area, type, LogSeverity.lsDefault);
        }

        public void Log(string message, LogType type)
        {
            this.Log(message, "General", type, LogSeverity.lsDefault);
        }

        public delegate void LogMessageHandler(string message);
        public event LogMessageHandler LogMessage;
        public void Log(string message, string area, LogType type, LogSeverity severity)
        {
            lock (lock_object_)
            {
                //if(severity <)
                string output = "<font style=\"font-size:12px;text-align:left\"><font style=\"text-decoration:underline;\">"
                    + DateTime.Now.ToString("h:mm:ss.fff");
                string origin = String.Format("<font style=\"color:{0}\">{1}</font>", LogTypeColors[(int)type], area);
                output += "(" + origin + ")</font>: " + message + "</font><br><hr>";
                logFile.WriteLine(output);
                if (LogMessage != null)
                    LogMessage(output);
            }
        }
    }
}
