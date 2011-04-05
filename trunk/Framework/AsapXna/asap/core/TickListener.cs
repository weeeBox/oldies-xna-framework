using System;

using System.Collections.Generic;

namespace asap.core
{
    public interface TickListener
    {
        void Tick(float delta);
    }    
}