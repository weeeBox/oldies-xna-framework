using System;

using System.Collections.Generic;


using asap.graphics;

namespace flipstones.menu
{
    public class PauseButton : Button
     {
        public PauseButton(int code ,MenuListener listener) 
         : base(ButtonType.PAUSE, code, listener)
        {
            Init(null, "pause_button_off.png", 38, 38);
        }
        
        public override void Draw(Graphics g)
        {
            if (IsPressedState()) 
            {
                g.DrawImage(GetImage("pause_button_on.png"), 0, 0, ((Graphics.LEFT) | (Graphics.TOP)));
            } 
            else 
            {
                g.DrawImage(image, 0, 0, ((Graphics.LEFT) | (Graphics.TOP)));
            }
        }
        
    }
    
    
}