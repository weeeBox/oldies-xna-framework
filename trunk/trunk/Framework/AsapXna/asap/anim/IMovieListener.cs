using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.anim;

namespace asap.anim
{
    public interface IMovieListener
    {
        void EnterFrame(SwfPlayer player);
        void AnimationFinished();
    }
}
