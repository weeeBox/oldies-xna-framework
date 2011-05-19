package font;

import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.util.List;

import utils.FileUtils;
import utils.psd.PsdFile;
import utils.psd.struct.res.blocks.PsdGuide;

public class FontInfoReader 
{
	public FontInfo read(File file, String fontName) throws IOException
	{		
		FontProps props = new FontProps(file);
		String charString = props.getString("chars");		
		int spaceWidth = props.getInt("space");
		int charOffset = props.getInt("charOffset");
		int externalLeading = props.getInt("externalLeading");
		
		BufferedImage fontImage;
		int internalLeading, ascender, descender;
		String inputExt = props.getString("file");
		if (inputExt.equalsIgnoreCase("psd"))
		{
			File psdFile = FileUtils.changeExt(file, ".psd");
			PsdFile psd = new PsdFile(psdFile);			
			fontImage = psd.getLayer(0).getImage();
			List<PsdGuide> guides = psd.getGuides();
			if (guides.size() != 2)
			{
				throw new IOException("Wrong font format: guides=" + guides.size());
			}
			
			PsdGuide guide0 = guides.get(0);
			PsdGuide guide1 = guides.get(1);

			if (guide0.isVertical() || guide1.isVertical())
			{
				throw new IOException("Wrong font format: both guildes should be horizontal");
			}
			
			int v0 = (int)Math.min(guide0.getLocation(), guide1.getLocation());
			int v1 = (int)Math.max(guide0.getLocation(), guide1.getLocation());
			
			internalLeading = v0;
			ascender = v1;
			descender = psd.getHeight() - ascender;			
		}
		else
		{
			throw new IOException("Can't read font: " + file);
		}		
		
		FontInfo fontInfo = FontExtractor.extract(fontImage, charString);
		fontInfo.setName(fontName);
		fontInfo.setInternalLeading(internalLeading);
		fontInfo.setAscender(ascender);
		fontInfo.setDescender(descender);
		fontInfo.setExternalLeading(externalLeading);
		fontInfo.setCharOffset(charOffset);
		fontInfo.setSpaceWidth(spaceWidth);		
		
		return fontInfo;		
	}
}