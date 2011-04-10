using asap.app;
using asap.core;
using asap.graphics;
using Flipstones2.app;
using Microsoft.Xna.Framework.Content;
using asap.resources;

namespace app
{
    public class TestApp : AppImpl
    {
        private FlipstonesApp app;        
        private bool running;

        public TestApp(int width, int height, ContentManager content)
        {
            new ResFactory(content);
            new XnaRecordStorage();
            new XnaMediaManager();            

            app = new FlipstonesApp(width, height);
            app.SetImpl(this);            
            running = true;           
        }        
        
        public void Stop()
        {
            running = false;
        }
                

        public void Tick(float deltaTime)
        {
            app.Tick(deltaTime);
        }

        public void PointerPressed(int x, int y)
        {
            app.PointerPressed(x, y, 0);
        }

        public void PointerDragged(int x, int y)
        {
            app.PointerDragged(x, y, 0);
        }

        public void PointerReleased(int x, int y)
        {
            app.PointerReleased(x, y, 0);
        }

        public void BackPressed()
        {
            app.KeyPressed(KeyCode.CANCEL, KeyAction.NONE);
        }

        public void Draw(Graphics g)
        {           
            app.Draw(g);           
        }

        public bool isRunning()
        {
            return running;
        }
    }
}
