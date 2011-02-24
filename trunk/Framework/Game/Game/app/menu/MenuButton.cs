using System;

using System.Collections.Generic;


using app;
using asap.core;
using asap.graphics;
using asap.anim;

namespace app.menu
{
    public class MenuButton : Button, TimerListener
     {
        public const int DEFAULT_WIDTH = 258;
        
        public const int DEFAULT_HEIGHT = 46;
        
        private const int BIG_WIDTH = 294;
        
        private const int BIG_HEIGHT = 80;
        
        private AnimationPlayer starPlayer;
        
        private int starX;
        
        private int starY;
        
        private Random rnd;
        
        public MenuButton(ButtonType type ,int code ,MenuListener listener) 
         : base(type, code, listener)
        {
            switch (type)
            {                
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
        }
        
        public MenuButton(ButtonType type ,String text ,int code ,MenuListener listener) 
         : base(type, code, listener)
        {
            switch (type)
            {
                case ButtonType.MORE_GAMES_GREEN:
                    Init(text, "gems_open_feint.png", DEFAULT_WIDTH, DEFAULT_HEIGHT);
                    break;
                case ButtonType.BACK:
                    Init(text, "gems_back.png", DEFAULT_WIDTH, DEFAULT_HEIGHT);
                    break;
                case ButtonType.PLAY_ORANGE:
                    Init(text, "gems_play.png", DEFAULT_WIDTH, DEFAULT_HEIGHT);
                    break;
                case ButtonType.PLAY_YELLOW:
                    Init(text, "gems_other.png", DEFAULT_WIDTH, DEFAULT_HEIGHT);
                    break;
                case ButtonType.MISC:
                    Init(text, "gems_info.png", DEFAULT_WIDTH, DEFAULT_HEIGHT);
                    break;
            }
        }
        
        public virtual void OnTimer(Timer timer)
        {
            if ((starPlayer) != null) 
            {
                int delay = ((int)(starPlayer.GetAnimation().GetFrame(starPlayer.GetFrame()).GetDelay()));
                if (starPlayer.IsStarted()) 
                {
                    starPlayer.Update(((int)(timer.GetDelay())));
                    if (!(starPlayer.IsStarted())) 
                    {
                        timer.Schedule(300, false);
                    } 
                    else 
                    {
                        timer.Schedule(delay, false);
                    }
                } 
                else 
                {                    
                    starX += 15;
                    starY += 10;
                    starPlayer.Start();
                    timer.Schedule(50, false);
                }
            } 
        }
        
        public override void Draw(Graphics g)
        {
            Image back;
            int deltaX = 0;
            int deltaY = 0;
            if (((_getType()) == (ButtonType.PLAY_FRENZY)) || ((_getType()) == (ButtonType.POST_SCORE))) 
            {
                if (IsPressedState())
                    back = GetImage("button_big_active.png");
                
                else
                    back = GetImage("button_big_passive.png");
                
                deltaX = -5;
                deltaY = -5;
            } 
            else 
            {
                if (IsPressedState())
                    back = GetImage("button_regular_active.png");
                
                else
                    back = GetImage("button_regular_passive.png");
                
                deltaX = 0;
                deltaY = -2;
            }
            int backX = ((GetWidth()) - (back.GetWidth())) / 2;
            int backY = ((GetHeight()) - (back.GetHeight())) / 2;
            g.DrawImage(back, backX, backY, ((Graphics.LEFT) | (Graphics.TOP)));
            if ((_getType()) == (ButtonType.POST_SCORE)) 
            {
                BitmapFont font = AppResManager.GetDefaultFont();
                int textHeight = font.GetHeight();
                int textY = (((GetHeight()) - textHeight) / 2) + 3;
                int textX = 83;
                font.DrawString(g, text, textX, textY);
            } 
            else if ((_getType()) == (ButtonType.PLAY_FRENZY)) 
            {
                Image textImage = GetImage("frenzy.png");
                int textY = (((GetHeight()) - (textImage.GetHeight())) / 2) + 2;
                g.DrawImage(textImage, 83, textY, ((Graphics.LEFT) | (Graphics.TOP)));
            } 
            else 
            {
                BitmapFont font = AppResManager.GetDefaultFont();
                int textHeight = font.GetHeight();
                int textY = (((GetHeight()) - textHeight) / 2) + 3;
                int textX = 63;
                font.DrawString(g, text, textX, textY);
            }
            g.DrawImage(image, -deltaX, -deltaY, ((Graphics.LEFT) | (Graphics.TOP)));
            if (((starPlayer) != null) && (starPlayer.IsStarted())) 
            {
                starPlayer.Draw(g, starX, starY);
            } 
        }
        
    }
    
    
}