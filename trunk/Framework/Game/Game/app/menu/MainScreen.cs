using System.Collections.Generic;
using System.Diagnostics;
using app;
using app.menu;
using asap.anim;
using asap.anim.objects;
using asap.visual;
using AsapXna.asap.visual;
using asap.util;
using Microsoft.Xna.Framework;
using asap.graphics;
using asap.resources;
using asap.sound;
using System;
using asap.ui;

namespace Game.app.menu
{
    public class MainScreen : Screen
    {
        private SpriteInstance sprite;

        public MainScreen() : base(ScreenId.MAIN_MENU)
        {
            AnimationMovie movie = new AnimationMovie(Application.sharedResourceMgr.GetMovie(Res.ANI_ANIM));
            movie.AnimationType = AnimationType.LOOP;
            movie.alignX = movie.parentAlignX = 0.5f;
            movie.Start();
            movie.drawBorder = true;

            SpriteInstance hand = movie.FindInstance("duckHand");
            Image duck = new Image(Application.sharedResourceMgr.GetTexture(Res.IMG_DUCK_DEAD));
            duck.rotation = -MathHelper.PiOver4;
            duck.x = 0;
            duck.y = 60;
            hand.AddChild(duck);

            float xmax = 100;
            float xmin = -100;
            float time = 1.0f;

            BaseAnimation animation = new BaseAnimation(movie);
            animation.alignX = animation.parentAlignX = 0.5f;
            animation.TurnTimelineSupportWithMaxKeyFrames(5);
            animation.SetTimelineLoopType(BaseAnimation.Timeline.REPLAY);
            animation.AddKeyFrame(new BaseAnimation.KeyFrame(animation.x + xmax, animation.y, ColorTransform.NONE, 1.0f, 1.0f, 0.0f, time));
            animation.AddKeyFrame(new BaseAnimation.KeyFrame(animation.x + xmax, animation.y, ColorTransform.NONE, -1.0f, 1.0f, 0.0f, 0.0f));
            animation.AddKeyFrame(new BaseAnimation.KeyFrame(animation.x + xmin, animation.y, ColorTransform.NONE, -1.0f, 1.0f, 0.0f, 2 * time));
            animation.AddKeyFrame(new BaseAnimation.KeyFrame(animation.x + xmin, animation.y, ColorTransform.NONE, 1.0f, 1.0f, 0.0f, 0.0f));
            animation.AddKeyFrame(new BaseAnimation.KeyFrame(animation.x, animation.y, ColorTransform.NONE, 1.0f, 1.0f, 0.0f, time));
            animation.PlayTimeline();

            AddChild(animation);
            //sprite = movie.FindInstance("InstanceName");
            //List<CharacterInstance> childs = sprite.CurrentFrameChilds;            
            //Image image = new Image(Application.sharedResourceMgr.GetTexture(Res.IMG_UI_BUTTON_A));
            //image.x = 50;
            //image.y = -50;
            //childs[0].AddChild(image);            

            //BaseFont font = Application.sharedResourceMgr.GetFont(Res.FNT_FONT_TEST);
            //Text text = new Text(font, "THIS IS TEST THIS IS TEST THIS IS TEST THIS IS TEST THIS IS TEST", 180);
            //text.SetAlign(TextAlign.RIGHT);
            //text.x = 100;
            //text.y = 100;
            //text.drawBorder = true;
            //AddChild(text);

            //MusicChannel channel = Application.sharedSoundMgr.PlayMusic(Res.MUSIC_MUSIC, true);
            //channel.Volume = 0.1f;
            //Debug.WriteLine(channel.State);

            //UiComponent content = new UiComponent(100, 0);

            //Button button = new Button(0, null);
            //content.AddChild(button);

            //button = new Button(1, null);
            //content.AddChild(button);

            //UiComponent container = new UiComponent(100, 0);

            //button = new Button(2, null);
            //container.AddChild(button);

            //button = new Button(3, null);
            //container.AddChild(button);

            //container.ArrangeVert(10);
            //container.ResizeToFitChilds();
            //content.AddChild(container);
            //container.x = 10;

            //content.ArrangeVert(10);
            //content.ResizeToFitChilds();
            //content.x = 10;
            //AddChild(content);

            //button = new Button(4, null);
            //AddChild(button);

            //ArrangeVert(10);
        }        
    }
}
