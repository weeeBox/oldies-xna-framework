using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * TODO: Comments
     */
    public class BevelFilter : Filter
    {
        private RGBA highlightColor;
        
        private RGBA shadowColor;
        
        private double x;
        
        private double y;
        
        private double angle;
        
        private double distance;
        
        private double strength;
        
        private bool inner;
        
        private int quality;
        
        private bool knockout;
        
        private bool onTop;
        
        /** 
         * Creates a new BevelFilter instance.
         */
        public BevelFilter() 
        {
            InitDefaults();
        }
        
        /** 
         * Creates a new BevelFilter instance.
         *
         * @param stream TODO: Comments
         *
         * @throws IOException TODO: Comments
         */
        public BevelFilter(InputBitStream stream) /* throws IOException */ 
        {
            highlightColor = new RGBA(stream);
            shadowColor = new RGBA(stream);
            x = stream.ReadFP32();
            y = stream.ReadFP32();
            angle = stream.ReadFP32();
            distance = stream.ReadFP32();
            strength = stream.ReadFP16();
            inner = stream.ReadBooleanBit();
            knockout = stream.ReadBooleanBit();
            stream.ReadBooleanBit();
            onTop = stream.ReadBooleanBit();
            quality = ((int)(stream.ReadUnsignedBits(4)));
        }
        
        public virtual void SetAngle(double angle)
        {
            this.angle = angle;
        }
        
        public virtual double GetAngle()
        {
            return angle;
        }
        
        public virtual void SetDistance(double distance)
        {
            this.distance = distance;
        }
        
        public virtual double GetDistance()
        {
            return distance;
        }
        
        public virtual void SetHighlightColor(RGBA color)
        {
            this.highlightColor = color;
        }
        
        public virtual RGBA GetHighlightColor()
        {
            return highlightColor;
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
        
        public virtual void SetOnTop(bool onTop)
        {
            this.onTop = onTop;
        }
        
        public virtual bool IsOnTop()
        {
            return onTop;
        }
        
        public virtual void SetQuality(int quality)
        {
            this.quality = quality;
        }
        
        public virtual int GetQuality()
        {
            return quality;
        }
        
        public virtual void SetShadowColor(RGBA color)
        {
            this.shadowColor = color;
        }
        
        public virtual RGBA GetShadowColor()
        {
            return shadowColor;
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
            highlightColor = RGBA.WHITE;
            shadowColor = RGBA.BLACK;
            x = 4;
            y = 4;
            angle = (Math.PI) / 4;
            distance = 4;
            strength = 1;
            quality = 1;
            inner = true;
        }
    }
}