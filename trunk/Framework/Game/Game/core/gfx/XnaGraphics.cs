using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.graphics;
using Microsoft.Xna.Framework.Graphics;
using Flipstones2.res;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Flipstones2.gfx
{
    public class XnaGraphics : Graphics
    {
        private int width;
        private int height;

        private int transX;
        private int transY;

        private Color color;
        private int alpha;

        private Rectangle srcRect;

        public XnaGraphics(int width, int height)
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

        public override void ClipRect(int x, int y, int width, int height)
        {
            AppGraphics.ClipRect(x + transX, y + transY, width, height);
        }

        public override void DrawArc(int x, int y, int width, int height, int startAngle, int arcAngle)
        {
            throw new NotImplementedException();
        }

        public override void DrawChar(char character, int x, int y, int anchor)
        {
            throw new NotImplementedException();
        }

        public override void DrawChars(char[] data, int offset, int Length, int x, int y, int anchor)
        {
            throw new NotImplementedException();
        }

        public override void DrawImage(Image img, int x, int y, int anchor)
        {
            DrawRegion(img, 0, 0, img.GetWidth(), img.GetHeight(), 0, x, y, anchor);
        }

        public override void DrawImageSector(Image img, int x, int y, int startAngle, int angle)
        {
            // throw new NotImplementedException();
            // FIXNOW: IMPLEMENT ME
        }

        public override void DrawLine(int x1, int y1, int x2, int y2)
        {
            throw new NotImplementedException();
        }

        public override void DrawRect(int x, int y, int width, int height)
        {
            float fAlpha = alpha / 255.0f;
            AppGraphics.DrawRect(x, y, width, height, color * fAlpha);
        }

        public override void DrawRegion(Image src, int x_src, int y_src, int width, int height, int transform, int x_dst, int y_dst, int anchor)
        {
            int anchorx = 0;
            int anchory = 0;

            if ((anchor & RIGHT) != 0)
            {
                anchorx = width;
            }
            else if ((anchor & HCENTER) != 0)
            {
                anchorx = width >> 1;
            }

            if ((anchor & BOTTOM) != 0)
            {
                anchory = height;
            }
            else if ((anchor & VCENTER) != 0)
            {
                anchory = height >> 1;
            }            

            float flipX = 0.0f;
            float flipY = 0.0f;
            int ox = 0;
            int oy = 0;
            float rotation = 0.0f;

            switch (transform)
            {
                case TRANS_NONE:
                {
                    x_dst -= anchorx;
                    y_dst -= anchory;
                    break;
                }                
                
                case TRANS_MIRROR_ROT180:
                {                    
                    ox = width >> 1;
                    oy = height >> 1;
                    x_dst += ox;
                    y_dst += oy;
                    rotation = MathHelper.Pi;
                    flipX = 1.0f;
                    break;
                }
                case TRANS_ROT180:
                {
                    ox = width >> 1;
                    oy = height >> 1;
                    x_dst += ox;
                    y_dst += oy;
                    rotation = MathHelper.Pi;
                    break;
                }
                case TRANS_MIRROR:
                {
                    ox = width >> 1;
                    oy = height >> 1;
                    x_dst += ox;
                    y_dst += oy;                    
                    flipX = 1.0f;
                    break;
                }
                case TRANS_MIRROR_ROT90:
                {
                    ox = width >> 1;
                    oy = height >> 1;
                    x_dst += ox;
                    y_dst += oy;
                    rotation = MathHelper.PiOver2;
                    flipX = 1.0f;
                    break;
                }
                case TRANS_MIRROR_ROT270:
                {
                    ox = width >> 1;
                    oy = height >> 1;
                    x_dst += ox;
                    y_dst += oy;
                    rotation = 3 * MathHelper.PiOver4;
                    flipX = 1.0f;
                    break;
                }
                case TRANS_ROT90:
                {
                    ox = width >> 1;
                    oy = height >> 1;
                    x_dst += ox;
                    y_dst += oy;
                    rotation = MathHelper.PiOver2;
                    break;
                }
                case TRANS_ROT270:                
                {
                    ox = width >> 1;
                    oy = height >> 1;
                    x_dst += ox;
                    y_dst += oy;
                    rotation = 3 * MathHelper.PiOver4;
                    break;
                }                
            }

            XnaImage img = (XnaImage) src;
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

        public override void DrawRGB(int[] rgbData, int offset, int scanlength, int x, int y, int width, int height, bool processAlpha)
        {
            throw new NotImplementedException();
        }

        public override void DrawRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight)
        {
            throw new NotImplementedException();
        }

        public override void DrawString(string str, int x, int y, int anchor)
        {
            throw new NotImplementedException();
        }

        public override void DrawSubstring(string str, int offset, int len, int x, int y, int anchor)
        {
            throw new NotImplementedException();
        }

        public override void FillArc(int x, int y, int width, int height, int startAngle, int arcAngle)
        {
            throw new NotImplementedException();
        }

        public override void FillRect(int x, int y, int width, int height)
        {
            float fAlpha = alpha / 255.0f;            
            AppGraphics.FillRect(x, y, width, height, color * fAlpha);
        }

        public override void FillRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight)
        {
            throw new NotImplementedException();
        }

        public override int GetBlueComponent()
        {
            throw new NotImplementedException();
        }

        public override int GetClipWidth()
        {
            return AppGraphics.GetClipWidth();
        }

        public override int GetClipHeight()
        {
            return AppGraphics.GetClipHeight();
        }        

        public override int GetClipX()
        {
            return AppGraphics.GetClipX() - transX;
        }

        public override int GetClipY()
        {
            return AppGraphics.GetClipY() - transY;
        }

        public override int GetColor()
        {
            throw new NotImplementedException();
        }

        public override int GetTranslateX()
        {
            return transX;
        }

        public override int GetTranslateY()
        {
            return transY;
        }

        public override void SetClip(int x, int y, int width, int height)
        {
            AppGraphics.SetClip(x + transX, y + transY, width, height);
        }

        public override void SetColor(int RGB)
        {
            int r = (RGB >> 16) & 0xff;
            int g = (RGB >> 8) & 0xff;
            int b = RGB & 0xff;

            color = new Color(r, g, b);
        }        

        public override void Translate(int x, int y)
        {            
            transX += x;
            transY += y;
            AppGraphics.Translate(x, y, 0);
        }

        public override void FillTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            throw new NotImplementedException();
        }

        public override void SetAlpha(int alpha)
        {            
            Debug.Assert(alpha >= 0 && alpha <= 255);
            this.alpha = alpha;
            float fAlpha = alpha / 255.0f;
            AppGraphics.SetColor(Color.White * fAlpha);
        }

        public override int GetAlpha()
        {
            return alpha;
        }

        public override Graphics Reset()
        {
            AppGraphics.SetIdentity();
            return this;
        }

        public override void PushTransform()
        {
            AppGraphics.PushMatrix();
        }

        public override void PopTransform()
        {
            AppGraphics.PopMatrix();
        }

        public override void Rotate(double theta)
        {
            AppGraphics.Rotate((float)theta, 0.0f, 0.0f, 1.0f);
        }

        public override void Rotate(double theta, double x, double y)
        {
            int fx = (int)x;
            int fy = (int)y;
            AppGraphics.Translate(fx, fy, 0);
            Rotate(theta);
            AppGraphics.Translate(-fx, -fy, 0);
        }

        public override void Scale(double sx, double sy)
        {
            AppGraphics.Scale((float)sx, (float)sy, 1.0f);
        }

        public override void Shear(double shx, double shy)
        {
            throw new NotImplementedException();
        }        
    }
}
