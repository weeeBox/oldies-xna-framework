using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * TODO: Comments
     */
    public class DropShadowFilter : Filter
    {
        private RGBA color;
        
        private double x;
        
        private double y;
        
        private double angle;
        
        private double distance;
        
        private double strength;
        
        private int quality;
        
        private bool inner;
        
        private bool knockout;
        
        private bool hideObject;
        
        /** 
         * Creates a new DropShadowFilter instance.
         */
        public DropShadowFilter() 
        {
            InitDefaults();
        }
        
        /** 
         * Creates a new DropShadowFilter instance.
         *
         * @param stream TODO: Comments
         *
         * @throws IOException TODO: Comments
         */
        public DropShadowFilter(InputBitStream stream) /* throws IOException */ 
        {
            color = new RGBA(stream);
            x = stream.ReadFP32();
            y = stream.ReadFP32();
            angle = stream.ReadFP32();
            distance = stream.ReadFP32();
            strength = stream.ReadFP16();
            inner = stream.ReadBooleanBit();
            knockout = stream.ReadBooleanBit();
            hideObject = !(stream.ReadBooleanBit());
            quality = ((int)(stream.ReadUnsignedBits(5)));
        }
        
        public virtual void SetAngle(double angle)
        {
            this.angle = angle;
        }
        
        public virtual double GetAngle()
        {
            return angle;
        }
        
        public virtual void SetColor(RGBA color)
        {
            this.color = color;
        }
        
        public virtual RGBA GetColor()
        {
            return color;
        }
        
        public virtual void SetDistance(double distance)
        {
            this.distance = distance;
        }
        
        public virtual double GetDistance()
        {
            return distance;
        }
        
        public virtual void SetHideObject(bool hideObject)
        {
            this.hideObject = hideObject;
        }
        
        public virtual bool IsHideObject()
        {
            return hideObject;
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
            color = RGBA.BLACK;
            x = 4;
            y = 4;
            angle = (Math.PI) / 4;
            distance = 4;
            strength = 1;
            quality = 1;
        }
    }
}