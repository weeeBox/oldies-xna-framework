using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * Represents a 24-bit RGB color.
     */
    public class RGB : Color
    {
        /** 
         *
         */
        public static readonly RGB BLACK = new RGB(0 , 0 , 0);
        
        /** 
         *
         */
        public static readonly RGB WHITE = new RGB(255 , 255 , 255);
        
        private short red;
        
        private short green;
        
        private short blue;
        
        /** 
         * Creates a new RGB instance.
         *
         * @param red red value (between 0 and 255)
         * @param green green value (between 0 and 255)
         * @param blue blue value (between 0 and 255)
         */
        public RGB(short red ,short green ,short blue) 
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
        
        /** 
         * Creates a new RGB instance. Convenience constructor which can be used to
         * avoid annoying type casts with literals like<br>
         * new RGB((short) 0, (short) 0, (short) 0);
         *
         * @param red red value (between 0 and 255)
         * @param green green value (between 0 and 255)
         * @param blue blue value (between 0 and 255)
         */
        public RGB(int red ,int green ,int blue) 
        {
            this.red = ((short)(red));
            this.green = ((short)(green));
            this.blue = ((short)(blue));
        }
        
        /** 
         * Reads an instance from a bit stream.
         *
         * @param stream source bit stream
         *
         * @throws IOException if an I/O error has occured
         */
        public RGB(InputBitStream stream) /* throws IOException */ 
        {
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
        
        public override String ToString()
        {
            return ((((("RGB (" + (red)) + "; ") + (green)) + "; ") + (blue)) + ")";
        }
    }
}