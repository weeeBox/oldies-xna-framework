using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * TODO: Comments
     */
    public class GradientGlowFilter : Filter
    {
        private RGBA[] colors;
        
        private short[] ratios;
        
        private double x;
        
        private double y;
        
        private double angle;
        
        private double distance;
        
        private double strength;
        
        private bool inner;
        
        private bool knockout;
        
        private bool onTop;
        
        private int quality;
        
        /** 
         * Creates a new GradientGlowFilter instance.
         *
         * @param colors TODO: Comments
         * @param ratios TODO: Comments
         */
        public GradientGlowFilter(RGBA[] colors ,short[] ratios) 
        {
            SetControlPoints(colors, ratios);
            InitDefaults();
        }
        
        /** 
         * Creates a new GradientGlowFilter instance.
         *
         * @param stream TODO: Comments
         *
         * @throws IOException TODO: Comments
         */
        public GradientGlowFilter(InputBitStream stream) /* throws IOException */ 
        {
            int count = stream.ReadUI8();
            colors = new RGBA[count];
            ratios = new short[count];
            for (int i = 0; i < count; i++) 
            {
                colors[i] = new RGBA(stream);
            }
            for (int i = 0; i < count; i++) 
            {
                ratios[i] = stream.ReadUI8();
            }
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
        
        public virtual void SetControlPoints(RGBA[] colors, short[] ratios)
        {
            if ((colors.Length) != (ratios.Length)) 
            {
                throw new ArgumentOutOfRangeException("colors and ratios arrays must have the same length!");
            } 
            this.colors = colors;
            this.ratios = ratios;
        }
        
        public virtual RGBA[] GetColors()
        {
            return colors;
        }
        
        public virtual void SetDistance(double distance)
        {
            this.distance = distance;
        }
        
        public virtual double GetDistance()
        {
            return distance;
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
        
        public virtual short[] GetRatios()
        {
            return ratios;
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