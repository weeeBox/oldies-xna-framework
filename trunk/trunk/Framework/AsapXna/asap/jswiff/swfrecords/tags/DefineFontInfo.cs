using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag maps a glyph font (defined with <code>DefineFont</code>) to a
     * device font, providing a font name, style attributes (e.g. bold, italic)
     * and a table of characters matching the glyph shape table contained in the
     * corresponding <code>DefineFont</code> tag, thereby defining a one-to-one
     * mapping between glyphs and characters.
     * </p>
     * 
     * <p>
     * With this mapping available, you can choose to use the specified device font
     * if available and use the glyph font as fallback by passing the
     * <code>devicefont</code> parameter to the flash player (within the object
     * tag: <code>&lt;param name=&quot;devicefont&quot;
     * value=&quot;true&quot;&gt;</code>). With dynamic text, this parameter is
     * not needed anymore, as this option can be specified within the
     * <code>DefineEditText</code> tag.
     * </p>
     * 
     * <p>
     * Note: Consider using <code>DefineFont2</code> instead of the
     * <code>DefineFont</code> - <code>DefineFontInfo</code> tag pair, as it
     * incorporates the same functionality in a single tag.
     * </p>
     * 
     * <p>
     * Note: despite its name, this tag isn't a definition tag. It doesn't define a
     * new character, it specifies attributes for an existing character.
     * </p>
     *
     * @see DefineFont
     * @see DefineFont2
     * @see DefineEditText
     * @since SWF 1
     */
    public class DefineFontInfo : Tag
    {
        private int fontId;
        
        private String fontName;
        
        private bool smallText;
        
        private bool shiftJIS;
        
        private bool ansi;
        
        private bool italic;
        
        private bool bold;
        
        private char[] codeTable;
        
        /** 
         * Creates a new DefineFontInfo tag.
         *
         * @param fontId character ID from <code>DefineFont</code>
         * @param fontName font name, direct (e.g. 'Times New Roman') or indirect
         * 		  (e.g. '_serif')
         * @param codeTable table of characters matching the glyph shape table of
         * 		  the font
         */
        public DefineFontInfo(int fontId ,String fontName ,char[] codeTable) 
        {
            code = TagConstants.DEFINE_FONT_INFO;
            this.fontId = fontId;
            this.fontName = fontName;
            this.codeTable = codeTable;
        }
        
        public DefineFontInfo() 
        {
        }
        
        public virtual void SetANSI(bool ansi)
        {
            this.ansi = ansi;
            if (ansi) 
            {
                shiftJIS = false;
            } 
        }
        
        public virtual bool IsANSI()
        {
            return ansi;
        }
        
        public virtual void SetBold(bool bold)
        {
            this.bold = bold;
        }
        
        public virtual bool IsBold()
        {
            return bold;
        }
        
        public virtual void SetCodeTable(char[] codeTable)
        {
            this.codeTable = codeTable;
        }
        
        public virtual char[] GetCodeTable()
        {
            return codeTable;
        }
        
        public virtual void SetFontId(int fontId)
        {
            this.fontId = fontId;
        }
        
        public virtual int GetFontId()
        {
            return fontId;
        }
        
        public virtual void SetFontName(String fontName)
        {
            this.fontName = fontName;
        }
        
        public virtual String GetFontName()
        {
            return fontName;
        }
        
        public virtual void SetItalic(bool italic)
        {
            this.italic = italic;
        }
        
        public virtual bool IsItalic()
        {
            return italic;
        }
        
        public virtual void SetShiftJIS(bool shiftJIS)
        {
            this.shiftJIS = shiftJIS;
            if (shiftJIS) 
            {
                ansi = false;
            } 
        }
        
        public virtual bool IsShiftJIS()
        {
            return shiftJIS;
        }
        
        public virtual void SetSmallText(bool smallText)
        {
            this.smallText = smallText;
        }
        
        public virtual bool IsSmallText()
        {
            return smallText;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            fontId = inStream.ReadUI16();
            short fontNameLen = inStream.ReadUI8();
            fontName = System.Text.Encoding.UTF8.GetString(inStream.ReadBytes(fontNameLen)); //new String(inStream.ReadBytes(fontNameLen) , "UTF-8");
            inStream.ReadUnsignedBits(2);
            smallText = inStream.ReadBooleanBit();
            shiftJIS = inStream.ReadBooleanBit();
            ansi = inStream.ReadBooleanBit();
            italic = inStream.ReadBooleanBit();
            bold = inStream.ReadBooleanBit();
            bool wideCodes = inStream.ReadBooleanBit();
            if (wideCodes) 
            {
                int codeTableSize = ((int)(((data.Length) - (inStream.GetOffset())) / 2));
                codeTable = new char[codeTableSize];
                for (int i = 0; i < codeTableSize; i++) 
                {
                    codeTable[i] = ((char)(inStream.ReadUI16()));
                }
            } 
            else 
            {
                int codeTableSize = ((int)((data.Length) - (inStream.GetOffset())));
                codeTable = new char[codeTableSize];
                for (int i = 0; i < codeTableSize; i++) 
                {
                    codeTable[i] = ((char)(inStream.ReadUI8()));
                }
            }
        }
    }
}