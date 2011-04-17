using System;

using System.Collections.Generic;
using System.Diagnostics;

namespace asap.core
{
    public class Controller : TickListener
    {
        public const int STOP_PARAM_APP_EXIT = -1;

        public Controller parent;

        private RootController rootController;

        public Controller(RootController rootController)
        {
            this.rootController = rootController;
        }

        public virtual void Start(int param)
        {

        }

        public virtual void Stop(int param)
        {
            Debug.WriteLine(GetType().Name + " stopped with param " + param);
            rootController.OnControllerStop(this, param);
        }        

        public virtual void Suspend()
        {

        }

        public virtual void Resume()
        {

        }

        public void Tick(float delta)
        {
            
        }        

        public virtual void OnChildStop(Controller child, int param)
        {

        }        
    }    
}