using asap.app;
using asap.core;
using asap.graphics;
using asap.resources;
using Microsoft.Xna.Framework.Content;

namespace app
{
    public class NativeApp : AppImpl
    {
        private Application application;        
        private bool running;

        public NativeApp(int width, int height, ContentManager content)
        {
            new ResFactory(content);            
            new TextureManager(content);

            application = new Application(width, height);
            application.SetImpl(this);            
            running = true;

            application.Start();
        }        
        
        public void Stop()
        {
            running = false;
        }                

        public void Tick(float deltaTime)
        {
            application.Tick(deltaTime);
        }

        public void PointerPressed(int x, int y)
        {
            application.PointerPressed(x, y, 0);
        }

        public void PointerDragged(int x, int y)
        {
            application.PointerDragged(x, y, 0);
        }

        public void PointerReleased(int x, int y)
        {
            application.PointerReleased(x, y, 0);
        }

        public void BackPressed()
        {
            application.KeyPressed(KeyCode.CANCEL, KeyAction.NONE);
        }

        public void Draw(Graphics g)
        {           
            application.Draw(g);           
        }

        public bool isRunning()
        {
            return running;
        }
    }
}
