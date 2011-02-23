using System;

using System.Collections.Generic;


using asap.graphics;

namespace app.menu
{
    public class LevelButton : Button
     {
        public const int LEVEL_BUTTON_WIDTH = 74;
        
        public const int LEVEL_BUTTON_HEIGHT = 70;
        
        public LevelButton(ButtonType type ,int code ,MenuListener listener) 
         : base(type, code, listener)
        {
            String text = JUtils.ValueOf((code + 1));
            Init(text, null, LEVEL_BUTTON_WIDTH, LEVEL_BUTTON_HEIGHT);
            SetType(type);
        }
        
        public override void Draw(Graphics g)
        {
            int imageX = ((GetWidth()) - (image.GetWidth())) / 2;
            int imageY = ((GetHeight()) - (image.GetHeight())) / 2;
            g.DrawImage(image, imageX, imageY, ((Graphics.LEFT) | (Graphics.TOP)));
            BitmapFont font = AppResManager.GetDefaultFont();
            int textWidth = font.GetStringWidth(text);
            int textHeight = font.GetHeight();
            int textX = ((GetWidth()) - textWidth) / 2;
            int textY = ((GetHeight()) - textHeight) / 2;
            font.DrawString(g, text, textX, textY);
            if ((_getType()) == (ButtonType.LEVEL_LOCKED)) 
            {
                g.DrawImage(GetImage("lock.png"), ((GetWidth()) / 2), GetHeight(), ((Graphics.HCENTER) | (Graphics.BOTTOM)));
            } 
            if (IsPressedState()) 
            {
                g.DrawImage(GetImage("actions_select.png"), imageX, imageY, ((Graphics.LEFT) | (Graphics.TOP)));
            } 
        }
        
        public virtual void SetType(ButtonType type)
        {
            System.Diagnostics.Debug.Assert(((type == (ButtonType.LEVEL_NORMAL)) || (type == (ButtonType.LEVEL_ACTIVE))) || (type == (ButtonType.LEVEL_LOCKED)));
            this.type = type;
            switch (type)
            {
                case ButtonType.LEVEL_NORMAL:
                    image = GetImage("actions_done.png");
                    break;
                case ButtonType.LEVEL_ACTIVE:
                    image = GetImage("actions_play.png");
                    break;
                case ButtonType.LEVEL_LOCKED:
                    image = GetImage("actions_lock.png");
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
        }
        
        public override bool IsEnabled()
        {
            return (_getType()) != (ButtonType.LEVEL_LOCKED);
        }
        
    }
    
    
}