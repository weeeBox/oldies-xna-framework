using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.graphics;

namespace asap.anim.objects
{
    public class BitmapInstance : CharacterInstance
    {
        private Image image;

        public BitmapInstance(Image image)
        {
            this.image = image;
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(image, 0, 0, 0);
        }
    }
}
