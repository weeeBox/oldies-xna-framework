package resources;

import java.io.File;

public class MusicRes extends ResourceBase 
{
	private static final String IMPORTER = "Mp3Importer";
	private static final String PROCESSOR = "SongProcessor";

	static
	{
		ResourceReg.register(IMPORTER, PROCESSOR);
	}
	
	public MusicRes(String name, File file)
	{
		super(name, file);
	}
	
	public MusicRes()
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
		return "MUSIC";
	}

	@Override
	public String getResourceTypePrefix() 
	{
		return "MUSIC_";
	}

}
