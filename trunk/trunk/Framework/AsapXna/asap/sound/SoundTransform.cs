using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace asap.sound
{
    public struct SoundTransform
    {
        public static SoundTransform NONE = new SoundTransform(1.0f);

        public float volume;        
        public float pan;
        public float pitch;

        public SoundTransform(float volume) : this(volume, 0.0f, 0.0f)
        {
        }

        public SoundTransform(float volume, float pan) : this(volume, pan, 0.0f)
        {
        }

        public SoundTransform(float volume, float pan, float pitch)
        {
            this.volume = volume;
            this.pan = pan;
            this.pitch = pitch;
        }
    }
}
