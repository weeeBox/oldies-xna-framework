using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * Gradients are used for interpolation between at least two colors defined by
     * control points. This structure contains the control points of the gradient
     * (see <code>GradRecord</code> for more details on control points).
     *
     * @see GradRecord
     */
    public class Gradient
    {
        /** 
         *
         */
        public const byte SPREAD_PAD = 0;
        
        /** 
         *
         */
        public const byte SPREAD_REFLECT = 1;
        
        /** 
         *
         */
        public const byte SPREAD_REPEAT = 2;
        
        /** 
         *
         */
        public const byte INTERPOLATION_RGB = 0;
        
        /** 
         *
         */
        public const byte INTERPOLATION_LINEAR_RGB = 1;
        
        private GradRecord[] gradientRecords;
        
        private byte spreadMethod;
        
        private byte interpolationMethod;
        
        /** 
         * Creates a new Gradient instance. Supply at least two gradient control
         * points as gradient record array.
         *
         * @param gradientRecords gradient control points
         */
        public Gradient(GradRecord[] gradientRecords) 
        {
            this.gradientRecords = gradientRecords;
        }
        
        public Gradient(InputBitStream stream ,bool hasAlpha) /* throws IOException */ 
        {
            spreadMethod = unchecked((byte)(stream.ReadUnsignedBits(2)));
            interpolationMethod = unchecked((byte)(stream.ReadUnsignedBits(2)));
            short count = ((short)(stream.ReadUnsignedBits(4)));
            gradientRecords = new GradRecord[count];
            for (int i = 0; i < count; i++) 
            {
                gradientRecords[i] = new GradRecord(stream , hasAlpha);
            }
        }
        
        public virtual void SetGradientRecords(GradRecord[] gradientRecords)
        {
            this.gradientRecords = gradientRecords;
        }
        
        public virtual GradRecord[] GetGradientRecords()
        {
            return gradientRecords;
        }
        
        public virtual void SetInterpolationMethod(byte interpolationMethod)
        {
            this.interpolationMethod = interpolationMethod;
        }
        
        public virtual byte GetInterpolationMethod()
        {
            return interpolationMethod;
        }
        
        public virtual void SetSpreadMethod(byte spreadMethod)
        {
            this.spreadMethod = spreadMethod;
        }
        
        public virtual byte GetSpreadMethod()
        {
            return spreadMethod;
        }
    }
}