using asap.resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace asap.graphics
{
    public class GameTexture : ManagedResource
    {
        private Texture2D tex;
        private Rectangle bounds;

        private Atlas atlas;

        public GameTexture()
        {

        }

        public GameTexture(Texture2D texture)
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

        public int GetWidth()
        {
            return bounds.Width;
        }

        public int GetHeight()
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
            get { return atlas != null; }
        }

        public void Unload()
        {
            if (tex != null)
            {
                if (atlas == null)
                {
                    tex.Dispose();
                    tex = null;
                }
                else
                {                    
                    atlas = null;
                    tex = null;
                }
            }
        }

        public override void Dispose()
        {
            Unload();
        }       

        public void setTexture(Atlas atlas, int x, int y, int w, int h)
        {
            Unload();

            this.tex = atlas.Texture.getTexture();
            bounds.X = x;
            bounds.Y = y;
            bounds.Width = w;
            bounds.Height = h;
            this.atlas = atlas;            
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