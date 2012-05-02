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
        private CLWSQL client;
        public CraigslistScraper()
        {
            client = new CLWSQL();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                client.Open("");
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
