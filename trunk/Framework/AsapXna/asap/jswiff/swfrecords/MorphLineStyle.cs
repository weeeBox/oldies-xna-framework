using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used to define line styles used in morph sequences. Line
     * styles are defined in pairs: for start and for end shapes.
     *
     * @see MorphLineStyles
     * @see com.jswiff.swfrecords.tags.DefineMorphShape
     */
    public class MorphLineStyle
    {
        private int startWidth;
        
        private int endWidth;
        
        private RGBA startColor;
        
        private RGBA endColor;
        
        /** 
         * Creates a new MorphLineStyle instance. Supply width and RGBA color for
         * lines in start and end shapes.
         *
         * @param startWidth width of line in start shape (in twips = 1/20 px)
         * @param startColor color of line in start shape
         * @param endWidth width of line in start shape (in twips = 1/20 px)
         * @param endColor color of line in end shape
         */
        public MorphLineStyle(int startWidth ,RGBA startColor ,int endWidth ,RGBA endColor) 
        {
            this.startWidth = startWidth;
            this.startColor = startColor;
            this.endWidth = endWidth;
            this.endColor = endColor;
        }
        
        public MorphLineStyle(InputBitStream stream) /* throws IOException */ 
        {
            startWidth = stream.ReadUI16();
            endWidth = stream.ReadUI16();
            startColor = new RGBA(stream);
            endColor = new RGBA(stream);
        }
        
        public virtual RGBA GetEndColor()
        {
            return endColor;
        }
        
        public virtual int GetEndWidth()
        {
            return endWidth;
        }
        
        public virtual RGBA GetStartColor()
        {
            return startColor;
        }
        
        public virtual int GetStartWidth()
        {
            return startWidth;
        }
    }
}