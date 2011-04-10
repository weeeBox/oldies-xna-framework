using System;

using System.Collections.Generic;


using asap.graphics;
using asap.anim;
using System.Diagnostics;

namespace asap.resources
{
    public class DefaultResManager : ResManager
     {
        public String TYPE_IMAGE = ".png";
        
        public String TYPE_FONT = ".fnt";
        
        public String TYPE_ANIMATION = ".swp";
        
        public String TYPE_STRINGS_PACK = ".str";
        
        public DefaultResManager() 
        {
            new StrRes(1);
        }
        
        protected override object LoadResource(String path)
        {
            String type = GetExt(path);
            
            if (type.Equals(TYPE_IMAGE))
            {
                return ResFactory.GetInstance().LoadManagedImage(path);                
            }
            if (type.Equals(TYPE_ANIMATION))
            {
                return ResFactory.GetInstance().LoadSwfMovie(path);
            }
            //else if (type.Equals(TYPE_FONT)) 
            //{
            //    InputStream _is = ResFactory.GetInstance().GetResourceAsStream(path);
            //    BitmapFont font = new BitmapFont(_is);
            //    AddRes(path, font);
            //    _is.Close();
            //    Debug.WriteLine(("Font loaded: " + path));
            //    return font;
            //}             
            return null;
        }
        
        public override void Unload(String path)
        {
            if (GetExt(path).Equals(TYPE_STRINGS_PACK)) 
            {
                int idBase = GetStringsPack(path).GetIdBase();
                StrRes.GetInstance().DelPack(idBase);
            } 
            base.Unload(path);
        }
        
        public virtual Image GetImage(String path)
        {
            return ((Image)(GetRes(path)));
        }
        
        public virtual BitmapFont GetFont(String path)
        {
            return ((BitmapFont)(GetRes(path)));
        }
        
        public virtual StringsPack GetStringsPack(String path)
        {
            return ((StringsPack)(GetRes(path)));
        }
        
        public virtual SwfMovie GetAnimation(String path)
        {
            return ((SwfMovie)(GetRes(path)));
        }        
    }    
}