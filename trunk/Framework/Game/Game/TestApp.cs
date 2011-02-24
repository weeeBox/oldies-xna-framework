using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.app;
using app;
using asap.core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Flipstones2.gfx;
using Flipstones2.res;
using Flipstones2.app;

namespace app
{
    public class TestApp : AppImpl
    {
        private FlipstonesApp app;
        private XnaGraphics appGraphics;
        private bool running;

        public TestApp(int width, int height, ContentManager content)
        {
            new XnaResFactory(content);
            new XnaRecordStorage();
            new XnaMediaManager();

            loadPacksInfo(content);

            app = new FlipstonesApp(width, height, 0);
            app.SetImpl(this);
            appGraphics = new XnaGraphics(width, height);
            running = true;           
        }

        private void loadPacksInfo(ContentManager content)
        {
            TextureManager texManager = new TextureManager(content);

            ContentManager manager = new ContentManager(content.ServiceProvider, "Content");
        	texManager.AddPackAtlas(manager.Load<Atlas>("STARTUP_RESOURCES"));
		    texManager.AddPackAtlas(manager.Load<Atlas>("COMMON_RESOURCES"));
            texManager.AddPackAtlas(manager.Load<Atlas>("MENU_RESOURCES"));
            texManager.AddPackAtlas(manager.Load<Atlas>("GAME_RESOURCES"));
            if (Config.freeVersion)
            {
                texManager.AddPackAtlas(manager.Load<Atlas>("RESOURCES_FREE"));
                texManager.AddPackAtlas(manager.Load<Atlas>("GAME_RESOURCES_FREE"));
                texManager.AddPackAtlas(manager.Load<Atlas>("GAME_BACKGROUNDS_FREE"));
            }
            else
            {
                texManager.AddPackAtlas(manager.Load<Atlas>("RESOURCES_FULL"));
                texManager.AddPackAtlas(manager.Load<Atlas>("GAME_RESOURCES_FULL"));
                texManager.AddPackAtlas(manager.Load<Atlas>("GAME_BACKGROUNDS_FULL"));
            }

            manager = null;
            System.GC.Collect();
        }

        public string GetProperty(string name)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            running = false;
        }

        public bool SetOrientation(int orientation)
        {
            throw new NotImplementedException();
        }        

        public void Tick(int deltaTime)
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

        public void Draw(GraphicsDevice gd)
        {
            appGraphics.Begin(gd);
            app.Draw(appGraphics);
            appGraphics.End();
        }

        public bool isRunning()
        {
            return running;
        }
    }
}
