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
        private Image[] images;

        private Image texture;

        public SwfPartset(Image texture, int size)
        {            
            this.texture = texture;
            images = new Image[size];
        }        

        public void SetPart(int index, int x, int y, int width, int height)
        {
            Debug.Assert(index >= 0 && index < images.Length);
            Debug.Assert(images[index] == null, "Already exists: " + index);

            Image image = new Image();
            image.setTexture(this, texture, x, y, width, height);
            images[index] = image; 
        }

        public Image this[int imageIndex]
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
                foreach (Image img in images)
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
