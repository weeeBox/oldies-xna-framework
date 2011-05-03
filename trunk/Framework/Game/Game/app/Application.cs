using app.menu;
using asap.app;
using asap.ui;
using asap.sound;

namespace app
{
    public class Application : BaseApp
    {
        public static CheatManager sharedCheatMgr;

        public static ScreenManager sharedScreenMgr;

        public static ScreensView sharedScreensView;

        public static AppResourceMgr sharedResourceMgr;

        public static SoundManager sharedSoundMgr;

        public static AppRootController sharedRootController;

        public Application(int width, int height) : base(width, height)
        {
            sharedCheatMgr = new CheatManager();
            sharedCheatMgr.AddCheatListener(this);
            sharedScreenMgr = new ScreenManager(this);
            sharedScreensView = new ScreensView();
            sharedResourceMgr = new AppResourceMgr();
            sharedSoundMgr = new SoundManager();
            sharedRootController = new AppRootController();
            SetTickListener(sharedRootController);
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
            sharedScreensView.Tick(deltaTime);
        }

        public override void Start()
        {
            base.Start();
            sharedRootController.OnStart();
        }

        public static Application Instance
        {
            get { return (Application)GetInstance(); }
        }        
    }
}
