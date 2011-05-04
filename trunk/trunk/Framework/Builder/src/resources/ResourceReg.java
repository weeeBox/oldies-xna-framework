package resources;

import java.util.ArrayList;
import java.util.List;

public class ResourceReg
{
	private static List<ContentPair> contentPairs = new ArrayList<ContentPair>();
	
	public static void register(String importer, String processor)
	{
		ContentPair pair = new ContentPair(importer, processor);
		if (!contentPairs.contains(pair))
		{
			contentPairs.add(pair);
		}
	}
	
	public static List<ContentPair> getContentPairs() 
	{
		return contentPairs;
	}
}
