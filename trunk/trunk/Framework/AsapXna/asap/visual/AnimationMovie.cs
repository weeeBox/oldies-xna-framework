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
    public class AnimationMovie : DisplayObjectContainer
    {        
        private SwfPlayer player;

        private bool needUpdatePlayer;

        public AnimationMovie(SwfMovie movie) : base(movie.GetWidth(), movie.GetHeight())
        {
            player = new SwfPlayer();
            player.SetMovie(movie);
            needUpdatePlayer = true;            
        }

        public AnimationMovie(SwfPlayer player)
        {
            this.player = player;
            needUpdatePlayer = false;
        }
       
        public void Start()
        {
            player.Start();
        }

        public override void Draw(Graphics g)
        {
            PreDraw(g);            
            player.Draw(g);            
            PostDraw(g);
        }

        public override void Update(float delta)
        {
            base.Update(delta);

            if (needUpdatePlayer)
            {
                player.Tick(delta);
            }
        } 
      
        public SwfPlayer GetPlayer()
        {
            return player;
        }

        public SpriteInstance FindInstance(string name)
        {
            return GetPlayer().FindInstance(name);
        }

        public List<CharacterInstance> FindInstances(int characterId)
        {
            return GetPlayer().FindInstances(characterId);
        }

        public List<CharacterInstance> FindInstancesOf(Type type)
        {
            return GetPlayer().FindInstancesOf(type);
        }

        public AnimationType AnimationType
        {
            get { return player.AnimationType; }
            set { player.AnimationType = value; }
        }
    }
}
