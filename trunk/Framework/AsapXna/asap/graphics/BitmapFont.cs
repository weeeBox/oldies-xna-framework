using System;

using System.Collections.Generic;


using asap.resources;

namespace asap.graphics
{
    public class BitmapFont : BaseFont
     {
        private const int VERSION = 103;
        
        /** 
         *
         */
        public int capHeight;
        
        /** 
         *
         */
        public int ascent;
        
        /** 
         *
         */
        public int tracking;
        
        /** 
         *
         */
        public int lineHeight;
        
        /** 
         *
         */
        public int descent;
        
        /** 
         *
         */
        public sbyte[] charsAscent;
        
        public Dictionary<char, int> charIndices;
        
        /** 
         * Массив размеров по ширине всех символов
         */
        public sbyte[] charsW;
        
        /** 
         * Массив размеров по высоте всех символов
         */
        public sbyte[] charsH;
        
        /** 
         * Расстояние по X до изображения символа в картинке шрифта.
         */
        public short[] charsOx;
        
        /** 
         * Расстояние по Y до изображения символа в картинке шрифта.
         */
        public short[] charsOy;
        
        /** 
         * Изображение шрифта
         */
        public Image fontImage;       
        
        
        /** 
         * Создает графический шрифт из потока, с разными палитрами
         *
         * @param is Входной поток с данными шрифта
         * @param palette Информация о палитрах, можно получить используя <code>ImageEx.loadPaletteDiff</code>
         * @param paletteNums номера палитр
         * @throws Exception ошибка чтения из потока
         */
        public BitmapFont() /* throws Exception */ 
        {
            //int signatureF = _is.Read();
            //int signatureN = _is.Read();
            //int signatureT = _is.Read();
            //Debug.Assert(((signatureF == 'F') && (signatureN == 'N')) && (signatureT == 'T'), "Font: unknown format");
            //int version = _is.Read();
            //Debug.Assert(version == (VERSION), "Font: unknown version");
            //capHeight = _is.Read();
            //ascent = _is.Read();
            //tracking = unchecked((sbyte)(_is.Read()));
            //lineHeight = unchecked((sbyte)(_is.Read()));
            //descent = unchecked((sbyte)(_is.Read()));
            //DataInputStream dis = new DataInputStream(_is);
            //int charsCnt = dis.ReadShort();
            //charsAscent = new sbyte[charsCnt];
            //charsOx = new short[charsCnt];
            //charsOy = new short[charsCnt];
            //charsW = new sbyte[charsCnt];
            //charsH = new sbyte[charsCnt];
            //charIndices = new Dictionary<char, int>(charsCnt);
            //char c;
            //for (int i = 0; i < charsCnt; i++) 
            //{
            //    c = dis.ReadChar();
            //    charIndices.Add(c, i);
            //    charsAscent[i] = dis.ReadByte();
            //    charsOx[i] = dis.ReadShort();
            //    charsOy[i] = dis.ReadShort();
            //    charsW[i] = dis.ReadByte();
            //    charsH[i] = dis.ReadByte();
            //}
            //fontImage = ResFactory.GetInstance().CreateImage(_is, _is.Available());
            throw new NotImplementedException();
        }
        
        public virtual void SetPalette(int paletteNum)
        {
        }
        
        public override void DrawString(Graphics g, String str, int x, int y)
        {
            int clipX = g.GetClipX();
            int clipY = g.GetClipY();
            int clipWidth = g.GetClipWidth();
            int clipHeight = g.GetClipHeight();
            if ((y > (clipY + clipHeight)) || (((y + (descent)) + (ascent)) < clipY))
                return ;
            
            int len = str.Length;
            int num;
            bool fontImageFlippedHorizontally = false;
            bool fontImageFlippedVertically = false;
            for (int i = 0; i < len; i++) 
            {
                if (x > (clipX + clipWidth))
                    break;
                
                num = GetCharIndex(str[i]);
                int charW = charsW[num];
                int charH = charsH[num];
                int charX = x;
                int charY = y;
                if (fontImageFlippedHorizontally)
                    charX += (charsW[num]) - charW;
                
                if (fontImageFlippedVertically)
                    charY += (charsH[num]) - ((charsAscent[num]) + charH);
                
                else
                    charY += (ascent) - (charsAscent[num]);
                
                if (((charX + charW) > clipX) && (charW > 0)) 
                {
                    g.DrawRegion(fontImage, charsOx[num], charsOy[num], charW, charH, charX, charY, 0);
                } 
                x += (charsW[num]) + (tracking);
            }
        }
        
        public override void DrawChar(Graphics g, char ch, int x, int y)
        {
            int num = GetCharIndex(ch);
            int charW = charsW[num];
            int charH = charsH[num];
            if (charW > 0) 
            {
                g.DrawRegion(fontImage, charsOx[num], charsOy[num], charW, charH, x, (((ascent) - (charsAscent[num])) + y), 0);
            } 
        }
        
        public override void DrawString(Graphics g, String str, int x, int y, int anchor)
        {
            int width = GetStringWidth(str);
            //if ((anchor & (Graphics.RIGHT)) != 0) 
            //{
            //    x -= width;
            //} 
            //else if ((anchor & (Graphics.HCENTER)) != 0) 
            //{
            //    x -= width >> 1;
            //} 
            //if ((anchor & (Graphics.BOTTOM)) != 0) 
            //{
            //    y -= (ascent) + (descent);
            //} 
            //else if ((anchor & (Graphics.VCENTER)) != 0) 
            //{
            //    y -= ((ascent) + (descent)) >> 1;
            //} 
            DrawString(g, str, x, y);
        }
        
        public virtual void SetTransform(int transform)
        {
        }
        
        public override int GetCapHeight()
        {
            return capHeight;
        }
        
        public override int GetAscent()
        {
            return ascent;
        }
        
        public override int GetDescent()
        {
            return descent;
        }
        
        public override int GetLineHeight()
        {
            return lineHeight;
        }
        
        public override int GetHeight()
        {
            return (descent) + (ascent);
        }
        
        public override int GetStringWidth(String str)
        {
            int w = 0;
            for (int i = (str.Length) - 1; i >= 0; i--) 
            {
                w += (GetCharWidth(str[i])) + (tracking);
            }
            if ((str.Length) >= 1)
                w -= tracking;
            
            return w;
        }
        
        public override int GetCharWidth(char ch)
        {
            return charsW[GetCharIndex(ch)];
        }
        
        public override void SetTracking(int value)
        {
            this.tracking = value;
        }
        
        public override int GetTracking()
        {
            return tracking;
        }
        
        private int GetCharIndex(char ch)
        {
            if (!charIndices.ContainsKey(ch))
                return 0;
            
            return charIndices[ch];
        }
        
        public virtual String[] WrapString(String str, int width)
        {
            return null;
        }        
    }    
}