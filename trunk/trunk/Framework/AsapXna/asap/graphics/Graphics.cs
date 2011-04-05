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

        private int transX;
        private int transY;

        private Color color;
        private int alpha;

        private Rectangle srcRect;

        public Graphics(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
         
        public void Begin(GraphicsDevice gd)
        {
            AppGraphics.Begin(gd, width, height);
            transX = transY = 0;
            color = Color.White;
            alpha = 255;
        }

        public void End()
        {
            AppGraphics.End();
        }

        public void ClipRect(int x, int y, int width, int height)
        {
            AppGraphics.ClipRect(x + transX, y + transY, width, height);
        }

        public void DrawArc(int x, int y, int width, int height, int startAngle, int arcAngle)
        {
            throw new NotImplementedException();
        }

        public void DrawChar(char character, int x, int y, int anchor)
        {
            throw new NotImplementedException();
        }

        public void DrawChars(char[] data, int offset, int Length, int x, int y, int anchor)
        {
            throw new NotImplementedException();
        }

        public void DrawImage(Image img, int x, int y, int anchor)
        {
            DrawRegion(img, 0, 0, img.GetWidth(), img.GetHeight(), 0, x, y, anchor);
        }

        public void DrawImageSector(Image img, int x, int y, int startAngle, int angle)
        {
            // throw new NotImplementedException();
            // FIXNOW: IMPLEMENT ME
        }

        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            throw new NotImplementedException();
        }

        public void DrawRect(int x, int y, int width, int height)
        {
            float fAlpha = alpha / 255.0f;
            AppGraphics.DrawRect(x, y, width, height, color * fAlpha);
        }

        public void DrawRegion(Image src, int x_src, int y_src, int width, int height, int transform, int x_dst, int y_dst, int anchor)
        {
            int anchorx = 0;
            int anchory = 0;

            //if ((anchor & RIGHT) != 0)
            //{
            //    anchorx = width;
            //}
            //else if ((anchor & HCENTER) != 0)
            //{
            //    anchorx = width >> 1;
            //}

            //if ((anchor & BOTTOM) != 0)
            //{
            //    anchory = height;
            //}
            //else if ((anchor & VCENTER) != 0)
            //{
            //    anchory = height >> 1;
            //}            

            float flipX = 0.0f;
            float flipY = 0.0f;
            int ox = 0;
            int oy = 0;
            float rotation = 0.0f;

            //switch (transform)
            //{
            //    case TRANS_NONE:
            //    {
            //        x_dst -= anchorx;
            //        y_dst -= anchory;
            //        break;
            //    }                
                
            //    case TRANS_MIRROR_ROT180:
            //    {                    
            //        ox = width >> 1;
            //        oy = height >> 1;
            //        x_dst += ox;
            //        y_dst += oy;
            //        rotation = MathHelper.Pi;
            //        flipX = 1.0f;
            //        break;
            //    }
            //    case TRANS_ROT180:
            //    {
            //        ox = width >> 1;
            //        oy = height >> 1;
            //        x_dst += ox;
            //        y_dst += oy;
            //        rotation = MathHelper.Pi;
            //        break;
            //    }
            //    case TRANS_MIRROR:
            //    {
            //        ox = width >> 1;
            //        oy = height >> 1;
            //        x_dst += ox;
            //        y_dst += oy;                    
            //        flipX = 1.0f;
            //        break;
            //    }
            //    case TRANS_MIRROR_ROT90:
            //    {
            //        ox = width >> 1;
            //        oy = height >> 1;
            //        x_dst += ox;
            //        y_dst += oy;
            //        rotation = MathHelper.PiOver2;
            //        flipX = 1.0f;
            //        break;
            //    }
            //    case TRANS_MIRROR_ROT270:
            //    {
            //        ox = width >> 1;
            //        oy = height >> 1;
            //        x_dst += ox;
            //        y_dst += oy;
            //        rotation = 3 * MathHelper.PiOver4;
            //        flipX = 1.0f;
            //        break;
            //    }
            //    case TRANS_ROT90:
            //    {
            //        ox = width >> 1;
            //        oy = height >> 1;
            //        x_dst += ox;
            //        y_dst += oy;
            //        rotation = MathHelper.PiOver2;
            //        break;
            //    }
            //    case TRANS_ROT270:                
            //    {
            //        ox = width >> 1;
            //        oy = height >> 1;
            //        x_dst += ox;
            //        y_dst += oy;
            //        rotation = 3 * MathHelper.PiOver4;
            //        break;
            //    }                
            //}

            Image img = (Image) src;
            Texture2D tex = img.getTexture();
            srcRect.X = x_src + img.getX();
            srcRect.Y = y_src + img.getY();
            srcRect.Width = width;
            srcRect.Height = height;            
            Vector2 position = new Vector2(x_dst, y_dst);
            Vector2 origin = new Vector2(ox, oy);
            Color color = Color.White;            
            Vector2 scale = new Vector2(1.0f, 1.0f);
            Vector2 flip = new Vector2(flipX, flipY);
            AppGraphics.DrawImage(tex, ref srcRect, ref position, ref color, rotation, ref origin, ref scale, ref flip);
        }

        public void DrawRGB(int[] rgbData, int offset, int scanlength, int x, int y, int width, int height, bool processAlpha)
        {
            throw new NotImplementedException();
        }

        public void DrawRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight)
        {
            throw new NotImplementedException();
        }

        public void DrawString(string str, int x, int y, int anchor)
        {
            throw new NotImplementedException();
        }

        public void DrawSubstring(string str, int offset, int len, int x, int y, int anchor)
        {
            throw new NotImplementedException();
        }

        public void FillArc(int x, int y, int width, int height, int startAngle, int arcAngle)
        {
            throw new NotImplementedException();
        }

        public void FillRect(int x, int y, int width, int height)
        {
            float fAlpha = alpha / 255.0f;            
            AppGraphics.FillRect(x, y, width, height, color * fAlpha);
        }

        public void FillRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight)
        {
            throw new NotImplementedException();
        }

        public int GetBlueComponent()
        {
            throw new NotImplementedException();
        }

        public int GetClipWidth()
        {
            return AppGraphics.GetClipWidth();
        }

        public int GetClipHeight()
        {
            return AppGraphics.GetClipHeight();
        }        

        public int GetClipX()
        {
            return AppGraphics.GetClipX() - transX;
        }

        public int GetClipY()
        {
            return AppGraphics.GetClipY() - transY;
        }

        public int GetColor()
        {
            throw new NotImplementedException();
        }

        public int GetTranslateX()
        {
            return transX;
        }

        public int GetTranslateY()
        {
            return transY;
        }

        public void SetClip(int x, int y, int width, int height)
        {
            AppGraphics.SetClip(x + transX, y + transY, width, height);
        }

        public void SetColor(int RGB)
        {
            int r = (RGB >> 16) & 0xff;
            int g = (RGB >> 8) & 0xff;
            int b = RGB & 0xff;

            color = new Color(r, g, b);
        }        

        public void Translate(int x, int y)
        {            
            transX += x;
            transY += y;
            AppGraphics.Translate(x, y, 0);
        }

        public void FillTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            throw new NotImplementedException();
        }

        public void SetAlpha(int alpha)
        {            
            Debug.Assert(alpha >= 0 && alpha <= 255);
            this.alpha = alpha;
            float fAlpha = alpha / 255.0f;
            AppGraphics.SetColor(Color.White * fAlpha);
        }

        public int GetAlpha()
        {
            return alpha;
        }

        public Graphics Reset()
        {
            AppGraphics.SetIdentity();
            return this;
        }

        public void PushTransform()
        {
            AppGraphics.PushMatrix();
        }

        public void PopTransform()
        {
            AppGraphics.PopMatrix();
        }

        public void Rotate(double theta)
        {
            AppGraphics.Rotate((float)theta, 0.0f, 0.0f, 1.0f);
        }

        public void Rotate(double theta, double x, double y)
        {
            int fx = (int)x;
            int fy = (int)y;
            AppGraphics.Translate(fx, fy, 0);
            Rotate(theta);
            AppGraphics.Translate(-fx, -fy, 0);
        }

        public void Scale(double sx, double sy)
        {
            AppGraphics.Scale((float)sx, (float)sy, 1.0f);
        }

        public void Shear(double shx, double shy)
        {
            throw new NotImplementedException();
        }               
    }    
}