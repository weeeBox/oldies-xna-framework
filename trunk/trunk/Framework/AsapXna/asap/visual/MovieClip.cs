using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.graphics;
using asap.anim;
using asap.core;
using asap.anim.objects;

namespace asap.visual
{
    public class MovieClip : DisplayObjectContainer, IMovieControl
    {        
        private SwfPlayer player;        

        public MovieClip(SwfMovie movie) : base(movie.GetWidth(), movie.GetHeight())
        {
            player = new SwfPlayer(this);
            player.SetMovie(movie);            
        }        

        public override void Update(float delta)
        {
            player.Tick(delta);            
        }        

        public AnimationType AnimationType
        {
            get { return player.AnimationType; }
            set { player.AnimationType = value; }
        }

        public void GotoAndPlay(int frameIndex)
        {
            player.GotoAndPlay(frameIndex);
        }

        public void GotoAndStop(int frameIndex)
        {
            player.GotoAndStop(frameIndex);
        }

        public void NextFrame()
        {
            player.NextFrame();
        }

        public void PrevFrame()
        {
            player.PrevFrame();
        }

        public void Play()
        {
            player.Play();
        }

        public void Stop()
        {
            player.Stop();
        }
    }
}
