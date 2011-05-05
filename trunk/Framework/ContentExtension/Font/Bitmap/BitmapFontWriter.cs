using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace ContentExtension.Font.Bitmap
{
    [ContentTypeWriter]
    public class BitmapFontWriter : ContentTypeWriterEx<BitmapFontInfo>
    {
        public BitmapFontWriter()
            : base("asap.resources.readers.BitmapFontReader")
        {
        }

        protected override void Write(ContentWriter output, BitmapFontInfo value)
        {
            output.Write(value.SourceName);

            output.Write(value.CharOffset);
            output.Write(value.LineOffset);
            output.Write(value.SpaceWidth);
            output.Write(value.FontOffset);
            
            output.Write(value.CharsCount);
            
            List<CharInfo> chars = value.Chars;
            foreach (CharInfo c in chars)
            {
                output.Write(c.chr);
                output.Write(c.x);
                output.Write(c.y);
                output.Write(c.w);
                output.Write(c.h);
                output.Write(c.ox);
                output.Write(c.oy);
            }            
        }        
    }
}
