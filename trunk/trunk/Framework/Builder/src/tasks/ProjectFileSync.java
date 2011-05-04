package tasks;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import org.apache.tools.ant.BuildException;

import utils.FileUtils;

public class ProjectFileSync 
{
	private List<File> files;
	private List<String> filters;
	
	public ProjectFileSync()
	{
		files = new ArrayList<File>();
		filters = new ArrayList<String>();
	}	
	
	public void addFile(File file)
	{
		if (file == null)
			throw new BuildException("Can't add null file");
		
		files.add(file);
	}
	
	public void addFilter(String filter)
	{
		filters.add(filter);
	}
	
	public void addFilters(List<String> filters)
	{
		this.filters.addAll(filters);
	}
	
	public void sync(File contentDir)
	{		
		String[] filtersArray = new String[filters.size()];
		for (int filterIndex = 0; filterIndex < filtersArray.length; filterIndex++) 
		{
			filtersArray[filterIndex] = filters.get(filterIndex);
		}
		
		FileUtils.deleteFiles(contentDir, filtersArray);
		for (File file : files) 
		{
			FileUtils.copy(file, contentDir);
		}
	}
}
