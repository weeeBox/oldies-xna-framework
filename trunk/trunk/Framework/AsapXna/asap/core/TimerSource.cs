using System;

using System.Collections.Generic;



namespace asap.core
{
    public interface TimerSource : TickListener
    {
        TimerController GetTimerController();
    }
    
    
}