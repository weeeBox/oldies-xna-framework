using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used for the representation of 24-bit pixel data.
     */
    public class Pix24 : BitmapPixelData
    {
        private short red;
        
        private short green;
        
        private short blue;
        
        /** 
         * Creates a new Pix24 instance. Specify red, green and blue values.
         *
         * @param red red value (between 0 and 255)
         * @param green green value (between 0 and 255)
         * @param blue blue value (between 0 and 255)
         */
        public Pix24(short red ,short green ,short blue) 
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
        
        public Pix24(InputBitStream stream) /* throws IOException */ 
        {
            stream.ReadUI8();
            red = stream.ReadUI8();
            green = stream.ReadUI8();
            blue = stream.ReadUI8();
        }
        
        public virtual short GetBlue()
        {
            return blue;
        }
        
        public virtual short GetGreen()
        {
            return green;
        }
        
        public virtual short GetRed()
        {
            return red;
        }
    }
}