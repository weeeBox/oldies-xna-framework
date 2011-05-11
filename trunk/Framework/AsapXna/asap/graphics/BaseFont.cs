using System;

using System.Collections.Generic;
using asap.resources;

namespace asap.graphics
{
    public struct FormattedString
    {
        public String text;
        public float width;

        public FormattedString(String text, float width)
        {
            this.text = text;
            this.width = width;
        }
    }

    public abstract class BaseFont : ManagedResource
    {
        public abstract void DrawString(Graphics g, String str, float x, float y);

        public abstract void DrawString(Graphics g, String str, float x, float y, int anchor);
        
        public abstract void DrawChar(Graphics g, char ch, float x, float y);        
        
        public abstract float GetStringWidth(String str);

        public abstract int GetHeight();

        public abstract int GetLineOffset();        

        public abstract int GetCharWidth(char ch);

        public abstract List<FormattedString> WrapString(string text, float width);
    }    
}