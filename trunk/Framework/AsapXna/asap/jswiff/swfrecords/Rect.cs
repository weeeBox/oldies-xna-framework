using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used for the representation of a rectangle. A rectangle is
     * defined by a minimum x- and y-coordinate and a maximum x- and y-coordinate
     * position.
     */
    public class Rect
    {
        private long xMin;
        
        private long xMax;
        
        private long yMin;
        
        private long yMax;
        
        /** 
         * Creates a new Rect instance. Four coordinates must be specified.
         *
         * @param xMin minimum x in twips (1/20 px)
         * @param xMax maximum x in twips
         * @param yMin minimum y in twips
         * @param yMax maximum y in twips
         */
        public Rect(long xMin ,long xMax ,long yMin ,long yMax) 
        {
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
        }
        
        public Rect(InputBitStream stream) /* throws IOException */ 
        {
            int nBits = ((int)(stream.ReadUnsignedBits(5)));
            xMin = stream.ReadSignedBits(nBits);
            xMax = stream.ReadSignedBits(nBits);
            yMin = stream.ReadSignedBits(nBits);
            yMax = stream.ReadSignedBits(nBits);
            stream.Align();
        }
        
        public virtual long GetXMax()
        {
            return xMax;
        }
        
        public virtual long GetXMin()
        {
            return xMin;
        }
        
        public virtual long GetYMax()
        {
            return yMax;
        }
        
        public virtual long GetYMin()
        {
            return yMin;
        }
        
        public override String ToString()
        {
            return ((((((("Rect (" + (xMin)) + ", ") + (xMax)) + ", ") + (yMin)) + ", ") + (yMax)) + ")";
        }
    }
}