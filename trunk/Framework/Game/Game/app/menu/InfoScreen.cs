using System;

using System.Collections.Generic;


using flipstones;
using asap.ui;

namespace flipstones.menu
{
    public class InfoScreen : Screen
     {
        public const int SCREEN_MENU = 0;
        
        public const int SCREEN_CHALLENGE = 1;
        
        public const int SCREEN_FRENZY = 2;
        
        private MenuButton musicButton;
        
        private MenuButton soundButton;
        
        private MenuButton flurryButton;
        
        public InfoScreen(MenuListener listener ,int screenType) 
         : base((screenType != (SCREEN_MENU) ? ScreenId.PAUSE_MENU : ScreenId.INFO))
        {
            flurryButton = null;
            if ((screenType == (SCREEN_CHALLENGE)) || (screenType == (SCREEN_FRENZY))) 
            {
                Container cont = ScreenFactory.CreateDefaultIngameScreen(this, StrRes.Get(Strings.PAUSE), null, false);
                Container buttons = new Container();
                MenuButton resumeButton = new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.PAUSE_RESUME) , ButtonId.PAUSE_RESUME , listener);
                buttons.AddView(resumeButton);
                SetBackButton(resumeButton);
                if (screenType == (SCREEN_CHALLENGE)) 
                {
                    buttons.AddView(new MenuButton(ButtonType.PLAY_YELLOW , StrRes.Get(Strings.RESTART_LEVEL) , ButtonId.PAUSE_RESTART , listener));
                } 
                musicButton = new MenuButton(ButtonType.MISC , "" , ButtonId.MUSIC , listener);
                buttons.AddView(musicButton);
                soundButton = new MenuButton(ButtonType.MISC , "" , ButtonId.SOUND , listener);
                buttons.AddView(soundButton);
                buttons.AddView(new MenuButton(ButtonType.MISC , StrRes.Get((screenType == (SCREEN_CHALLENGE) ? Strings.PAUSE_MENU_EXIT : Strings.RESULT_EXIT)) , ButtonId.PAUSE_EXIT_MENU , listener));
                ScreenFactory.AddAndLayoutIngameButtons(cont, buttons);
            } 
            else 
            {
                ScreenFactory.CreateDefaultMenuScreen(this, StrRes.Get(Strings.INFO_HEADER));
                Container container = new Container();
                musicButton = new MenuButton(ButtonType.MISC , "" , ButtonId.MUSIC , listener);
                container.AddView(musicButton);
                soundButton = new MenuButton(ButtonType.MISC , "" , ButtonId.SOUND , listener);
                container.AddView(soundButton);
                flurryButton = new MenuButton(ButtonType.MISC , "" , ButtonId.FLURRY , listener);
                container.AddView(flurryButton);
                container.AddView(new MenuButton(ButtonType.MISC , StrRes.Get(Strings.HOW_TO_PLAY) , ButtonId.HOW_TO_PLAY , listener));
                container.AddView(new MenuButton(ButtonType.MISC , StrRes.Get(Strings.ABOUT) , ButtonId.ABOUT , listener));
                MenuButton backButton = new MenuButton(ButtonType.BACK , ButtonId.BACK , listener);
                container.AddView(backButton);
                SetBackButton(backButton);
                ScreenFactory.AddAndLayoutButtons(this, container, 138);
            }
            UpdateScreen();
        }
        
        public virtual void UpdateScreen()
        {
            bool sound = !(Prefs.GetInstance().IsSoundEnabled());
            soundButton.SetText(((StrRes.Get(Strings.SOUND)) + (StrRes.Get((sound ? Strings.OFF : Strings.ON)))));
            bool music = !(Prefs.GetInstance().IsMusicEnabled());
            musicButton.SetText(((StrRes.Get(Strings.MUSIC)) + (StrRes.Get((music ? Strings.OFF : Strings.ON)))));
            if ((flurryButton) != null) 
            {
                bool flurry = !(Prefs.GetInstance().IsFlurryEnabled());
                flurryButton.SetText(((StrRes.Get(Strings.FLURRY)) + (StrRes.Get((flurry ? Strings.OFF : Strings.ON)))));
            } 
        }
        
    }
    
    
}