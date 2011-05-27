using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace asap.graphics
{
    public enum Anchor
    {
        HCENTER = 1,        
        VCENTER = 2,        
        LEFT = 4,        
        RIGHT = 8,        
        TOP = 16,        
        BOTTOM = 32,
        CENTER = HCENTER | VCENTER
    }

    public class Graphics
    {
        private int width;
        private int height;        

        private Rectangle srcRect;
        private Rectangle dstRect;

        public Color color;

        public Graphics(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
         
        public void Begin(GraphicsDevice gd)
        {
            AppGraphics.Begin(gd, width, height);
            color = Color.White;
        }

        public void End()
        {
            AppGraphics.End();
        }        

        public void FillRect(float x, float y, float width, float height, Color color)
        {            
            AppGraphics.FillRect(x, y, width, height, color);
        }

        public void DrawImage(GameTexture img, float x, float y)
        {
            DrawImage(img, 0, 0, img.GetWidth(), img.GetHeight(), x, y);
        }

        public void DrawImage(GameTexture img, float x, float y, float alpha)
        {
            Color oldColor = AppGraphics.GetColor();
            Color drawColor = Color.White * alpha;
            AppGraphics.SetColor(drawColor);
            DrawImage(img, 0, 0, img.GetWidth(), img.GetHeight(), x, y);
            AppGraphics.SetColor(oldColor);
        }

        public void DrawImage(GameTexture img, float x, float y, Anchor anchor)
        {
            int width = img.GetWidth();
            int height = img.GetHeight();

            if ((anchor & Anchor.RIGHT) != 0)
            {
                x -= width;
            }
            else if ((anchor & Anchor.HCENTER) != 0)
            {
                x -= 0.5f * width;
            }

            if ((anchor & Anchor.BOTTOM) != 0)
            {
                y -= height;
            }
            else if ((anchor & Anchor.VCENTER) != 0)
            {
                y -= 0.5f * height;
            }

            DrawImage(img, 0, 0, img.GetWidth(), img.GetHeight(), x, y);
        }

        public void DrawImage(GameTexture img, int xSrc, int ySrc, int width, int height, float x, float y)
        {            
            Texture2D tex = img.getTexture();
            srcRect.X = xSrc + img.getX();
            srcRect.Y = ySrc + img.getY();
            srcRect.Width = width;
            srcRect.Height = height;
            AppGraphics.DrawImage(tex, ref srcRect, x, y);
        }        

        public void DrawTiled(GameTexture img, int xSrc, int ySrc, int srcWidth, int srcHeight, int x, int y, int width, int height)
        {
            Texture2D tex = img.getTexture();
            srcRect.X = xSrc + img.getX();
            srcRect.Y = ySrc + img.getY();            
            
            int tiledHeight = height;
            while (tiledHeight > 0)
            {
                int partHeight = srcHeight < tiledHeight ? srcHeight : tiledHeight;
                tiledHeight -= partHeight;
                srcRect.Height = partHeight;

                int tiledWidth = width;
                while (tiledWidth > 0)
                {
                    int partWidth = srcWidth < tiledWidth ? srcWidth : tiledWidth;
                    tiledWidth -= partWidth;

                    srcRect.Width = partWidth;

                    AppGraphics.DrawImage(tex, ref srcRect, x, y);
                    x += partWidth;
                }
                y += partHeight;
            }
        }

        public void DrawLine(int x1, int y1, int x2, int y2, Color color)
        {
            AppGraphics.DrawLine(x1, y1, x2, y2, color);
        }

        public void DrawRect(float x, float y, float width, float height, Color color)
        {            
            AppGraphics.DrawRect(x, y, width, height, color);
        }        

        public void Translate(float x, float y)
        {                    
            AppGraphics.Translate(x, y);
        }

        public void PushTransform()
        {
            AppGraphics.PushMatrix();
        }

        public void PopTransform()
        {
            AppGraphics.PopMatrix();
        }

        public void SetTransform(ref Matrix m)        
        {
            AppGraphics.SetMatrix(m);
        }

        public void AddTransform(ref Matrix m)
        {
            AppGraphics.Transform(ref m);
        }
    }    
}