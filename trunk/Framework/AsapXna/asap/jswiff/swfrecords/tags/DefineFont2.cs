using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag is used to supply font information. Unlike with
     * <code>DefineFont</code>, fonts defined with this tag can be used for
     * dynamic text. Font metrics for improved layout can be supplied. Mapping to
     * device fonts is also possible.
     *
     * @since SWF 3.
     */
    public class DefineFont2 : DefinitionTag
    {
        private bool shiftJIS;
        
        private bool smallText;
        
        private bool ansi;
        
        private bool italic;
        
        private bool bold;
        
        private LangCode languageCode;
        
        private String fontName;
        
        private Shape[] glyphShapeTable;
        
        private char[] codeTable;
        
        private short ascent;
        
        private short descent;
        
        private short leading;
        
        private short[] advanceTable;
        
        private Rect[] boundsTable;
        
        private KerningRecord[] kerningTable;
        
        private int numGlyphs;
        
        private bool hasLayout;
        
        /** 
         * <p>
         * Creates a new DefineFont2 tag. Requires the font's character ID and name,
         * a glyph shape table and a code table.
         * </p>
         * 
         * <p>
         * The shape table contains one shape for each glyph. When using dynamic
         * device text, the shape table can be empty (set to <code>null</code>). In
         * this case, the code table is ignored.
         * </p>
         * 
         * <p>
         * The code table is an array of characters equal in size to the shape table.
         * It assigns a character to each glyph.
         * </p>
         *
         * @param characterId character ID of the font
         * @param fontName font name, either direct, e.g. 'Times New Roman', or
         *        indirect, like '_serif'
         * @param glyphShapeTable array of shapes (for each glyph one)
         * @param codeTable array of chars (for each glyph one)
         *
         * @throws IllegalArgumentException if code table is different from glyph
         *         count
         */
        public DefineFont2(int characterId ,String fontName ,Shape[] glyphShapeTable ,char[] codeTable) 
        {
            code = TagConstants.DEFINE_FONT_2;
            this.characterId = characterId;
            this.fontName = fontName;
            if (glyphShapeTable != null) 
            {
                this.glyphShapeTable = glyphShapeTable;
                numGlyphs = glyphShapeTable.Length;
                if ((codeTable.Length) != (numGlyphs)) 
                {
                    throw new ArgumentOutOfRangeException("Size of codeTable must be equal to glyph count!");
                } 
                this.codeTable = codeTable;
            } 
        }
        
        public DefineFont2() 
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
        
        public virtual short[] GetAdvanceTable()
        {
            return advanceTable;
        }
        
        public virtual short GetAscent()
        {
            return ascent;
        }
        
        public virtual void SetBold(bool bold)
        {
            this.bold = bold;
        }
        
        public virtual bool IsBold()
        {
            return bold;
        }
        
        public virtual Rect[] GetBoundsTable()
        {
            return boundsTable;
        }
        
        public virtual void SetCodeTable(char[] codeTable)
        {
            this.codeTable = codeTable;
        }
        
        public virtual char[] GetCodeTable()
        {
            return codeTable;
        }
        
        public virtual short GetDescent()
        {
            return descent;
        }
        
        public virtual void SetFontName(String fontName)
        {
            this.fontName = fontName;
        }
        
        public virtual String GetFontName()
        {
            return fontName;
        }
        
        public virtual void SetGlyphShapeTable(Shape[] glyphShapeTable)
        {
            this.glyphShapeTable = glyphShapeTable;
        }
        
        public virtual Shape[] GetGlyphShapeTable()
        {
            return glyphShapeTable;
        }
        
        public virtual void SetItalic(bool italic)
        {
            this.italic = italic;
        }
        
        public virtual bool IsItalic()
        {
            return italic;
        }
        
        public virtual KerningRecord[] GetKerningTable()
        {
            return kerningTable;
        }
        
        public virtual void SetLanguageCode(LangCode languageCode)
        {
            this.languageCode = languageCode;
        }
        
        public virtual LangCode GetLanguageCode()
        {
            return languageCode;
        }
        
        public virtual void SetLayout(short ascent, short descent, short leading, short[] advanceTable, Rect[] boundsTable, KerningRecord[] kerningTable)
        {
            hasLayout = true;
            this.ascent = ascent;
            this.descent = descent;
            this.leading = leading;
            this.advanceTable = advanceTable;
            if ((advanceTable == null) || ((advanceTable.Length) != (numGlyphs))) 
            {
                throw new ArgumentOutOfRangeException("Size of advanceTable must be equal to glyph count!");
            } 
            if ((boundsTable != null) && ((boundsTable.Length) != (numGlyphs))) 
            {
                throw new ArgumentOutOfRangeException("Size of boundsTable must be equal to glyph count!");
            } 
            this.boundsTable = boundsTable;
            this.kerningTable = kerningTable;
        }
        
        public virtual short GetLeading()
        {
            return leading;
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
        
        public virtual bool HasLayout()
        {
            return hasLayout;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            hasLayout = inStream.ReadBooleanBit();
            shiftJIS = inStream.ReadBooleanBit();
            smallText = inStream.ReadBooleanBit();
            ansi = inStream.ReadBooleanBit();
            bool wideOffsets = inStream.ReadBooleanBit();
            bool wideCodes = inStream.ReadBooleanBit();
            italic = inStream.ReadBooleanBit();
            bold = inStream.ReadBooleanBit();
            languageCode = new LangCode(unchecked((byte)(inStream.ReadUI8())));
            short fontNameLen = inStream.ReadUI8();
            byte[] fontNameBuffer = inStream.ReadBytes(fontNameLen);
            if ((fontNameLen > 0) && ((fontNameBuffer[(fontNameLen - 1)]) == 0)) 
            {
                fontNameLen--;
            }             
            fontName = System.Text.Encoding.UTF8.GetString(fontNameBuffer, 0, fontNameBuffer.Length);
            numGlyphs = inStream.ReadUI16();
            if (wideOffsets) 
            {
                inStream.ReadBytes(((numGlyphs) * 4));
                inStream.ReadBytes(4);
            } 
            else 
            {
                inStream.ReadBytes(((numGlyphs) * 2));
                inStream.ReadBytes(2);
            }
            if ((numGlyphs) > 0) 
            {
                glyphShapeTable = new Shape[numGlyphs];
                for (int i = 0; i < (numGlyphs); i++) 
                {
                    glyphShapeTable[i] = new Shape(inStream);
                }
                codeTable = new char[numGlyphs];
                if (wideCodes) 
                {
                    for (int i = 0; i < (numGlyphs); i++) 
                    {
                        codeTable[i] = ((char)(inStream.ReadUI16()));
                    }
                } 
                else 
                {
                    for (int i = 0; i < (numGlyphs); i++) 
                    {
                        codeTable[i] = ((char)(inStream.ReadUI8()));
                    }
                }
            } 
            if (hasLayout) 
            {
                ascent = inStream.ReadSI16();
                descent = inStream.ReadSI16();
                leading = inStream.ReadSI16();
                if ((numGlyphs) > 0) 
                {
                    advanceTable = new short[numGlyphs];
                    for (int i = 0; i < (numGlyphs); i++) 
                    {
                        advanceTable[i] = inStream.ReadSI16();
                    }
                    boundsTable = new Rect[numGlyphs];
                    for (int i = 0; i < (numGlyphs); i++) 
                    {
                        boundsTable[i] = new Rect(inStream);
                    }
                } 
                int kerningCount = inStream.ReadUI16();
                if (kerningCount > 0) 
                {
                    kerningTable = new KerningRecord[kerningCount];
                    for (int i = 0; i < kerningCount; i++) 
                    {
                        kerningTable[i] = new KerningRecord(inStream , wideCodes);
                    }
                } 
            } 
        }
    }
}