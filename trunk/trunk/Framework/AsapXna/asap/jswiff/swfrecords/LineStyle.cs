using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used to define a line style. Contains line width and color.
     */
    public class LineStyle
    {
        private int width;
        
        private ColorRecord color;
        
        /** 
         * Creates a new line style. Specify the width of the line in twips (1/20 px)
         * and the line color, which can be an <code>RGBA</code> or an
         * <code>RGB</code> instance depending on whether the tag the line style is
         * contained in supports transparency or not (<code>DefineShape3</code>
         * supports transparency).
         *
         * @param width line width
         * @param color line color
         *
         * @see com.jswiff.swfrecords.tags.DefineShape3
         */
        public LineStyle(int width ,ColorRecord color) 
        {
            this.width = width;
            this.color = color;
        }
        
        public LineStyle(InputBitStream stream ,bool hasAlpha) /* throws IOException */ 
        {
            width = stream.ReadUI16();
            if (hasAlpha) 
            {
                color = new RGBA(stream);
            } 
            else 
            {
                color = new RGB(stream);
            }
        }
        
        public virtual ColorRecord GetColor()
        {
            return color;
        }
        
        public virtual int GetWidth()
        {
            return width;
        }
    }
}