using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace asap.resources
{
    public class Music : BaseSound
    {
        private Song song;

        public Music(Song song)
        {
            this.song = song;
        }

        public override void Dispose()
        {
            if (song != null)
            {
                song.Dispose();
                song = null;
            }
        }
    }
}
