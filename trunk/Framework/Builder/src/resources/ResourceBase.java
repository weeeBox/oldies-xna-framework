package resources;

import java.io.File;

import org.apache.tools.ant.BuildException;

import product.Product;
import product.ProductClass;
import tasks.ContentProjTask;
import utils.FileUtils;

public abstract class ResourceBase extends ProductClass
{	
	private PackageRes pack;
	
	private String name;
	
	private File sourceFile;
	
	private File destFile;
	
	public ResourceBase()
	{
	}
	
	public ResourceBase(String name, File file)
	{
		setName(name);
		setFile(file);
	}
	
	public void process()
	{
		preProcess();
		postProcess();
	}

	protected void preProcess()
	{
		File productsDir = new File(getPackage().getProductDir(), name.toUpperCase());
		productsDir.mkdir();
		
		setProductFile(productsDir, name);
		
		Product product = new Product(name);
		if (sourceFile != null)
		{
			product.addFileAttribute(sourceFile);
		}
		setProduct(product);
		
		destFile = sourceFile;
	}
	
	protected void postProcess()
	{
		if (sourceChanged())
		{			
			addToSync();
			saveProductsCache();
		}
	}
	
	protected void addChildRes(ResourceBase res)
	{
		res.destFile = res.sourceFile;
		res.addToSync();
	}
	
	protected void addToSync()
	{
		ContentProjTask.fileSync.addFile(getDestFile());
		ContentProjTask.projSync.addResource(this);
	}

	public PackageRes getPackage() 
	{
		return pack;
	}

	public void setPackage(PackageRes parent) 
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
		return FileUtils.getFilenameNoExt(destFile);
	}
	
	public File getSourceFile() 
	{
		return sourceFile;
	}
	
	public File getDestFile()
	{
		return destFile;
	}

	public void setDestFile(File destFile)
	{
		this.destFile = destFile;
	}

	public void setFile(File file) 
	{
		this.sourceFile = file;
	}
	
	public abstract String getImporter();
	public abstract String getProcessor();
	public abstract String getResourceType();
	public abstract String getResourceTypePrefix();
	
	@Override
	public String toString() 
	{
		return String.format("name='%s' path='%s'", name, sourceFile);
	}	
}
