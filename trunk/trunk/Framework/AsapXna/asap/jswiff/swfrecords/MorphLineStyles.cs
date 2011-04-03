using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * <p>
     * Implements an array of line styles used in a morph sequence. Line styles are
     * defined pairwise in <code>MorphLineStyle</code> or <code>MorphLineStyle2</code> instances.
     * </p>
     * 
     * <p>
     * <b>WARNING:</b> array index starts at 1, not 0!
     * </p>
     *
     * @see com.jswiff.swfrecords.MorphLineStyle
     * @see com.jswiff.swfrecords.MorphLineStyle2
     * @see com.jswiff.swfrecords.tags.DefineMorphShape
     * @see com.jswiff.swfrecords.tags.DefineMorphShape2
     */
    public class MorphLineStyles
    {
        private List<Object>  styles = new List<Object> ();
        
        /** 
         * Creates a new MorphLineStyles instance.
         */
        public MorphLineStyles() 
        {
        }
        
        /** 
         * Reads a new instance from a bit stream.
         *
         * @param stream source bit stream
         * @param useNewMorphLineStyle TODO: Comments
         *
         * @throws IOException if an I/O error occured
         */
        public MorphLineStyles(InputBitStream stream ,bool useNewMorphLineStyle) /* throws IOException */ 
        {
            int styleCount = stream.ReadUI8();
            if (styleCount == 255) 
            {
                styleCount = stream.ReadUI16();
            } 
            for (int i = 0; i < styleCount; i++) 
            {
                if (useNewMorphLineStyle) 
                {
                    styles.Add(new MorphLineStyle2(stream));
                } 
                else 
                {
                    styles.Add(new MorphLineStyle(stream));
                }
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