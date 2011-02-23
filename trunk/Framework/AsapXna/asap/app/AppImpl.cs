using System;

using System.Collections.Generic;



namespace java.asap.app
{
    public interface AppImpl
    {
        String GetProperty(String name);
        void Stop();
        bool SetOrientation(int orientation);
        bool PlatformRequest(String url);
    }
    
    
}