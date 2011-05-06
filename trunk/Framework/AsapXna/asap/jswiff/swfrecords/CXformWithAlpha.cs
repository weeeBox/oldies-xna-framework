using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using Microsoft.Xna.Framework;
using asap.util;
using System.Diagnostics;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This record defines a simple transform that can be applied to the color
     * space and the alpha channel of an object.
     */
    public class CXformWithAlpha
    {
        private Color4 multTerm = new Color4(1.0f, 1.0f, 1.0f, 1.0f);

        private Color4 addTerm = new Color4(0.0f, 0.0f, 0.0f, 0.0f);        
        
        private bool hasMultTerms;
        
        private bool hasAddTerms;
        
        /** 
         * Creates a new CXformWithAlpha instance. After creation, use setter methods
         * to set the values of the transform terms.
         */
        public CXformWithAlpha() 
        {
        }
        
        /** 
         * Reads a CXform instance from a bit stream.
         *
         * @param stream the source bit stream
         *
         * @throws IOException if an I/O error has occured
         */
        public CXformWithAlpha(InputBitStream stream) /* throws IOException */ 
        {
            hasAddTerms = stream.ReadBooleanBit();
            hasMultTerms = stream.ReadBooleanBit();
            int nBits = ((int)(stream.ReadUnsignedBits(4)));
            if (hasMultTerms) 
            {                
                multTerm.R = stream.ReadSignedBits(nBits) / 255.0f;
                multTerm.G = stream.ReadSignedBits(nBits) / 255.0f;
                multTerm.B = stream.ReadSignedBits(nBits) / 255.0f;
                multTerm.A = stream.ReadSignedBits(nBits) / 255.0f;              
            } 
            if (hasAddTerms) 
            {
                addTerm.R = stream.ReadSignedBits(nBits) / 255.0f;
                addTerm.G = stream.ReadSignedBits(nBits) / 255.0f;
                addTerm.B = stream.ReadSignedBits(nBits) / 255.0f;
                addTerm.A = stream.ReadSignedBits(nBits) / 255.0f;                
            } 
            stream.Align();
        }              
        
        public Color4 GetAddTerm()
        {
            return addTerm;
        }

        public Color4 GetMulTerm()
        {
            return multTerm;
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