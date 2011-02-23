using System;

using System.Collections.Generic;


using asap.graphics;

namespace app.menu
{
    public class CheatButton : Button
     {
        public CheatButton(int code ,MenuListener listener) 
         : base(ButtonType.CHEAT, code, listener)
        {
            width = 40;
            height = 40;
        }
        
        public override void Draw(Graphics g)
        {
            g.SetColor((IsPressedState() ? 255 : 136));
            g.FillRect(0, 0, GetWidth(), GetHeight());
            String text = JUtils.ValueOf(code);
            BitmapFont font = AppResManager.GetDefaultFont();
            int textHeight = font.GetHeight();
            int textY = (((GetHeight()) - textHeight) / 2) + 3;
            int textX = ((GetWidth()) - (font.GetStringWidth(text))) / 2;
            font.DrawString(g, text, textX, textY);
        }
        
    }
    
    
}