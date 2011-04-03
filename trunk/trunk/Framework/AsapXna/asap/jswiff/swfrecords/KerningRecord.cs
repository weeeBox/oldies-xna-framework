using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * A kerning record is used to adjust the distance between two glyphs.
     */
    public class KerningRecord
    {
        private char left;
        
        private char right;
        
        private short adjustment;
        
        /** 
         * Creates a new KerningRecord instance. Specify a character pair and an
         * adjustment to the advance value (i.e. the distance between glyph
         * reference points) of the left character.
         *
         * @param left left character
         * @param right right character
         * @param adjustment adjustment relative to advance value of left character
         *        (in EM square coords)
         */
        public KerningRecord(char left ,char right ,short adjustment) 
        {
            this.left = left;
            this.right = right;
            this.adjustment = adjustment;
        }
        
        /** 
         * Creates a new KerningRecord instance, reading data from a bit stream.
         *
         * @param stream source bit stream
         * @param wideCodes if <code>true</code>, 16 bits are used for character code
         *        representation (instead of 8)
         *
         * @throws IOException if an I/O error occured
         */
        public KerningRecord(InputBitStream stream ,bool wideCodes) /* throws IOException */ 
        {
            if (wideCodes) 
            {
                left = ((char)(stream.ReadUI16()));
                right = ((char)(stream.ReadUI16()));
            } 
            else 
            {
                left = ((char)(stream.ReadUI8()));
                right = ((char)(stream.ReadUI8()));
            }
            adjustment = stream.ReadSI16();
        }
        
        public virtual short GetAdjustment()
        {
            return adjustment;
        }
        
        public virtual char GetLeft()
        {
            return left;
        }
        
        public virtual char GetRight()
        {
            return right;
        }
    }
}