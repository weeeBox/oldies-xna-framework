package product;

import java.io.File;
import java.io.IOException;

import org.apache.tools.ant.BuildException;

public class ProductClass
{
	public static final String PRODUCT_PREFIX = "product-";
	public static final String PRODUCT_EXT = ".xml";
	
	private Product product;
	private Product productCache;
	
	private boolean valid;
	
	private File productFile;
	
	public void loadProductsCache()
	{
		// productCache = ProductIO.load(getProductFile());
	}
	
	public void saveProductsCache()
	{
		try
		{
			ProductIO.write(getProduct(), getProductFile());
		}
		catch (IOException e)
		{
			e.printStackTrace();
			throw new BuildException(e.getMessage());
		}
	}
	
	public void markValid()
	{
		valid = true;
	}
	
	public boolean isValid()
	{
		return valid;
	}
	
	protected void setProduct(Product product)
	{
		this.product = product;
	}

	protected Product getProduct()
	{
		return product;
	}

	public File getProductFile()
	{
		return productFile;
	}

	public void setProductFile(File productDir, String name)
	{
		productFile = new File(productDir, makeProductName(name));
	}
	
	public File getProductDir()
	{
		return productFile.getParentFile();
	}

	protected boolean sourceChanged()
	{
		return productCache == null || !product.equals(productCache);
	}
	
	protected static String makeProductName(String name)
	{
		return PRODUCT_PREFIX + name.toUpperCase() + PRODUCT_EXT;
	}
}
