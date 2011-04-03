using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * TODO: Comments
     */
    public class GlowFilter : Filter
    {
        private RGBA color;
        
        private double x;
        
        private double y;
        
        private double strength;
        
        private int quality;
        
        private bool inner;
        
        private bool knockout;
        
        /** 
         * Creates a new DropSGlowFilterhadowFilter instance.
         */
        public GlowFilter() 
        {
            InitDefaults();
        }
        
        /** 
         * Creates a new GlowFilter instance.
         *
         * @param stream TODO: Comments
         *
         * @throws IOException TODO: Comments
         */
        public GlowFilter(InputBitStream stream) /* throws IOException */ 
        {
            color = new RGBA(stream);
            x = stream.ReadFP32();
            y = stream.ReadFP32();
            strength = stream.ReadFP16();
            inner = stream.ReadBooleanBit();
            knockout = stream.ReadBooleanBit();
            stream.ReadBooleanBit();
            quality = ((int)(stream.ReadUnsignedBits(5)));
        }
        
        public virtual void SetColor(RGBA color)
        {
            this.color = color;
        }
        
        public virtual RGBA GetColor()
        {
            return color;
        }
        
        public virtual void SetInner(bool inner)
        {
            this.inner = inner;
        }
        
        public virtual bool IsInner()
        {
            return inner;
        }
        
        public virtual void SetKnockout(bool knockout)
        {
            this.knockout = knockout;
        }
        
        public virtual bool IsKnockout()
        {
            return knockout;
        }
        
        public virtual void SetQuality(int quality)
        {
            this.quality = quality;
        }
        
        public virtual int GetQuality()
        {
            return quality;
        }
        
        public virtual void SetStrength(double strength)
        {
            this.strength = strength;
        }
        
        public virtual double GetStrength()
        {
            return strength;
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
        
        private void InitDefaults()
        {
            color = new RGBA(255 , 0 , 0 , 255);
            x = 6;
            y = 6;
            strength = 2;
            quality = 1;
        }
    }
}