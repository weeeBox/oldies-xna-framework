using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * <p>
     * Implements an array of fill styles.
     * </p>
     * 
     * <p>
     * <b>WARNING:</b> array index starts at 1, not 0!
     * </p>
     */
    public class FillStyleArray
    {
        private List<FillStyle>  styles = new List<FillStyle> ();
        
        /** 
         * Creates a new FillStyleArray instance.
         */
        public FillStyleArray() 
        {
        }
        
        public FillStyleArray(InputBitStream stream ,bool hasAlpha) /* throws IOException */ 
        {
            int styleCount = stream.ReadUI8();
            if (styleCount == 255) 
            {
                styleCount = stream.ReadUI16();
            } 
            for (int i = 0; i < styleCount; i++) 
            {
                FillStyle fillStyle = new FillStyle(stream , hasAlpha);
                styles.Add(fillStyle);
            }
        }
        
        public virtual int GetSize()
        {
            return styles.Count;
        }
        
        public virtual FillStyle GetStyle(int index)
        {
            return styles[(index - 1)];
        }
        
        public virtual void AddStyle(FillStyle fillStyle)
        {
            styles.Add(fillStyle);
        }        
    }
}