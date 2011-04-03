using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * <p>
     * This class is used within <code>DefineBitsLossless</code> tags (with 8-bit
     * colormapped images). It contains a color table and an array of pixel data.
     * The color table contains a palette of up to 256 RGB colors. The pixel data
     * array contains color table indices. Its size is the product of padded image
     * width and image height.
     * </p>
     * 
     * <p>
     * Each line is padded with a scanline pad which makes sure the internal
     * representation starts and ends at a 32-bit boundary. Use
     * <code>getScanlinePadLength()</code> to compute this padding length
     * depending on the width of the image. The computed number of pixels must be
     * added as pad to the end of each image line. The color of the pad pixels is
     * ignored.
     * </p>
     */
    public class ColorMapData : ZlibBitmapData
    {
        private RGB[] colorTableRGB;
        
        private short[] colorMapPixelData;
        
        /** 
         * Creates a new AlphaColorMapData instance. Supply a color table (of up to
         * 256 RGB values) and an array of pixel data of size [paddedWidth x
         * height]. The pixel data consists of color table indices.
         *
         * @param colorTableRGB color table, i.e. an array of up to 256 RGB values
         * @param colorMapPixelData array of color table indices
         */
        public ColorMapData(RGB[] colorTableRGB ,short[] colorMapPixelData) 
        {
            this.colorTableRGB = colorTableRGB;
            this.colorMapPixelData = colorMapPixelData;
        }
        
        /** 
         * Creates a new ColorMapData instance, reading data from a bit stream.
         *
         * @param stream source bit stream
         * @param colorTableSize color of table size (up to 256)
         * @param width image width in pixels (without padding!)
         * @param height image height in pixels
         *
         * @throws IOException if an I/O error occured
         */
        public ColorMapData(InputBitStream stream ,short colorTableSize ,int width ,int height) /* throws IOException */ 
        {
            colorTableRGB = new RGB[colorTableSize];
            for (int i = 0; i < colorTableSize; i++) 
            {
                colorTableRGB[i] = new RGB(stream);
            }
            int imageDataSize = (width + (ColorMapData.GetScanlinePadLength(width))) * height;
            colorMapPixelData = new short[imageDataSize];
            for (int i = 0; i < imageDataSize; i++) 
            {
                colorMapPixelData[i] = stream.ReadUI8();
            }
        }
        
        public static int GetScanlinePadLength(int width)
        {
            int pad = 0;
            if ((width & 3) != 0) 
            {
                pad = 4 - (width & 3);
            } 
            return pad;
        }
        
        public virtual short[] GetColorMapPixelData()
        {
            return colorMapPixelData;
        }
        
        public virtual RGB[] GetColorTableRGB()
        {
            return colorTableRGB;
        }        
    }
}