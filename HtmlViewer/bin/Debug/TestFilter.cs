using System;
using System.Collections.Generic;
using HtmlParser;
public class TestFilter : PreciseParseFilter
{
	#region ParentNode HTML
	/*
    <html></html>
     <head></head>
      <title>craigslist: chicago classifieds for jobs, apartments, personals, for sale, services, community, and events</title>
      <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
      <meta name="description" content="craigslist provides local classifieds and forums for jobs, housing, for sale, personals, services, local community, and events">
      <meta name="google-site-verification" content="Ie0Do80edB2EurJYj-bxqMdX7zkMjx_FnjtQp_XMFio">
      <link type="text/css" rel="stylesheet" media="all" href="http://www.craigslist.org/styles/craigslist.css?v=11">
     <body class="hp"></body>
      <table cellspacing="0" cellpadding="0" summary="page" id="container"></table>
       <tr></tr>
        <td id="leftbar"></td>
         <div id="logo"></div>
          <a href="http://www.craigslist.org/about/sites">craigslist</a>
         <ul id="postlks"></ul>
          <li>
          <a href="https://post.craigslist.org/c/chi?lang=en">post to classifieds</a>
          <li>
          <a href="https://accounts.craigslist.org">my account</a>
         <form id="search" action="/search/" method="GET"></form>
          <div>search craigslist</div>
          <input type="hidden" name="areaID" value="11">
          <input type="hidden" name="subAreaID" value="">
          <input id="query" name="query">
          <select name="catAbb"></select>
           <option value="ccc">community
           <option value="eee">events
           <option value="ggg">gigs
           <option value="hhh">housing
           <option value="jjj">jobs
           <option value="ppp">personals
           <option value="res">resumes
           <option value="sss" selected="selected">for sale
           <option value="bbb">services
          <input id="go" type="submit" value="&gt;">
         <div class="ban" id="calttl"></div>
          <a href="cal/">event calendar</a>
         <table class="cal" summary="calendar"></table>
          <tr class="dys"></tr>
           <th>S</th>
           <th>M</th>
           <th>T</th>
           <th>W</th>
           <th>T</th>
           <th>F</th>
           <th>S</th>
          <tr></tr>
           <td></td>
            <a href="cal/index20120415.html">15</a>
           <td></td>
            <a href="cal/index20120416.html">16</a>
           <td></td>
            <a href="cal/index20120417.html">17</a>
           <td></td>
            <a href="cal/index20120418.html">18</a>
           <td></td>
            <a href="cal/index20120419.html">19</a>
           <td class="tdy"></td>
            <a href="cal/index20120420.html">20</a>
           <td></td>
            <a href="cal/index20120421.html">21</a>
          <tr></tr>
           <td></td>
            <a href="cal/index20120422.html">22</a>
           <td></td>
            <a href="cal/index20120423.html">23</a>
           <td></td>
            <a href="cal/index20120424.html">24</a>
           <td></td>
            <a href="cal/index20120425.html">25</a>
           <td></td>
            <a href="cal/index20120426.html">26</a>
           <td></td>
            <a href="cal/index20120427.html">27</a>
           <td></td>
            <a href="cal/index20120428.html">28</a>
          <tr></tr>
           <td></td>
            <a href="cal/index20120429.html">29</a>
           <td></td>
            <a href="cal/index20120430.html">30</a>
           <td></td>
            <a href="cal/index20120501.html">1</a>
           <td></td>
            <a href="cal/index20120502.html">2</a>
           <td></td>
            <a href="cal/index20120503.html">3</a>
           <td></td>
            <a href="cal/index20120504.html">4</a>
           <td></td>
            <a href="cal/index20120505.html">5</a>
          <tr></tr>
           <td></td>
            <a href="cal/index20120506.html">6</a>
           <td></td>
            <a href="cal/index20120507.html">7</a>
           <td></td>
            <a href="cal/index20120508.html">8</a>
           <td></td>
            <a href="cal/index20120509.html">9</a>
           <td></td>
            <a href="cal/index20120510.html">10</a>
           <td></td>
            <a href="cal/index20120511.html">11</a>
           <td></td>
            <a href="cal/index20120512.html">12</a>
         <ul id="leftlinks"></ul>
          <li>
          <a href="http://www.craigslist.org/about/help/">help, faq, abuse, legal</a>
          <li>
          <a href="http://www.craigslist.org/about/scams">avoid scams &amp; fraud</a>
          <li>
          <a href="http://www.craigslist.org/about/safety">personal safety tips</a>
          <li>
          <a href="http://www.craigslist.org/about/terms.of.use">terms of use </a>
           <font color="orange">new</font>
          <li></li>
           <a href="http://www.craigslist.org/about/help/system-status.html">system status</a>
          <li>
          <a href="http://www.craigslist.org/about/">about craigslist</a>
          <li>
          <a href="http://blog.craigslist.org/">craigslist blog</a>
          <li>
          <a href="http://www.craigslist.org/about/best/all/">best-of-craigslist</a>
          <li>
          <a href="http://www.craigslist-tv.com/">craigslist TV</a>
          <li>
          <a href="http://craigconnects.org/">craig connects</a>
        <td></td>
         <div id="topban" class="ban"></div>
          <h2>chicago</h2>
          <sup></sup>
           <a href="http://en.wikipedia.org/wiki/Chicago">w</a>
          <span class="sublinks"></span>
           <a href="/chc/" title="city of chicago">chc</a>
           <a href="/nch/" title="north chicagoland">nch</a>
           <a href="/wcl/" title="west chicagoland">wcl</a>
           <a href="/sox/" title="south chicagoland">sox</a>
           <a href="/nwi/" title="northwest indiana">nwi</a>
           <a href="/nwc/" title="northwest suburbs">nwc</a>
         <table summary="main" id="main"></table>
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
        <td id="rightbar"></td>
         <ul class="menu collapsible"></ul>
          <li class=" expand s"></li>
           <h5 class="ban">nearby cl</h5>
           <ul class="acitem"></ul>
            <li class="s">
            <a href="http://battlecreek.craigslist.org">battle creek</a>
            <li class="s">
            <a href="http://bn.craigslist.org">bloomington</a>
            <li class="s">
            <a href="http://chambana.craigslist.org">chambana</a>
            <li class="s">
            <a href="http://chicago.craigslist.org">chicago</a>
            <li class="s">
            <a href="http://grandrapids.craigslist.org">grand rapids</a>
            <li class="s">
            <a href="http://holland.craigslist.org">holland</a>
            <li class="s">
            <a href="http://janesville.craigslist.org">janesville</a>
            <li class="s">
            <a href="http://kalamazoo.craigslist.org">kalamazoo</a>
            <li class="s">
            <a href="http://racine.craigslist.org">kenosha-racine</a>
            <li class="s">
            <a href="http://kokomo.craigslist.org">kokomo</a>
            <li class="s">
            <a href="http://lasalle.craigslist.org">la salle co</a>
            <li class="s">
            <a href="http://madison.craigslist.org">madison</a>
            <li class="s">
            <a href="http://milwaukee.craigslist.org">milwaukee</a>
            <li class="s">
            <a href="http://muskegon.craigslist.org">muskegon</a>
            <li class="s">
            <a href="http://peoria.craigslist.org">peoria</a>
            <li class="s">
            <a href="http://rockford.craigslist.org">rockford</a>
            <li class="s">
            <a href="http://sheboygan.craigslist.org">sheboygan</a>
            <li class="s">
            <a href="http://southbend.craigslist.org">south bend</a>
            <li class="s">
            <a href="http://swmi.craigslist.org">southwest mi</a>
            <li class="s">
            <a href="http://tippecanoe.craigslist.org">tippecanoe</a>
          <li class=" s"></li>
           <h5 class="ban">us cities</h5>
           <ul class="acitem"></ul>
            <li class="s">
            <a href="http://atlanta.craigslist.org/">atlanta</a>
            <li>
            <a href="http://austin.craigslist.org/">austin</a>
            <li class="s">
            <a href="http://boston.craigslist.org/">boston</a>
            <li class="s">
            <a href="http://chicago.craigslist.org/">chicago</a>
            <li>
            <a href="http://dallas.craigslist.org/">dallas</a>
            <li>
            <a href="http://denver.craigslist.org/">denver</a>
            <li class="s">
            <a href="http://detroit.craigslist.org/">detroit</a>
            <li>
            <a href="http://houston.craigslist.org/">houston</a>
            <li class="s">
            <a href="http://lasvegas.craigslist.org/">las&nbsp;vegas</a>
            <li class="s">
            <a href="http://losangeles.craigslist.org/">los&nbsp;angeles</a>
            <li>
            <a href="http://miami.craigslist.org/">miami</a>
            <li>
            <a href="http://minneapolis.craigslist.org/">minneapolis</a>
            <li class="s">
            <a href="http://newyork.craigslist.org/">new&nbsp;york</a>
            <li>
            <a href="http://orangecounty.craigslist.org/">orange&nbsp;co</a>
            <li class="s">
            <a href="http://philadelphia.craigslist.org/">philadelphia</a>
            <li>
            <a href="http://phoenix.craigslist.org/">phoenix</a>
            <li class="s">
            <a href="http://portland.craigslist.org/">portland</a>
            <li>
            <a href="http://raleigh.craigslist.org/">raleigh</a>
            <li>
            <a href="http://sacramento.craigslist.org/">sacramento</a>
            <li>
            <a href="http://sandiego.craigslist.org/">san&nbsp;diego</a>
            <li>
            <a href="http://seattle.craigslist.org/">seattle</a>
            <li class="s">
            <a href="http://sfbay.craigslist.org/">sf&nbsp;bayarea</a>
            <li>
            <a href="http://washingtondc.craigslist.org/">wash dc</a>
            <li class="s more"></li>
             <a href="http://geo.craigslist.org/iso/us">more ...</a>
          <li class=""></li>
           <h5 class="ban">us states</h5>
           <ul class="acitem"></ul>
            <li>
            <a href="http://geo.craigslist.org/iso/us/al">alabama</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/ak">alaska</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/az">arizona</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/ar">arkansas</a>
            <li class="s">
            <a href="http://geo.craigslist.org/iso/us/ca">california</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/co">colorado</a>
            <li class="s">
            <a href="http://geo.craigslist.org/iso/us/ct">connecticut</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/dc">dc</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/de">delaware</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/fl">florida</a>
            <li class="s">
            <a href="http://geo.craigslist.org/iso/us/ga">georgia</a>
            <li>
            <a href="http://micronesia.craigslist.org">guam</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/hi">hawaii</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/id">idaho</a>
            <li class="s">
            <a href="http://geo.craigslist.org/iso/us/il">illinois</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/in">indiana</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/ia">iowa</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/ks">kansas</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/ky">kentucky</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/la">louisiana</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/me">maine</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/md">maryland</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/ma">mass</a>
            <li class="s">
            <a href="http://geo.craigslist.org/iso/us/mi">michigan</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/mn">minnesota</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/ms">mississippi</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/mo">missouri</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/mt">montana</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/nc">n carolina</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/nh">n hampshire</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/ne">nebraska</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/nv">nevada</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/nj">new jersey</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/nm">new mexico</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/ny">new york</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/nd">north dakota</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/oh">ohio</a>
            <li class="s">
            <a href="http://geo.craigslist.org/iso/us/ok">oklahoma</a>
            <li class="s">
            <a href="http://geo.craigslist.org/iso/us/or">oregon</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/pa">pennsylvania</a>
            <li>
            <a href="http://geo.craigslist.org/iso/pr">puerto rico</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/ri">rhode island</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/sc">s carolina</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/sd">south dakota</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/tn">tennessee</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/tx">texas</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/ut">utah</a>
            <li class="s">
            <a href="http://geo.craigslist.org/iso/us/vt">vermont</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/va">virginia</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/wa">washington</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/wv">west virginia</a>
            <li>
            <a href="http://geo.craigslist.org/iso/us/wi">wisconsin</a>
            <li class="s">
            <a href="http://geo.craigslist.org/iso/us/wy">wyoming</a>
            <li class="s more"></li>
             <a href="http://geo.craigslist.org/iso/us">more ...</a>
          <li class=" s"></li>
           <h5 class="ban">canada</h5>
           <ul class="acitem"></ul>
            <li>
            <a href="http://calgary.craigslist.ca">calgary</a>
            <li>
            <a href="http://edmonton.craigslist.ca">edmonton</a>
            <li>
            <a href="http://halifax.craigslist.ca">halifax</a>
            <li class="s">
            <a href="http://montreal.craigslist.ca">montreal</a>
            <li>
            <a href="http://ottawa.craigslist.ca">ottawa</a>
            <li>
            <a href="http://saskatoon.craigslist.ca">saskatoon</a>
            <li class="s">
            <a href="http://toronto.craigslist.ca">toronto</a>
            <li>
            <a href="http://vancouver.craigslist.ca">vancouver</a>
            <li class="s">
            <a href="http://victoria.craigslist.ca">victoria</a>
            <li>
            <a href="http://winnipeg.craigslist.ca">winnipeg</a>
            <li class="s more"></li>
             <a href="http://geo.craigslist.org/iso/ca">more ...</a>
          <li></li>
           <h5 class="ban">cl worldwide</h5>
           <ul class="acitem collapsible"></ul>
            <li class="cont s"></li>
             <h5>africa</h5>
             <ul class="acitem"></ul>
              <li class="s">
              <a href="http://geo.craigslist.org/iso/eg">egypt</a>
              <li>
              <a href="http://geo.craigslist.org/iso/et">ethiopia</a>
              <li>
              <a href="http://geo.craigslist.org/iso/gh">ghana</a>
              <li>
              <a href="http://geo.craigslist.org/iso/ke">kenya</a>
              <li>
              <a href="http://geo.craigslist.org/iso/ma">morocco</a>
              <li>
              <a href="http://craigslist.co.za">south africa</a>
              <li>
              <a href="http://geo.craigslist.org/iso/tn">tunisia</a>
            <li class="cont s"></li>
             <h5>americas</h5>
             <ul class="acitem"></ul>
              <li>
              <a href="http://geo.craigslist.org/iso/ar">argentina</a>
              <li>
              <a href="http://geo.craigslist.org/iso/bo">bolivia</a>
              <li class="s">
              <a href="http://rio.craigslist.org">brazil</a>
              <li>
              <a href="http://craigslist.ca">canada</a>
              <li>
              <a href="http://geo.craigslist.org/iso/cl">chile</a>
              <li>
              <a href="http://geo.craigslist.org/iso/co">colombia</a>
              <li>
              <a href="http://geo.craigslist.org/iso/cr">costa rica</a>
              <li>
              <a href="http://geo.craigslist.org/iso/do">dom republic</a>
              <li class="s">
              <a href="http://geo.craigslist.org/iso/ec">ecuador</a>
              <li>
              <a href="http://geo.craigslist.org/iso/sv">el salvador</a>
              <li>
              <a href="http://geo.craigslist.org/iso/gt">guatemala</a>
              <li class="s">
              <a href="http://craigslist.com.mx">mexico</a>
              <li>
              <a href="http://geo.craigslist.org/iso/ni">nicaragua</a>
              <li>
              <a href="http://geo.craigslist.org/iso/pa">panama</a>
              <li class="s">
              <a href="http://craigslist.com.pe">peru</a>
              <li>
              <a href="http://geo.craigslist.org/iso/pr">puerto rico</a>
              <li>
              <a href="http://geo.craigslist.org/iso/us">united states</a>
              <li>
              <a href="http://geo.craigslist.org/iso/uy">uruguay</a>
              <li>
              <a href="http://geo.craigslist.org/iso/vi">us virgin islands</a>
              <li>
              <a href="http://geo.craigslist.org/iso/ve">venezuela</a>
            <li class="cont s"></li>
             <h5>asia</h5>
             <ul class="acitem"></ul>
              <li class="s">
              <a href="http://geo.craigslist.org/iso/bd">bangladesh</a>
              <li>
              <a href="http://shanghai.craigslist.com.cn">china</a>
              <li>
              <a href="http://craigslist.hk">hong kong</a>
              <li class="s">
              <a href="http://craigslist.co.in">india</a>
              <li>
              <a href="http://geo.craigslist.org/iso/id">indonesia</a>
              <li>
              <a href="http://geo.craigslist.org/iso/ir">iran</a>
              <li>
              <a href="http://geo.craigslist.org/iso/iq">iraq</a>
              <li>
              <a href="http://telaviv.craigslist.org">israel</a>
              <li class="s">
              <a href="http://craigslist.jp">japan</a>
              <li>
              <a href="http://geo.craigslist.org/iso/kw">kuwait</a>
              <li>
              <a href="http://geo.craigslist.org/iso/lb">lebanon</a>
              <li>
              <a href="http://geo.craigslist.org/iso/my">malaysia</a>
              <li>
              <a href="http://geo.craigslist.org/iso/pk">pakistan</a>
              <li>
              <a href="http://manila.craigslist.com.ph">philippines</a>
              <li class="s">
              <a href="http://moscow.craigslist.org">russia</a>
              <li>
              <a href="http://craigslist.com.sg">singapore</a>
              <li>
              <a href="http://geo.craigslist.org/iso/kr">south korea</a>
              <li class="s">
              <a href="http://craigslist.com.tw">taiwan</a>
              <li>
              <a href="http://geo.craigslist.org/iso/th">thailand</a>
              <li>
              <a href="http://craigslist.com.tr">turkey</a>
              <li>
              <a href="http://geo.craigslist.org/iso/ae">uae</a>
              <li class="s">
              <a href="http://geo.craigslist.org/iso/vn">vietnam</a>
              <li>
              <a href="http://geo.craigslist.org/iso/ps">west bank</a>
            <li class="cont s"></li>
             <h5>europe</h5>
             <ul class="acitem"></ul>
              <li>
              <a href="http://craigslist.at">austria</a>
              <li>
              <a href="http://craigslist.be">belgium</a>
              <li>
              <a href="http://geo.craigslist.org/iso/bg">bulgaria</a>
              <li>
              <a href="http://geo.craigslist.org/iso/hr">croatia</a>
              <li>
              <a href="http://geo.craigslist.org/iso/cz">czech republic</a>
              <li>
              <a href="http://craigslist.dk">denmark</a>
              <li>
              <a href="http://craigslist.fi">finland</a>
              <li>
              <a href="http://craigslist.fr">france</a>
              <li>
              <a href="http://craigslist.de">germany</a>
              <li>
              <a href="http://craigslist.gr">greece</a>
              <li>
              <a href="http://geo.craigslist.org/iso/hu">hungary</a>
              <li>
              <a href="http://geo.craigslist.org/iso/is">iceland</a>
              <li>
              <a href="http://geo.craigslist.org/iso/ie">ireland</a>
              <li class="s">
              <a href="http://rome.craigslist.it">italy</a>
              <li>
              <a href="http://geo.craigslist.org/iso/lu">luxembourg</a>
              <li>
              <a href="http://geo.craigslist.org/iso/nl">netherlands</a>
              <li class="s">
              <a href="http://geo.craigslist.org/iso/no">norway</a>
              <li>
              <a href="http://craigslist.pl">poland</a>
              <li class="s">
              <a href="http://craigslist.pt">portugal</a>
              <li>
              <a href="http://geo.craigslist.org/iso/ro">romania</a>
              <li class="s">
              <a href="http://moscow.craigslist.org">russia</a>
              <li class="s">
              <a href="http://craigslist.es">spain</a>
              <li>
              <a href="http://craigslist.se">sweden</a>
              <li>
              <a href="http://craigslist.ch">switzerland</a>
              <li>
              <a href="http://craigslist.com.tr">turkey</a>
              <li>
              <a href="http://geo.craigslist.org/iso/ua">ukraine</a>
              <li>
              <a href="http://craigslist.co.uk">united kingdom</a>
            <li class="cont s"></li>
             <h5>middle east</h5>
             <ul class="acitem"></ul>
              <li class="s">
              <a href="http://geo.craigslist.org/iso/eg">egypt</a>
              <li>
              <a href="http://geo.craigslist.org/iso/iq">iraq</a>
              <li>
              <a href="http://telaviv.craigslist.org">israel</a>
              <li>
              <a href="http://geo.craigslist.org/iso/kw">kuwait</a>
              <li>
              <a href="http://geo.craigslist.org/iso/lb">lebanon</a>
              <li>
              <a href="http://craigslist.com.tr">turkey</a>
              <li>
              <a href="http://geo.craigslist.org/iso/ae">uae</a>
              <li>
              <a href="http://geo.craigslist.org/iso/ps">west bank</a>
            <li class="cont s"></li>
             <h5>oceania</h5>
             <ul class="acitem"></ul>
              <li>
              <a href="http://sydney.craigslist.com.au">australia</a>
              <li>
              <a href="http://geo.craigslist.org/iso/gu">guam</a>
              <li>
              <a href="http://auckland.craigslist.org">new zealand</a>
              <li>
              <a href="http://manila.craigslist.com.ph">philippines</a>
         <p class="more"></p>
          <a href="http://www.craigslist.org/about/sites"></a>
           <em>more...</em>
	*/
	#endregion
	public HtmlTag ParentNode;
	public TestFilter()
	{
		ParentNode = new HtmlTag();
	}
	public void Populate(string url) 
	{
		htmlParser.AddOmitTags(new List<string>() {"<br>","</br>"});
		Init(url);
		ParentNode = (FilterBySequence(new int[] {1}));
	}
};
