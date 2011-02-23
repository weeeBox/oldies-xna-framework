using System;

using System.Collections.Generic;



namespace asap.app
{
    public interface AppImpl
    {
        String GetProperty(String name);
        void Stop();
        bool SetOrientation(int orientation);        
    }    
}