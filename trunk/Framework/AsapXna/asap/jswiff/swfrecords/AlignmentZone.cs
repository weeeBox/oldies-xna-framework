using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * TODO: Comments
     */
    public class AlignmentZone
    {
        private float left;
        
        private float width;
        
        private float baseline;
        
        private float height;
        
        private bool hasX;
        
        private bool hasY;
        
        /** 
         * Creates a new AlignmentZone instance.
         */
        public AlignmentZone() 
        {
        }
        
        /** 
         * Creates a new AlignmentZone instance.
         *
         * @param stream TODO: Comments
         *
         * @throws IOException TODO: Comments
         */
        public AlignmentZone(InputBitStream stream) /* throws IOException */ 
        {
            stream.ReadUI8();
            left = stream.ReadFloat16();
            width = stream.ReadFloat16();
            baseline = stream.ReadFloat16();
            height = stream.ReadFloat16();
            stream.ReadUnsignedBits(6);
            hasX = stream.ReadBooleanBit();
            hasY = stream.ReadBooleanBit();
        }
        
        public virtual float GetBaseline()
        {
            return baseline;
        }
        
        public virtual float GetHeight()
        {
            return height;
        }
        
        public virtual float GetLeft()
        {
            return left;
        }
        
        public virtual float GetWidth()
        {
            return width;
        }
        
        public virtual void SetX(float left, float width)
        {
            this.left = left;
            this.width = width;
            hasX = true;
        }
        
        public virtual void SetY(float baseline, float height)
        {
            this.baseline = baseline;
            this.height = height;
            hasY = true;
        }
        
        public virtual bool HasX()
        {
            return hasX;
        }
        
        public virtual bool HasY()
        {
            return hasY;
        }        
    }
}