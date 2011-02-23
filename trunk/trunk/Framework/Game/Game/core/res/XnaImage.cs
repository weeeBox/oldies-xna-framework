using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Flipstones2.gfx;

namespace Flipstones2.res
{
    public class XnaImage : Image, IDisposable
    {
        private Texture2D tex;
        private Rectangle bounds;       

        private TextureManager tm;        

        public XnaImage()
        {

        }

        public XnaImage(Texture2D texture)
        {
            setTexture(texture);
        }

        public int getX()
        {
            return bounds.X;
        }

        public int getY()
        {
            return bounds.Y;
        }

        public override int GetWidth()
        {
            return bounds.Width;
        }

        public override int GetHeight()
        {
            return bounds.Height;
        }

        public void CopyBounds(ref Rectangle r)
        {
            r.X = bounds.X;
            r.Y = bounds.Y;
            r.Width = bounds.Width;
            r.Height = bounds.Height;
        }

        public Rectangle Bounds
        {
            get { return bounds; }
        }

        public bool IsAdv
        {
            get { return tm != null; }
        }

        public void Unload()
        {
            if (tex != null)
            {
                if (tm == null)
                {
                    tex.Dispose();
                    tex = null;
                }
                else
                {
                    tm.UnloadTexture(tex);
                    tm = null;
                    tex = null;
                }
            }
        }

        public void Dispose()
        {
            Unload();
        }

        public void setTexture(TextureManager tm, Texture2D t, int x, int y, int w, int h)
        {
            Unload();

            this.tex = t;
            bounds.X = x;
            bounds.Y = y;
            bounds.Width = w;
            bounds.Height = h;
            this.tm = tm;            
        }

        public void setTexture(Texture2D t)
        {
            Unload();

            tex = t;
            bounds.Width = tex.Width;
            bounds.Height = tex.Height;
            bounds.X = 0;
            bounds.Y = 0;
        }

        public Texture2D getTexture()
        {
            return tex;
        }
    }
}
