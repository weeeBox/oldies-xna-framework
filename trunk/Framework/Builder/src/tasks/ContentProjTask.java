package tasks;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import org.apache.tools.ant.BuildException;
import org.apache.tools.ant.Task;

import resources.PackageRes;
import resources.ResourceBase;
import resources.ResourceReg;

public class ContentProjTask extends Task
{
	private File projFile;
	private File codeFile;
	private List<PackageRes> packages = new ArrayList<PackageRes>();
	
	public static ProjectFileSync fileSync;
	public static ContentProjSync projSync;
	public static File contentDir;
	public static File resDir;
	
	public ContentProjTask() 
	{
		fileSync = new ProjectFileSync();
		fileSync.addFilters(ResourceReg.getFilters());
		projSync = new ContentProjSync();
	}
	
	@Override
	public void execute() throws BuildException 
	{
		try
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
		catch (Exception e)
		{
			e.printStackTrace();
			throw new BuildException(e.getMessage());
		}
	}

	private void processResources() 
	{		
		for (PackageRes pack : packages) 
		{
			pack.process();
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
	
	public void addPackage(PackageRes pack)
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
