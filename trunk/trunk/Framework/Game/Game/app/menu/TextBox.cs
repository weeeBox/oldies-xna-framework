using System;

using System.Collections.Generic;


using asap.util;
using asap.ui;
using asap.graphics;

namespace flipstones.menu
{
    public class TextBox : View
     {
        private const int MAX_LINES_COUNT = 128;
        
        private int alpha = 255;
        
        private String text;
        
        private int width;
        
        private int height;
        
        private short[] lines;
        
        private int linesCount;
        
        private BaseFont font;
        
        public TextBox(String text) 
         : this(text, AppResManager.GetDefaultFont())
        {
        }
        
        public TextBox(String text ,BaseFont font) 
        {
            System.Diagnostics.Debug.Assert(text != null);
            this.width = font.GetStringWidth(text);
            this.height = font.GetHeight();
            this.font = font;
            lines = new short[(MAX_LINES_COUNT) * 2];
            SetText(text);
        }
        
        public TextBox(String text ,int width ,int height) 
        {
            System.Diagnostics.Debug.Assert(text != null);
            this.width = width;
            this.height = height;
            font = AppResManager.GetDefaultFont();
            lines = new short[(MAX_LINES_COUNT) * 2];
            SetText(text);
        }
        
        public TextBox(String text ,int width) 
        {
            System.Diagnostics.Debug.Assert(text != null);
            this.width = width;
            font = AppResManager.GetDefaultFont();
            lines = new short[(MAX_LINES_COUNT) * 2];
            SetText(text);
            height = (linesCount) * (font.GetLineHeight());
        }
        
        public TextBox(String text ,BitmapFont font ,int width) 
        {
            System.Diagnostics.Debug.Assert(text != null);
            this.width = width;
            this.font = font;
            lines = new short[(MAX_LINES_COUNT) * 2];
            SetText(text);
            height = (linesCount) * (font.GetLineHeight());
        }
        
        public virtual void SetAlpha(int alpha)
        {
            this.alpha = alpha;
        }
        
        public virtual void SetText(String text)
        {
            System.Diagnostics.Debug.Assert(text != null);
            this.text = text;
            FontTextDataProvider textProvider = new FontTextDataProvider(text , font);
            linesCount = StringWrapper.WrapString(width, textProvider, lines);
        }
        
        public virtual void SetFont(BaseFont font)
        {
            this.font = font;
        }
        
        public override int GetWidth()
        {
            return width;
        }
        
        public override int GetHeight()
        {
            return height;
        }
        
        public override void Draw(Graphics g)
        {
            int a = g.GetAlpha();
            g.SetAlpha(alpha);
            int y = 0;
            for (int i = 0; i < (2 * (linesCount)); i += 2) 
            {
                String line = JUtils.Substring(text, lines[i], lines[(i + 1)]);
                int x = ((GetWidth()) - (font.GetStringWidth(line))) / 2;
                font.DrawString(g, line, x, y);
                y += font.GetLineHeight();
            }
            g.SetAlpha(a);
        }
        
    }
    
    
}