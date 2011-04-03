using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * TODO: Comments
     */
    public class BlurFilter : Filter
    {
        private double x;
        
        private double y;
        
        private int quality;
        
        /** 
         * Creates a new BlurFilter instance.
         *
         * @param x TODO: Comments
         * @param y TODO: Comments
         */
        public BlurFilter(double x ,double y) 
        {
            this.x = x;
            this.y = y;
            quality = 1;
        }
        
        public BlurFilter(InputBitStream stream) /* throws IOException */ 
        {
            x = stream.ReadFP32();
            y = stream.ReadFP32();
            quality = ((int)(stream.ReadUnsignedBits(5)));
            stream.Align();
        }        
        
        public virtual void SetQuality(int quality)
        {
            if ((quality < 0) || (quality > 15)) 
            {
                throw new ArgumentOutOfRangeException("quality must be between 0 and 15");
            } 
            this.quality = quality;
        }
        
        public virtual int GetQuality()
        {
            return quality;
        }
        
        public virtual void SetX(double x)
        {
            this.x = x;
        }
        
        public virtual double GetX()
        {
            return x;
        }
        
        public virtual void SetY(double y)
        {
            this.y = y;
        }
        
        public virtual double GetY()
        {
            return y;
        }
    }
}