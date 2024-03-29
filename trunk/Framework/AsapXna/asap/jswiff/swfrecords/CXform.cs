using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This record defines a simple transform that can be applied to the color
     * space of an object.
     */
    public class CXform
    {
        private int redMultTerm = 256;
        
        private int greenMultTerm = 256;
        
        private int blueMultTerm = 256;
        
        private int redAddTerm = 0;
        
        private int greenAddTerm = 0;
        
        private int blueAddTerm = 0;
        
        private bool hasAddTerms;
        
        private bool hasMultTerms;
        
        /** 
         * Creates a new CXform instance. After creation, use setter methods to set
         * the values of the transform terms.
         */
        public CXform() 
        {
        }
        
        /** 
         * Reads a CXform instance from a bit stream.
         *
         * @param stream the source bit stream
         *
         * @throws IOException if an I/O error has occured
         */
        public CXform(InputBitStream stream) /* throws IOException */ 
        {
            hasAddTerms = stream.ReadBooleanBit();
            hasMultTerms = stream.ReadBooleanBit();
            int nBits = ((int)(stream.ReadUnsignedBits(4)));
            if (hasMultTerms) 
            {
                redMultTerm = ((int)(stream.ReadSignedBits(nBits)));
                greenMultTerm = ((int)(stream.ReadSignedBits(nBits)));
                blueMultTerm = ((int)(stream.ReadSignedBits(nBits)));
            } 
            if (hasAddTerms) 
            {
                redAddTerm = ((int)(stream.ReadSignedBits(nBits)));
                greenAddTerm = ((int)(stream.ReadSignedBits(nBits)));
                blueAddTerm = ((int)(stream.ReadSignedBits(nBits)));
            } 
            stream.Align();
        }
        
        public virtual void SetAddTerms(int redAddTerm, int greenAddTerm, int blueAddTerm)
        {
            this.redAddTerm = redAddTerm;
            this.greenAddTerm = greenAddTerm;
            this.blueAddTerm = blueAddTerm;
            hasAddTerms = true;
        }
        
        public virtual int GetBlueAddTerm()
        {
            return blueAddTerm;
        }
        
        public virtual int GetBlueMultTerm()
        {
            return blueMultTerm;
        }
        
        public virtual int GetGreenAddTerm()
        {
            return greenAddTerm;
        }
        
        public virtual int GetGreenMultTerm()
        {
            return greenMultTerm;
        }
        
        public virtual void SetMultTerms(int redMultTerm, int greenMultTerm, int blueMultTerm)
        {
            this.redMultTerm = redMultTerm;
            this.greenMultTerm = greenMultTerm;
            this.blueMultTerm = blueMultTerm;
            hasMultTerms = true;
        }
        
        public virtual int GetRedAddTerm()
        {
            return redAddTerm;
        }
        
        public virtual int GetRedMultTerm()
        {
            return redMultTerm;
        }
        
        public virtual bool HasAddTerms()
        {
            return hasAddTerms;
        }
        
        public virtual bool HasMultTerms()
        {
            return hasMultTerms;
        }        
    }
}