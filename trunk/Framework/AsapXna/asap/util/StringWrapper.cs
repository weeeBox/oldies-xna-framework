using System;

using System.Collections.Generic;



namespace java.asap.util
{
    public class StringWrapper
     {
        public static int WrapString(int width, TextDataProvider provider, short[] strIdx)
        {
            int strLen = provider.Length();
            int dataIndex = 0;
            int xc = 0;
            int wordWidth = 0;
            int strStartIndex = 0;
            int wordLastCharIndex = 0;
            int stringWidth = 0;
            int charIndex = 0;
            while (charIndex < strLen) 
            {
                int curCharIndex = charIndex;
                char curChar = provider.GetCharAt(curCharIndex);
                charIndex++;
                if ((curChar == ' ') || (curChar == '\n')) 
                {
                    wordLastCharIndex = curCharIndex;
                    if ((stringWidth == 0) && (wordWidth > 0))
                        wordWidth -= provider.CharDistanceOfCharAt((curCharIndex - 1));
                    
                    stringWidth += wordWidth;
                    wordWidth = 0;
                    xc = charIndex;
                    if (curChar == ' ') 
                    {
                        xc--;
                        wordWidth = (provider.WidthOfCharAt(curCharIndex)) + (provider.CharDistanceOfCharAt(curCharIndex));
                    } 
                } 
                else 
                {
                    wordWidth += (provider.WidthOfCharAt(curCharIndex)) + (provider.CharDistanceOfCharAt(curCharIndex));
                }
                if ((((stringWidth + wordWidth) > width) && (wordLastCharIndex != strStartIndex)) || (curChar == '\n')) 
                {
                    strIdx[dataIndex++] = ((short)(strStartIndex));
                    strIdx[dataIndex++] = ((short)(wordLastCharIndex));
                    while ((xc < (provider.Length())) && ((provider.GetCharAt(xc)) == ' ')) 
                    {
                        wordWidth -= (provider.WidthOfCharAt(xc)) + (provider.CharDistanceOfCharAt(xc));
                        xc++;
                    }
                    wordWidth -= provider.CharDistanceOfCharAt(xc);
                    strStartIndex = xc;
                    wordLastCharIndex = strStartIndex;
                    stringWidth = 0;
                } 
            }
            if (wordWidth != 0) 
            {
                strIdx[dataIndex++] = ((short)(strStartIndex));
                strIdx[dataIndex++] = ((short)(strLen));
            } 
            return dataIndex / 2;
        }
        
    }
    
    
}