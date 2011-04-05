using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using asap.resources;

namespace Flipstones2.res
{
    public class StringsPackReader : ContentTypeReader<StringsPack>
    {
        protected override StringsPack Read(ContentReader input, StringsPack existingInstance)
        {
            StringsPack pack = new StringsPack();

            int stringsCount = input.ReadInt16();
            string[] strings = new string[stringsCount];

            pack.idBase = input.ReadInt16();
            for (int i = 0; i < stringsCount; ++i)
            {
                strings[i] = input.ReadString();
            }
            pack.strings = strings;

            return pack;
        }
    }
}
