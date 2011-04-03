using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * Use only with <code>DefineMorphShape2</code>.
     */
    public class FocalMorphGradient : MorphGradient
    {
        private double startFocalPointRatio;
        
        private double endFocalPointRatio;
        
        /** 
         * Creates a new FocalMorphGradient instance.
         *
         * @param gradientRecords TODO: Comments
         * @param startFocalPointRatio TODO: Comments
         * @param endFocalPointRatio TODO: Comments
         */
        public FocalMorphGradient(MorphGradRecord[] gradientRecords ,double startFocalPointRatio ,double endFocalPointRatio) 
         : base(gradientRecords)
        {
            this.startFocalPointRatio = startFocalPointRatio;
            this.endFocalPointRatio = endFocalPointRatio;
        }
        
        public FocalMorphGradient(InputBitStream stream) /* throws IOException */ 
         : base(stream)
        {
            startFocalPointRatio = stream.ReadFP16();
            endFocalPointRatio = stream.ReadFP16();
        }
        
        public virtual void SetEndFocalPointRatio(double endFocalPointRatio)
        {
            this.endFocalPointRatio = endFocalPointRatio;
        }
        
        public virtual double GetEndFocalPointRatio()
        {
            return endFocalPointRatio;
        }
        
        public virtual void SetStartFocalPointRatio(double startFocalPointRatio)
        {
            this.startFocalPointRatio = startFocalPointRatio;
        }
        
        public virtual double GetStartFocalPointRatio()
        {
            return startFocalPointRatio;
        }        
    }
}