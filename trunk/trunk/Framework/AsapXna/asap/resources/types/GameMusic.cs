using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace asap.resources
{
    public class GameMusic : BaseSound
    {
        private Song song;

        public GameMusic(Song song)
        {
            this.song = song;
        }

        public Song GetSong()
        {
            return song;
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
