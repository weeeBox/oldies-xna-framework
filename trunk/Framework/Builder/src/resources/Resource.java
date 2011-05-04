package resources;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import tasks.ContentProjTask;
import utils.pack.FileUtils;

public abstract class Resource 
{	
	private String name;
	private File file;
	private ResPackage pack;
		
	private static List<ContentPair> contentPairs = new ArrayList<ContentPair>();
	private static List<String> filterExtensions = new ArrayList<String>();
	
	public static void registerResource(String importer, String processor, String...filters)
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
	
	public ResPackage getPackage() 
	{
		return pack;
	}

	public void setPackage(ResPackage parent) 
	{
		this.pack = parent;
	}

	public String getName()
	{
		return name;
	}
	
	public String getLongName() 
	{
		return (getResourceTypePrefix() + name).toUpperCase();
	}
	
	public void setName(String name) 
	{
		this.name = name;
	}
	
	public String getShortName()
	{
		return FileUtils.getFilenameNoExt(file);
	}
	
	public File getFile() 
	{
		return file;
	}
	
	public void setFile(File file) 
	{
		this.file = file;
	}
	
	public abstract String getImporter();
	public abstract String getProcessor();
	public abstract String getResourceType();
	public abstract String getResourceTypePrefix();
	
	@Override
	public String toString() 
	{
		return String.format("name='%s' path='%s'", name, file);
	}	

	@Override
	public boolean equals(Object obj) 
	{
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Resource other = (Resource) obj;
		if (!file.equals(other.file))
			return false;
		if (!name.equals(other.name))
			return false;
		return true;
	}

	public void process()
	{
		ContentProjTask.fileSync.addFile(getFile());
		ContentProjTask.projSync.addResource(this);
	}	
}
