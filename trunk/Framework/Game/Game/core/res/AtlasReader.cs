using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Flipstones2.gfx;

namespace Flipstones2.res
{
    public class AtlasReader : ContentTypeReader<Atlas>
    {
        protected override Atlas Read(ContentReader input, Atlas existingInstance)
        {
            string textureName = input.ReadString();
            int imagesCount = input.ReadInt32();

            Atlas atlas = new Atlas(textureName, imagesCount);
            for (int imageIndex = 0; imageIndex < imagesCount; ++imageIndex)
            {
                SubTexData part = new SubTexData();

                part.texId = input.ReadString();
                part.x = input.ReadInt32();
                part.y = input.ReadInt32();
                part.w = input.ReadInt32();
                part.h = input.ReadInt32();                
                                
                atlas.Add(part);
            }

            return atlas;
        }
    }
}
