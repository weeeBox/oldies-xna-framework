using System;

using System.Collections.Generic;

using asap.resources;
using asap.graphics;
using asap.anim;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace app
{
    public class AppResourceMgr : ResourceMgr
    {
        public AppResourceMgr() : base(Res.RES_COUNT)
        {
        }

        public void LoadPacks(ResourceMgrListener listener, params int[] packs)
        {
            initLoading();
            foreach (int packIndex in packs)
            {
                AddPackToLoad(packIndex);
            }
            StartLoading(listener);
        }

        private void AddPackToLoad(int packIndex)
        {
            Debug.Assert(packIndex >= 0 && packIndex < ResPacks.PACKS_COUNT);

            ResourceLoadInfo[] infos = Resources.PACKS[packIndex];
            for (int resIndex = 0; resIndex < infos.Length; ++resIndex)
            {
                AddResourceToLoadQueue(ref infos[resIndex]);
            }
        }

        private void UnloadPack(int packIndex)
        {
            Debug.Assert(packIndex >= 0 && packIndex < ResPacks.PACKS_COUNT);
            ResourceLoadInfo[] infos = Resources.PACKS[packIndex];
            for (int resIndex = 0; resIndex < infos.Length; ++resIndex)
            {
                freeResource(infos[resIndex].resId);
            }
        }

        public static AppResourceMgr GetInstance()
        {
            return null;
        }               
        
        public static BitmapFont GetDefaultFont()
        {
            return null;
        }
        
        public Object GetRes(String path)
        {
            return null;
        }

        internal BitmapFont GetFont(string fontName)
        {
            throw new NotImplementedException();
        }

        internal Image GetImage(string imageRes)
        {
            throw new NotImplementedException();
        }        

        internal void Unload(string p)
        {
            throw new NotImplementedException();
        }        
    }    
}