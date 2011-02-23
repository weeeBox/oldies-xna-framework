using System;

using System.Collections.Generic;


using asap.ui;
using asap.core;
using asap.app;
using flipstones.menu;
using asap.graphics;

namespace flipstones
{
    public class FlipstonesApp : DefaultApp, CheatListener
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
        
        private PlacePlay placePlay;
        
        private PlacePlayListener placePlayListener;
        
        private long lastMobclixTime = 0;
        
        public FlipstonesApp(int width ,int height ,int inputMode) 
         : base(width, height, inputMode)
        {
            AudioSession.SilenceOther(false);
            cheatManager = new CheatManager();
            cheatManager.AddCheatListener(this);
            screenManager = new ScreenManager(this);
            screensView = new ScreensView();
            resManager = new AppResManager();
            menuController = new MenuController();
            SetTickListener(menuController);
            String url = "https://placeplay-api.appspot.com/placeplay";
            String gameId = "4001";
            String secretKey = "fa424dfb-6683-4dc1-b1e1-a06bb09a98b6";
            String leaderboardId = "6001";
            placePlayListener = new PlacePlayListener();
            placePlay = new PlacePlay(url , gameId , secretKey , leaderboardId , placePlayListener);
        }
        
        public virtual void TryToStartMobclix()
        {
            long currentTime = System.CurrentTimeMillis();
            if (currentTime > ((lastMobclixTime) + ((2 * 60) * 1000))) 
            {
                Mobclix.ShowAd();
                UpdateLastMobclixTime();
            } 
        }
        
        public virtual void UpdateLastMobclixTime()
        {
            lastMobclixTime = System.CurrentTimeMillis();
        }
        
        public virtual void PrefetchPlacePlay(ShopListener listener)
        {
            placePlayListener.SetShopListener(listener);
            placePlay.Prefetch();
        }
        
        public virtual void PlacePlayStartBeforeGame(ShopListener listener)
        {
            placePlayListener.SetShopListener(listener);
            placePlayListener.ResetBoughtItems();
            placePlay.StartBeforeGame();
        }
        
        public virtual void PlacePlayStartAfterGame(int score, int stars, ShopListener listener)
        {
            placePlayListener.SetShopListener(listener);
            placePlayListener.ResetBoughtItems();
            placePlay.StartAfterGame(score, stars);
        }
        
        public virtual void PlacePlayShowLeaderboard()
        {
            placePlay.ShowLeaderboard();
        }
        
        public virtual PlacePlayListener GetPlacePlayListener()
        {
            return placePlayListener;
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
            switch (code)
            {
                case ButtonId.SOUND:
                    bool sound = !(Prefs.GetInstance().IsSoundEnabled());
                    Prefs.GetInstance().SetSoundEnabled(sound);
                    ((InfoScreen)(FlipstonesApp.GetScreensView().GetActiveScreen())).UpdateScreen();
                    return true;
                case ButtonId.MUSIC:
                    bool music = !(Prefs.GetInstance().IsMusicEnabled());
                    Prefs.GetInstance().SetMusicEnabled(music);
                    if (music)
                        Sounds.PlayMenuMusic();
                    
                    else
                        Sounds.StopMusic();
                    
                    ((InfoScreen)(FlipstonesApp.GetScreensView().GetActiveScreen())).UpdateScreen();
                    return true;
                case ButtonId.FLURRY:
                    bool flurry = !(Prefs.GetInstance().IsFlurryEnabled());
                    Prefs.GetInstance().SetFlurryEnabled(flurry);
                    FlurryAdapter.SetFlurryLoggingEnabled(flurry);
                    ((InfoScreen)(FlipstonesApp.GetScreensView().GetActiveScreen())).UpdateScreen();
                    return true;
            }
            return false;
        }
        
        public override void Draw(Graphics g)
        {
            AtlasRepacker.RepackIfNeeded();
            base.Draw(g);
            DrawCheats(g);
        }
        
        private void DrawCheats(Graphics g)
        {
            cheatManager.Draw(g);
            if (showFps) 
            {
                long currentTime = System.CurrentTimeMillis();
                int deltaTime = ((int)(currentTime - (lastTime)));
                lastTime = currentTime;
                int fps = 10000 / (deltaTime == 0 ? 1 : deltaTime);
                String fpsStr = (("fps: " + (fps / 10)) + ".") + (fps % 10);
                fpsStr = ((fpsStr + ", vram: ") + ((AtlasRepacker.UsedVideoMemory()) / 1024)) + " kb";
                BitmapFont font = AppResManager.GetDefaultFont();
                font.DrawString(g, fpsStr, 0, 0);
            } 
            if (showHeap) 
            {
                System.Gc();
                long freeHeap = (Runtime.GetRuntime().FreeMemory()) / 1024;
                long totalHeap = (Runtime.GetRuntime().TotalMemory()) / 1024;
                String heapStr = (("heap: " + (totalHeap - freeHeap)) + "/") + totalHeap;
                BitmapFont font = AppResManager.GetDefaultFont();
                font.DrawString(g, heapStr, ((GetWidth()) - (font.GetStringWidth(heapStr))), 0);
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
        
        public virtual void CheatEntered(int cheatCode)
        {
            if (cheatCode == 0) 
            {
                AtlasRepacker.DumpVideoMemory();
            } 
            else if (cheatCode == 1) 
            {
                showFps = !(showFps);
                System._out.Println(("fps: " + (showFps)));
            } 
            else if (cheatCode == 2) 
            {
                showHeap = !(showHeap);
                System._out.Println(("heap: " + (showHeap)));
            } 
            else if (cheatCode == 6) 
            {
                menuController.DebugSetStoryAsPassed();
            } 
        }
        
        public virtual void BrowseFullVersion()
        {
            PlatformRequest((("http://phobos.apple.com/WebObjects/MZStore.woa/wa/viewSoftware?id=" + (APPLE_ID_FULL)) + "&amp;mt=8"));
        }
        
    }
    
    
}