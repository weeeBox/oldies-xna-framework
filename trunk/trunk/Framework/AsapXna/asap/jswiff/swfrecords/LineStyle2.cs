using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used to define a line style. Contains line width and color.
     */
    public class LineStyle2 : EnhancedStrokeStyle
    {
        private int width;
        
        private byte startCapStyle = EnhancedStrokeStyle.CAPS_ROUND;
        
        private byte endCapStyle = EnhancedStrokeStyle.CAPS_ROUND;
        
        private byte jointStyle = EnhancedStrokeStyle.JOINT_ROUND;
        
        private bool pixelHinting;
        
        private bool close = true;
        
        private byte scaleStroke = EnhancedStrokeStyle.SCALE_BOTH;
        
        private double miterLimit = 3;
        
        private RGBA color = RGBA.BLACK;
        
        private FillStyle fillStyle;
        
        /** 
         * Creates a new line style. Specify the width of the line in twips (1/20
         * px).
         *
         * @param width line width
         */
        public LineStyle2(int width) 
        {
            this.width = width;
        }
        
        public LineStyle2(InputBitStream stream) /* throws IOException */ 
        {
            width = stream.ReadUI16();
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
                fillStyle = new FillStyle(stream , true);
                color = null;
            } 
            else 
            {
                color = new RGBA(stream);
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
        
        public virtual void SetColor(RGBA color)
        {
            this.color = color;
        }
        
        public virtual RGBA GetColor()
        {
            return color;
        }
        
        public virtual void SetEndCapStyle(byte endCapStyle)
        {
            this.endCapStyle = endCapStyle;
        }
        
        public virtual byte GetEndCapStyle()
        {
            return endCapStyle;
        }
        
        public virtual void SetFillStyle(FillStyle fillStyle)
        {
            this.fillStyle = fillStyle;
        }
        
        public virtual FillStyle GetFillStyle()
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
        
        public virtual void SetWidth(int width)
        {
            this.width = width;
        }
        
        public virtual int GetWidth()
        {
            return width;
        }
    }
}