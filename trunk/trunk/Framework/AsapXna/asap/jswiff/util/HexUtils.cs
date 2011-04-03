using System;

using System.Collections.Generic;
using System.Text;

namespace swiff.com.jswiff.util
{
    /** 
     * This class provides methods for working with hexadecimal representations of data.
     */
    public class HexUtils
    {
        private static readonly char[] HEX_DIGITS = new char[]{ '0' , '1' , '2' , '3' , '4' , '5' , '6' , '7' , '8' , '9' , 'A' , 'B' , 'C' , 'D' , 'E' , 'F' };
        
        public static String ToHex(byte[] data)
        {
            return HexUtils.ToHex(data, 0, data.Length);
        }
        
        public static String ToHex(byte[] data, int startPos, int Length)
        {
            StringBuilder b = new StringBuilder();
            int endPos = startPos + Length;
            for (int i = startPos; i < endPos; i++) 
            {
                if (i > 0) 
                {
                    b.Append(' ');
                } 
                int c = data[i];
                b.Append(HEX_DIGITS[((c & 240) >> 4)]);
                b.Append(HEX_DIGITS[((c & 15) >> 0)]);
            }
            return b.ToString();
        }
    }
}