using System;

using System.Collections.Generic;


using asap.graphics;
using System.Diagnostics;

namespace asap.app
{
    public class App
    {                
        private static App instance;
        
        private AppImpl appImpl;
        
        private int width;
        
        private int height;        
        
        public App(int width, int height) 
        {
            Debug.Assert((App.instance) == null);
            App.instance = this;
            this.width = width;
            this.height = height;            
        }
        
        public virtual void Start()
        {
        }
        
        public static App GetInstance()
        {
            return App.instance;
        }
        
        public virtual void SetImpl(AppImpl obj)
        {
            this.appImpl = obj;
        }        
        
        public virtual int GetWidth()
        {
            return width;
        }
        
        public virtual int GetHeight()
        {
            return height;
        }        
        
        public virtual void SizeChanged(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        
        public virtual void Stop()
        {
            appImpl.Stop();
        }
        
        public virtual void Tick(float deltaTime)
        {
        }
        
        public virtual void PointerPressed(int x, int y, int fingerId)
        {
        }
        
        public virtual void PointerReleased(int x, int y, int fingerId)
        {
        }
        
        public virtual void PointerDragged(int x, int y, int fingerId)
        {
        }
        
        public virtual void KeyPressed(int keyCode, int keyAction)
        {
        }
        
        public virtual void KeyReleased(int keyCode, int keyAction)
        {
        }
        
        public virtual void KeyRepeated(int keyCode, int keyAction)
        {
        }
        
        public virtual void Accelerated(double x, double y, double z)
        {
        }
        
        public virtual void ApplicationExits()
        {
        }
        
        public virtual void Suspend()
        {
        }
        
        public virtual void Resume()
        {
        }
        
        public virtual void Draw(Graphics g)
        {
        }
        
    }
    
    
}