using System;

using System.Collections.Generic;



namespace asap.graphics
{
    /** 
     * Base class for bitmap and native fonts
     */
    abstract public class BaseFont
     {
        public abstract void DrawString(Graphics g, String str, int x, int y);
        
        public abstract void DrawChar(Graphics g, char ch, int x, int y);
        
        public abstract void DrawString(Graphics g, String str, int x, int y, int anchor);
        
        public abstract int GetCapHeight();
        
        public abstract int GetAscent();
        
        public abstract int GetDescent();
        
        public abstract int GetLineHeight();
        
        public abstract int GetStringWidth(String str);
        
        public abstract int GetCharWidth(char ch);
        
        public abstract int GetTracking();
        
        public abstract void SetTracking(int value);
        
        public virtual int GetHeight()
        {
            return (GetDescent()) + (GetAscent());
        }
        
    }
    
    
}