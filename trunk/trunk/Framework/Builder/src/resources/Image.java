package resources;

public class Image extends Resource 
{
	private static final String IMPORTER = "TextureImporter";
	private static final String PROCESSOR = "TextureProcessor";

	static
	{
		registerResource(IMPORTER, PROCESSOR, ".png");
	}
	
	@Override
	public String getImporter() 
	{
		return IMPORTER;
	}

	@Override
	public String getProcessor() 
	{
		return PROCESSOR;
	}

	@Override
	public String getResourceType() 
	{
		return "IMAGE";
	}
	
	@Override
	public String getResourceTypePrefix() 
	{
		return "IMG_";
	}
}
