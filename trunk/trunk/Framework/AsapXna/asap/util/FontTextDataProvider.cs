using System;

using System.Collections.Generic;


using asap.graphics;

namespace asap.util
{
    public class FontTextDataProvider : TextDataProvider
     {
        private String str;
        
        private BaseFont font;
        
        public FontTextDataProvider(String str ,BaseFont font) 
        {
            this.str = str;
            this.font = font;
        }
        
        public override int Length()
        {
            return str.Length;
        }
        
        public override char GetCharAt(int index)
        {
            return str[index];
        }
        
        public override int WidthOfCharAt(int index)
        {
            return font.GetCharWidth(str[index]);
        }
        
        public override int CharDistanceOfCharAt(int index)
        {
            return font.GetTracking();
        }
        
    }
    
    
}