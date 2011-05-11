using asap.graphics;
using Microsoft.Xna.Framework.Content;

namespace asap.resources.readers
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
            GameTexture fontTexture = ResFactory.GetInstance().LoadManagedImage(texture);            

            sbyte internalLeading = input.ReadSByte();
            sbyte ascender = input.ReadSByte();
            sbyte descender = input.ReadSByte();
            sbyte externalLeading = input.ReadSByte();
            float charOffset = input.ReadSingle();            
            sbyte spaceWidth = input.ReadSByte();
            
            int charsCount = input.ReadInt16();
            int totalCharsCount = charsCount + 1; // the first will be space
            BitmapFont font = new BitmapFont(fontTexture, totalCharsCount);

            // the first character is space. If user attempt to draw illegal character, the space will be drawn instead (blank space)
            CharInfo space;
            space.chr = ' ';
            space.x = 0;
            space.y = 0;
            space.width = spaceWidth;
            space.height = 0;
            space.ox = 0;
            space.oy = 0;
            font.SetCharInfo(space, 0);

            for (int charIndex = 1; charIndex < totalCharsCount; charIndex++)
            {
                CharInfo info;
                info.chr = input.ReadChar();
                info.x = input.ReadInt16();
                info.y = input.ReadInt16();
                info.width = input.ReadSByte();
                info.height = input.ReadSByte();
                info.ox = input.ReadSByte();
                info.oy = input.ReadSByte();
                font.SetCharInfo(info, charIndex);
            }

            font.InternalLeading = internalLeading;
            font.Ascender = ascender;
            font.Descender = descender;
            font.ExternalLeading = externalLeading;
            font.CharOffset = charOffset;           

            return font;
        }
    }
}
