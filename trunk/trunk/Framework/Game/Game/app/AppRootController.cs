using app;
using asap.core;
using asap.ui;
using app.menu;
using Game.app;

namespace app
{
    public class AppRootController : RootController
    {
        private GameController gameController;

        private MenuController menuController;

        public AppRootController()
        {     
        }

        public override void OnStart()
        {
            base.OnStart();            

            StartController(new StartupController(), 0);
        }
    }
}
