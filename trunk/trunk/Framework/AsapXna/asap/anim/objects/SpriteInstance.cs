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

        public SpriteInstance(DefineSprite sprite, SwfMovie movie) : base(CharacterConstansts.SPRITE, sprite.GetCharacterId())
        {            
            player = new SwfPlayer(this);
            player.SetMovie(movie);
            player.FramesCount = sprite.GetFrameCount();
            player.Frames = sprite.GetFrames();
            player.Play();
            player.AnimationType = AnimationType.LOOP;            
        }                                
        
        public bool HasName
        {
            get { return name != null; }
        }             

        public override void Update(float delta)
        {
            player.Tick(delta);
            base.Update(delta);            
        }
    }
}
