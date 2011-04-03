using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * Defines a bitmap character with JPEG compression. Unlike
     * <code>DefineBitsJPEG2</code>, this tag adds alpha channel (transparency)
     * support. As transparency is not a standard feature in JPEG images, the
     * alpha channel information is encoded separately from the JPEG data.
     *
     * @since SWF 3
     */
    public class DefineBitsJPEG3 : DefinitionTag
    {
        private byte[] jpegData;
        
        private byte[] bitmapAlphaData;
        
        /** 
         * Creates a new DefineBitsJPEG3 tag.
         *
         * @param characterId character ID of the bitmap
         * @param jpegData JPEG data (image and encoding)
         * @param bitmapAlphaData alpha channel data (ZLIB-compressed)
         */
        public DefineBitsJPEG3(int characterId ,byte[] jpegData ,byte[] bitmapAlphaData) 
        {
            code = TagConstants.DEFINE_BITS_JPEG_3;
            this.characterId = characterId;
            this.jpegData = jpegData;
            this.bitmapAlphaData = bitmapAlphaData;
        }
        
        public DefineBitsJPEG3() 
        {
        }
        
        public virtual void SetBitmapAlphaData(byte[] bitmapAlphaData)
        {
            this.bitmapAlphaData = bitmapAlphaData;
        }
        
        public virtual byte[] GetBitmapAlphaData()
        {
            return bitmapAlphaData;
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
            int alphaDataOffset = ((int)(inStream.ReadUI32()));
            jpegData = new byte[alphaDataOffset];
            Array.Copy(data, 6, jpegData, 0, alphaDataOffset);
            int alphaDataSize = ((data.Length) - 6) - alphaDataOffset;
            bitmapAlphaData = new byte[alphaDataSize];
            Array.Copy(data, (6 + alphaDataOffset), bitmapAlphaData, 0, alphaDataSize);
        }
    }
}