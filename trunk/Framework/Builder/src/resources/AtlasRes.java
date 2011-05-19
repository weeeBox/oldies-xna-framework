package resources;

import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import javax.imageio.ImageIO;

import org.apache.tools.ant.BuildException;

import product.Product;
import product.ProductNode;

import atlas.Atlas;
import atlas.AtlasImage;
import atlas.AtlasPacker;
import atlas.AtlasWriter;
import atlas.Packable;
import font.CharImage;
import font.FontInfo;
import font.FontInfoReader;
import font.FontWriter;

public class AtlasRes extends ResourceBase 
{
	private static final String IMPORTER = "AtlasImporter";
	private static final String PROCESSOR = "AtlasProcessor";

	static
	{
		ResourceReg.register(IMPORTER, PROCESSOR);
	}
	
	private List<ResourceBase> resources = new ArrayList<ResourceBase>();
	private List<FontInfo> fonts = new ArrayList<FontInfo>();
	
	public AtlasRes(String name, File file)
	{
		super(name, file);
	}
	
	public AtlasRes()
	{
		super();
	}
	
	@Override
	public void process() 
	{		
		preProcess();
		
		if (sourceChanged())
		{	
			addToProduct(resources);
			
			List<Packable> packables = process(resources);
			
			AtlasPacker packer = new AtlasPacker();
			packer.doPacking(packables, AtlasPacker.FAST);
			
			Atlas atlas = new Atlas(getName());
			for (int imageIndex = 0; imageIndex < packables.size(); ++imageIndex)
			{			
				AtlasImage img = (AtlasImage) packables.get(imageIndex);
				atlas.add(img);
			}
			
			try 
			{
				File parentDir = getProductDir();
				File atlasfFile = new File(parentDir, atlas.getName() + ".atlas");
				File textureFile = new File(parentDir, atlas.getTexName() + ".png");
				
				setDestFile(atlasfFile);
				
				AtlasWriter writer = new AtlasWriter();
				writer.write(atlas, atlasfFile, textureFile);
				
				ImageRes textureImage = new ImageRes(atlas.getTexName(), textureFile);
				addChildRes(textureImage);
				
				exportFonts(resources);
				postProcess();
			} 
			catch (IOException e) 
			{
				e.printStackTrace();
			}
			
		}
	}
	
	private void addToProduct(List<ResourceBase> resources)
	{
		Product product = getProduct();
		product.addAttribute("count", resources.size());
		
		for (ResourceBase res : resources)
		{
			ProductNode imageElement = product.addNode("image");
			Product.addFileAttribute(imageElement, res.getSourceFile());
		}
	}

	private void exportFonts(List<ResourceBase> resources) 
	{
		try 
		{
			for (FontInfo info : fonts) 
			{
				exportFont(info);
			}
		} 
		catch (IOException e) 
		{
			e.printStackTrace();
			throw new BuildException(e.getMessage());
		}
	}

	public void exportFont(FontInfo info) throws IOException 
	{
		String fontName = info.getName();
		
		File fontOutput = new File(getProductDir(), fontName + ".bitmapfont");
		FontWriter writer = new FontWriter(info, "tex_" + getName());
		writer.write(fontOutput);
		
		BitmapFontRes pixelFont = new BitmapFontRes(fontName, fontOutput);
		addChildRes(pixelFont);
		
		getPackage().addPixelFont(pixelFont);
	}

	private List<Packable> process(List<ResourceBase> resources) 
	{
		List<Packable> packables = new ArrayList<Packable>();
		for (ResourceBase res : resources) 
		{
			if (res instanceof ImageRes)
			{
				try 
				{
					BufferedImage image = ImageIO.read(res.getSourceFile());
					if (image == null)
						throw new BuildException("Can't read image: " + res.getSourceFile());
					
					AtlasImage atlasImage = new AtlasImage(image);
					packables.add(atlasImage);
				} 
				catch (IOException e) 
				{
					e.printStackTrace();
				}
			}
			else if (res instanceof BitmapFontRes)
			{
				File file = res.getSourceFile();
				try 
				{
					FontInfoReader reader = new FontInfoReader();
					FontInfo info = reader.read(file, res.getName());
					fonts.add(info);
					
					List<CharImage> images = info.getCharsImages();
					for (CharImage charImage : images) 
					{
						packables.add(charImage);
					}					
				} 
				catch (IOException e) 
				{
					e.printStackTrace();
				}
			}
		}
		
		return packables;
	}

	public void addImage(ImageRes res)
	{
		resources.add(res);
	}
	
	public void addFont(BitmapFontRes font)
	{
		resources.add(font);
	}
	
	public List<ResourceBase> resources()
	{
		return resources;
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
		return "ATLAS";
	}

	@Override
	public String getResourceTypePrefix() 
	{
		return "ATLAS_";
	}
}
