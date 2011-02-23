using System;

using System.Collections.Generic;
using System.IO;



namespace asap.media
{
    public class Manager
     {
        public static Player CreatePlayer(String locator)
        {
            return ManagerImpl.GetInstance().CreatePlayer(locator);
        }        
    }    
}