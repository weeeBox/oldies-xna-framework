using System;

using System.Collections.Generic;


using java.asap.sound;
using java.asap.graphics;
using System.Diagnostics;

namespace java.asap.resources
{
    abstract public class ResFactory
     {
        private static ResFactory instance;
        
        public static ResFactory GetInstance()
        {
            return ResFactory.instance;
        }
        
        public ResFactory() 
        {
            Debug.Assert((ResFactory.instance) == null);
            ResFactory.instance = this;
        }               
        
        public abstract Image CreateImage(String path) /* throws Exception */;        
        public abstract SoundPlayer CreateSoundPlayer(String fileName, bool streaming);
        
    }
    
    
}