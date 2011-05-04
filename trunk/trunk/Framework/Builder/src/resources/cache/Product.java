package resources.cache;

import java.io.File;
import java.util.LinkedHashMap;
import java.util.Map;

public class Product
{
	private Map<String, String> properties = new LinkedHashMap<String, String>();

	private String name;
	private File file;
	private long lastModified;
	
	public Product(String name)
	{
		this(name, null);
	}
	
	public Product(String name, File file)
	{
		this.name = name;
		this.file = file;
	}
	
	public void addProperty(String name, Object value)
	{
		properties.put(name, value.toString());
	}
	
	public String getName()
	{
		return name;
	}

	public File getFile()
	{
		return file;
	}

	public long getModified()
	{
		return lastModified;
	}

	public void setLastModified(long lastModified)
	{
		this.lastModified = lastModified;
	}

	public Map<String, String> getProperties()
	{
		return properties;
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
		if (!name.equals(other.name))
			return false;
		if (file != null && other.file != null)
			if (lastModified != other.lastModified)
				return false;
			
		if (!properties.equals(other.properties))
			return false;
		return true;
	}
	
	
}
