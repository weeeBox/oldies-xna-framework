using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * Unlike <code>StraightEdgeRecord</code> and <code>CurvedEdgeRecord</code>,
     * this shape record is used for changing the actual style rather than
     * defining new shapes. There are three ways to do this:
     * 
     * <ul>
     * <li>
     * select a previously defined fill or line style
     * </li>
     * <li>
     * move current drawing position to specified coordinates
     * </li>
     * <li>
     * replace current fill and line style arrays with new ones
     * </li>
     * </ul>
     */
    public class StyleChangeRecord : ShapeRecord
    {
        private int moveToX;
        
        private int moveToY;
        
        private int fillStyle0;
        
        private int fillStyle1;
        
        private int lineStyle;
        
        private FillStyleArray fillStyles;
        
        private LineStyleArray lineStyles;
        
        private byte numFillBits;
        
        private byte numLineBits;
        
        private bool hasNewStyles;
        
        private bool hasLineStyle;
        
        private bool hasFillStyle1;
        
        private bool hasFillStyle0;
        
        private bool hasMoveTo;
        
        /** 
         * Creates a new StyleChangeRecord instance.
         */
        public StyleChangeRecord() 
        {
        }
        
        public StyleChangeRecord(InputBitStream stream ,byte flags ,byte numFillBits ,byte numLineBits ,bool useNewLineStyle ,bool hasAlpha) /* throws IOException */ 
        {
            hasNewStyles = (flags & 16) != 0;
            hasLineStyle = (flags & 8) != 0;
            hasFillStyle1 = (flags & 4) != 0;
            hasFillStyle0 = (flags & 2) != 0;
            hasMoveTo = (flags & 1) != 0;
            if (hasMoveTo) 
            {
                int moveBits = ((int)(stream.ReadUnsignedBits(5)));
                moveToX = ((int)(stream.ReadSignedBits(moveBits)));
                moveToY = ((int)(stream.ReadSignedBits(moveBits)));
            } 
            if (hasFillStyle0) 
            {
                fillStyle0 = ((int)(stream.ReadUnsignedBits(numFillBits)));
            } 
            if (hasFillStyle1) 
            {
                fillStyle1 = ((int)(stream.ReadUnsignedBits(numFillBits)));
            } 
            if (hasLineStyle) 
            {
                lineStyle = ((int)(stream.ReadUnsignedBits(numLineBits)));
            } 
            if (hasNewStyles) 
            {
                fillStyles = new FillStyleArray(stream , hasAlpha);
                if (useNewLineStyle) 
                {
                    lineStyles = new LineStyleArray(stream);
                } 
                else 
                {
                    lineStyles = new LineStyleArray(stream , hasAlpha);
                }
                this.numFillBits = unchecked((byte)(stream.ReadUnsignedBits(4)));
                this.numLineBits = unchecked((byte)(stream.ReadUnsignedBits(4)));
            } 
            else 
            {
                this.numFillBits = numFillBits;
                this.numLineBits = numLineBits;
            }
        }
        
        public virtual void SetFillStyle0(int fillStyle0)
        {
            this.fillStyle0 = fillStyle0;
            hasFillStyle0 = true;
        }
        
        public virtual int GetFillStyle0()
        {
            return fillStyle0;
        }
        
        public virtual void SetFillStyle1(int fillStyle1)
        {
            this.fillStyle1 = fillStyle1;
            hasFillStyle1 = true;
        }
        
        public virtual int GetFillStyle1()
        {
            return fillStyle1;
        }
        
        public virtual void SetLineStyle(int lineStyle)
        {
            this.lineStyle = lineStyle;
            hasLineStyle = true;
        }
        
        public virtual int GetLineStyle()
        {
            return lineStyle;
        }
        
        public virtual void SetMoveTo(int x, int y)
        {
            moveToX = x;
            moveToY = y;
            hasMoveTo = true;
        }
        
        public virtual int GetMoveToX()
        {
            return moveToX;
        }
        
        public virtual int GetMoveToY()
        {
            return moveToY;
        }
        
        public virtual FillStyleArray GetNewFillStyles()
        {
            return fillStyles;
        }
        
        public virtual LineStyleArray GetNewLineStyles()
        {
            return lineStyles;
        }
        
        public virtual void SetNewStyles(LineStyleArray lineStyles, FillStyleArray fillStyles)
        {
            this.lineStyles = lineStyles;
            this.fillStyles = fillStyles;
            hasNewStyles = true;
        }
        
        public virtual bool HasFillStyle0()
        {
            return hasFillStyle0;
        }
        
        public virtual bool HasFillStyle1()
        {
            return hasFillStyle1;
        }
        
        public virtual bool HasLineStyle()
        {
            return hasLineStyle;
        }
        
        public virtual bool HasMoveTo()
        {
            return hasMoveTo;
        }
        
        public virtual bool HasNewStyles()
        {
            return hasNewStyles;
        }
        
        public virtual void SetNumFillBits(byte numFillBits)
        {
            this.numFillBits = numFillBits;
        }
        
        public virtual byte GetNumFillBits()
        {
            return numFillBits;
        }
        
        public virtual void SetNumLineBits(byte numLineBits)
        {
            this.numLineBits = numLineBits;
        }
        
        public virtual byte GetNumLineBits()
        {
            return numLineBits;
        }
    }
}