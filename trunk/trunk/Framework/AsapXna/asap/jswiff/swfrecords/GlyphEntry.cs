using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class describes a single text character by referencing a glyph from the
     * text font's glyph table.
     */
    public class GlyphEntry
    {
        private int glyphIndex;
        
        private int glyphAdvance;
        
        /** 
         * Creates a new GlyphEntry instance. Specify the index of the glyph in the
         * glyph table of the text font, and the advance value (i.e. the horizontal
         * distance between the reference points of current and subsequent glyph)
         *
         * @param glyphIndex index of glyph in glyph table
         * @param glyphAdvance advance in twips (1/20 px)
         */
        public GlyphEntry(int glyphIndex ,int glyphAdvance) 
        {
            this.glyphIndex = glyphIndex;
            this.glyphAdvance = glyphAdvance;
        }
        
        /** 
         * Creates a new GlyphEntry instance, reading data from a bit stream.
         *
         * @param stream source bit stream
         * @param glyphBits bit count used for glyph index representation
         * @param advanceBits bit count used for advance value representation
         *
         * @throws IOException if an I/O error occured
         */
        public GlyphEntry(InputBitStream stream ,short glyphBits ,short advanceBits) /* throws IOException */ 
        {
            glyphIndex = ((int)(stream.ReadUnsignedBits(glyphBits)));
            glyphAdvance = ((int)(stream.ReadSignedBits(advanceBits)));
        }
        
        public virtual int GetGlyphAdvance()
        {
            return glyphAdvance;
        }
        
        public virtual int GetGlyphIndex()
        {
            return glyphIndex;
        }        
    }
}