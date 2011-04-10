using asap.anim;
using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    public struct PartsetInfo
    {
        public int x;
        public int y;
        public int width;
        public int height;

        public PartsetInfo(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }

    public class DefineAtlas : DefinitionTag
    {
        private string texture;
        private PartsetInfo[] images;

        public DefineAtlas()
        {
            code = TagConstants.DEFINE_ATLAS;
        }

        public override void SetData(byte[] data)
        {
            using (InputBitStream stream = new InputBitStream(data))
            {
                texture = stream.ReadString();
                int imagesCount = stream.ReadUI16();
                images = new PartsetInfo[imagesCount];
                for (int imageIndex = 0; imageIndex < imagesCount; imageIndex++)
                {
                    int x = stream.ReadUI16();
                    int y = stream.ReadUI16();
                    int width = stream.ReadUI16();
                    int height = stream.ReadUI16();

                    images[imageIndex] = new PartsetInfo(x, y, width, height);
                }
            }            
        }        

        public PartsetInfo[] Parts
        {
            get { return images; }
        }

        public string GetTextureName()
        {
            return texture;
        }
    }
}
