using System;

using System.Collections.Generic;
using System.Diagnostics;



namespace asap.core
{
    public class Timer
    {
        public WeakReference listener;
        
        public TimerController controller;
        
        public bool repeated;
        
        public long delay;
        
        public int heapPosition;
        
        private Object attachedObj;
        
        /** 
         * @param source Controls timer lifecycle
         * @param listener Will receive timer callback
         * @param attachedObj Any object that can be attached to timer, e.g. to identify timer instead of timer object itself
         */
        public Timer(TimerSource source ,TimerListener listener ,Object attachedObj) 
        {
            Debug.Assert((source != null) && (listener != null));
            this.controller = source.GetTimerController();
            this.listener = new WeakReference(listener);
            this.attachedObj = attachedObj;
            repeated = false;
            heapPosition = TimerController.UNSCHEDULED;
        }
        
        /** 
         * @param source Controls timer lifecycle
         * @param listener Will receive timer callback
         */
        public Timer(TimerSource source ,TimerListener listener) 
         : this(source, listener, null)
        {
        }
        
        public virtual Timer ScheduleAsap()
        {
            return Schedule(0);
        }
        
        public virtual Timer Schedule(long delay)
        {
            return Schedule(delay, false);
        }
        
        public virtual Timer Schedule(long delay, bool repeated)
        {
            this.repeated = repeated;
            this.delay = delay;
            controller.RegisterTimer(this);
            return this;
        }
        
        public virtual Timer Cancel()
        {
            controller.UnregisterTimer(this);
            return this;
        }
        
        public virtual bool IsRepeated()
        {
            return repeated;
        }
        
        public virtual bool IsScheduled()
        {
            return (heapPosition) != (TimerController.UNSCHEDULED);
        }
        
        public virtual Object GetAttachedObj()
        {
            return attachedObj;
        }
        
        public virtual long GetDelay()
        {
            return delay;
        }
        
        public virtual float GetRemainingTime()
        {
            Debug.Assert(IsScheduled());
            return (controller.times[heapPosition]) - (controller.instant);
        }
        
        public virtual float GetPassedTime()
        {
            Debug.Assert(IsScheduled());
            return (delay) - (GetRemainingTime());
        }        
    }    
}