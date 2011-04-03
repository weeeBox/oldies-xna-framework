using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * Contains XML metadata in Dublin Core RDF format. Do NOT add this tag to your
     * <code>SWFDocument</code>, use its <code>setMetadata</code> method instead!
     */
    public class Metadata : Tag
    {
        private String dataString;
        
        /** 
         * Creates a new Metadata instance.
         *
         * @param dataString metadata as Dublin Core RDF
         */
        public Metadata(String dataString) 
        {
            this.dataString = dataString;
            code = TagConstants.METADATA;
        }
        
        public Metadata() 
        {
        }
        
        public virtual void SetDataString(String dataString)
        {
            this.dataString = dataString;
        }
        
        public virtual String GetDataString()
        {
            return dataString;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            dataString = inStream.ReadString();
        }
    }
}