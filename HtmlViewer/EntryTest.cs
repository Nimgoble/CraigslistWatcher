using System;
using System.Collections.Generic;
using HtmlParser;

public class EntryTest : PreciseParseFilter
{
	public List<string> EntryList;
	public EntryTest()
	{
		EntryList = new List<string>();
	}
	public void Populate(string url) 
	{
		Init(url);
		EntryList.Add(FilterBySequence(new int[] {1,1,5,2,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,3,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,4,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,5,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,6,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,7,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,8,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,9,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,10,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,11,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,12,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,13,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,14,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,15,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,16,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,17,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,18,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,19,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,20,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,21,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,22,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,23,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,24,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,25,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,26,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,27,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,28,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,29,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,30,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,31,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,32,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,33,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,35,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,36,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,37,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,38,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,39,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,40,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,41,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,42,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,43,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,44,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,45,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,46,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,47,1}));
		EntryList.Add(FilterBySequence(new int[] {1,1,5,48,1}));
	}
};
