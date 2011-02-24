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
        
        public static readonly String[] PARTSET_NAMES = new String[]{ "back.parts" , "black.parts" , "blue.parts" , "bonus.parts" , "colorless.parts" , "colorless1.parts" , "default.parts" , "generator.parts" , "green.parts" , "hud.parts" , "orange.parts" , "purple.parts" , "red.parts" , "static.parts" , "white.parts" , "yellow.parts" };
        
        public override Object Load(String path)
        {
            return Load(path, null);
        }
        
        public override Object Load(String path, ResCallback callback)
        {
            if (_getType(path).Equals(TYPE_ANIMATION)) 
            {
                throw new NotImplementedException();
            } 
            return base.Load(path, callback);
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