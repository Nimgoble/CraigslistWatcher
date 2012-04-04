using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public sealed class Sections
{
    private static readonly Sections instance = new Sections();

    public static Sections Instance
    {
        get { return instance; }
    }

    public Dictionary<string/*Parent Section*/,Dictionary<string/*Section*/, string/*suffix*/>> SectionDictionary { get; set; }

    private Sections()
    {
        SectionDictionary = new Dictionary<string/*Parent Section*/,Dictionary<string/*Section*/, string/*suffix*/>>();

        Dictionary<string, string> temp_dictionary = null;

        /*Community*/
        string current_section = "Community";
        SectionDictionary.Add(current_section, new Dictionary<string, string>());
        SectionDictionary[current_section].Add("Community", "ccc");
        SectionDictionary[current_section].Add("artists", "ats");
        SectionDictionary[current_section].Add("childcare", "kid");
        SectionDictionary[current_section].Add("general", "com");
        SectionDictionary[current_section].Add("groups", "grp");
        SectionDictionary[current_section].Add("pets", "pets");
        SectionDictionary[current_section].Add("events", "cal");
        SectionDictionary[current_section].Add("lost+found", "laf");
        SectionDictionary[current_section].Add("musicians", "muc");
        SectionDictionary[current_section].Add("local news", "vnn");
        SectionDictionary[current_section].Add("politics", "pol");
        SectionDictionary[current_section].Add("tideshare", "rid");
        SectionDictionary[current_section].Add("volunteers", "vol");
        SectionDictionary[current_section].Add("classes", "cal/#classes");

        /*Events*/
        current_section = "Events";
        SectionDictionary.Add(current_section, new Dictionary<string, string>());
        SectionDictionary[current_section].Add("Events", "eee");

        /*Gigs*/
        current_section = "Gigs";
        SectionDictionary.Add(current_section, new Dictionary<string, string>());
        SectionDictionary[current_section].Add("Gigs", "ggg");
        SectionDictionary[current_section].Add("crew", "cwg");
        SectionDictionary[current_section].Add("event", "evg");
        SectionDictionary[current_section].Add("labor", "lbg");
        SectionDictionary[current_section].Add("talent", "tlg");
        SectionDictionary[current_section].Add("computer", "cpg");
        SectionDictionary[current_section].Add("creative", "crg");
        SectionDictionary[current_section].Add("domestic", "dmg");
        SectionDictionary[current_section].Add("writing", "wrg");
        

        temp_dictionary = new Dictionary<string, string>();

        /*Housing*/
        current_section = "Housing";
        SectionDictionary.Add(current_section, new Dictionary<string, string>());
        SectionDictionary[current_section].Add("Housing", "hhh");
        SectionDictionary[current_section].Add("apts / housing", "apa");
        SectionDictionary[current_section].Add("rooms / shared", "roo");
        SectionDictionary[current_section].Add("sublets / temporary", "sub");
        SectionDictionary[current_section].Add("housing wanted", "hsw");
        SectionDictionary[current_section].Add("housing swap", "swp");
        SectionDictionary[current_section].Add("vacation rentals", "vac");
        SectionDictionary[current_section].Add("parking / storage", "prk");
        SectionDictionary[current_section].Add("office / commercial", "off");
        SectionDictionary[current_section].Add("real estate for sale", "rea");

        /*Jobs*/
        current_section = "Jobs";
        SectionDictionary.Add(current_section, new Dictionary<string, string>());
        SectionDictionary[current_section].Add("Jobs", "jjj");
        SectionDictionary[current_section].Add("accounting + finance", "acc");
        SectionDictionary[current_section].Add("admin / office", "ofc");
        SectionDictionary[current_section].Add("arch / engineering", "egr");
        SectionDictionary[current_section].Add("art / media / design", "med");
        SectionDictionary[current_section].Add("biotech / science", "sci");
        SectionDictionary[current_section].Add("business / mgmt", "bus");
        SectionDictionary[current_section].Add("customer service", "csr");
        SectionDictionary[current_section].Add("education", "edu");
        SectionDictionary[current_section].Add("food / bev / hosp", "fbh");
        SectionDictionary[current_section].Add("general labor", "lab");
        SectionDictionary[current_section].Add("government", "gov");
        SectionDictionary[current_section].Add("human resources", "hum");
        SectionDictionary[current_section].Add("internet engineers", "eng");
        SectionDictionary[current_section].Add("legal  /  paralegal", "lgl");
        SectionDictionary[current_section].Add("manufacturing", "mnu");
        SectionDictionary[current_section].Add("marketing / pr / ad", "mar");
        SectionDictionary[current_section].Add("medical / health", "hea");
        SectionDictionary[current_section].Add("nonprofit sector", "npo");
        SectionDictionary[current_section].Add("real estate", "rej");
        SectionDictionary[current_section].Add("retail / wholesale", "ret");
        SectionDictionary[current_section].Add("sales / biz dev", "sls");
        SectionDictionary[current_section].Add("salon / spa / fitness", "spa");
        SectionDictionary[current_section].Add("security", "sec");
        SectionDictionary[current_section].Add("skilled trade / craft", "trd");
        SectionDictionary[current_section].Add("software / qa / dba", "sof");
        SectionDictionary[current_section].Add("systems / network", "sad");
        SectionDictionary[current_section].Add("technical support", "tch");
        SectionDictionary[current_section].Add("transport", "trp");
        SectionDictionary[current_section].Add("tv / film / video", "tfr");
        SectionDictionary[current_section].Add("web / info design", "web");
        SectionDictionary[current_section].Add("writing / editing", "wri");
        SectionDictionary[current_section].Add("[ETC]", "etc");
        SectionDictionary[current_section].Add("[ part-time ]", "search/jjj?addFour=part-time");

        /*Personals*/
        current_section = "Personals";
        SectionDictionary.Add(current_section, new Dictionary<string, string>());
        SectionDictionary[current_section].Add("Personals", "ppp");
        SectionDictionary[current_section].Add("strictly platonic", "stp");
        SectionDictionary[current_section].Add("women seek women", "w4w");
        SectionDictionary[current_section].Add("women seeking men", "w4m");
        SectionDictionary[current_section].Add("men seeking women", "m4w");
        SectionDictionary[current_section].Add("men seeking men", "m4m");
        SectionDictionary[current_section].Add("misc romance", "msr");
        SectionDictionary[current_section].Add("casual encounters", "cas");
        SectionDictionary[current_section].Add("missed connections", "mis");
        SectionDictionary[current_section].Add("rants and raves", "rnr");

        /*For Sale*/
        current_section = "For Sale";
        SectionDictionary.Add(current_section, new Dictionary<string, string>());
        SectionDictionary[current_section].Add("For Sale", "sss");
        SectionDictionary[current_section].Add("appliances", "app");
        SectionDictionary[current_section].Add("antiques", "atq");
        SectionDictionary[current_section].Add("barter", "bar");
        SectionDictionary[current_section].Add("bikes", "bik");
        SectionDictionary[current_section].Add("boats", "boa");
        SectionDictionary[current_section].Add("books", "bks");
        SectionDictionary[current_section].Add("business", "bfs");
        SectionDictionary[current_section].Add("computer", "sys");
        SectionDictionary[current_section].Add("free", "zip");
        SectionDictionary[current_section].Add("furniture", "fua");
        SectionDictionary[current_section].Add("general", "for");
        SectionDictionary[current_section].Add("jewelry", "jwl");
        SectionDictionary[current_section].Add("materials", "mat");
        SectionDictionary[current_section].Add("rvs", "rvs");
        SectionDictionary[current_section].Add("sporting", "spo");
        SectionDictionary[current_section].Add("tickets", "tia");
        SectionDictionary[current_section].Add("tools", "tls");
        SectionDictionary[current_section].Add("wanted", "wan");
        SectionDictionary[current_section].Add("arts+crafts", "art");
        SectionDictionary[current_section].Add("auto parts", "pts");
        SectionDictionary[current_section].Add("baby+kids", "bab");
        SectionDictionary[current_section].Add("beauty+hlth", "hab");
        SectionDictionary[current_section].Add("cars+trucks", "cgi-bin/autos.cgi?&amp;category=cta");
        SectionDictionary[current_section].Add("cds/dvd/vhs", "emd");
        SectionDictionary[current_section].Add("cell phones", "mob");
        SectionDictionary[current_section].Add("clothes+acc", "clo");
        SectionDictionary[current_section].Add("collectibles", "clt");
        SectionDictionary[current_section].Add("electronics", "ele");
        SectionDictionary[current_section].Add("farm+garden", "grd");
        SectionDictionary[current_section].Add("garage sale", "gms");
        SectionDictionary[current_section].Add("household", "hsh");
        SectionDictionary[current_section].Add("motorcycles", "mca");
        SectionDictionary[current_section].Add("music instr", "msg");
        SectionDictionary[current_section].Add("photo+video", "pho");
        SectionDictionary[current_section].Add("toys+games", "tag");
        SectionDictionary[current_section].Add("video gaming", "vgm");

        /*Resumes*/
        current_section = "Resumes";
        SectionDictionary.Add(current_section, new Dictionary<string, string>());
        SectionDictionary[current_section].Add("Resumes", "res");

        /*Services*/
        current_section = "Services";
        SectionDictionary.Add(current_section, new Dictionary<string, string>());
        SectionDictionary[current_section].Add("Services", "bbb");
        SectionDictionary[current_section].Add("beauty", "bts");
        SectionDictionary[current_section].Add("creative", "crs");
        SectionDictionary[current_section].Add("computer", "cps");
        SectionDictionary[current_section].Add("cycle", "cys");
        SectionDictionary[current_section].Add("event", "evs");
        SectionDictionary[current_section].Add("financial", "fns");
        SectionDictionary[current_section].Add("legal", "lgs");
        SectionDictionary[current_section].Add("lessons", "lss");
        SectionDictionary[current_section].Add("marine", "mas");
        SectionDictionary[current_section].Add("automotive", "aos");
        SectionDictionary[current_section].Add("farm+garden", "fgs");
        SectionDictionary[current_section].Add("household", "hss");
        SectionDictionary[current_section].Add("labor/move", "lbs");
        SectionDictionary[current_section].Add("skill'd trade", "sks");
        SectionDictionary[current_section].Add("sm biz ads", "biz");
        SectionDictionary[current_section].Add("therapeutic", "thp");
        SectionDictionary[current_section].Add("travel/vac", "trv");
        SectionDictionary[current_section].Add("write/ed/tr8", "wet");
         
    }

    public void PopulateTreeView(ref TreeView tree_view)
    {
        TreeView new_view = new TreeView();
        tree_view.CheckBoxes = true;

        for (int sections = 0; sections < SectionDictionary.Count; sections++)
        {
            string current_section = SectionDictionary.ElementAt(sections).Key;
            tree_view.Nodes.Add(current_section);
            Dictionary<string, string> sub_sections = SectionDictionary.ElementAt(sections).Value;

            //Skip the first child, because that's just the Sections generic suffix
            for (int sub_sections_counter = 1; sub_sections_counter < sub_sections.Count; sub_sections_counter++)
            {
                string current_subsection = sub_sections.ElementAt(sub_sections_counter).Key;
                tree_view.Nodes[sections].Nodes.Add(current_subsection);
            }
        }
    }
}
