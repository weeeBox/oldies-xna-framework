using System;

using System.Collections.Generic;



namespace asap.graphics
{
    abstract public class Graphics
     {
        public const int TRANS_NONE = 0;
        
        public const int TRANS_ROT90 = 5;
        
        public const int TRANS_ROT180 = 3;
        
        public const int TRANS_ROT270 = 6;
        
        public const int TRANS_MIRROR = 2;
        
        public const int TRANS_MIRROR_ROT90 = 7;
        
        public const int TRANS_MIRROR_ROT180 = 1;
        
        public const int TRANS_MIRROR_ROT270 = 4;
        
        public const int HCENTER = 1;
        
        public const int VCENTER = 2;
        
        public const int LEFT = 4;
        
        public const int RIGHT = 8;
        
        public const int TOP = 16;
        
        public const int BOTTOM = 32;
        
        public const int BASELINE = 64;
        
        public abstract void ClipRect(int x, int y, int width, int height);
        
        public abstract void DrawArc(int x, int y, int width, int height, int startAngle, int arcAngle);
        
        public abstract void DrawChar(char character, int x, int y, int anchor);
        
        public abstract void DrawChars(char[] data, int offset, int Length, int x, int y, int anchor);
        
        public abstract void DrawImage(Image img, int x, int y, int anchor);
        
        public abstract void DrawImageSector(Image img, int x, int y, int startAngle, int angle);
        
        public abstract void DrawLine(int x1, int y1, int x2, int y2);
        
        public abstract void DrawRect(int x, int y, int width, int height);
        
        public abstract void DrawRegion(Image src, int x_src, int y_src, int width, int height, int transform, int x_dst, int y_dst, int anchor);
        
        public abstract void DrawRGB(int[] rgbData, int offset, int scanlength, int x, int y, int width, int height, bool processAlpha);
        
        public abstract void DrawRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight);
        
        public abstract void DrawString(String str, int x, int y, int anchor);
        
        public abstract void DrawSubstring(String str, int offset, int len, int x, int y, int anchor);
        
        public abstract void FillArc(int x, int y, int width, int height, int startAngle, int arcAngle);
        
        public abstract void FillRect(int x, int y, int width, int height);
        
        public abstract void FillRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight);
        
        public abstract int GetBlueComponent();
        
        public abstract int GetClipHeight();
        
        public abstract int GetClipWidth();
        
        public abstract int GetClipX();
        
        public abstract int GetClipY();
        
        public abstract int GetColor();
        
        public abstract int GetTranslateX();
        
        public abstract int GetTranslateY();
        
        public abstract void SetClip(int x, int y, int width, int height);
        
        public abstract void SetColor(int RGB);
        
        public abstract void Translate(int x, int y);
        
        public abstract void FillTriangle(int x1, int y1, int x2, int y2, int x3, int y3);
        
        public abstract void SetAlpha(int alpha);
        
        public abstract int GetAlpha();
        
        public abstract Graphics Reset();
        
        public abstract void PushTransform();
        
        public abstract void PopTransform();
        
        public abstract void Rotate(double theta);
        
        public abstract void Rotate(double theta, double x, double y);
        
        public abstract void Scale(double sx, double sy);
        
        public abstract void Shear(double shx, double shy);
        
    }
    
    
}