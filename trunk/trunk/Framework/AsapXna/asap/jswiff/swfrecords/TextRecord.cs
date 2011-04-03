using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * A TextRecord contains a group of characters which share the same style and
     * are on the same text line. It is used within <code>DefineText</code> and
     * <code>DefineText2</code> tags.
     *
     * @see com.jswiff.swfrecords.tags.DefineText
     * @see com.jswiff.swfrecords.tags.DefineText2
     */
    public class TextRecord
    {
        private bool hasXOffset;
        
        private short xOffset;
        
        private bool hasYOffset;
        
        private short yOffset;
        
        private bool hasFont;
        
        private int fontId;
        
        private int textHeight;
        
        private bool hasColor;
        
        private Color textColor;
        
        private GlyphEntry[] glyphEntries;
        
        /** 
         * Creates a new TextRecord instance. The text contained in this instance is
         * defined by a list of references to entries from the text font's glyph
         * table.
         *
         * @param glyphEntries glyph entries (indexes in glyph table)
         */
        public TextRecord(GlyphEntry[] glyphEntries) 
        {
            this.glyphEntries = glyphEntries;
        }
        
        /** 
         * Reads a new TextRecord instance from a bit stream.
         *
         * @param stream source stream
         * @param glyphBits bit count used for glyph index representation
         * @param advanceBits bit count used for advance value representation
         * @param hasAlpha specifies whether transparency is supported or not
         *
         * @throws IOException if an I/O error has occured
         */
        public TextRecord(InputBitStream stream ,short glyphBits ,short advanceBits ,bool hasAlpha) /* throws IOException */ 
        {
            stream.ReadUnsignedBits(4);
            hasFont = stream.ReadBooleanBit();
            hasColor = stream.ReadBooleanBit();
            hasYOffset = stream.ReadBooleanBit();
            hasXOffset = stream.ReadBooleanBit();
            if (hasFont) 
            {
                fontId = stream.ReadUI16();
            } 
            if (hasColor) 
            {
                textColor = hasAlpha ? ((Color)(new RGBA(stream))) : ((Color)(new RGB(stream)));
            } 
            if (hasXOffset) 
            {
                xOffset = stream.ReadSI16();
            } 
            if (hasYOffset) 
            {
                yOffset = stream.ReadSI16();
            } 
            if (hasFont) 
            {
                textHeight = stream.ReadUI16();
            } 
            int glyphCount = stream.ReadUI8();
            glyphEntries = new GlyphEntry[glyphCount];
            for (int i = 0; i < glyphCount; i++) 
            {
                glyphEntries[i] = new GlyphEntry(stream , glyphBits , advanceBits);
            }
            stream.Align();
        }
        
        public virtual void SetFont(int fontId, int textHeight)
        {
            this.fontId = fontId;
            this.textHeight = textHeight;
            hasFont = true;
        }
        
        public virtual int GetFontId()
        {
            return fontId;
        }
        
        public virtual GlyphEntry[] GetGlyphEntries()
        {
            return glyphEntries;
        }
        
        public virtual void SetTextColor(Color textColor)
        {
            this.textColor = textColor;
            hasColor = true;
        }
        
        public virtual Color GetTextColor()
        {
            return textColor;
        }
        
        public virtual int GetTextHeight()
        {
            return textHeight;
        }
        
        public virtual void SetXOffset(short offset)
        {
            xOffset = offset;
            hasXOffset = true;
        }
        
        public virtual short GetXOffset()
        {
            return xOffset;
        }
        
        public virtual void SetYOffset(short offset)
        {
            yOffset = offset;
            hasYOffset = true;
        }
        
        public virtual short GetYOffset()
        {
            return yOffset;
        }
        
        public virtual bool HasColor()
        {
            return hasColor;
        }
        
        public virtual bool HasFont()
        {
            return hasFont;
        }
        
        public virtual bool HasXOffset()
        {
            return hasXOffset;
        }
        
        public virtual bool HasYOffset()
        {
            return hasYOffset;
        }
    }
}