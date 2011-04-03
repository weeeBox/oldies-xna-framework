using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This class implements a container for tag data which cannot be interpreted
     * because the tag type is unknown (e.g. for new Flash versions).
     */
    public class UnknownTag : Tag
    {
        private byte[] inData;
        
        /** 
         * Creates a new UnknownTag instance.
         *
         * @param code tag code (indicating the tag type)
         * @param data tag data
         */
        public UnknownTag(short code ,byte[] data) 
        {
            this.code = code;
            inData = data;
        }
        
        public UnknownTag() 
        {
        }
        
        public virtual byte[] GetData()
        {
            return inData;
        }
        
        public override String ToString()
        {
            return ((("Unknown tag (tag code: " + (code)) + "; data size: ") + (GetData().Length)) + " bytes)";
        }
        
        public override void SetData(byte[] data)
        {
            inData = data;
        }
    }
}