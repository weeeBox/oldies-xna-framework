using System;

using System.Collections.Generic;


using asap.ui;
using asap.core;
using asap.app;
using asap.graphics;

namespace app.menu
{
    /** 
     * Fullscreen component, a single item of screens flow
     */
    public class Screen : UiComponent, KeyListener, TickListener, TimerSource
    {
        public ScreenId id;        
        
        private TimerController timerController;
        
        private Button backButton;
        
        private MenuListener backListener;
        
        private int backCode;
        
        public Screen(ScreenId id) : this(id, Application.Width, Application.Height)
        {
        }

        public Screen(ScreenId id, float width, float height) : base(width, height)
        {
            this.id = id;            
            timerController = new TimerController();
        }
        
        public virtual void Tick(float delta)
        {
            timerController.Tick(delta);
            Update(delta);
        }
        
        public virtual TimerController GetTimerController()
        {
            return timerController;
        }
        
        public virtual ScreenId GetId()
        {
            return id;
        }        
        
        public virtual void OnScreenBack()
        {
        }
        
        public virtual void SetBackButton(Button backButton)
        {
            this.backButton = backButton;
        }
        
        public virtual void SetBackListener(MenuListener listener, int code)
        {
            backListener = listener;
            backCode = code;
        }
        
        public virtual bool KeyPressed(int keyCode, int keyAction)
        {
            if (keyCode == (KeyCode.CANCEL)) 
            {
                if ((backButton) != null) 
                {
                    backButton.Click();
                    return true;
                } 
                else if ((backListener) != null) 
                {
                    backListener.ButtonPressed(backCode);
                    return true;
                } 
            } 
            return false;
        }
        
        public virtual bool KeyReleased(int keyCode, int keyAction)
        {
            return false;
        }
        
        public virtual bool KeyRepeated(int keyCode, int keyAction)
        {
            return false;
        }
        
    }
    
    
}