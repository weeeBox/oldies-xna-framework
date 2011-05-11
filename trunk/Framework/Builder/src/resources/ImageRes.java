package resources;

import java.io.File;

public class ImageRes extends ResourceBase 
{
	private boolean dxtCompressed;
	
	public ImageRes(String name, File file)
	{
		super(name, file);
	}
	
	public ImageRes()
	{
		super();
	}

	private static final String IMPORTER = "TextureImporter";
	private static final String PROCESSOR = "TextureProcessor";

	static
	{
		ResourceReg.register(IMPORTER, PROCESSOR);
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

	public boolean isDxtCompressed()
	{
		return dxtCompressed;
	}

	public void setDxtCompressed(boolean dxtCompressed)
	{
		this.dxtCompressed = dxtCompressed;
	}
}
