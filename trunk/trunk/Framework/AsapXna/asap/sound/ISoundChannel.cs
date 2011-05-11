using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace asap.sound
{
    public enum SoundChannelState
    {
        PLAYING,
        STOPPED,
        PAUSED,
    }

    public interface ISoundChannel
    {
        void Stop();
        void Pause();
        void Resume();
        float Volume { get; set; }        
        bool Looped { get; set; }
        SoundChannelState State { get; }
    }
}
