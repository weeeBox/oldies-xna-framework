using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;
using System.IO;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * Defines a lossless bitmap character that contains RGB data compressed with
     * ZLIB. The following bitmap formats are supported:
     * 
     * <ul>
     * <li>
     * <i>colormapped images</i>, which use a colormap of up to 256 RGB values
     * accessed through an 8-bit index (<code>FORMAT_8_BIT_COLORMAPPED</code>)
     * </li>
     * <li>
     * <i>direct images</i> with 15 bit RGB color representation
     * (<code>FORMAT_15_BIT_RGB</code>)
     * </li>
     * <li>
     * direct images with 24 bit RGB color representation
     * (<code>FORMAT_24_BIT_RGB</code>)
     * </li>
     * </ul>
     * </p>
     *
     * @since SWF 2
     */
    public class DefineBitsLossless : DefinitionTag
    {
        /** 
         *
         */
        public const short FORMAT_8_BIT_COLORMAPPED = 3;
        
        /** 
         *
         */
        public const short FORMAT_15_BIT_RGB = 4;
        
        /** 
         *
         */
        public const short FORMAT_24_BIT_RGB = 5;
        
        private short format;
        
        private int width;
        
        private int height;
        
        private ZlibBitmapData zlibBitmapData;
        
        /** 
         * Creates a new DefineBitsLossless instance.
         *
         * @param characterId the image's character ID
         * @param format image format (use provided constants)
         * @param width image width
         * @param height image height
         * @param zlibBitmapData image data (ZLIB compressed)
         */
        public DefineBitsLossless(int characterId ,short format ,int width ,int height ,ZlibBitmapData zlibBitmapData) 
        {
            code = TagConstants.DEFINE_BITS_LOSSLESS;
            this.characterId = characterId;
            this.format = format;
            this.width = width;
            this.height = height;
            this.zlibBitmapData = zlibBitmapData;
        }
        
        public DefineBitsLossless() 
        {
        }
        
        public virtual void SetFormat(short format)
        {
            this.format = format;
        }
        
        public virtual short GetFormat()
        {
            return format;
        }
        
        public virtual void SetHeight(int height)
        {
            this.height = height;
        }
        
        public virtual int GetHeight()
        {
            return height;
        }
        
        public virtual void SetWidth(int width)
        {
            this.width = width;
        }
        
        public virtual int GetWidth()
        {
            return width;
        }
        
        public virtual void SetZlibBitmapData(ZlibBitmapData zlibBitmapData)
        {
            this.zlibBitmapData = zlibBitmapData;
        }
        
        public virtual ZlibBitmapData GetZlibBitmapData()
        {
            return zlibBitmapData;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            //InputBitStream inStream = new InputBitStream(data);
            //characterId = inStream.ReadUI16();
            //format = inStream.ReadUI8();
            //width = inStream.ReadUI16();
            //height = inStream.ReadUI16();
            //short colorTableSize = 0;
            //if ((format) == (FORMAT_8_BIT_COLORMAPPED)) 
            //{
            //    colorTableSize = ((short)((inStream.ReadUI8()) + 1));
            //} 
            //int zLength = ((int)((data.Length) - (inStream.GetOffset())));
            //byte[] zData = new byte[zLength];
            //Array.Copy(data, ((int)(inStream.GetOffset())), zData, 0, zLength);
            //InputBitStream zStream = new InputBitStream(zData);
            //zStream.EnableCompression();
            //switch (format)
            //{
            //    case FORMAT_8_BIT_COLORMAPPED:
            //        zlibBitmapData = new ColorMapData(zStream , colorTableSize , width , height);
            //        break;
            //    case FORMAT_15_BIT_RGB:
            //    case FORMAT_24_BIT_RGB:
            //        zlibBitmapData = new BitmapData(zStream , format , width , height);
            //        break;
            //    default:
            //        throw new IOException("Unknown bitmap format!");
            //        break;
            //}
            throw new NotImplementedException();
        }
    }
}