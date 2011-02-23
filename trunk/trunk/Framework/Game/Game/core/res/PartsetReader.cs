using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using asap.anim;
using asap.graphics;
using asap.resources;

namespace Flipstones2.res
{
    public class PartsetReader : ContentTypeReader<PartSet>
    {
        protected override PartSet Read(ContentReader input, PartSet existingInstance)
        {
            string texture = input.ReadString();
            Image image = ResFactory.GetInstance().LoadImage(texture);

            PartSet partset = new PartSet();
            partset.image = image;

            partset.partsCount = input.ReadInt16();
            int dataSize = input.ReadInt32();
            short[] partData = new short[dataSize];
            for (int i = 0; i < dataSize; ++i)
            {
                partData[i] = input.ReadInt16();
            }

            partset.partData = partData;
            return partset;
        }
    }
}
