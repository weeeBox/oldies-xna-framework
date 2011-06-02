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
using asap.core;

namespace Game.app.menu
{
    public class MainScreen : Screen
    {
        private MovieClip movie;

        public MainScreen() : base(ScreenId.MAIN_MENU)
        {
            movie = new MovieClip(Application.sharedResourceMgr.GetMovie(Res.ANI_ANIM));
            movie.AnimationType = AnimationType.LOOP;
            movie.alignX = movie.parentAlignX = 0.5f;
            movie.Play();
            AddChild(movie);
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

        public override bool KeyPressed(asap.core.KeyEvent evt)
        {
            if (evt.code == KeyCode.VK_Right)
            {
                movie.NextFrame();
                return true;
            }
            else if (evt.code == KeyCode.VK_Left)
            {
                movie.PrevFrame();
                return true;
            }

            return base.KeyPressed(evt);
        }
    }
}
