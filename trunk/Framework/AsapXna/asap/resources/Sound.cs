using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace asap.resources
{
    public class Sound : BaseSound
    {
        private SoundEffect effect;

        public Sound(SoundEffect effect)
        {
            this.effect = effect;
        }

        public void Dispose()
        {
            if (effect != null)
            {
                effect.Dispose();
                effect = null;
            }
        }
    }
}
