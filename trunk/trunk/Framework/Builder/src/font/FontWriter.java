package font;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.List;

import org.dom4j.Document;
import org.dom4j.DocumentHelper;
import org.dom4j.Element;
import org.dom4j.io.OutputFormat;
import org.dom4j.io.XMLWriter;

public class FontWriter 
{
	private Document document;
	private FontInfo info;
	private String texName;
	
	public FontWriter(FontInfo info, String texName)
	{
		document = DocumentHelper.createDocument();
		this.info = info;
		this.texName = texName;
	}
	
	public void write(File output) throws IOException
	{			
		Element root = document.addElement("font");
		root.addAttribute("filename", texName);
		root.addAttribute("internalLeading", Integer.toString(info.getInternalLeading()));
		root.addAttribute("ascender", Integer.toString(info.getAscender()));
		root.addAttribute("descender", Integer.toString(info.getDescender()));
		root.addAttribute("externalLeading", Integer.toString(info.getExternalLeading()));
		root.addAttribute("charOffset", Float.toString(info.getCharOffset()));
		root.addAttribute("spaceWidth", Integer.toString(info.getSpaceWidth()));
		
		List<CharImage>chars = info.getCharsImages();
		for (CharImage c : chars) 
		{
			Element e = root.addElement("char");
			e.addAttribute("value", Character.toString(c.getChar()));			
			e.addAttribute("x", Integer.toString(c.getRealX()));
			e.addAttribute("y", Integer.toString(c.getRealY()));
			e.addAttribute("w", Integer.toString(c.getRealWidth()));
			e.addAttribute("h", Integer.toString(c.getRealHeight()));
			e.addAttribute("ox", Integer.toString(c.getOx()));
			e.addAttribute("oy", Integer.toString(c.getOy()));
		}		
				
		OutputFormat format = OutputFormat.createPrettyPrint();
		XMLWriter writer = new XMLWriter(new FileOutputStream(output), format);
		writer.write(document);
		writer.close();
	}
}
