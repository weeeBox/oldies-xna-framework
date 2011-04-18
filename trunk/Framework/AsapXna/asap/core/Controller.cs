using System;

using System.Collections.Generic;
using System.Diagnostics;

namespace asap.core
{
    public class Controller : TickListener
    {
        public const int STOP_PARAM_APP_EXIT = -1;

        public Controller parent;        

        public virtual void Start(int param)
        {

        }

        public virtual void Stop(int param)
        {
            Debug.WriteLine(GetType().Name + " stopped with param " + param);
            RootController.OnControllerStop(this, param);
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

        protected void StartController(Controller controller, int param)
        {
            RootController.StartController(controller, param);
        }

        protected void StartChildController(Controller child, int param)
        {
            RootController.StartChildController(child, param);
        }

        public virtual void OnChildStop(Controller child, int param)
        {

        }        
    }    
}