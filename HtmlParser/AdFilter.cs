using HtmlParser;
using System.Collections.Generic;
public class AdFilter : PreciseParseFilter
{
	public string Title;
	public string Body;
	public AdFilter()
	{
		Title = "";
		Body = "";
	}
	public void Populate(string url) 
	{
        AddOmitTags(new List<string>() { "<br>", "</br>" });
		Init(url);
		Title = (FilterBySequence(new int[] {1,0,0}).Value);
		Body = (FilterBySequence(new int[] {1,1,8}).Value);
	}
};
