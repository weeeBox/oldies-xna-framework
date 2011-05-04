package resources;

import java.io.File;

public class SoundRes extends ResourceBase 
{
	private static final String IMPORTER = "WavImporter";
	private static final String PROCESSOR = "SoundEffectProcessor";

	static
	{
		ResourceReg.register(IMPORTER, PROCESSOR, ".wav");
	}
	
	public SoundRes(String name, File file)
	{
		super(name, file);
	}
	
	public SoundRes()
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
		return "SOUND";
	}

	@Override
	public String getResourceTypePrefix() 
	{
		return "SND_";
	}

}
