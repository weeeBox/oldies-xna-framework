using System;

using System.Collections.Generic;


using app;
using asap.ui;
using asap.graphics;

namespace app.menu
{
    public class PageNavigator : Container
     {
        private int page;
        
        private int pagesCount;
        
        public PageNavigator(MenuListener listener ,int page ,int pagesCount) 
        {
            this.page = page;
            this.pagesCount = pagesCount;
            if (pagesCount > 0) 
            {
                Button button = new NavigButton(ButtonType.NAVIG_LEFT , listener);
                int buttonIndex = AddView(button);
                SetViewX(buttonIndex, 0);
                SetViewY(buttonIndex, ((GetHeight()) - (button.GetHeight())));
                button = new NavigButton(ButtonType.NAVIG_RIGHT , listener);
                buttonIndex = AddView(button);
                SetViewX(buttonIndex, ((GetWidth()) - (button.GetWidth())));
                SetViewY(buttonIndex, ((GetHeight()) - (button.GetHeight())));
            } 
        }
        
        public override int GetHeight()
        {
            return 38;
        }
        
        public override int GetWidth()
        {
            return 260;
        }
        
        public virtual void SetPage(int page)
        {
            System.Diagnostics.Debug.Assert((page >= 0) && (page < (GetPagesCount())));
            this.page = page;
        }
        
        public virtual int GetPage()
        {
            return page;
        }
        
        public virtual int GetPagesCount()
        {
            return pagesCount;
        }
        
        public override void Draw(Graphics g)
        {
            base.Draw(g);
            AppResManager resManager = AppResManager.GetInstance();
            Image pointPassive = resManager.GetImage("point_passive.png");
            Image pointActive = resManager.GetImage("point_active.png");
            Image navigImage = resManager.GetImage("arrow_passive.png");
            int INDENT_BETWEEN_POINTS = 10;
            int x = (((GetWidth()) - ((pointPassive.GetWidth()) * (GetPagesCount()))) - (INDENT_BETWEEN_POINTS * ((GetPagesCount()) - 1))) / 2;
            int y = (GetHeight()) - (((navigImage.GetHeight()) + (pointPassive.GetHeight())) / 2);
            for (int i = 0; i < (GetPagesCount()); i++) 
            {
                g.DrawImage((i == (page) ? pointActive : pointPassive), (x + (i * (INDENT_BETWEEN_POINTS + (pointPassive.GetWidth())))), y, ((Graphics.LEFT) | (Graphics.TOP)));
            }
        }
        
        public virtual bool ButtonPressed(int code)
        {
            if ((code == (ButtonId.NAVIG_BUTTON_LEFT)) && ((GetPage()) > 0)) 
            {
                SetPage(((GetPage()) - 1));
                return true;
            } 
            else if ((code == (ButtonId.NAVIG_BUTTON_RIGHT)) && (((GetPage()) + 1) < (GetPagesCount()))) 
            {
                SetPage(((GetPage()) + 1));
                return true;
            } 
            return false;
        }
        
    }
    
    
}