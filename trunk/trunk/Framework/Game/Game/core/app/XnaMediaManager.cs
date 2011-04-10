using System.Diagnostics;
using asap.media;
using asap.sound;

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
