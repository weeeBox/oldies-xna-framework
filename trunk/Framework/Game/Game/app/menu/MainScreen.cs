using app;
using app.menu;
using asap.visual;
using AsapXna.asap.visual;
using Microsoft.Xna.Framework;
using asap.anim;
using asap.util;

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

            BaseAnimation animation = new BaseAnimation(new Image(Application.sharedResourceMgr.GetTexture(Res.IMG_UI_BUTTON_A)));            
            animation.x = animation.y = 100;            
            animation.TurnTimelineSupportWithMaxKeyFrames(1);
            animation.AddKeyFrame(new BaseAnimation.KeyFrame(animation.x, animation.y, ColorTransform.NONE, 1.0f, 1.0f, MathHelper.TwoPi, 1.5f));
            animation.PlayTimeline();
            animation.SetTimelineLoopType(BaseAnimation.Timeline.REPLAY);

            AddChild(animation);
        }                
    }
}
