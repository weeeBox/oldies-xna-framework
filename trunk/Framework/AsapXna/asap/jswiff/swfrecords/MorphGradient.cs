using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * <p>
     * This class is used for defining gradients used in morph sequences (within
     * <code>MorphFillStyle</code> instances).  Gradients are used for
     * interpolation between at least two colors defined by control points. In a
     * morph sequence, different gradients can be used for filling start and end
     * shapes. However, these gradients must have the same number of control
     * points, therefore these are defined pairwise in
     * <code>MorphGradRecord</code> instances.
     * </p>
     *
     * @see Gradient
     * @see MorphGradRecord
     * @see MorphFillStyle
     */
    public class MorphGradient
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
        
        private MorphGradRecord[] gradientRecords;
        
        private byte spreadMethod;
        
        private byte interpolationMethod;
        
        /** 
         * Creates a new MorphGradient instance. Specify an array of at least two
         * morph gradient records containing pairwise definitions of gradient
         * control points for filling a morph's start and end shapes.
         *
         * @param gradientRecords morph gradient records
         */
        public MorphGradient(MorphGradRecord[] gradientRecords) 
        {
            this.gradientRecords = gradientRecords;
        }
        
        public MorphGradient(InputBitStream stream) /* throws IOException */ 
        {
            spreadMethod = unchecked((byte)(stream.ReadUnsignedBits(2)));
            interpolationMethod = unchecked((byte)(stream.ReadUnsignedBits(2)));
            short count = ((short)(stream.ReadUnsignedBits(4)));
            gradientRecords = new MorphGradRecord[count];
            for (int i = 0; i < count; i++) 
            {
                gradientRecords[i] = new MorphGradRecord(stream);
            }
        }
        
        public virtual MorphGradRecord[] GetGradientRecords()
        {
            return gradientRecords;
        }
        
        public virtual byte GetInterpolationMethod()
        {
            return interpolationMethod;
        }
        
        public virtual void SetInterpolationMethod(byte interpolationMethod)
        {
            this.interpolationMethod = interpolationMethod;
        }
        
        public virtual byte GetSpreadMethod()
        {
            return spreadMethod;
        }
        
        public virtual void SetSpreadMethod(byte spreadMethod)
        {
            this.spreadMethod = spreadMethod;
        }
        
        public virtual void SetGradientRecords(MorphGradRecord[] gradientRecords)
        {
            this.gradientRecords = gradientRecords;
        }
    }
}