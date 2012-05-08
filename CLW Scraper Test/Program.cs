using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLWFramework;
using CraigslistScraper;
namespace CLW_Scraper_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            CLWSQL sqlManager = new CLWSQL();
            sqlManager.Open("eagle-d8c30ec3c\\jhar");
            sqlManager.Close();
        }
    }
}
