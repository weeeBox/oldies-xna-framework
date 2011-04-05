using System;

using System.Collections.Generic;

using app;
using asap.core;

namespace app.menu
{
    public class MenuController : TickListener, MenuListener
     {        
        private LoadingState loadingState;        
        
        private static readonly String[] STARTUP_RESOURCES = new String[] {};
        
        private static readonly String[] COMMON_RESOURCES = new String[] { "test.swp" };
        
        private static readonly String[] RESOURCES_FULL = new String[] {};
        
        private static readonly String[] RESOURCES_FREE = new String[] {};
        
        private static readonly String[] MENU_RESOURCES = new String[] {};
        
        private static readonly String[] GAME_RESOURCES = new String[] {};
        
        private static readonly String[] GAME_RESOURCES_FULL = new String[] {};
        
        private static readonly String[] GAME_RESOURCES_FREE = new String[] {};
        
        public MenuController() 
        {
            LoadPack(STARTUP_RESOURCES);
            LoadPack(COMMON_RESOURCES);            
            StartScreen(ScreenFactory.CreateStartLoading(this));
            loadingState = LoadingState.APP;
        }
        
        public virtual void Tick(float delta)
        {         
            if ((loadingState) != (LoadingState.NONE)) 
            {
                if (FlipstonesApp.GetScreensView().GetActiveScreen().WasDrawn())
                    ProcessLoadingState();                
            } 
            else 
            {                    
                FlipstonesApp.GetScreensView().Tick(delta);              
            }
        }
        
        private void ProcessLoadingState()
        {
            switch (loadingState)
            {
                case LoadingState.APP:                    
                {
                    LoadResourcesOnStart();                    
                    UnloadPack(STARTUP_RESOURCES);
                    StartScreen(ScreenFactory.CreateMainMenu(this));                    
                }
                break;                
                default:                    
                {
                    System.Diagnostics.Debug.Assert(false);
                }
                break;
            }
            loadingState = LoadingState.NONE;
        }        
        
        private void LoadResourcesOnStart()
        {
            LoadPack(RESOURCES_FULL);
            LoadPack(MENU_RESOURCES);            
        }
        
        private void LoadResourcesBeforeGame()
        {
            UnloadPack(RESOURCES_FULL);
            UnloadPack(MENU_RESOURCES);
            LoadPack(GAME_RESOURCES);
            LoadPack(GAME_RESOURCES_FULL);            
        }
        
        private void LoadResourcesAfterGame()
        {
            UnloadPack(GAME_RESOURCES);
            UnloadPack(GAME_RESOURCES_FULL);
            LoadPack(RESOURCES_FULL);
            LoadPack(MENU_RESOURCES);            
        }
        
        private void LoadPack(String[] resources)
        {
            for (int i = 0; i < (resources.Length); i++)
                AppResManager.GetInstance().Load(resources[i]);
        }
        
        private void UnloadPack(String[] resources)
        {
            for (int i = 0; i < (resources.Length); i++)
                AppResManager.GetInstance().Unload(resources[i]);
        }        
        
        public virtual void PrefetchFinished()
        {            
        } 
        
        public virtual void ButtonPressed(int code)
        {
            if (FlipstonesApp.ButtonPressed(code))
                return;            
        }        
        
        private void StartScreen(Screen screen)
        {
            FlipstonesApp.GetScreensView().StartScreen(screen);            
        }
        
        private void StartNextScreen(Screen screen)
        {
            FlipstonesApp.GetScreensView().StartNextScreen(screen);            
        }
        
        private void BackScreen()
        {
            FlipstonesApp.GetScreensView().BackScreen();            
        }
        
        public virtual void OnAppExit()
        {            
        }
        
        public virtual void Suspend()
        {     
        }        
    }        
}