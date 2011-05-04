package resources;

import java.io.File;
import java.io.IOException;

import org.apache.tools.ant.BuildException;

import tasks.ContentProjTask;
import utils.pack.FileUtils;
import utils.swf.SwfAnimation;
import utils.swf.AnimationReader;
import utils.swf.AnimationWriter;

public class Animation extends Resource
{
	private static final String IMPORTER = "AnimationImporter";
	private static final String PROCESSOR = null;

	static
	{
		registerResource(IMPORTER, PROCESSOR, ".swp", ".swf", ".png");
	}
	
	@Override
	public void process()
	{
		File input = getFile();
		File output = new File(ContentProjTask.resDir, FileUtils.getFilenameNoExt(input) + ".swp");
		setFile(output);
		
		try
		{
			AnimationReader exporter = new AnimationReader();
			SwfAnimation animation = exporter.read(input);
			
			AnimationWriter writer = new AnimationWriter();
			writer.write(animation, output);
			
			String name = FileUtils.getFilenameNoExt(output);
			String texname = "tex_" + name;
			File textureFile = new File(output.getParent(), texname + ".png");
			Image image = new Image();
			image.setFile(textureFile);
			image.setName(texname);
			image.process();			
			
			ContentProjTask.fileSync.addFile(output);
			ContentProjTask.projSync.addResource(this);
		}
		catch (IOException e)
		{
			e.printStackTrace();
			throw new BuildException(e.getMessage());
		}
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
		return "SWF";
	}

	@Override
	public String getResourceTypePrefix()
	{
		return "ANI_";
	}

}
