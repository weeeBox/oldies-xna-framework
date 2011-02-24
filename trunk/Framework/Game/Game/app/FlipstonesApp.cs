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
        public const String APPLE_ID_FULL = "379345229";
        
        public const String APPLE_ID_FREE = "379580662";
        
        private CheatManager cheatManager;
        
        private bool showFps = false;
        
        private bool showHeap = false;
        
        private long lastTime;
        
        public ScreenManager screenManager;
        
        public ScreensView screensView;
        
        public AppResManager resManager;
        
        public MenuController menuController;        
        
        private long lastMobclixTime = 0;
        
        public FlipstonesApp(int width ,int height ,int inputMode) 
         : base(width, height, inputMode)
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
            if (showFps) 
            {
                long currentTime = DateTime.Now.Millisecond;
                int deltaTime = ((int)(currentTime - (lastTime)));
                lastTime = currentTime;
                int fps = 10000 / (deltaTime == 0 ? 1 : deltaTime);
                String fpsStr = (("fps: " + (fps / 10)) + ".") + (fps % 10);                
                BitmapFont font = AppResManager.GetDefaultFont();
                font.DrawString(g, fpsStr, 0, 0);
            }
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