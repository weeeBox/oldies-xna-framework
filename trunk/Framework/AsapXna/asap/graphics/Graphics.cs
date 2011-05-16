using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace asap.graphics
{
    public class Graphics
    {
        private int width;
        private int height;        

        private Rectangle srcRect;
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

        public void DrawImage(GameTexture img, int x_src, int y_src, int width, int height, float x, float y)
        {            
            Texture2D tex = img.getTexture();
            srcRect.X = x_src + img.getX();
            srcRect.Y = y_src + img.getY();
            srcRect.Width = width;
            srcRect.Height = height;
            AppGraphics.DrawImage(tex, ref srcRect, x, y);
        }        

        public void DrawLine(int x1, int y1, int x2, int y2, Color color)
        {
            AppGraphics.DrawLine(x1, y1, x2, y2, color);
        }

        public void DrawRect(int x, int y, float width, float height, Color color)
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