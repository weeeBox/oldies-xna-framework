using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace ContentExtension.Font.Bitmap
{
    [ContentProcessor(DisplayName = "Bitmap Font Processor")]
    public class BitmapFontProcessor : ContentProcessor<BitmapFontInfo, BitmapFontInfo>
    {
        public override BitmapFontInfo Process(BitmapFontInfo input, ContentProcessorContext context)
        {
            return input;
        }
    }
}