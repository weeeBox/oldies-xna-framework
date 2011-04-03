using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * Use only with <code>DefineShape4</code>.
     */
    public class FocalGradient : Gradient
    {
        private double focalPointRatio;
        
        /** 
         * Creates a new FocalGradient instance.
         *
         * @param gradientRecords TODO: Comments
         * @param focalPointRatio TODO: Comments
         */
        public FocalGradient(GradRecord[] gradientRecords ,double focalPointRatio) 
         : base(gradientRecords)
        {
            this.focalPointRatio = focalPointRatio;
        }
        
        public FocalGradient(InputBitStream stream) /* throws IOException */ 
         : base(stream, true)
        {
            focalPointRatio = stream.ReadFP16();
        }
        
        public virtual void SetFocalPointRatio(double focalPointRatio)
        {
            this.focalPointRatio = focalPointRatio;
        }
        
        public virtual double GetFocalPointRatio()
        {
            return focalPointRatio;
        }        
    }
}