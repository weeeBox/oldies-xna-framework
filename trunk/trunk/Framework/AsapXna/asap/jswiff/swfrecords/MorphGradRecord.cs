using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used for defining a pair of control points for morph gradients
     * (see <code>MorphGradient</code> for details). Start and end gradients in a
     * morph must have the same number of control points, therefore these are
     * defined pairwise. A control point definition consists of a ratio (i.e. the
     * position of the control point in the gradient) and a color value.
     *
     * @see MorphGradient
     * @see MorphFillStyle
     * @see com.jswiff.swfrecords.tags.DefineMorphShape
     */
    public class MorphGradRecord
    {
        private short startRatio;
        
        private RGBA startColor;
        
        private short endRatio;
        
        private RGBA endColor;
        
        /** 
         * <p>
         * Creates a new MorphGradRecord instance. Specify ratio and (RGBA) color for
         * each control point.
         * </p>
         * 
         * <p>
         * The ratio is a value between 0 and 255. 0 maps to the left edge of the
         * gradient square for a linear gradient, 255 to the right edge. For radial
         * gradients, 0 maps to the center of the square and 255 to the largest
         * circle fitting inside the square.
         * </p>
         *
         * @param startRatio ratio of control point for start gradient
         * @param startColor color of control point for start gradient
         * @param endRatio ratio of control point for end gradient
         * @param endColor color of control point for end gradient
         */
        public MorphGradRecord(short startRatio ,RGBA startColor ,short endRatio ,RGBA endColor) 
        {
            this.startRatio = startRatio;
            this.startColor = startColor;
            this.endRatio = endRatio;
            this.endColor = endColor;
        }
        
        public MorphGradRecord(InputBitStream stream) /* throws IOException */ 
        {
            startRatio = stream.ReadUI8();
            startColor = new RGBA(stream);
            endRatio = stream.ReadUI8();
            endColor = new RGBA(stream);
        }
        
        public virtual RGBA GetEndColor()
        {
            return endColor;
        }
        
        public virtual short GetEndRatio()
        {
            return endRatio;
        }
        
        public virtual RGBA GetStartColor()
        {
            return startColor;
        }
        
        public virtual short GetStartRatio()
        {
            return startRatio;
        }
    }
}