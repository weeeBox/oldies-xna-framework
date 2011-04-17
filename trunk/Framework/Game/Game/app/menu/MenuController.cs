using System;

using System.Collections.Generic;

using app;
using asap.core;
using System.Diagnostics;
using asap.resources;

namespace app.menu
{
    public class MenuController : TickListener, MenuListener
    {        
        private LoadingState loadingState;        
        
        public MenuController() 
        {
            LoadPack(ResPacks.PACK_COMMON1);
            LoadPack(ResPacks.PACK_COMMON2);            
            StartScreen(ScreenFactory.CreateStartLoading(this));
            loadingState = LoadingState.APP;
        }
        
        public virtual void Tick(float delta)
        {         
            if ((loadingState) != (LoadingState.NONE)) 
            {
                if (GameApp.GetScreensView().GetActiveScreen().WasDrawn())
                    ProcessLoadingState();                
            } 
            else 
            {                    
                GameApp.GetScreensView().Tick(delta);              
            }
        }
        
        private void ProcessLoadingState()
        {
            switch (loadingState)
            {
                case LoadingState.APP:                    
                {
                    LoadResourcesOnStart();                                        
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
            
        }
        
        private void LoadResourcesBeforeGame()
        {            
        }
        
        private void LoadResourcesAfterGame()
        {
                      
        }
        
        private void LoadPack(int packIndex)
        {
            
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
            if (GameApp.ButtonPressed(code))
                return;            
        }        
        
        private void StartScreen(Screen screen)
        {
            GameApp.GetScreensView().StartScreen(screen);            
        }
        
        private void StartNextScreen(Screen screen)
        {
            GameApp.GetScreensView().StartNextScreen(screen);            
        }
        
        private void BackScreen()
        {
            GameApp.GetScreensView().BackScreen();            
        }
        
        public virtual void OnAppExit()
        {            
        }
        
        public virtual void Suspend()
        {     
        }        
    }        
}