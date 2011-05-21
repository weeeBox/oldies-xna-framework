using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * Like <code>DefineFontInfo</code>, this tag also maps a glyph font (defined
     * with <code>DefineFont</code>) to a device font, providing a font name,
     * style attributes (e.g. bold, italic) and a table of characters matching the
     * glyph shape table contained in the corresponding <code>DefineFont</code>
     * tag, thereby defining a one-to-one mapping between glyphs and characters.
     * </p>
     * 
     * <p>
     * Unlike <code>DefineFontInfo</code>, <code>DefineFontInfo2</code> contains a
     * field for a language code, making text behavior independent on the locale
     * in which Flash Player is running. This field is considered e.g. when
     * determining line breaking rules. Also, the ANSI and ShiftJIS encodings are
     * not available anymore, as Unicode encoding is used.
     * </p>
     * 
     * <p>
     * Note: Consider using <code>DefineFont2</code> instead of the
     * <code>DefineFont</code> - <code>DefineFontInfo2</code> tag pair, as it
     * incorporates the same functionality in a single tag.
     * </p>
     * 
     * <p>
     * Note: despite its name, this tag isn't a definition tag. It doesn't define a
     * new character, it specifies attributes for an existing character.
     * </p>
     *
     * @see DefineFontInfo
     * @see DefineFont
     * @see DefineFont2
     * @since SWF 6
     */
    public class DefineFontInfo2 : Tag
    {
        private int fontId;
        
        private String fontName;
        
        private bool smallText;
        
        private bool italic;
        
        private bool bold;
        
        private LangCode langCode;
        
        private char[] codeTable;
        
        /** 
         * Creates a new DefineFontInfo2 tag.
         *
         * @param fontId character ID from <code>DefineFont</code>
         * @param fontName font name, direct (e.g. 'Times New Roman') or indirect
         * 		  (e.g. '_serif')
         * @param codeTable table of characters matching the glyph shape table of
         * 		  the font
         * @param langCode font language code
         */
        public DefineFontInfo2(int fontId ,String fontName ,char[] codeTable ,LangCode langCode) 
        {
            code = TagConstants.DEFINE_FONT_INFO_2;
            this.fontId = fontId;
            this.fontName = fontName;
            this.codeTable = codeTable;
            this.langCode = langCode;
        }
        
        public DefineFontInfo2() 
        {
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
        
        public virtual void SetLangCode(LangCode langCode)
        {
            this.langCode = langCode;
        }
        
        public virtual LangCode GetLangCode()
        {
            return langCode;
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
            byte[] fontNameBuffer = inStream.ReadBytes(fontNameLen);
            fontName = System.Text.Encoding.UTF8.GetString(fontNameBuffer, 0, fontNameBuffer.Length);
            inStream.ReadUnsignedBits(2);
            smallText = inStream.ReadBooleanBit();
            inStream.ReadBooleanBit();
            inStream.ReadBooleanBit();
            italic = inStream.ReadBooleanBit();
            bold = inStream.ReadBooleanBit();
            langCode = new LangCode(unchecked((byte)(inStream.ReadUI8())));
            int codeTableSize = ((int)(((data.Length) - (inStream.GetOffset())) / 2));
            codeTable = new char[codeTableSize];
            for (int i = 0; i < codeTableSize; i++) 
            {
                codeTable[i] = ((char)(inStream.ReadUI16()));
            }
        }
    }
}