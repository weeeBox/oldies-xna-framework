using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used to represent a standard 2D transform matrix (used for
     * affine transforms).
     */
    public class Matrix
    {
        private double scaleX = 1.0;
        
        private double scaleY = 1.0;
        
        private double rotateSkew0 = 0.0;
        
        private double rotateSkew1 = 0.0;
        
        private int translateX = 0;
        
        private int translateY = 0;
        
        private bool hasScale;
        
        private bool hasRotateSkew;
        
        /** 
         * Creates a new Matrix instance. Specify the translate values. If the matrix
         * also has scale or rotate/skew values, use the appropriate setters for
         * setting these values.
         *
         * @param translateX x translate value in twips
         * @param translateY y translate value in twips
         */
        public Matrix(int translateX ,int translateY) 
        {
            this.translateX = translateX;
            this.translateY = translateY;
        }
        
        /** 
         * Reads a Matrix instance from a bit stream.
         *
         * @param stream source bit stream
         *
         * @throws IOException if an I/O error has occured
         */
        public Matrix(InputBitStream stream) /* throws IOException */ 
        {
            hasScale = stream.ReadBooleanBit();
            if (hasScale) 
            {
                int nScaleBits = ((int)(stream.ReadUnsignedBits(5)));
                scaleX = stream.ReadFPBits(nScaleBits);
                scaleY = stream.ReadFPBits(nScaleBits);
            } 
            hasRotateSkew = stream.ReadBooleanBit();
            if (hasRotateSkew) 
            {
                int nRotateBits = ((int)(stream.ReadUnsignedBits(5)));
                rotateSkew0 = stream.ReadFPBits(nRotateBits);
                rotateSkew1 = stream.ReadFPBits(nRotateBits);
            } 
            int nTranslateBits = ((int)(stream.ReadUnsignedBits(5)));
            translateX = ((int)(stream.ReadSignedBits(nTranslateBits)));
            translateY = ((int)(stream.ReadSignedBits(nTranslateBits)));
            stream.Align();
        }
        
        public virtual void SetRotateSkew(double rotateSkew0, double rotateSkew1)
        {
            this.rotateSkew0 = rotateSkew0;
            this.rotateSkew1 = rotateSkew1;
            hasRotateSkew = true;
        }
        
        public virtual double GetRotateSkew0()
        {
            return rotateSkew0;
        }
        
        public virtual double GetRotateSkew1()
        {
            return rotateSkew1;
        }
        
        public virtual void SetScale(double scaleX, double scaleY)
        {
            this.scaleX = scaleX;
            this.scaleY = scaleY;
            hasScale = true;
        }
        
        public virtual double GetScaleX()
        {
            return scaleX;
        }
        
        public virtual double GetScaleY()
        {
            return scaleY;
        }
        
        public virtual int GetTranslateX()
        {
            return translateX;
        }
        
        public virtual int GetTranslateY()
        {
            return translateY;
        }
        
        public virtual bool HasRotateSkew()
        {
            return hasRotateSkew;
        }
        
        public virtual bool HasScale()
        {
            return hasScale;
        }
        
        public override String ToString()
        {
            return (((((((((((("Matrix (" + "scaleX=") + (scaleX)) + " scaleY=") + (scaleY)) + " rotateSkew0=") + (rotateSkew0)) + " rotateSkew1=") + (rotateSkew1)) + " translateX=") + (translateX)) + " translateY=") + (translateY)) + ")";
        }
    }
}