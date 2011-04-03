using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.util
{
    public class Convert
    {
        public static float Twips2pixels(long twips)
        {
            return twips / 20.0F;
        }
    }
}