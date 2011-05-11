package resources;

import java.io.File;

public class BitmapFontRes extends ResourceBase 
{	
	public BitmapFontRes(String name, File file)
	{
		super(name, file);
	}
	
	public BitmapFontRes()
	{
		super();
	}

	private static final String IMPORTER = "BitmapFontImporter";
	private static final String PROCESSOR = "BitmapFontProcessor";

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