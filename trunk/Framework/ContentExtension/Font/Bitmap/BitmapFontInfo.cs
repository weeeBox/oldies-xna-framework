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
        public sbyte w;
        public sbyte h;
        public sbyte ox;
        public sbyte oy;

        public CharInfo(char chr, short x, short y, sbyte w, sbyte h, sbyte ox, sbyte oy)
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

        public sbyte InternalLeading { get; internal set; }

        public sbyte Ascender { get; internal set; }

        public sbyte Descender { get; internal set; }

        public sbyte ExternalLeading { get; internal set; }

        public float CharOffset { get; internal set; }

        public sbyte SpaceWidth { get; internal set; }        

        public short CharsCount
        {
            get { return (short)chars.Count; }
        }        
    }   
}
