using System;

using System.Collections.Generic;


using asap.graphics;

namespace app.menu
{
    public class NavigButton : Button
     {
        public NavigButton(ButtonType type ,MenuListener listener) 
         : base(type, (type == (ButtonType.NAVIG_LEFT) ? ButtonId.NAVIG_BUTTON_LEFT : ButtonId.NAVIG_BUTTON_RIGHT), listener)
        {
            System.Diagnostics.Debug.Assert((type == (ButtonType.NAVIG_LEFT)) || (type == (ButtonType.NAVIG_RIGHT)));
            Image image = GetImage("arrow_passive.png");
            Init(null, "arrow_passive.png", image.GetWidth(), image.GetHeight());
        }
        
        public override void Draw(Graphics g)
        {
            Image image = IsPressedState() ? GetImage("arrow_active.png") : this.image;
            int transform;
            if ((_getType()) == (ButtonType.NAVIG_LEFT))
                transform = Graphics.TRANS_NONE;
            
            else
                transform = Graphics.TRANS_ROT180;
            
            g.DrawRegion(image, 0, 0, image.GetWidth(), image.GetHeight(), transform, 0, 0, ((Graphics.LEFT) | (Graphics.TOP)));
        }
        
    }
    
    
}