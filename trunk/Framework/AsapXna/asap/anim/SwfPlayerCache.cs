using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.anim.objects;

namespace asap.anim
{
    public class SwfPlayerCache
    {
        private struct CacheInfo
        {            
            public int depth;
            public int frameIndex;
            public CharacterInstance instance;

            public CacheInfo(CharacterInstance instance, int depth, int frameIndex)
            {
                this.instance = instance;
                this.depth = depth;
                this.frameIndex = frameIndex;                
            }       
     
            public int CharacterId
            {
                get { return instance.GetCharacterId(); }
            }
        }

        private List<CacheInfo> cache;

        public SwfPlayerCache()
        {
            cache = new List<CacheInfo>();
        }

        public void AddCached(CharacterInstance instance, int depth, int frame)
        {            
            cache.Add(new CacheInfo(instance, depth, frame));
        }

        public CharacterInstance FindCached(int characterId, int depth, int frameIndex)
        {
            foreach (CacheInfo info in cache)
            {
                if (info.CharacterId != characterId)
                    continue;

                if (info.depth != depth)
                    continue;

                if (info.frameIndex != frameIndex)
                    continue;

                return info.instance;
            }

            return null;
        }
    }
}
