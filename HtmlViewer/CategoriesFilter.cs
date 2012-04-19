using System;
using System.Collections.Generic;
using HtmlParser;
public class CategoriesFilter : PreciseParseFilter
{
    #region ParentNode HTML
    /*
    <tr></tr>
     <td></td>
      <div id="ccc" class="col"></div>
       <h4 class="ban"></h4>
        <a href="ccc/">community</a>
       <div class="cats"></div>
        <ul id="ccc0"></ul>
         <li></li>
          <a href="act/">activities</a>
         <li></li>
          <a href="ats/">artists</a>
         <li></li>
          <a href="kid/">childcare</a>
         <li></li>
          <a href="com/">general</a>
         <li></li>
          <a href="grp/">groups</a>
         <li></li>
          <a href="pet/">pets</a>
         <li></li>
          <a href="cal/">events</a>
        <ul id="ccc1"></ul>
         <li></li>
          <a href="laf/">lost+found</a>
         <li></li>
          <a href="muc/">musicians</a>
         <li></li>
          <a href="vnn/">local&nbsp;news</a>
         <li></li>
          <a href="pol/">politics</a>
         <li></li>
          <a href="rid/">rideshare</a>
         <li></li>
          <a href="vol/">volunteers</a>
         <li></li>
          <a href="cal/#classes">classes</a>
      <div id="ppp" class="col"></div>
       <h4 class="ban">personals</h4>
       <div class="cats"></div>
        <ul id="ppp0"></ul>
         <li></li>
          <a href="/cgi-bin/personals.cgi?category=stp">strictly platonic</a>
         <li></li>
          <a href="/cgi-bin/personals.cgi?category=w4w">women seek women</a>
         <li></li>
          <a href="/cgi-bin/personals.cgi?category=w4m">women seeking men</a>
         <li></li>
          <a href="/cgi-bin/personals.cgi?category=m4w">men seeking women</a>
         <li></li>
          <a href="/cgi-bin/personals.cgi?category=m4m">men seeking men</a>
         <li></li>
          <a href="/cgi-bin/personals.cgi?category=msr">misc romance</a>
         <li></li>
          <a href="/cgi-bin/personals.cgi?category=cas">casual encounters</a>
         <li></li>
          <a href="/cgi-bin/personals.cgi?category=mis">missed connections</a>
         <li></li>
          <a href="/cgi-bin/personals.cgi?category=rnr">rants and raves</a>
      <div id="forums" class="col"></div>
       <h4 class="ban"></h4>
        <a href="/forums/">discussion forums</a>
       <div class="cats"></div>
        <ul id="forums0"></ul>
         <li></li>
          <a href="/forums/?forumID=89">1099</a>
         <li></li>
          <a href="/forums/?forumID=3232">apple</a>
         <li></li>
          <a href="/forums/?forumID=49">arts</a>
         <li></li>
          <a href="/forums/?forumID=78">atheist</a>
         <li></li>
          <a href="/forums/?forumID=5">autos</a>
         <li></li>
          <a href="/forums/?forumID=88">beauty</a>
         <li></li>
          <a href="/forums/?forumID=95">bikes</a>
         <li></li>
          <a href="/forums/?forumID=47">celebs</a>
         <li></li>
          <a href="/forums/?forumID=34">comp</a>
         <li></li>
          <a href="/forums/?forumID=83">crafts</a>
         <li></li>
          <a href="/forums/?forumID=122">diet</a>
         <li></li>
          <a href="/forums/?forumID=76">divorce</a>
         <li></li>
          <a href="/forums/?forumID=130">dying</a>
         <li></li>
          <a href="/forums/?forumID=99">eco</a>
         <li></li>
          <a href="/forums/?forumID=90">educ</a>
         <li></li>
          <a href="/forums/?forumID=79">etiquet</a>
         <li></li>
          <a href="/forums/?forumID=8">feedbk</a>
         <li></li>
          <a href="/forums/?forumID=41">film</a>
         <li></li>
          <a href="/forums/?forumID=92">fitness</a>
         <li></li>
          <a href="/forums/?forumID=64">fixit</a>
         <li></li>
          <a href="/forums/?forumID=22">food</a>
         <li></li>
          <a href="/forums/?forumID=2007">frugal</a>
         <li></li>
          <a href="/forums/?forumID=85">gaming</a>
         <li></li>
          <a href="/forums/?forumID=54">garden</a>
        <ul id="forums1"></ul>
         <li></li>
          <a href="/forums/?forumID=1204">gifts</a>
         <li></li>
          <a href="/forums/?forumID=575">haiku</a>
         <li></li>
          <a href="/forums/?forumID=43">health</a>
         <li></li>
          <a href="/forums/?forumID=9">help</a>
         <li></li>
          <a href="/forums/?forumID=81">history</a>
         <li></li>
          <a href="/forums/?forumID=6">housing</a>
         <li></li>
          <a href="/forums/?forumID=7">jobs</a>
         <li></li>
          <a href="/forums/?forumID=1257">jokes</a>
         <li></li>
          <a href="/forums/?forumID=200269">kink</a>
         <li></li>
          <a href="/forums/?forumID=1926">l.t.r.</a>
         <li></li>
          <a href="/forums/?forumID=73">legal</a>
         <li></li>
          <a href="/forums/?forumID=1991">linux</a>
         <li></li>
          <a href="/forums/?forumID=48">loc pol</a>
         <li></li>
          <a href="/forums/?forumID=72">m4m</a>
         <li></li>
          <a href="/forums/?forumID=53">money</a>
         <li></li>
          <a href="/forums/?forumID=1947">motocy</a>
         <li></li>
          <a href="/forums/?forumID=24">music</a>
         <li></li>
          <a href="/forums/?forumID=501">npo</a>
         <li></li>
          <a href="/forums/?forumID=4">open</a>
         <li></li>
          <a href="/forums/?forumID=16">outdoor</a>
         <li></li>
          <a href="/forums/?forumID=50">over 50</a>
         <li></li>
          <a href="/forums/?forumID=84">p.o.c.</a>
         <li></li>
          <a href="/forums/?forumID=39">parent</a>
         <li></li>
          <a href="/forums/?forumID=19">pefo</a>
        <ul id="forums2"></ul>
         <li></li>
          <a href="/forums/?forumID=26">pets</a>
         <li></li>
          <a href="/forums/?forumID=71">philos</a>
         <li></li>
          <a href="/forums/?forumID=20">politic</a>
         <li></li>
          <a href="/forums/?forumID=29">psych</a>
         <li></li>
          <a href="/forums/?forumID=46">queer</a>
         <li></li>
          <a href="/forums/?forumID=12">recover</a>
         <li></li>
          <a href="/forums/?forumID=59">religion</a>
         <li></li>
          <a href="/forums/?forumID=28">rofo</a>
         <li></li>
          <a href="/forums/?forumID=96">science</a>
         <li></li>
          <a href="/forums/?forumID=91">shop</a>
         <li></li>
          <a href="/forums/?forumID=93">spirit</a>
         <li></li>
          <a href="/forums/?forumID=32">sports</a>
         <li></li>
          <a href="/forums/?forumID=98">t.v.</a>
         <li></li>
          <a href="/forums/?forumID=1040">tax</a>
         <li></li>
          <a href="/forums/?forumID=21">testing</a>
         <li></li>
          <a href="/forums/?forumID=97">transg</a>
         <li></li>
          <a href="/forums/?forumID=42">travel</a>
         <li></li>
          <a href="/forums/?forumID=2400">vegan</a>
         <li></li>
          <a href="/forums/?forumID=69">w4w</a>
         <li></li>
          <a href="/forums/?forumID=15">wed</a>
         <li></li>
          <a href="/forums/?forumID=120">wine</a>
         <li></li>
          <a href="/forums/?forumID=94">women</a>
         <li></li>
          <a href="/forums/?forumID=7000">words</a>
         <li></li>
          <a href="/forums/?forumID=27">writers</a>
     <td></td>
      <div id="hhh" class="col"></div>
       <h4 class="ban"></h4>
        <a href="hhh/">housing</a>
       <div class="cats"></div>
        <ul id="hhh0"></ul>
         <li></li>
          <a href="apa/">apts / housing</a>
         <li></li>
          <a href="roo/">rooms / shared</a>
         <li></li>
          <a href="sub/">sublets / temporary</a>
         <li></li>
          <a href="hsw/">housing wanted</a>
         <li></li>
          <a href="swp/">housing swap</a>
         <li></li>
          <a href="vac/">vacation rentals</a>
         <li></li>
          <a href="prk/">parking / storage</a>
         <li></li>
          <a href="off/">office / commercial</a>
         <li></li>
          <a href="rea/">real estate for sale</a>
      <div id="sss" class="col"></div>
       <h4 class="ban"></h4>
        <a href="sss/">for sale</a>
       <div class="cats"></div>
        <ul id="sss0"></ul>
         <li></li>
          <a href="ppa/">appliances</a>
         <li></li>
          <a href="ata/">antiques</a>
         <li></li>
          <a href="bar/">barter</a>
         <li></li>
          <a href="bia/">bikes</a>
         <li></li>
          <a href="boo/">boats</a>
         <li></li>
          <a href="bka/">books</a>
         <li></li>
          <a href="bfa/">business</a>
         <li></li>
          <a href="sya/">computer</a>
         <li></li>
          <a href="zip/">free</a>
         <li></li>
          <a href="fua/">furniture</a>
         <li></li>
          <a href="foa/">general</a>
         <li></li>
          <a href="jwa/">jewelry</a>
         <li></li>
          <a href="maa/">materials</a>
         <li></li>
          <a href="rva/">rvs</a>
         <li></li>
          <a href="sga/">sporting</a>
         <li></li>
          <a href="tia/">tickets</a>
         <li></li>
          <a href="tla/">tools</a>
         <li></li>
          <a href="wan/">wanted</a>
        <ul id="sss1"></ul>
         <li></li>
          <a href="ara/">arts+crafts</a>
         <li></li>
          <a href="pta/">auto parts</a>
         <li></li>
          <a href="baa/">baby+kids</a>
         <li></li>
          <a href="haa/">beauty+hlth</a>
         <li></li>
          <a href="/cgi-bin/autos.cgi?&amp;category=cta/">cars+trucks</a>
         <li></li>
          <a href="ema/">cds/dvd/vhs</a>
         <li></li>
          <a href="moa/">cell phones</a>
         <li></li>
          <a href="cla/">clothes+acc</a>
         <li></li>
          <a href="cba/">collectibles</a>
         <li></li>
          <a href="ela/">electronics</a>
         <li></li>
          <a href="gra/">farm+garden</a>
         <li></li>
          <a href="gms/">garage sale</a>
         <li></li>
          <a href="hsa/">household</a>
         <li></li>
          <a href="mca/">motorcycles</a>
         <li></li>
          <a href="msa/">music instr</a>
         <li></li>
          <a href="pha/">photo+video</a>
         <li></li>
          <a href="taa/">toys+games</a>
         <li></li>
          <a href="vga/">video gaming</a>
      <div id="bbb" class="col"></div>
       <h4 class="ban"></h4>
        <a href="bbb/">services</a>
       <div class="cats"></div>
        <ul id="bbb0"></ul>
         <li></li>
          <a href="bts/">beauty</a>
         <li></li>
          <a href="crs/">creative</a>
         <li></li>
          <a href="cps/">computer</a>
         <li></li>
          <a href="cys/">cycle</a>
         <li></li>
          <a href="evs/">event</a>
         <li></li>
          <a href="fns/">financial</a>
         <li></li>
          <a href="lgs/">legal</a>
         <li></li>
          <a href="lss/">lessons</a>
         <li></li>
          <a href="mas/">marine</a>
         <li></li>
          <a href="pas/">pet</a>
        <ul id="bbb1"></ul>
         <li></li>
          <a href="aos/">automotive</a>
         <li></li>
          <a href="fgs/">farm+garden</a>
         <li></li>
          <a href="hss/">household</a>
         <li></li>
          <a href="lbs/">labor/move</a>
         <li></li>
          <a href="sks/">skill'd trade</a>
         <li></li>
          <a href="rts/">real estate</a>
         <li></li>
          <a href="biz/">sm biz ads</a>
         <li></li>
          <a href="thp/">therapeutic</a>
         <li></li>
          <a href="trv/">travel/vac</a>
         <li></li>
          <a href="wet/">write/ed/tr8</a>
     <td></td>
      <div id="jjj" class="col"></div>
       <h4 class="ban"></h4>
        <a href="jjj/">jobs</a>
       <div class="cats"></div>
        <ul id="jjj0"></ul>
         <li></li>
          <a href="acc/">accounting+finance</a>
         <li></li>
          <a href="ofc/">admin / office</a>
         <li></li>
          <a href="egr/">arch / engineering</a>
         <li></li>
          <a href="med/">art / media / design</a>
         <li></li>
          <a href="sci/">biotech / science</a>
         <li></li>
          <a href="bus/">business / mgmt</a>
         <li></li>
          <a href="csr/">customer service</a>
         <li></li>
          <a href="edu/">education</a>
         <li></li>
          <a href="fbh/">food / bev / hosp</a>
         <li></li>
          <a href="lab/">general labor</a>
         <li></li>
          <a href="gov/">government</a>
         <li></li>
          <a href="hum/">human resources</a>
         <li></li>
          <a href="eng/">internet engineers</a>
         <li></li>
          <a href="lgl/">legal  /  paralegal</a>
         <li></li>
          <a href="mnu/">manufacturing</a>
         <li></li>
          <a href="mar/">marketing / pr / ad</a>
         <li></li>
          <a href="hea/">medical / health</a>
         <li></li>
          <a href="npo/">nonprofit sector</a>
         <li></li>
          <a href="rej/">real estate</a>
         <li></li>
          <a href="ret/">retail / wholesale</a>
         <li></li>
          <a href="sls/">sales / biz dev</a>
         <li></li>
          <a href="spa/">salon / spa / fitness</a>
         <li></li>
          <a href="sec/">security</a>
         <li></li>
          <a href="trd/">skilled trade / craft</a>
         <li></li>
          <a href="sof/">software / qa / dba</a>
         <li></li>
          <a href="sad/">systems / network</a>
         <li></li>
          <a href="tch/">technical support</a>
         <li></li>
          <a href="trp/">transport</a>
         <li></li>
          <a href="tfr/">tv / film / video</a>
         <li></li>
          <a href="web/">web / info design</a>
         <li></li>
          <a href="wri/">writing / editing</a>
         <li></li>
          <a href="etc/">[ETC]</a>
         <li></li>
          <a href="/search/jjj?addFour=part-time">[ part-time ]</a>
      <div id="ggg" class="col"></div>
       <h4 class="ban"></h4>
        <a href="ggg/">gigs</a>
       <div class="cats"></div>
        <ul id="ggg0"></ul>
         <li></li>
          <a href="cwg/">crew</a>
         <li></li>
          <a href="evg/">event</a>
         <li></li>
          <a href="lbg/">labor</a>
         <li></li>
          <a href="tlg/">talent</a>
        <ul id="ggg1"></ul>
         <li></li>
          <a href="cpg/">computer</a>
         <li></li>
          <a href="crg/">creative</a>
         <li></li>
          <a href="dmg/">domestic</a>
         <li></li>
          <a href="wrg/">writing</a>
      <div id="res" class="col"></div>
       <h4 class="ban"></h4>
        <a href="res/">resumes</a>
       <div class="cats"></div>
	*/
    #endregion
	public HtmlTag ParentNode;
	public CategoriesFilter()
	{
		ParentNode = new HtmlTag();
	}
	public void Populate(string url) 
	{
        htmlParser.AddOmitTags(new List<string>() { "<br>", "</br>" });
		Init(url);
		ParentNode = (FilterBySequence(new int[] {1,1,0,0,1,1,0}));
        ParentNode.RemoveTags(new List<string> {"ul", "li", "div"});
	}
};
