using System;

using System.Collections.Generic;


using asap.ui;
using asap.app;
using asap.graphics;

namespace asap.core
{
    public class App : BaseApp, TimerSource
    {
        private TickListener tickListener;
        
        private PointerListener pointerListener;
        
        private KeyListener keyListener;

        private TimerController timerController;

        private View mainView;
        
        public App(int width ,int height) 
         : base(width, height)
        {
            timerController = new TimerController();
        }
        
        public virtual void SetTickListener(TickListener listener)
        {
            this.tickListener = listener;
        }
        
        public virtual void SetPointerListener(PointerListener listener)
        {
            pointerListener = listener;
        }
        
        public virtual void SetKeyListener(KeyListener listener)
        {
            keyListener = listener;
        }
        
        public virtual void SetMainView(View mainView)
        {
            this.mainView = mainView;
        }
        
        public override void Tick(float deltaTime)
        {            
            if ((tickListener) != null) 
            {
                tickListener.Tick(deltaTime);         
            }
            timerController.Tick(deltaTime);
        }
        
        public override void PointerPressed(int x, int y, int fingerId)
        {
            if ((pointerListener) != null) 
            {
                pointerListener.PointerPressed(x, y, fingerId);
            } 
        }
        
        public override void PointerReleased(int x, int y, int fingerId)
        {
            if ((pointerListener) != null) 
            {
                pointerListener.PointerReleased(x, y, fingerId);
            } 
        }
        
        public override void PointerDragged(int x, int y, int fingerId)
        {
            if ((pointerListener) != null) 
            {
                pointerListener.PointerDragged(x, y, fingerId);
            } 
        }
        
        public override void KeyPressed(int keyCode, int keyAction)
        {
            if ((keyListener) != null) 
            {
                keyListener.KeyPressed(keyCode, keyAction);
            } 
        }
        
        public override void KeyReleased(int keyCode, int keyAction)
        {
            if ((keyListener) != null) 
            {
                keyListener.KeyReleased(keyCode, keyAction);
            } 
        }
        
        public override void KeyRepeated(int keyCode, int keyAction)
        {
            if ((keyListener) != null) 
            {
                keyListener.KeyRepeated(keyCode, keyAction);
            } 
        }
        
        public override void Accelerated(double x, double y, double z)
        {
        }
        
        public override void ApplicationExits()
        {
        }
        
        public override void Suspend()
        {
        }
        
        public override void Resume()
        {
        }
        
        public override void Draw(Graphics g)
        {
            if ((mainView) != null) 
            {
                mainView.Draw(g);
            } 
        }

        public TimerController GetTimerController()
        {
            return timerController;
        }

        public static Timer CreateTimer(TimerListener timerListener)
        {
            App instance = (App)GetInstance();
            return new Timer(instance, timerListener);
        }
    }    
}