using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CraigslistWatcher;
using System.IO;
using System.Text.RegularExpressions;

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
    private static Logger instance = new Logger();
    public CraigslistWatcher.TabStripTest log_form_ = null;
    private static object lock_object_ = new object();
    private System.IO.StreamWriter log_file_;
    private LogSeverity log_severity_;
    public static Logger Instance
    {
        get { return instance; }
    }

    public Logger()
    {
        string file_name = Directory.GetCurrentDirectory() + "\\logs";
        if (!Directory.Exists(file_name))
            Directory.CreateDirectory(file_name);

        file_name += "\\log_" + DateTime.Now.ToString("ddMMyyyy") + ".html";
        log_file_ = new System.IO.StreamWriter(file_name, true);
        log_severity_ = LogSeverity.lsDefault;
    }

    public void Finish()
    {
        log_file_.Close();
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

    public void Log(string message, string area, LogType type, LogSeverity severity)
    {
        lock (lock_object_)
        {
            //if(severity <)
            string output = "<font style=\"font-size:12px;text-align:left\"><font style=\"text-decoration:underline;\">"
                + DateTime.Now.ToString("h:mm:ss.fff");
            string origin = String.Format("<font style=\"color:{0}\">{1}</font>", LogTypeColors[(int)type], area);
            output += "(" + origin + ")</font>: " + message + "</font><br><hr>";
            log_file_.WriteLine(output);
            log_form_.Log(output);
        }
    }
}