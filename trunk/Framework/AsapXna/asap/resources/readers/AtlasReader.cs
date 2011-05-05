using asap.graphics;
using AsapXna.asap.resources.types;
using Microsoft.Xna.Framework.Content;

namespace asap.resources.readers
{
    public class AtlasReader : ContentTypeReader<AtlasRes>
    {
        protected override AtlasRes Read(ContentReader input, AtlasRes existingInstance)
        {
            string textureName = input.ReadString();
            int imagesCount = input.ReadInt32();

            GameTexture texture = ResFactory.GetInstance().LoadImage(textureName);
            
            AtlasRes atlas = new AtlasRes(texture, imagesCount);
            for (int imageIndex = 0; imageIndex < imagesCount; ++imageIndex)
            {
                AtlasPartInfo part;
                
                part.x = input.ReadInt32();
                part.y = input.ReadInt32();
                part.w = input.ReadInt32();
                part.h = input.ReadInt32();
                part.ox = input.ReadInt32();
                part.oy = input.ReadInt32();

                atlas.Add(ref part);
            }

            return atlas;
        }
    }
}
