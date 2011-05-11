using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace asap.resources
{
    public class GameSound : BaseSound
    {
        private SoundEffect effect;

        public GameSound(SoundEffect effect)
        {
            this.effect = effect;
        }

        public override void Dispose()
        {
            if (effect != null)
            {
                effect.Dispose();
                effect = null;
            }
        }

        public SoundEffectInstance CreateInstance()
        {
            return effect.CreateInstance();
        }
    }
}
