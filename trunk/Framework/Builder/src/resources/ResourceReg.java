package resources;

import java.util.ArrayList;
import java.util.List;

public class ResourceReg
{
	private static List<ContentPair> contentPairs = new ArrayList<ContentPair>();
	private static List<String> filterExtensions = new ArrayList<String>();
	
	public static void register(String importer, String processor, String...filters)
	{
		ContentPair pair = new ContentPair(importer, processor);
		if (!contentPairs.contains(pair))
		{
			contentPairs.add(pair);
		}
		
		for (String filter : filters)
		{
			if (!filterExtensions.contains(filter))
			{
				filterExtensions.add(filter);
			}
		}
	}
	
	public static List<ContentPair> getContentPairs() 
	{
		return contentPairs;
	}
	
	public static List<String> getFilters()
	{
		return filterExtensions;
	}
}
