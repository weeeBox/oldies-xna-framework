using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.resources;
using Microsoft.Xna.Framework.Media;

namespace asap.sound
{
    public class MusicChannel : ISoundChannel
    {
        private GameMusic music;

        public void Play(GameMusic music, bool looped)
        {
            this.music = music;

            MediaPlayer.Play(music.GetSong());
            Looped = looped;
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }

        public void Pause()
        {
            MediaPlayer.Pause();
        }

        public void Resume()
        {
            MediaPlayer.Resume();
        }

        public float Volume
        {
            get
            {
                return MediaPlayer.Volume;
            }
            set
            {
                MediaPlayer.Volume = value;
            }
        }                

        public bool Looped
        {
            get
            {
                return MediaPlayer.IsRepeating;
            }
            set
            {
                MediaPlayer.IsRepeating = value;
            }
        }

        public SoundChannelState State
        {
            get
            {
                MediaState state = MediaPlayer.State;
                if (state == MediaState.Playing)
                {
                    return SoundChannelState.PLAYING;
                }

                if (state == MediaState.Paused)
                {
                    if (MediaPlayer.PlayPosition == music.GetSong().Duration && !Looped)
                    {
                        return SoundChannelState.STOPPED;
                    }

                    return SoundChannelState.PAUSED;
                }

                return SoundChannelState.STOPPED;
            }            
        }
    }
}
