using System;

using System.Collections.Generic;


using asap.resources;
using asap.graphics;
using asap.anim;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace app
{
    public class AppResManager : ResourceMgr
    {
        public AppResManager() : base(Res.RES_COUNT)
        {
        }

        public void LoadPack(int packIndex)
        {
            Debug.Assert(packIndex >= 0 && packIndex < ResPacks.PACKS_COUNT);

            ResourceLoadInfo[] infos = Resources.PACKS[packIndex];
            for (int resIndex = 0; resIndex < infos.Length; ++resIndex)
            {
                loadResource(infos[resIndex]);
            }
        }

        public void UnloadPack(int packIndex)
        {
            Debug.Assert(packIndex >= 0 && packIndex < ResPacks.PACKS_COUNT);
            ResourceLoadInfo[] infos = Resources.PACKS[packIndex];
            for (int resIndex = 0; resIndex < infos.Length; ++resIndex)
            {
                freeResource(infos[resIndex].resId);
            }
        }

        public static AppResManager GetInstance()
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