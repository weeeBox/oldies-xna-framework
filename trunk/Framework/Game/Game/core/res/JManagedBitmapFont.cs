using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.graphics;

namespace Flipstones2.res
{
    public class JManagedBitmapFont : BitmapFont, IDisposable
    {
        public void Dispose()
        {
            XnaImage img = (XnaImage)fontImage;
            img.Unload();
        }
    }
}
