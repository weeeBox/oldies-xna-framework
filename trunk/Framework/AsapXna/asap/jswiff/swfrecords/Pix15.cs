using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used for the representation of 15-bit pixel data.
     */
    public class Pix15 : BitmapPixelData
    {
        private byte red;
        
        private byte green;
        
        private byte blue;
        
        /** 
         * Creates a new Pix15 instance. Specify red, green and blue values.
         *
         * @param red red value (between 0 and 31)
         * @param green green value (between 0 and 31)
         * @param blue blue value (between 0 and 31)
         */
        public Pix15(byte red ,byte green ,byte blue) 
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
        
        public Pix15(InputBitStream stream) /* throws IOException */ 
        {
            stream.ReadUnsignedBits(1);
            red = unchecked((byte)(stream.ReadUnsignedBits(5)));
            green = unchecked((byte)(stream.ReadUnsignedBits(5)));
            blue = unchecked((byte)(stream.ReadUnsignedBits(5)));
        }
        
        public virtual byte GetBlue()
        {
            return blue;
        }
        
        public virtual byte GetGreen()
        {
            return green;
        }
        
        public virtual byte GetRed()
        {
            return red;
        }
    }
}