using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * Defines a bitmap character with JPEG compression. It contains only the JPEG
     * compressed image data. The JPEG encoding data is contained in a
     * <code>JPEGTables</code> tag.
     *
     * @since SWF 1
     */
    public class DefineBits : DefinitionTag
    {
        private byte[] jpegData;
        
        /** 
         * Creates a new DefineBits tag.
         *
         * @param characterId character ID of the bitmap
         * @param jpegData image data
         */
        public DefineBits(int characterId ,byte[] jpegData) 
        {
            code = TagConstants.DEFINE_BITS;
            this.characterId = characterId;
            this.jpegData = jpegData;
        }
        
        public DefineBits() 
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
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            jpegData = new byte[(data.Length) - 2];
            Array.Copy(data, 2, jpegData, 0, jpegData.Length);
        }
    }
}