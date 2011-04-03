using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag contains the JPEG encoding table (the Tables/Misc segment) for all
     * JPEG images defined in the SWF file with the <code>DefineBits</code> tag.
     *
     * @since SWF 1
     */
    public class JPEGTables : Tag
    {
        private byte[] jpegData;
        
        /** 
         * Creates a new JPEGTables instance.
         *
         * @param jpegData JPEG encoding data
         */
        public JPEGTables(byte[] jpegData) 
        {
            code = TagConstants.JPEG_TABLES;
            this.jpegData = jpegData;
        }
        
        public JPEGTables() 
        {
        }
        
        public virtual void SetJpegData(byte[] jpegData)
        {
            this.jpegData = jpegData;
        }
        
        public virtual byte[] GetJpegData()
        {
            return jpegData;
        }
        
        public override void SetData(byte[] data)
        {
            jpegData = data;
        }
    }
}