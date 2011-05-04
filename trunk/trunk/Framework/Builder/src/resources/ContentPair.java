package resources;

public class ContentPair
{
	private String importer;
	private String processor;

	public ContentPair(String importer, String processor) 
	{
		this.importer = importer;
		this.processor = processor;
	}

	@Override
	public int hashCode() 
	{
		final int prime = 31;
		int result = 1;
		result = prime * result
				+ ((importer == null) ? 0 : importer.hashCode());
		result = prime * result
				+ ((processor == null) ? 0 : processor.hashCode());
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
		ContentPair other = (ContentPair) obj;
		if (importer == null) 
		{
			if (other.importer != null)
				return false;
		} 
		else if (!importer.equals(other.importer))
			return false;
		if (processor == null) 
		{
			if (other.processor != null)
				return false;
		} 
		else if (!processor.equals(other.processor))
			return false;
		return true;
	}
	
	@Override
	public String toString() 
	{
		return String.format("importer=%s processor=%s", importer, processor);
	}
}