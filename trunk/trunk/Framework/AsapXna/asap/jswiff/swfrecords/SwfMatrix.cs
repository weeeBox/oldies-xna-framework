using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used to represent a standard 2D transform matrix (used for
     * affine transforms).
     */
    public class SwfMatrix
    {
        private double scaleX = 1.0;
        
        private double scaleY = 1.0;
        
        private double rotateSkew0 = 0.0;
        
        private double rotateSkew1 = 0.0;
        
        private int translateX = 0;
        
        private int translateY = 0;
        
        /** 
         * Creates a new Matrix instance. Specify the translate values. If the matrix
         * also has scale or rotate/skew values, use the appropriate setters for
         * setting these values.
         *
         * @param translateX x translate value in twips
         * @param translateY y translate value in twips
         */
        public SwfMatrix(int translateX ,int translateY) 
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
        public SwfMatrix(InputBitStream stream) /* throws IOException */ 
        {
            bool hasScale = stream.ReadBooleanBit();
            if (hasScale) 
            {
                int nScaleBits = ((int)(stream.ReadUnsignedBits(5)));
                scaleX = stream.ReadFPBits(nScaleBits);
                scaleY = stream.ReadFPBits(nScaleBits);
            } 
            bool hasRotateSkew = stream.ReadBooleanBit();
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
        
        public double GetRotateSkew0()
        {
            return rotateSkew0;
        }
        
        public double GetRotateSkew1()
        {
            return rotateSkew1;
        }
        
        public double GetScaleX()
        {
            return scaleX;
        }
        
        public double GetScaleY()
        {
            return scaleY;
        }
        
        public int GetTranslateX()
        {
            return translateX;
        }
        
        public int GetTranslateY()
        {
            return translateY;
        }
        
        public override String ToString()
        {
            return (((((((((((("Matrix (" + "scaleX=") + (scaleX)) + " scaleY=") + (scaleY)) + " rotateSkew0=") + (rotateSkew0)) + " rotateSkew1=") + (rotateSkew1)) + " translateX=") + (translateX)) + " translateY=") + (translateY)) + ")";
        }
    }
}