using System;

using System.Collections.Generic;


using asap.graphics;
using System.Diagnostics;

namespace asap.app
{
    public class App
     {
        public const int DISPLAY_ORIENTATION_PORTRAIT = 0;
        
        public const int DISPLAY_ORIENTATION_LANDSCAPE = 1;
        
        private static App instance;
        
        private AppImpl appImpl;
        
        private int width;
        
        private int height;
        
        private int inputMode;
        
        public App(int width ,int height ,int inputMode) 
        {
            Debug.Assert((App.instance) == null);
            App.instance = this;
            this.width = width;
            this.height = height;
            this.inputMode = inputMode;
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
        
        public virtual String GetProperty(String name)
        {
            Debug.Assert(name != null);
            return appImpl.GetProperty(name);
        }
        
        public virtual bool PlatformRequest(String name)
        {
            Debug.Assert(name != null);
            return appImpl.PlatformRequest(name);
        }
        
        public virtual int GetWidth()
        {
            return width;
        }
        
        public virtual int GetHeight()
        {
            return height;
        }
        
        public virtual bool SetOrientation(int orientation)
        {
            Debug.Assert((orientation == (DISPLAY_ORIENTATION_LANDSCAPE)) || (orientation == (DISPLAY_ORIENTATION_PORTRAIT)));
            return appImpl.SetOrientation(orientation);
        }
        
        public virtual bool HasPointerEvents()
        {
            return true;
        }
        
        public virtual bool HasKeyEvents()
        {
            return true;
        }
        
        public virtual int GetInputMode()
        {
            return inputMode;
        }
        
        public virtual void InputModeChanged(int newInputMode)
        {
            this.inputMode = newInputMode;
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
        
        public virtual void Tick(int deltaTime)
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