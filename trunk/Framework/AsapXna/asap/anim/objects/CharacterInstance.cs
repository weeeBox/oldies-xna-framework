using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using swiff.com.jswiff.swfrecords;
using asap.graphics;

namespace asap.anim.objects
{
    public abstract class CharacterInstance
    {
        private SwfMatrix matrix;

        public abstract void Draw(Graphics g);        

        public SwfMatrix Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }
    }
}
