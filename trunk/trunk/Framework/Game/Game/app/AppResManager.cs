using System;

using System.Collections.Generic;


using asap.resources;
using asap.graphics;
using asap.anim;

namespace app
{
    public class AppResManager : DefaultResManager
    {
        public static AppResManager GetInstance()
        {
            return ((AppResManager)(ResManager.instance));
        }               
        
        public static BitmapFont GetDefaultFont()
        {
            return AppResManager.GetInstance().GetFont("font_menu.fnt");
        }
        
        public override Object GetRes(String path)
        {
            Object res = base.GetRes(path);
            System.Diagnostics.Debug.Assert(res != null, ("resource " + path) + " is not loaded");
            return res;
        }        
    }    
}