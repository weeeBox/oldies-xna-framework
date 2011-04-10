
using System.Diagnostics;
using asap.media;
using asap.resources;

namespace asap.sound
{
    public class MusicPlayer : Player
    {
        SoundPlayer player;

        public MusicPlayer(string filename)
        {
            player = ResFactory.GetInstance().CreateSoundPlayer(filename, false);
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
