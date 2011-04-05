using System;

using System.Collections.Generic;


using asap.ui;
using asap.core;
using asap.app;
using app.menu;
using asap.graphics;

namespace app
{
    public class FlipstonesApp : DefaultApp
    {        
        private CheatManager cheatManager;        
        
        public ScreenManager screenManager;
        
        public ScreensView screensView;
        
        public AppResManager resManager;
        
        public MenuController menuController;        
        
        public FlipstonesApp(int width, int height) 
         : base(width, height)
        {            
            cheatManager = new CheatManager();
            cheatManager.AddCheatListener(this);
            screenManager = new ScreenManager(this);
            screensView = new ScreensView();
            resManager = new AppResManager();
            menuController = new MenuController();
            SetTickListener(menuController);         
        }
        
        public virtual ScreenManager GetScreenManager()
        {
            return screenManager;
        }
        
        public virtual CheatManager GetCheatManager()
        {
            return cheatManager;
        }
        
        public static ScreensView GetScreensView()
        {
            return FlipstonesApp.GetInstance().screensView;
        }
        
        public static FlipstonesApp GetInstance()
        {
            return ((FlipstonesApp)(App.GetInstance()));
        }
        
        public override void Suspend()
        {
            base.Resume();
            menuController.Suspend();
            Sounds.Suspend();
        }
        
        public override void Resume()
        {
            base.Resume();
            Sounds.Resume();
        }
        
        public override void ApplicationExits()
        {
            base.ApplicationExits();
            menuController.OnAppExit();
            Sounds.StopMusic();
        }
        
        public static bool ButtonPressed(int code)
        {            
            return false;
        }
        
        public override void Draw(Graphics g)
        {            
            base.Draw(g);
            DrawCheats(g);
        }
        
        private void DrawCheats(Graphics g)
        {
            cheatManager.Draw(g);            
        }
        
        public override void PointerPressed(int x, int y, int fingerId)
        {
            if (cheatManager.PointerPressed(x, y, fingerId))
                return ;
            
            base.PointerPressed(x, y, fingerId);
        }
        
        public override void PointerReleased(int x, int y, int fingerId)
        {
            if (cheatManager.PointerReleased(x, y, fingerId))
                return ;
            
            base.PointerReleased(x, y, fingerId);
        }
        
        public override void PointerDragged(int x, int y, int fingerId)
        {
            if (cheatManager.PointerDragged(x, y, fingerId))
                return ;
            
            base.PointerDragged(x, y, fingerId);
        }
        
        public override void KeyPressed(int keyCode, int keyAction)
        {
            if (cheatManager.KeyPressed(keyCode, keyAction))
                return ;
            
            base.KeyPressed(keyCode, keyAction);
        }
        
        public override void KeyReleased(int keyCode, int keyAction)
        {
            if (cheatManager.KeyReleased(keyCode, keyAction))
                return ;
            
            base.KeyReleased(keyCode, keyAction);
        }                
    }    
}