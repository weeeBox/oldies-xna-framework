using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.visual;

namespace asap.anim
{
    public interface IMovieControl
    {        
        void GotoAndPlay(int frameIndex);
        void GotoAndStop(int frameIndex);
        void NextFrame();
        void PrevFrame();
        void Play();
        void Stop();
    }
}
