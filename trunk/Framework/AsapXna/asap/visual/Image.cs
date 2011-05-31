using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.visual;
using asap.graphics;

namespace asap.visual
{
    public class Image : DisplayObject
    {
        protected GameTexture texture;

        public Image()
        {
        }

        public Image(GameTexture texture)
        {
            SetTexture(texture);
        }

        public virtual void SetTexture(GameTexture texture)
        {
            this.texture = texture;
            if (texture == null)
            {
                width = height = 0;
            }
            else
            {
                this.width = texture.GetWidth();
                this.height = texture.GetHeight();
            }
        }

        public override void Draw(Graphics g)
        {
            PreDraw(g);
            g.DrawImage(texture, 0, 0);
            PostDraw(g);
        }
    }
}
