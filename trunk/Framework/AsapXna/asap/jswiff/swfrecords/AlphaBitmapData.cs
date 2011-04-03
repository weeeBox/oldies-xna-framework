using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used within <code>DefineBitsLossless2</code> tags (with 32-bit
     * RGBA images). It contains an array of pixel colors and transparency
     * information. No scanline padding is needed.
     */
    public class AlphaBitmapData : ZlibBitmapData
    {
        private RGBA[] bitmapPixelData;
        
        /** 
         * Creates a new AlphaBitmapData instance. Supply an RGBA array of size
         * [width x height]. No scanline padding is needed.
         *
         * @param bitmapPixelData RGBA array
         */
        public AlphaBitmapData(RGBA[] bitmapPixelData) 
        {
            this.bitmapPixelData = bitmapPixelData;
        }
        
        /** 
         * Creates a new AlphaBitmapData instance, reading data from a bit stream.
         *
         * @param stream source bit stream
         * @param width image width
         * @param height image height
         *
         * @throws IOException if an I/O error occured
         */
        public AlphaBitmapData(InputBitStream stream ,int width ,int height) /* throws IOException */ 
        {
            int imageDataSize = width * height;
            bitmapPixelData = new RGBA[imageDataSize];
            for (int i = 0; i < imageDataSize; i++) 
            {
                bitmapPixelData[i] = RGBA.ReadARGB(stream);
            }
        }
        
        public virtual RGBA[] GetBitmapPixelData()
        {
            return bitmapPixelData;
        }        
    }
}