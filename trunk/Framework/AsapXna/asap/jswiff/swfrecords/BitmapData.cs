using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords.tags;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used within <code>DefineBitsLossless</code> tags (with 15-bit
     * or 24-bit RGB images). It contains an array of pixel colors.
     */
    public class BitmapData : ZlibBitmapData
    {
        private BitmapPixelData[] bitmapPixelData;
        
        /** 
         * Creates a new BitmapData instance.
         *
         * @param bitmapPixelData
         */
        public BitmapData(BitmapPixelData[] bitmapPixelData) 
        {
            this.bitmapPixelData = bitmapPixelData;
        }
        
        /** 
         * Creates a new BitmapData instance, reading data from a bit stream.
         *
         * @param stream source bit stream
         * @param bitmapFormat <code>DefineBitsLossless.FORMAT_15_BIT_RGB</code> or
         *        <code>DefineBitsLossless.FORMAT_24_BIT_RGB</code>
         * @param width image width in pixels (without padding!)
         * @param height image height in pixels
         *
         * @throws IOException if an I/O error occured
         */
        public BitmapData(InputBitStream stream ,short bitmapFormat ,int width ,int height) /* throws IOException */ 
        {
            int imageDataSize = 0;
            switch (bitmapFormat)
            {
                case DefineBitsLossless.FORMAT_15_BIT_RGB:
                    imageDataSize = (width + (BitmapData.GetScanlinePadLength(width))) * height;
                    bitmapPixelData = new Pix15[imageDataSize];
                    for (int i = 0; i < imageDataSize; i++) 
                    {
                        bitmapPixelData[i] = new Pix15(stream);
                    }
                    break;
                case DefineBitsLossless.FORMAT_24_BIT_RGB:
                    imageDataSize = width * height;
                    bitmapPixelData = new Pix24[imageDataSize];
                    for (int i = 0; i < imageDataSize; i++) 
                    {
                        bitmapPixelData[i] = new Pix24(stream);
                    }
                    break;
            }
        }
        
        public static int GetScanlinePadLength(int width)
        {
            return width & 1;
        }
        
        public virtual BitmapPixelData[] GetBitmapPixelData()
        {
            return bitmapPixelData;
        }        
    }
}