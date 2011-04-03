using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * <p>
     * Implements an array of fill styles used in a morphing sequence.
     * </p>
     * 
     * <p>
     * <b>WARNING:</b> array index starts at 1, not 0!
     * </p>
     */
    public class MorphFillStyles
    {
        private List<MorphFillStyle>  styles = new List<MorphFillStyle> ();
        
        /** 
         * Creates a new MorphFillStyles instance.
         */
        public MorphFillStyles() 
        {
        }
        
        /** 
         * Reads a new instance from a bit stream.
         * 
         * @param stream
         *            source bit stream
         * 
         * @throws IOException
         *             if an I/O error occured
         */
        public MorphFillStyles(InputBitStream stream) /* throws IOException */ 
        {
            int styleCount = stream.ReadUI8();
            if (styleCount == 255) 
            {
                styleCount = stream.ReadUI16();
            } 
            for (int i = 0; i < styleCount; i++) 
            {
                styles.Add(new MorphFillStyle(stream));
            }
        }
        
        public virtual int GetSize()
        {
            return styles.Count;
        }
        
        public virtual MorphFillStyle GetStyle(int index)
        {
            return styles[(index - 1)];
        }
        
        public virtual void AddStyle(MorphFillStyle fillStyle)
        {
            styles.Add(fillStyle);
        }
    }
}