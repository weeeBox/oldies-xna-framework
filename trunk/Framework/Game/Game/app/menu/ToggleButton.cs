using System;

using System.Collections.Generic;


using asap.graphics;

namespace flipstones.menu
{
    public class ToggleButton : Button
     {
        private const int DEFAULT_WIDTH = 258;
        
        private const int DEFAULT_HEIGHT = 46;
        
        private bool _checked = false;
        
        private ToggleListener toggleListener;
        
        public ToggleButton(String text ,int code ,ToggleListener listener) 
         : base(ButtonType.TOGGLE, code, null)
        {
            Init(text, null, DEFAULT_WIDTH, DEFAULT_HEIGHT);
            this.toggleListener = listener;
        }
        
        public override void Draw(Graphics g)
        {
            Image back;
            if (IsPressedState())
                back = GetImage("button_regular_active.png");
            
            else
                back = GetImage("button_regular_passive.png");
            
            int backX = ((GetWidth()) - (back.GetWidth())) / 2;
            int backY = ((GetHeight()) - (back.GetHeight())) / 2;
            g.DrawImage(back, backX, backY, ((Graphics.LEFT) | (Graphics.TOP)));
            BitmapFont font = AppResManager.GetDefaultFont();
            int textHeight = font.GetHeight();
            int textY = (((GetHeight()) - textHeight) / 2) + 3;
            int textX = 40;
            font.DrawString(g, text, textX, textY);
            if (_checked) 
            {
                g.SetColor(255);
                g.FillRect(10, (((GetHeight()) - 20) / 2), 20, 20);
            } 
            g.SetColor(0);
            g.DrawRect(10, (((GetHeight()) - 20) / 2), 20, 20);
        }
        
        public override void Click()
        {
            _checked = !(_checked);
            toggleListener.ButtonToggled(code, _checked);
        }
        
        public virtual bool IsChecked()
        {
            return _checked;
        }
        
    }
    
    
}