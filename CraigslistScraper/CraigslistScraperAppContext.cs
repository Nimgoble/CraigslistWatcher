using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace CraigslistScraper
{
    public class CraigslistScraperAppContext : ApplicationContext
    {
        private CLWSQL client;
        public CraigslistScraperAppContext()
        {
            client = new CLWSQL();
        }

        public void OnStart()
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
    }
}
