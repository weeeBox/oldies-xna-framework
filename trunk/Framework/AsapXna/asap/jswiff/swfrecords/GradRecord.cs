using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class implements a gradient control point. A control point is defined
     * by a ratio (i.e. the position of the control point in the gradient) and a
     * color value. Depending on whether the tag this record is used in supports
     * transparency or not, the color is either an <code>RGBA</code> or an
     * <code>RGB</code> value (e.g. <code>DefineShape3</code> supports
     * transparency).
     *
     * @see Gradient
     * @see com.jswiff.swfrecords.tags.DefineShape3
     */
    public class GradRecord
    {
        private short ratio;
        
        private Color color;
        
        /** 
         * <p>
         * Creates a new GradRecord (i.e. a gradient control point) instance. You
         * have to specify the ratio and the color value of the control point.
         * </p>
         * 
         * <p>
         * The ratio is a value between 0 and 255. 0 maps to the left edge of the
         * gradient square for a linear gradient, 255 to the right edge. For radial
         * gradients, 0 maps to the center of the square and 255 to the largest
         * circle fitting inside the square.
         * </p>
         * 
         * <p>
         * The color is either an <code>RGB</code> or an <code>RGBA</code> instance,
         * depending on whether the tag this record is used in supports transparency
         * or not (e.g. <code>DefineShape3</code> does).
         * </p>
         *
         * @param ratio control point ratio (from [0; 255])
         * @param color the color value of the gradient control point
         *
         * @see com.jswiff.swfrecords.tags.DefineShape3
         */
        public GradRecord(short ratio ,Color color) 
        {
            this.ratio = ratio;
            this.color = color;
        }
        
        public GradRecord(InputBitStream stream ,bool hasAlpha) /* throws IOException */ 
        {
            ratio = stream.ReadUI8();
            if (hasAlpha) 
            {
                color = new RGBA(stream);
            } 
            else 
            {
                color = new RGB(stream);
            }
        }
        
        public virtual Color GetColor()
        {
            return color;
        }
        
        public virtual short GetRatio()
        {
            return ratio;
        }
    }
}