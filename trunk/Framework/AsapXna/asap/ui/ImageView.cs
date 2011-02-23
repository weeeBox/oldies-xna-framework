using System;

using System.Collections.Generic;


using asap.graphics;

namespace asap.ui
{
    public class ImageView : View
     {
        private Image image;
        
        public ImageView(Image image) 
        {
            this.image = image;
        }
        
        public override int GetHeight()
        {
            return image.GetHeight();
        }
        
        public override int GetWidth()
        {
            return image.GetWidth();
        }
        
        public override void Draw(Graphics g)
        {
            g.DrawImage(image, 0, 0, ((Graphics.LEFT) | (Graphics.TOP)));
        }
        
    }
    
    
}