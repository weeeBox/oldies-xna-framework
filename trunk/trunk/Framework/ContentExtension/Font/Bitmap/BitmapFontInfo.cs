using System;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Collections.Generic;
using System.IO;

namespace ContentExtension.Font.Bitmap
{
    public struct CharInfo
    {
        public char chr;
        public short x;
        public short y;
        public byte w;
        public byte h;
        public byte ox;
        public byte oy;

        public CharInfo(char chr, short x, short y, byte w, byte h, byte ox, byte oy)
        {
            this.chr = chr;
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.ox = ox;
            this.oy = oy;
        }
    }

    public class BitmapFontInfo
    {
        private List<CharInfo> chars;

        public BitmapFontInfo(string sourcePath)
        {
            SourceName = sourcePath;
            chars = new List<CharInfo>();
        }

        public void AddCharInfo(CharInfo e)
        {
            chars.Add(e);
        }

        public List<CharInfo> Chars
        {
            get { return chars; }
        }

        public string SourceName { get; internal set; }

        public byte InternalLeading { get; internal set; }

        public byte Ascender { get; internal set; }

        public byte Descender { get; internal set; }

        public byte ExternalLeading { get; internal set; }

        public float CharOffset { get; internal set; }

        public byte SpaceWidth { get; internal set; }        

        public short CharsCount
        {
            get { return (short)chars.Count; }
        }        
    }   
}
