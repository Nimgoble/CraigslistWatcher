using System;
using System.Collections.Generic;
using HtmlParser;
public class LocationsFilter : PreciseParseFilter
{
	public Dictionary<String, String> SectionToName;
	public LocationsFilter()
	{
        SectionToName = new Dictionary<String, String>();
	}
	public void Populate(string url) 
	{
		AddOmitTags(new List<string>() {"<br>","</br>","<ul>","</ul>","<li>","</li>"});
		Init(url);
        HtmlTag parent = (FilterBySequence(new int[] {1,1}));
        Dictionary<string, KeyValuePair<string, string>> classAndAttributes = new Dictionary<string, KeyValuePair<string, string>>();

        //classAndAttributes.Add();
        classAndAttributes.Add("div", new KeyValuePair<string,string>("class", "colmask"));
        List<HtmlTag> parentList = new List<HtmlTag>();
        parent.FilterForChildrenByNameAndAttribute(classAndAttributes, out parentList);
        //parent.FilterForChildrenByNameAndAttribute
        
	}
    private void ParseSectionNames(HtmlTag parent)
    {
        List<HtmlTag> parentTag = null;
        parent.FilterForChildrenByNameAndAttribute("div", new KeyValuePair<string, string>("class", "jump_to_continents"), out parentTag);
        if (parentTag != null)
        {
            HtmlTag locationsStuff = parentTag[0];
            //foreach(
        }
    }
};
