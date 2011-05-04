package resources;

import java.io.File;

public class PixelFontRes extends ResourceBase 
{	
	public PixelFontRes(String name, File file)
	{
		super(name, file);
	}
	
	public PixelFontRes()
	{
		super();
	}

	private static final String IMPORTER = "PixelFontImporter";
	private static final String PROCESSOR = "PixelFontProcessor";

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
		return "BITMAP_FONT";
	}
	
	@Override
	public String getResourceTypePrefix() 
	{
		return "FNT_";
	}
}