package product;

import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;

public class ProductNode
{
	private ProductNode parent;
	
	private Map<String, String> attributes;
	private List<ProductNode> nodes;
	private String name;
	
	public static ProductNode createRoot()
	{
		return new ProductNode(null, "product");
	}
	
	public ProductNode(ProductNode parent, String name)
	{
		this.parent = parent;
		this.name = name;
		attributes = new LinkedHashMap<String, String>();
		nodes = new ArrayList<ProductNode>();
	}
	
	public ProductNode addNode(String name)
	{
		ProductNode element = new ProductNode(this, name);
		nodes.add(element);
		return element;
	}
	
	public ProductNode node(String name)
	{
		for (ProductNode element : nodes)
		{
			if (element.getName().equals(name))
				return element;
		}
		
		return null;
	}
	
	protected List<ProductNode> nodes()
	{
		return nodes;
	}
	
	protected Map<String, String> attributes()
	{
		return attributes;
	}
	
	public ProductNode addAttribute(String name, Object value)
	{
		attributes.put(name, value.toString());
		return this;
	}
	
	public int attributeInt(String name)
	{
		String value = attribute(name);
		return value == null ? 0 : Integer.parseInt(value);
	}
	
	public boolean attributeBool(String name)
	{
		String value = attribute(name);
		return value != null ? false : Boolean.parseBoolean(value);
	}
	
	public long attributeLong(String name)
	{
		String value = attribute(name);
		return value != null ? 0L : Long.parseLong(value);
	}
	
	public String attribute(String name)
	{
		return attributes.get(name);
	}

	public String getName()
	{
		return name;
	}

	public ProductNode getParent()
	{
		return parent;
	}

	@Override
	public int hashCode()
	{
		final int prime = 31;
		int result = 1;
		result = prime * result + ((attributes == null) ? 0 : attributes.hashCode());
		result = prime * result + ((name == null) ? 0 : name.hashCode());
		result = prime * result + ((nodes == null) ? 0 : nodes.hashCode());
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
		ProductNode other = (ProductNode) obj;
		if (attributes == null)
		{
			if (other.attributes != null)
				return false;
		}
		else if (!attributes.equals(other.attributes))
			return false;
		if (name == null)
		{
			if (other.name != null)
				return false;
		}
		else if (!name.equals(other.name))
			return false;
		if (nodes == null)
		{
			if (other.nodes != null)
				return false;
		}
		else if (!nodes.equals(other.nodes))
			return false;
		return true;
	}
}
