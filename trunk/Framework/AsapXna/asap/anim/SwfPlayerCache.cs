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

        public CharacterInstance FindCached(int depth, int frameIndex)
        {
            foreach (CacheInfo info in cache)
            {
                if (info.depth == depth && info.frameIndex == frameIndex)
                {
                    return info.instance;
                }
            }

            return null;
        }

        public CharacterInstance FindAddedAtDepthBeforeFrame(int depth, int frameIndex)
        {
            for (int i = frameIndex - 1; i >= 0; --i)
            {
                CacheInfo info = cache[i];
                if (info.depth == depth)
                {
                    return info.instance;
                }
            }

            return null;
        }
    }
}
