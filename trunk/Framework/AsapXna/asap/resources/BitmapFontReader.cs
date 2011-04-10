using System.Collections.Generic;
using asap.graphics;
using asap.resources;
using Microsoft.Xna.Framework.Content;

namespace asap.resouces
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content
    /// Pipeline to read the specified data type from binary .xnb format.
    /// 
    /// Unlike the other Content Pipeline support classes, this should
    /// be a part of your main game project, and not the Content Pipeline
    /// Extension Library project.
    /// </summary>
    public class BitmapFontReader : ContentTypeReader<BitmapFont>
    {
        protected override BitmapFont Read(ContentReader input, BitmapFont existingInstance)
        {
            string texture = input.ReadString();
            Image fontImage = ResFactory.GetInstance().LoadManagedImage(texture);

            BitmapFont font = new BitmapFont();
            font.fontImage = fontImage;

            font.capHeight = input.ReadSByte();
            font.ascent = input.ReadSByte();
            font.tracking = input.ReadSByte();
            font.lineHeight = input.ReadSByte();
            font.descent = input.ReadSByte();            
            int charsCnt = input.ReadInt16();
            sbyte[] charsAscent = new sbyte[charsCnt];
            short[] charsOx = new short[charsCnt];
            short[] charsOy = new short[charsCnt];
            sbyte[] charsW = new sbyte[charsCnt];
            sbyte[] charsH = new sbyte[charsCnt];
            Dictionary<char, int> charIndices = new Dictionary<char, int>(charsCnt);
            char c;
            for (int i = 0; i < charsCnt; i++)
            {
                c = input.ReadChar();
                charIndices.Add(c, i);
                charsAscent[i] = input.ReadSByte();
                charsOx[i] = input.ReadInt16();
                charsOy[i] = input.ReadInt16();
                charsW[i] = input.ReadSByte();
                charsH[i] = input.ReadSByte();
            }

            font.charsAscent = charsAscent;
            font.charsOx = charsOx;
            font.charsOy = charsOy;
            font.charsW = charsW;
            font.charsH = charsH;
            font.charIndices = charIndices;

            return font;
        }
    }
}
