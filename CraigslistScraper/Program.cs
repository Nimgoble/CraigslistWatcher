using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
namespace CraigslistScraper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if ((1 == args.Length) && ("-runAsApp" == args[0]))
            {
                Application.Run(new CraigslistScraperAppContext());
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
			    { 
				    new CraigslistScraper() 
			    };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
