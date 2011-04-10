using System;
using Microsoft.Xna.Framework.Audio;

namespace asap.sound
{
    public class WaveSoundPlayer : SoundPlayer
    {
        private SoundEffectInstance effectInstance;
        private SoundEffect effect;
        private string fileName;

        public WaveSoundPlayer(SoundEffect effect, string fileName) : base(fileName)
        {
            // TODO: Complete member initialization
            this.effect = effect;
            this.fileName = fileName;
        }

        public override void Play(bool looped)
        {
            Stop();

            effectInstance = effect.CreateInstance();
            effectInstance.IsLooped = looped;
            effectInstance.Play();
        }

        public override void Pause()
        {
            throw new Exception("implement me");
        }

        public override void Resume()
        {
            throw new Exception("implement me");
        }

        public override void Stop()
        {
            if (effectInstance != null)
            {
                effectInstance.Stop(true);
                effectInstance.Dispose();
            }
        }

        public override bool IsPaused()
        {
            throw new Exception("implement me");
        }

        public override bool IsPlaying()
        {
            throw new Exception("implement me");
        }

    }
}
