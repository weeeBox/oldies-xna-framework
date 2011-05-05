using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.graphics;

namespace AsapXna.asap.resources.types
{
    public class AtlasRes : Atlas
    {
        private List<AtlasPartInfo> parts;

        public AtlasRes(GameTexture texture, int partsCount) : base(texture)
        {            
            parts = new List<AtlasPartInfo>(partsCount);
        }

        public void Add(ref AtlasPartInfo part)
        {
            parts.Add(part);
        }        

        public List<AtlasPartInfo> Parts
        {
            get { return parts; }            
        }

        public override void Dispose()
        {
            base.Dispose();

            if (parts != null)
            {
                parts.Clear();
                parts = null;
            }
        }
    }
}
