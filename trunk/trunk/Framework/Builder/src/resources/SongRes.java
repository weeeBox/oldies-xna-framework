package resources;

import java.io.File;

public class SongRes extends ResourceBase 
{
	private static final String IMPORTER = "Mp3Importer";
	private static final String PROCESSOR = "SongProcessor";

	static
	{
		ResourceReg.register(IMPORTER, PROCESSOR, ".mp3");
	}
	
	public SongRes(String name, File file)
	{
		super(name, file);
	}
	
	public SongRes()
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
		return "SONG";
	}

	@Override
	public String getResourceTypePrefix() 
	{
		return "SONG_";
	}

}
