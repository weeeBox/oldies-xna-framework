using app;
using app.menu;
using asap.visual;
using AsapXna.asap.visual;
using Microsoft.Xna.Framework;
using asap.anim;

namespace Game.app.menu
{
    public class MainScreen : Screen
    {
        public MainScreen() : base(ScreenId.MAIN_MENU)
        {
            AnimationMovie movie = new AnimationMovie(Application.sharedResourceMgr.GetMovie(Res.ANI_SWF_TEST));
            movie.AnimationType = AnimationType.LOOP;
            movie.Start();
            AddChild(movie);            
        }                
    }
}
