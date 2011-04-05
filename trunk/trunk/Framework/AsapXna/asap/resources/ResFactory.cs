using System;

using System.Collections.Generic;


using asap.sound;
using asap.graphics;
using System.Diagnostics;
using asap.anim;

namespace asap.resources
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
                
        public abstract BitmapFont LoadFont(String path);
        public abstract Image LoadImage(String path) /* throws Exception */;
        public abstract StringsPack LoadStrings(string path);
        public virtual void UnloadResource(object res) {}
        
        public abstract SoundPlayer CreateSoundPlayer(String fileName, bool streaming);        
    }
    
}