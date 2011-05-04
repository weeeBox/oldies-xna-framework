package resources;

import java.io.File;

public class VectorFontRes extends ResourceBase 
{	
	private static final String IMPORTER = "FontDescriptionImporter";
	private static final String PROCESSOR = "FontDescriptionProcessor";

	static
	{
		ResourceReg.register(IMPORTER, PROCESSOR, ".vectorfont");
	}
	
	public VectorFontRes(String name, File file)
	{
		super(name, file);
	}
	
	public VectorFontRes()
	{
		super();
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
		return "SYSTEM_FONT";
	}
	
	@Override
	public String getResourceTypePrefix() 
	{
		return "FNT_";
	}
}
