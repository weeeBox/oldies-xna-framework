using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This class represents a SWF tag header.
     */
    public class TagHeader
    {
        private short code;
        
        private int Length;
        
        public TagHeader() 
        {
        }
        
        public TagHeader(InputBitStream stream) /* throws IOException */ 
        {
            Read(stream);
        }
        
        public virtual short GetCode()
        {
            return code;
        }
        
        public virtual int GetLength()
        {
            return Length;
        }
        
        private void Read(InputBitStream stream) /* throws IOException */
        {
            int codeAndLength = stream.ReadUI16();
            code = ((short)(codeAndLength >> 6));
            Length = codeAndLength & 63;
            if ((Length) == 63) 
            {
                Length = ((int)(stream.ReadUI32()));
            } 
        }
    }
}