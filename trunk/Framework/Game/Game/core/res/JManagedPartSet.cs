using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.anim;

namespace Flipstones2.res
{
    public class JManagedPartSet : PartSet, IDisposable
    {
        public void Dispose()
        {
            XnaImage img = (XnaImage)image;
            img.Unload();
        }
    }
}
