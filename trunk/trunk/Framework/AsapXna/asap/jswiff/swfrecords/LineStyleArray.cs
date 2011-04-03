using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * <p>
     * Implements an array of line styles. If used within <code>DefineShape4</code>,
     * the array contains only <code>LineStyle2</code> instances. Otherwise it
     * contains only <code>LineStyle</code> instances.
     * </p>
     * 
     * <p>
     * <b>WARNING:</b> array index starts with 1, not 0
     * </p>
     * 
     * @see com.jswiff.swfrecords.tags.DefineShape4
     * @see com.jswiff.swfrecords.LineStyle
     * @see com.jswiff.swfrecords.LineStyle2
     */
    public class LineStyleArray
    {
        private List<Object>  styles = new List<Object> ();
        
        /** 
         * Creates a new LineStyleArray instance.
         */
        public LineStyleArray() 
        {
        }
        
        public LineStyleArray(InputBitStream stream ,bool hasAlpha) /* throws IOException */ 
        {
            int styleCount = stream.ReadUI8();
            if (styleCount == 255) 
            {
                styleCount = stream.ReadUI16();
            } 
            for (int i = 0; i < styleCount; i++) 
            {
                styles.Add(new LineStyle(stream , hasAlpha));
            }
        }
        
        public LineStyleArray(InputBitStream stream) /* throws IOException */ 
        {
            int styleCount = stream.ReadUI8();
            if (styleCount == 255) 
            {
                styleCount = stream.ReadUI16();
            } 
            for (int i = 0; i < styleCount; i++) 
            {
                styles.Add(new LineStyle2(stream));
            }
        }
        
        public virtual int GetSize()
        {
            return styles.Count;
        }
        
        public virtual Object GetStyle(int index)
        {
            return styles[(index - 1)];
        }
        
        public virtual List<Object>  GetStyles()
        {
            return styles;
        }
        
        public virtual void AddStyle(Object lineStyle)
        {
            styles.Add(lineStyle);
        }
    }
}