package product;

import java.io.File;
import java.util.List;


public class Product
{
	private static final String ATTR_NAME = "name";
	private static final String ATTR_FILE = "file";
	private static final String ATTR_LAST_MODIFIED = "lastModified";
	
	private ProductNode productRoot = ProductNode.createRoot();
	
	public Product(String name)
	{
		rootNode().addAttribute(ATTR_NAME, name);
	}
	
	protected ProductNode rootNode()
	{
		return productRoot;
	}
	
	public ProductNode addNode(String name)
	{
		return rootNode().addNode(name);
	}
	
	public ProductNode node(String name)
	{
		return rootNode().node(name);
	}
	
	public List<ProductNode> nodes()
	{
		return rootNode().nodes();
	}
	
	public ProductNode addAttribute(String name, Object value)
	{
		return rootNode().addAttribute(name, value);
	}
	
	public String attribute(String name)
	{
		return rootNode().attribute(name);
	}
	
	public int attributeInt(String name)
	{
		return rootNode().attributeInt(name);
	}
	
	public boolean attributeBool(String name)
	{
		return rootNode().attributeBool(name);
	}
	
	public long attributeLong(String name)
	{
		return rootNode().attributeLong(name);
	}
	
	public String getName()
	{
		return rootNode().attribute(ATTR_NAME);
	}

	public ProductNode addFileAttribute(File file)
	{
		return addFileAttribute(rootNode(), file);
	}
	
	public static ProductNode addFileAttribute(ProductNode node, File file)
	{
		return node.addAttribute(ATTR_FILE, file).addAttribute(ATTR_LAST_MODIFIED, file.lastModified());
	}

	@Override
	public int hashCode()
	{
		final int prime = 31;
		int result = 1;
		result = prime * result + ((productRoot == null) ? 0 : productRoot.hashCode());
		return result;
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
		Product other = (Product) obj;
		if (productRoot == null)
		{
			if (other.productRoot != null)
				return false;
		}
		else if (!productRoot.equals(other.productRoot))
			return false;
		return true;
	}
	
	
}
