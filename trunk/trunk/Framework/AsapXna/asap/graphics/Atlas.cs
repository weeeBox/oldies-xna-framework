using System.Collections.Generic;
using asap.resources;

namespace asap.graphics
{
    public struct AtlasPartInfo
    {
        public int x;
        public int y;
        public int w;
        public int h;
        public int ox;
        public int oy;
    }

    public class Atlas : ManagedResource
    {
        private GameTexture texture;        

        public Atlas(GameTexture texture)
        {
            this.texture = texture;            
        }        

        public GameTexture Texture 
        { 
            get { return texture; }
        }

        public override void Dispose()
        {
            if (texture != null)    
            {
                texture.Dispose();
                texture = null;
            }
        }        
    }
}
