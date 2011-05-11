using System;

using System.Collections.Generic;


using asap.resources;
using asap.util;
using System.Diagnostics;

namespace asap.graphics
{
    public struct CharInfo
    {
        public char chr;
        public short x;
        public short y;
        public sbyte width;
        public sbyte height;
        public sbyte ox;
        public sbyte oy;
    }

    public class BitmapFont : BaseFont
    {
        private sbyte ascender;

        private sbyte descender;

        private sbyte internalLeading;

        private float charOffset;

        private sbyte externalLeading;

        private short spaceWidth;

        public Dictionary<char, int> charsIndices;

        public CharInfo[] chars;
                
        public GameTexture fontTexture;

        public BitmapFont(GameTexture fontTexture, int charsCount)
        {
            this.fontTexture = fontTexture;
            charsIndices = new Dictionary<char, int>(charsCount);
            chars = new CharInfo[charsCount];
        }

        public void SetCharInfo(CharInfo info, int index)
        {
            Debug.Assert(index >= 0 && index < chars.Length);
            Debug.Assert(!charsIndices.ContainsKey(info.chr), "Can't add char twice: '" + info.chr +"' at " + index);
            chars[index] = info;
            charsIndices.Add(info.chr, index);
        }

        public override void DrawString(Graphics g, String str, float x, float y)
        {
            int len = str.Length;
            int num;
            for (int i = 0; i < len; i++)
            {
                num = GetCharIndex(str[i]);                
                DrawChar(g, ref chars[num], x, y);
                x += chars[num].width + charOffset;
            }
        }

        public override void DrawChar(Graphics g, char ch, float x, float y)
        {
            int charIndex = GetCharIndex(ch);
            DrawChar(g, ref chars[charIndex], x, y);
        }

        private void DrawChar(Graphics g, ref CharInfo info, float x, float y)
        {
            int charW = info.width;
            int charH = info.height;
            if (charH > 0 && charW > 0)
            {
                g.DrawImage(fontTexture, info.x, info.y, charW, charH, x + info.ox, y + info.oy - InternalLeading);
            }
        }

        public override void DrawString(Graphics g, String str, float x, float y, int anchor)
        {
            //int width = GetStringWidth(str);
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

        public sbyte InternalLeading
        {
            get { return internalLeading; }
            set { internalLeading = value; }
        }

        public sbyte Ascender
        {
            get { return ascender; }
            set { ascender = value; }
        }

        public sbyte Descender
        {
            get { return descender; }
            set { descender = value; }
        }        

        public float CharOffset
        {
            get { return charOffset; }
            set { charOffset = value; }
        }

        public sbyte ExternalLeading
        {
            get { return externalLeading; }
            set { externalLeading = value; }
        }

        public short SpaceWidth
        {
            get { return spaceWidth; }
            set { spaceWidth = value; }
        }

        public override int GetHeight()
        {
            return ascender - internalLeading;
        }

        public override int GetLineOffset()
        {
            return internalLeading + externalLeading;
        }        

        public override float GetStringWidth(String str)
        {
            float w = 0;
            for (int i = 0; i < str.Length; ++i)
            {
                w += GetCharWidth(str[i]) + charOffset;
            }
            if (str.Length >= 1)
            {
                w -= charOffset;
            }

            return w;
        }

        public override int GetCharWidth(char ch)
        {
            int index = GetCharIndex(ch);
            return chars[index].width;
        }        

        private int GetCharIndex(char ch)
        {            
            int index;
            charsIndices.TryGetValue(ch, out index); // 0 in case of failure

            return index;
        }        

        public override List<FormattedString> WrapString(string text, float width)
        {
            List<FormattedString> strings = new List<FormattedString>();

            int strLen = text.Length;            
            int xc = 0;
            float wordWidth = 0;
            int strStartIndex = 0;
            int wordLastCharIndex = 0;
            float stringWidth = 0;
            int charIndex = 0;
            while (charIndex < strLen)
            {
                int curCharIndex = charIndex;
                char curChar = text[curCharIndex];
                charIndex++;
                if ((curChar == ' ') || (curChar == '\n'))
                {
                    wordLastCharIndex = curCharIndex;
                    if ((stringWidth == 0) && (wordWidth > 0))
                        wordWidth -= charOffset;

                    stringWidth += wordWidth;
                    wordWidth = 0;
                    xc = charIndex;
                    if (curChar == ' ')
                    {
                        xc--;
                        wordWidth = GetCharWidth(curChar) + charOffset;
                    }
                }
                else
                {
                    wordWidth += GetCharWidth(curChar) + charOffset;
                }
                if ((((stringWidth + wordWidth) > width) && (wordLastCharIndex != strStartIndex)) || (curChar == '\n'))
                {
                    string sub = text.Substring(strStartIndex, wordLastCharIndex - strStartIndex);
                    strings.Add(new FormattedString(sub, GetStringWidth(sub)));

                    char xcc;
                    while (xc < text.Length && (xcc = text[xc]) == ' ')
                    {
                        wordWidth -= GetCharWidth(xcc) + charOffset;
                        xc++;
                    }
                    wordWidth -= charOffset;
                    strStartIndex = xc;
                    wordLastCharIndex = strStartIndex;
                    stringWidth = 0;
                }
            }
            if (wordWidth != 0)
            {
                string sub = text.Substring(strStartIndex, strLen - strStartIndex);
                strings.Add(new FormattedString(sub, GetStringWidth(sub)));
            }
            return strings;
        }
    }
}