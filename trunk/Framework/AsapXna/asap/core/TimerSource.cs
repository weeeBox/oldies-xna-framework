using System;

using System.Collections.Generic;



namespace java.asap.core
{
    public interface TimerSource : TickListener
    {
        TimerController GetTimerController();
    }
    
    
}