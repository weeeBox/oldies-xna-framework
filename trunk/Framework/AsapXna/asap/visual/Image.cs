using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.visual;
using asap.graphics;

namespace AsapXna.asap.visual
{
    public class Image : BaseElementContainer
    {
        private GameTexture texture;

        public Image(GameTexture texture) : base(texture.GetWidth(), texture.GetHeight())
        {
            this.texture = texture;
        }

        public override void Draw(Graphics g)
        {
            PreDraw(g);
            g.DrawImage(texture, 0, 0);
            PostDraw(g);
        }
    }
}
