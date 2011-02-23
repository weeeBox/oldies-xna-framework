using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Flipstones2.sfx;
using asap.media;

namespace Flipstones2.app
{
    public class XnaMediaManager : ManagerImpl
    {
        public XnaMediaManager()
        {         
        }

        public override Player CreatePlayer(string locator)
        {
            Debug.Assert(locator.EndsWith(".mp3"));
            string filename = "song_" + locator;            
            return new MusicPlayer(filename);
        }       
    }
}
