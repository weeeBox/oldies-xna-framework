using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used to define line styles used in morph sequences. Line
     * styles are defined in pairs: for start and for end shapes.
     *
     * @see MorphLineStyles
     * @see com.jswiff.swfrecords.tags.DefineMorphShape
     * @since SWF 8
     */
    public class MorphLineStyle2 : EnhancedStrokeStyle
    {
        private int startWidth;
        
        private int endWidth;
        
        private byte startCapStyle = EnhancedStrokeStyle.CAPS_ROUND;
        
        private byte endCapStyle = EnhancedStrokeStyle.CAPS_ROUND;
        
        private byte jointStyle = EnhancedStrokeStyle.JOINT_ROUND;
        
        private bool pixelHinting;
        
        private bool close = true;
        
        private byte scaleStroke = EnhancedStrokeStyle.SCALE_BOTH;
        
        private double miterLimit = 3;
        
        private RGBA startColor;
        
        private RGBA endColor;
        
        private MorphFillStyle fillStyle;
        
        /** 
         * Creates a new MorphLineStyle2 instance. Supply width and RGBA color for
         * lines in start and end shapes.
         *
         * @param startWidth width of line in start shape (in twips = 1/20 px)
         * @param startColor color of line in start shape
         * @param endWidth width of line in start shape (in twips = 1/20 px)
         * @param endColor color of line in end shape
         */
        public MorphLineStyle2(int startWidth ,RGBA startColor ,int endWidth ,RGBA endColor) 
        {
            this.startWidth = startWidth;
            this.startColor = startColor;
            this.endWidth = endWidth;
            this.endColor = endColor;
        }
        
        /** 
         * Creates a new MorphLineStyle2 instance. Supply start and end width and
         * fill style.
         *
         * @param startWidth width of line in start shape (in twips = 1/20 px)
         * @param endWidth width of line in start shape (in twips = 1/20 px)
         * @param fillStyle color of line in start shape
         */
        public MorphLineStyle2(int startWidth ,int endWidth ,MorphFillStyle fillStyle) 
        {
            this.startWidth = startWidth;
            this.endWidth = endWidth;
            this.fillStyle = fillStyle;
        }
        
        public MorphLineStyle2(InputBitStream stream) /* throws IOException */ 
        {
            startWidth = stream.ReadUI16();
            endWidth = stream.ReadUI16();
            startCapStyle = unchecked((byte)(stream.ReadUnsignedBits(2)));
            jointStyle = unchecked((byte)(stream.ReadUnsignedBits(2)));
            bool hasFill = stream.ReadBooleanBit();
            bool noHScale = stream.ReadBooleanBit();
            bool noVScale = stream.ReadBooleanBit();
            scaleStroke = unchecked((byte)((noHScale ? 0 : EnhancedStrokeStyle.SCALE_HORIZONTAL) | (noVScale ? 0 : EnhancedStrokeStyle.SCALE_VERTICAL)));
            pixelHinting = stream.ReadBooleanBit();
            stream.ReadUnsignedBits(5);
            close = !(stream.ReadBooleanBit());
            endCapStyle = unchecked((byte)(stream.ReadUnsignedBits(2)));
            if ((jointStyle) == (EnhancedStrokeStyle.JOINT_MITER)) 
            {
                miterLimit = stream.ReadFP16();
            } 
            if (hasFill) 
            {
                fillStyle = new MorphFillStyle(stream);
            } 
            else 
            {
                startColor = new RGBA(stream);
                endColor = new RGBA(stream);
            }
        }
        
        public virtual void SetClose(bool close)
        {
            this.close = close;
        }
        
        public virtual bool IsClose()
        {
            return close;
        }
        
        public virtual void SetEndCapStyle(byte endCapStyle)
        {
            this.endCapStyle = endCapStyle;
        }
        
        public virtual byte GetEndCapStyle()
        {
            return endCapStyle;
        }
        
        public virtual void SetEndColor(RGBA endColor)
        {
            this.endColor = endColor;
            if (endColor != null) 
            {
                fillStyle = null;
            } 
        }
        
        public virtual RGBA GetEndColor()
        {
            return endColor;
        }
        
        public virtual void SetEndWidth(int endWidth)
        {
            this.endWidth = endWidth;
        }
        
        public virtual int GetEndWidth()
        {
            return endWidth;
        }
        
        public virtual void SetFillStyle(MorphFillStyle fillStyle)
        {
            this.fillStyle = fillStyle;
            if (fillStyle != null) 
            {
                startColor = null;
                endColor = null;
            } 
        }
        
        public virtual MorphFillStyle GetFillStyle()
        {
            return fillStyle;
        }
        
        public virtual void SetJointStyle(byte jointStyle)
        {
            this.jointStyle = jointStyle;
        }
        
        public virtual byte GetJointStyle()
        {
            return jointStyle;
        }
        
        public virtual void SetMiterLimit(double miterLimit)
        {
            this.miterLimit = miterLimit;
        }
        
        public virtual double GetMiterLimit()
        {
            return miterLimit;
        }
        
        public virtual void SetPixelHinting(bool pixelHinting)
        {
            this.pixelHinting = pixelHinting;
        }
        
        public virtual bool IsPixelHinting()
        {
            return pixelHinting;
        }
        
        public virtual void SetScaleStroke(byte scaleStroke)
        {
            this.scaleStroke = scaleStroke;
        }
        
        public virtual byte GetScaleStroke()
        {
            return scaleStroke;
        }
        
        public virtual void SetStartCapStyle(byte startCapStyle)
        {
            this.startCapStyle = startCapStyle;
        }
        
        public virtual byte GetStartCapStyle()
        {
            return startCapStyle;
        }
        
        public virtual void SetStartColor(RGBA startColor)
        {
            this.startColor = startColor;
            if (startColor != null) 
            {
                fillStyle = null;
            } 
        }
        
        public virtual RGBA GetStartColor()
        {
            return startColor;
        }
        
        public virtual void SetStartWidth(int startWidth)
        {
            this.startWidth = startWidth;
        }
        
        public virtual int GetStartWidth()
        {
            return startWidth;
        }
    }
}