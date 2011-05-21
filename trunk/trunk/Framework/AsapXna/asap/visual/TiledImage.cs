using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.graphics;
using Microsoft.Xna.Framework;

namespace asap.visual
{
    public class TiledImage : Image
    {
        private Rectangle src;        

        public TiledImage(GameTexture texture, float widht, float height) : this(texture, widht, height, new Rectangle(0, 0, texture.GetWidth(), texture.GetHeight()))
        {
        }

        public TiledImage(GameTexture texture, float width, float height, Rectangle src) : base(texture)
        {
            this.src = src;
            this.width = width;
            this.height = height;
        }        

        public override void Draw(Graphics g)
        {
            PreDraw(g);
            g.DrawTiled(texture, src.X, src.Y, src.Width, src.Height, 0, 0, (int)width, (int)height);
            PostDraw(g);
        }
    }
}
