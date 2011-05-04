
using System.Diagnostics;
using asap.core;
using asap.graphics;
using asap.ui;

namespace asap.app
{
    public class BaseApp : TimerSource
    {
        private static BaseApp instance;

        private TickListener tickListener;
        
        private PointerListener pointerListener;
        
        private KeyListener keyListener;

        private TimerController timerController;

        private View mainView;        

        private AppImpl appImpl;

        private int width;

        private int height;

        public BaseApp(int width, int height)
        {
            Debug.Assert((BaseApp.instance) == null);
            BaseApp.instance = this;
            this.width = width;
            this.height = height;
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
        
        public virtual void Tick(float deltaTime)
        {            
            if ((tickListener) != null) 
            {
                tickListener.Tick(deltaTime);         
            }
            timerController.Tick(deltaTime);
        }
        
        public virtual void PointerPressed(int x, int y, int fingerId)
        {
            if ((pointerListener) != null) 
            {
                pointerListener.PointerPressed(x, y, fingerId);
            } 
        }
        
        public virtual void PointerReleased(int x, int y, int fingerId)
        {
            if ((pointerListener) != null) 
            {
                pointerListener.PointerReleased(x, y, fingerId);
            } 
        }
        
        public virtual void PointerDragged(int x, int y, int fingerId)
        {
            if ((pointerListener) != null) 
            {
                pointerListener.PointerDragged(x, y, fingerId);
            } 
        }
        
        public virtual void KeyPressed(int keyCode, int keyAction)
        {
            if ((keyListener) != null) 
            {
                keyListener.KeyPressed(keyCode, keyAction);
            } 
        }
        
        public virtual void KeyReleased(int keyCode, int keyAction)
        {
            if ((keyListener) != null) 
            {
                keyListener.KeyReleased(keyCode, keyAction);
            } 
        }
        
        public virtual void KeyRepeated(int keyCode, int keyAction)
        {
            if ((keyListener) != null) 
            {
                keyListener.KeyRepeated(keyCode, keyAction);
            } 
        }        
        
        public virtual void Draw(Graphics g)
        {
            if ((mainView) != null) 
            {
                mainView.Draw(g);
            } 
        }        
        
        public virtual void Start()
        {
        }

        public virtual void Stop()
        {
            appImpl.Stop();
        }         
        
        public static BaseApp GetInstance()
        {
            return BaseApp.instance;
        }
        
        public virtual void SetImpl(AppImpl obj)
        {
            this.appImpl = obj;
        }

        public static int Width
        {
            get { return GetInstance().width; }
        }

        public static int Height
        {
            get { return GetInstance().height; }
        }
        
        public virtual void SizeChanged(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public TimerController GetTimerController()
        {
            return timerController;
        }

        public static Timer CreateTimer(TimerListener listener)
        {
            return new Timer(GetInstance(), listener);
        }
    }    
}