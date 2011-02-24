using System;

using System.Collections.Generic;


using app;
using asap.ui;
using asap.core;
using asap.graphics;

namespace app.menu
{
    /** 
     * Factory for screens and screen elements.
     */
    public class ScreenFactory
     {
        public static Screen CreateMainMenu(MenuListener listener)
        {
            Screen screen = new Screen(ScreenId.MAIN_MENU);
            ScreenFactory.CreateDefaultMenuScreen(screen, null);
            Image headerImage = ScreenFactory.GetImage((Config.freeVersion ? "logo_free.png" : "logo.png"));
            ImageView header = new ImageView(headerImage);
            screen.AddView(header);
            screen.AlignCenter(header);
            Container container = new Container();
            container.AddView(new MenuButton(ButtonType.PLAY_FRENZY , ButtonId.START_FRENZY , listener));
            container.AddView(new MenuButton(ButtonType.PLAY_CHALLENGE , ButtonId.START_CHALLENGE , listener));
            ScreenFactory.AddAndLayoutButtons(screen, container, 150);
            container = new Container();            
            ScreenFactory.AddAndLayoutButtons(screen, container, 310);
            screen.SetBackListener(listener, ButtonId.APP_EXIT);
            return screen;
        }

        public static Screen CreateStartLoading(MenuListener listener)
        {
            Screen screen = new Screen(ScreenId.START_LOADING);
            screen.AddView(new ImageView(ScreenFactory.GetImage("menu_background.png")));
            Container content = new Container();
            content.SetSize(screen.GetWidth(), screen.GetHeight());
            content.AddView(new ImageView(ScreenFactory.GetImage("poke_logo.png")));
            Container spacer = new Container();
            content.AddView(spacer);            
            spacer = new Container();
            spacer.SetSize(0, 10);
            content.AddView(spacer);
            content.AddView(new TextBox("Loading", ScreenFactory.GetFont("font_menu_mini.fnt"), ((screen.GetWidth()) - 20)));
            content.SpreadVertically();
            content.AlignCenterAll();
            screen.AddView(content);
            return screen;
        }
        
        private static BitmapFont GetFont(String fontName)
        {
            return AppResManager.GetInstance().GetFont(fontName);
        }        
        
        private const int TEXT_INDENT = 10;
        
        private const int SPACE_BETWEEN_BUTTONS = 5;
        
        private static Image GetImage(String imageRes)
        {
            return AppResManager.GetInstance().GetImage(imageRes);
        }             
        
        public static void CreateDefaultMenuScreen(Screen screen, String header)
        {
            ImageView back = new ImageView(ScreenFactory.GetImage("menu_background.png"));
            screen.AddView(back);
            if (header != null) 
            {
                TextBox headerBox = new TextBox(header , AppResManager.GetInstance().GetFont("font_head.fnt"));
                int headerIndex = screen.AddView(headerBox);
                screen.AlignCenter(headerBox);
                screen.SetViewY(headerIndex, 14);
            } 
        }
        
        public static int AddAndLayoutButtons(Container parentCont, Container buttonsCont, int contY)
        {
            return ScreenFactory.AddAndLayoutButtons(parentCont, buttonsCont, contY, SPACE_BETWEEN_BUTTONS);
        }
        
        public static int AddAndLayoutButtons(Container parentCont, Container buttonsCont, int contY, int spaceBetweenButtons)
        {
            int contIndex = parentCont.AddView(buttonsCont);
            buttonsCont.SpreadVertically(spaceBetweenButtons);
            buttonsCont.ResizeToFitViews();
            buttonsCont.AlignCenterAll();
            parentCont.AlignCenter(buttonsCont);
            parentCont.SetViewY(contIndex, contY);
            return contIndex;
        }
        
        public static int AddAndLayoutMenuBottomButtons(Container parentCont, Container buttonsCont)
        {
            int contIndex = ScreenFactory.AddAndLayoutButtons(parentCont, buttonsCont, 0);
            parentCont.SetViewY(contIndex, (((parentCont.GetHeight()) - (buttonsCont.GetHeight())) - 12));
            return contIndex;
        } 
        
        public static void AddMenuBackBottomButton(Screen screen, MenuListener listener)
        {
            Container buttonsContainer = new Container();
            MenuButton backButton = new MenuButton(ButtonType.BACK , ButtonId.BACK , listener);
            buttonsContainer.AddView(backButton);
            screen.SetBackButton(backButton);
            ScreenFactory.AddAndLayoutMenuBottomButtons(screen, buttonsContainer);
        }
    }    
}