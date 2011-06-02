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
    public class MovieClip : DisplayObjectContainer
    {        
        private SwfPlayer player;        

        public MovieClip(SwfMovie movie) : base(movie.GetWidth(), movie.GetHeight())
        {
            player = new SwfPlayer(this);
            player.SetMovie(movie);            
        }        
       
        public void Start()
        {
            player.Play();
        }

        public override void Update(float delta)
        {
            player.Tick(delta);
            base.Update(delta);            
        } 
      
        public SwfPlayer GetPlayer()
        {
            return player;
        }

        public AnimationType AnimationType
        {
            get { return player.AnimationType; }
            set { player.AnimationType = value; }
        }
    }
}
