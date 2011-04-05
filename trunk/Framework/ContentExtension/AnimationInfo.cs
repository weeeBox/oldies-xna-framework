using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ContentExtension
{
    public class AnimationBin
    {
        private byte[] data;

        public AnimationBin(byte[] data)
        {
            this.data = data;
        }

        public byte[] Data
        {
            get { return data; }
        }
    }
}
