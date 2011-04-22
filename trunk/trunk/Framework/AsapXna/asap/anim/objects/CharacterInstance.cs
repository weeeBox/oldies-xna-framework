using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using swiff.com.jswiff.swfrecords;
using asap.graphics;

namespace asap.anim.objects
{
    public abstract class CharacterInstance : BaseElement
    {
        private SwfMatrix matrix;        

        public SwfMatrix Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }
    }
}
