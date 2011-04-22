using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using asap.graphics;
using Microsoft.Xna.Framework.Graphics;

namespace asap.anim
{
    public class SwfPartset : ITextureManager, IDisposable
    {
        private GameTexture[] images;

        private GameTexture texture;

        public SwfPartset(GameTexture texture, int size)
        {            
            this.texture = texture;
            images = new GameTexture[size];
        }        

        public void SetPart(int index, int x, int y, int width, int height)
        {
            Debug.Assert(index >= 0 && index < images.Length);
            Debug.Assert(images[index] == null, "Already exists: " + index);

            GameTexture image = new GameTexture();
            image.setTexture(this, texture, x, y, width, height);
            images[index] = image; 
        }

        public GameTexture this[int imageIndex]
        {
            get 
            {
                Debug.Assert(imageIndex >= 0 && imageIndex < images.Length);
                return images[imageIndex];
            }
        }

        public void Dispose()
        {
            if (images != null)
            {
                foreach (GameTexture img in images)
                {
                    img.Unload();
                }
                images = null;
            }
            if (texture != null)
            {
                texture.Unload();
                texture.Dispose();
                texture = null;
            }
        }

        public void UnloadTexture(Texture2D tex)
        {
            // do nothing
        }
    }
}
