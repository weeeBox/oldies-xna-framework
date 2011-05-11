package font;

import java.util.ArrayList;
import java.util.List;

public class FontInfo 
{
	private String name;
	private int internalLeading;
	private int ascender;
	private int descender;
	private int externalLeading;
	private float charOffset;
	private int spaceWidth;
	private List<CharImage> charsImages;
	
	public FontInfo()
	{
		charsImages = new ArrayList<CharImage>();
	}
	
	public String getName()
	{
		return name;
	}

	public void setName(String name)
	{
		this.name = name;
	}

	public void addCharImage(CharImage c)
	{		
		charsImages.add(c);
	}
	
	public List<CharImage> getCharsImages()
	{
		return charsImages;
	}
	
	public int getInternalLeading()
	{
		return internalLeading;
	}

	public void setInternalLeading(int internalLeading)
	{
		this.internalLeading = internalLeading;
	}

	public int getAscender()
	{
		return ascender;
	}

	public void setAscender(int ascender)
	{
		this.ascender = ascender;
	}

	public int getDescender()
	{
		return descender;
	}

	public void setDescender(int descender)
	{
		this.descender = descender;
	}
	
	public int getExternalLeading()
	{
		return externalLeading;
	}

	public void setExternalLeading(int externalLeading)
	{
		this.externalLeading = externalLeading;
	}

	public float getCharOffset()
	{
		return charOffset;
	}

	public void setCharOffset(float charOffset)
	{
		this.charOffset = charOffset;
	}

	public int getSpaceWidth()
	{
		return spaceWidth;
	}

	public void setSpaceWidth(int spaceWidth)
	{
		this.spaceWidth = spaceWidth;
	}
}
