package tasks;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import org.apache.tools.ant.BuildException;
import org.apache.tools.ant.Task;

import resources.ResPackage;
import resources.Resource;

public class ContentProjTask extends Task
{
	private File projFile;
	private File codeFile;
	private List<ResPackage> packages = new ArrayList<ResPackage>();
	
	public static ProjectFileSync fileSync;
	public static ContentProjSync projSync;
	public static File contentDir;
	public static File resDir;
	
	public ContentProjTask() 
	{
		fileSync = new ProjectFileSync();
		fileSync.addFilters(Resource.getFilters());
		projSync = new ContentProjSync();
	}
	
	@Override
	public void execute() throws BuildException 
	{
		if (projFile == null || projFile.isDirectory())
			throw new BuildException("Bad 'projFile': " + projFile);
		if (codeFile == null || codeFile.isDirectory())
			throw new BuildException("Bad 'codeFile': " + codeFile);
		
		contentDir = projFile.getParentFile();
		
		processResources();
		processContentProj();
		generateResourcesCode(codeFile);
	}

	private void processResources() 
	{		
		List<Resource> resources = new ArrayList<Resource>();
		
		for (ResPackage pack : packages) 
		{
			List<Resource> packResources = pack.getResources();
			for (Resource res : packResources) 
			{
				resources.add(res);
			}
		}
		
		for (Resource res : resources)
		{
			res.process();
		}
	}
	
	private void processContentProj() 
	{
		fileSync.sync(contentDir);
		projSync.sync(projFile);
	}

	private void generateResourcesCode(File file) 
	{
		try 
		{
			new CodeFileGenerator().generate(file, packages);
		} 
		catch (Exception e) 
		{
			e.printStackTrace();
			throw new BuildException(e.getMessage());
		}
	}	
	
	public void addPackage(ResPackage pack)
	{
		packages.add(pack);
	}
	
	public void setCodeFile(File codeFile) 
	{
		this.codeFile = codeFile;
	}
	
	public void setProjFile(File projFile) 
	{
		this.projFile = projFile;
	}
	
	public void setResTemp(File resTmp)
	{
		resDir = resTmp;
	}
}
