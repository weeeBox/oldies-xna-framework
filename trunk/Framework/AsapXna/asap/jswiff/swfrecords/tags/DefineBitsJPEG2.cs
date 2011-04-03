using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * Defines a bitmap character with JPEG compression. It contains both the JPEG
     * encoding table and the JPEG image data, allowing multiple JPEG images with
     * differing encoding tables to be defined within a SWF file
     *
     * @since SWF 2
     */
    public class DefineBitsJPEG2 : DefinitionTag
    {
        private byte[] jpegData;
        
        /** 
         * Creates a new DefineBitsJPEG2 tag.
         *
         * @param characterId character ID of the bitmap
         * @param jpegData JPEG data (image and encoding)
         */
        public DefineBitsJPEG2(int characterId ,byte[] jpegData) 
        {
            code = TagConstants.DEFINE_BITS_JPEG_2;
            this.characterId = characterId;
            this.jpegData = jpegData;
        }
        
        public DefineBitsJPEG2() 
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