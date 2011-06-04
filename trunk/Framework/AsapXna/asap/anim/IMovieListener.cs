using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.anim;

namespace asap.anim
{
    public interface IMovieListener
    {
        void EnterLabelFrame(SwfPlayer player, string label);        
        void AnimationFinished();
    }
}
