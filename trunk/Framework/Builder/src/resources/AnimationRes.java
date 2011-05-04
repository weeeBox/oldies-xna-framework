package resources;

import java.io.File;
import java.io.IOException;

import org.apache.tools.ant.BuildException;

import utils.pack.FileUtils;
import utils.swf.AnimationReader;
import utils.swf.AnimationWriter;
import utils.swf.SwfAnimation;

public class AnimationRes extends ResourceBase
{
	private static final String IMPORTER = "AnimationImporter";
	private static final String PROCESSOR = null;

	static
	{
		ResourceReg.register(IMPORTER, PROCESSOR, ".swp", ".swf", ".png");
	}
	
	public AnimationRes(String name, File file)
	{
		super(name, file);
	}
	
	public AnimationRes()
	{
		super();
	}
	
	@Override
	public void process()
	{
		preProcess();
		
		try
		{		
			String simpleFilename = FileUtils.getFilenameNoExt(getSourceFile());
			
			File output = new File(getProductsDir(), simpleFilename + ".swp");
			setDestFile(output);
			
			AnimationReader exporter = new AnimationReader();
			SwfAnimation animation = exporter.read(getSourceFile());
			
			AnimationWriter writer = new AnimationWriter();
			writer.write(animation, output);
			
			String texname = "tex_" + simpleFilename;
			File textureFile = new File(getProductsDir(), texname + ".png");
			addDependingRes(new ImageRes(texname, textureFile));			

			postProcess();		
			
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
