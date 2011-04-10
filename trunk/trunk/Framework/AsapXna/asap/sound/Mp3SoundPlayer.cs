using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using asap.sound;

namespace asap.sound
{
    public class Mp3SoundPlayer : SoundPlayer
    {
        private Song song;

        public Mp3SoundPlayer(Song song, string filename) : base(filename)
        {
            this.song = song;
        }

        public override void Play(bool looped)
        {
            MediaPlayer.Stop();
            MediaPlayer.IsRepeating = looped;
            MediaPlayer.Play(song);
        }

        public override void Pause()
        {
            if (IsPlaying())
            {
                MediaPlayer.Pause();
            }
        }

        public override void Resume()
        {
            if (IsPaused())
            {
                MediaPlayer.Play(song);
            }
        }

        public override void Stop()
        {
            if (IsPlaying() || IsPaused())
            {
                MediaPlayer.Stop();
            }
        }

        public override bool IsPaused()
        {
            return MediaPlayer.State == MediaState.Paused;
        }

        public override bool IsPlaying()
        {
            return MediaPlayer.State == MediaState.Playing;
        }
    }
}
