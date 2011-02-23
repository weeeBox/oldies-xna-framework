using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Flipstones2.gfx;

namespace Flipstones2.res
{
    public class Atlas
    {
        private string textureName;
        private List<SubTexData> parts;

        public Atlas(string textureName, int imagesCount)
        {            
            this.textureName = textureName;
            parts = new List<SubTexData>(imagesCount);
        }

        public List<SubTexData> Parts 
        {
            get { return parts; }             
        }

        public void Add(SubTexData part)
        {
            parts.Add(part);
        }

        public string TextureName
        {
            get { return textureName; }
        }
    }
}
