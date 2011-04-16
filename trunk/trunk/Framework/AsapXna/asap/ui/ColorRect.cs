using System;

using System.Collections.Generic;


using asap.graphics;

namespace asap.ui
{
    public class ColorRect : View
     {
        private int width;
        
        private int height;
        
        private int color;
        
        private int alpha = 255;
        
        public ColorRect(int width ,int height ,int color) 
        {
            this.width = width;
            this.height = height;
            this.color = color;
        }
        
        public override int GetWidth()
        {
            return width;
        }
        
        public override int GetHeight()
        {
            return height;
        }
        
        public virtual int GetColor()
        {
            return color;
        }
        
        public virtual void SetColor(int color)
        {
            this.color = color;
        }
        
        public virtual void SetAlpha(int alpha)
        {
            this.alpha = alpha;
        }
        
        public override void Draw(Graphics g)
        {
            g.SetColor(color);
            int prevAlpha = g.GetAlpha();
            g.SetAlpha(alpha);
            g.FillRect(0, 0, GetWidth(), GetHeight());
            g.SetAlpha(prevAlpha);
        }
        
    }
    
    
}