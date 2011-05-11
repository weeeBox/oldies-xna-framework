using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.resources;
using Microsoft.Xna.Framework.Audio;

namespace asap.sound
{
    public class SoundChannel : ISoundChannel
    {
        private GameSound sound;
        private SoundEffectInstance soundInstance;

        public void Play(GameSound sound, SoundTransform transform, bool looped)
        {
            this.sound = sound;
            if (soundInstance != null)
            {
                Stop();
            }
            soundInstance = sound.CreateInstance();
            Volume = transform.volume;
            Pitch = transform.pitch;
            Pan = transform.pan;
            Looped = looped;

            soundInstance.Play();
        }

        public void Stop()
        {
            soundInstance.Stop(true);
            soundInstance.Dispose();
        }

        public void Pause()
        {
            soundInstance.Pause();
        }

        public void Resume()
        {
            soundInstance.Resume();
        }

        public float Volume
        {
            get
            {
                return soundInstance.Volume;
            }
            set
            {
                soundInstance.Volume = value;
            }
        }

        public float Pitch
        {
            get
            {
                return soundInstance.Pitch;
            }
            set
            {
                soundInstance.Pitch = value;
            }
        }

        public float Pan
        {
            get
            {
                return soundInstance.Pan;
            }
            set
            {
                soundInstance.Pan = value;
            }
        }

        public bool Looped
        {
            get
            {
                return soundInstance.IsLooped;
            }
            set
            {
                soundInstance.IsLooped = value;
            }
        }

        public SoundChannelState State
        {
            get
            {
                SoundState state = soundInstance.State;
                if (state == SoundState.Playing)
                {
                    return SoundChannelState.PLAYING;
                }

                if (state == SoundState.Paused)
                {
                    return SoundChannelState.PAUSED;
                }

                return SoundChannelState.STOPPED;
            }
        }
    }
}
