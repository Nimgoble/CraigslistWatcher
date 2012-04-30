using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using CLWFramework;
namespace CraigslistScraper
{
    public partial class CraigslistScraper : ServiceBase
    {
        private PollHandler pollHandler;
        public CraigslistScraper()
        {
            Logger.Initiate("");
            InitializeComponent();
            pollHandler = null;
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Locations.Instance.DownloadLocations();
                Categories.Instance.DownloadCategories();

            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        protected override void OnStop()
        {
        }
    }
}
