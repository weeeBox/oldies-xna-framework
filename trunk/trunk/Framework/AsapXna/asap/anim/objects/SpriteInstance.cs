using System;
using System.Collections.Generic;
using System.Diagnostics;
using asap.graphics;
using Microsoft.Xna.Framework;
using swiff.com.jswiff.swfrecords;
using swiff.com.jswiff.swfrecords.tags;

namespace asap.anim.objects
{
    public class SpriteInstance : CharacterInstance
    {
        private SwfPlayer player;        

        public SpriteInstance(DefineSprite sprite, SwfMovie movie)
        {            
            player = new SwfPlayer();
            player.SetMovie(movie);
            player.FramesCount = sprite.GetFrameCount();
            player.Tags = sprite.GetControlTags();
            player.Start();
        }                                
        
        public override void Draw(Graphics g)
        {
            base.PreDraw(g);
            player.Draw(g);
            base.PostDraw(g);
        }        

        public override void Update(float delta)
        {
            base.Update(delta);
            player.Tick(delta);
        }
    }
}
