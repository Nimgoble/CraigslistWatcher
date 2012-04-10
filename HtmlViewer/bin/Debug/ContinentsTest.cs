using System;
using System.Collections.Generic;
using HtmlParser;
public class ContinentsTest : PreciseParseFilter
{
	public string ContinentsList;
	public ContinentsTest()
	{
		ContinentsList = "";
	}
	public void Populate(string url) 
	{
		AddOmitTags(new List<string>() {"<br>","</br>","<ul>","</ul>","<li>","</li>"});

		Init(url);
		ContinentsList = (FilterBySequence(new int[] {1,1,1}));
	}
};
