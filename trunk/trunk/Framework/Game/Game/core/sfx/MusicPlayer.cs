using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Flipstones2.app;
using System.Diagnostics;
using asap.media;
using asap.sound;

namespace Flipstones2.sfx
{
    public class MusicPlayer : Player
    {
        SoundPlayer player;

        public MusicPlayer(string filename)
        {
            player = XnaResFactory.GetInstance().CreateSoundPlayer(filename, false);
        }

        public override void Realize()
        {
            Debug.WriteLine("Music realize");
        }

        public override void Prefetch()
        {
            Debug.WriteLine("Music frefetch");
        }

        public override void Start()
        {
            player.Play(true);
        }

        public override void Stop()
        {
            player.Stop();
        }

        public override void SetLoopCount(int count)
        {
        }

        public override void Deallocate()
        {
        }

        public override void Close()
        {
        }
    }
}
