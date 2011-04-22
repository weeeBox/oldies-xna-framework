using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class represents a color as a 32-bit red, green, blue and alpha value.
     * The alpha value represents the transparency (or opacity). 0 means
     * completely transparent, 255 means completely opaque.
     */
    public class RGBA : ColorRecord
    {
        /** 
         *
         */
        public static readonly RGBA BLACK = new RGBA(0 , 0 , 0 , 255);
        
        /** 
         *
         */
        public static readonly RGBA WHITE = new RGBA(255 , 255 , 255 , 255);
        
        private short red;
        
        private short green;
        
        private short blue;
        
        private short alpha;
        
        /** 
         * Creates a new RGBA instance.
         *
         * @param red red value (between 0 and 255)
         * @param green green value (between 0 and 255)
         * @param blue blue value (between 0 and 255)
         * @param alpha alpha value (between 0 and 255)
         */
        public RGBA(short red ,short green ,short blue ,short alpha) 
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }
        
        /** 
         * Creates a new RGBA instance. Convenience constructor which can be used to
         * avoid annoying type casts with literals like<br>
         * new RGBA((short) 0, (short) 0, (short) 0, (short) 255);
         *
         * @param red red value (between 0 and 255)
         * @param green green value (between 0 and 255)
         * @param blue blue value (between 0 and 255)
         * @param alpha alpha value (between 0 and 255)
         */
        public RGBA(int red ,int green ,int blue ,int alpha) 
        {
            this.red = ((short)(red));
            this.green = ((short)(green));
            this.blue = ((short)(blue));
            this.alpha = ((short)(alpha));
        }
        
        /** 
         * Reads an instance from a bit stream.
         *
         * @param stream source bit stream
         *
         * @throws IOException if an I/O error has occured
         */
        public RGBA(InputBitStream stream) /* throws IOException */ 
        {
            red = stream.ReadUI8();
            green = stream.ReadUI8();
            blue = stream.ReadUI8();
            alpha = stream.ReadUI8();
        }
        
        public static RGBA ReadARGB(InputBitStream stream) /* throws IOException */
        {
            int a = stream.ReadUI8();
            int r = stream.ReadUI8();
            int g = stream.ReadUI8();
            int b = stream.ReadUI8();
            return new RGBA(r , g , b , a);
        }
        
        public virtual int GetARGB()
        {
            return (((((alpha) & 255) << 24) | (((red) & 255) << 16)) | (((green) & 255) << 8)) | ((blue) & 255);
        }
        
        public virtual short GetAlpha()
        {
            return alpha;
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
            return ((((((("RGBA (" + (red)) + "; ") + (green)) + "; ") + (blue)) + "; ") + (alpha)) + ")";
        }
    }
}