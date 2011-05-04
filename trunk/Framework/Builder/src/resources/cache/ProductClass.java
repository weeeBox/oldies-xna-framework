package resources.cache;

import java.io.File;
import java.io.IOException;

import org.apache.tools.ant.BuildException;

public class ProductClass
{
	private Products productsCache;
	private Products products = new Products();
	
	private File productsFile;
	
	public void loadProductsCache()
	{
		productsCache = ProductsIO.load(productsFile);
	}
	
	public void saveProductsCache()
	{
		try
		{
			ProductsIO.write(products, productsFile);
		}
		catch (IOException e)
		{
			e.printStackTrace();
			throw new BuildException(e.getMessage());
		}
	}
	
	protected void addProduct(Product product)
	{
		products.add(product);
	}
	
	protected boolean sourceChanged()
	{
		return productsCache == null || !productsCache.equals(products);
	}

	public File getProductsFile()
	{
		return productsFile;
	}
	
	public File getProductsDir()
	{
		return productsFile.getParentFile();
	}

	public void setProductsFile(File productsFile)
	{
		this.productsFile = productsFile;
	}
}
