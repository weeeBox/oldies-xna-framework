package resources;

import java.io.File;

import resources.cache.Product;
import resources.cache.ProductClass;
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
		
		FileUtils.copy(getSourceFile(), getDestFile());
		
		postProcess();
	}

	protected void preProcess()
	{
		File productsDir = new File(getPackage().getProductsDir(), name.toUpperCase());
		productsDir.mkdir();
		
		setProductsFile(new File(productsDir, name + ".xml"));
		loadProductsCache();
		
		addProduct(new Product(name, sourceFile));
		destFile = new File(getProductsDir(), sourceFile.getName());
	}
	
	protected void postProcess()
	{
		if (sourceChanged())
		{			
			addToSync();
			saveProductsCache();
		}
	}
	
	protected void addDependingRes(ResourceBase res)
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
		return FileUtils.getFilenameNoExt(sourceFile);
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

	@Override
	public boolean equals(Object obj) 
	{
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		ResourceBase other = (ResourceBase) obj;
		if (!sourceFile.equals(other.sourceFile))
			return false;
		if (!name.equals(other.name))
			return false;
		return true;
	}
}
