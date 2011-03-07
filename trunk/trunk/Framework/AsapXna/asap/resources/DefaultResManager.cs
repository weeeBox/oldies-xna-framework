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
        
        public String TYPE_ANIMATION = ".ani";
        
        public String TYPE_PARTSET = ".parts";
        
        public String TYPE_STRINGS_PACK = ".str";
        
        public DefaultResManager() 
        {
            new StrRes(1);
        }
        
        public override Object Load(String path, ResCallback callback)
        {
            String type = _getType(path);
            
            if (type.Equals(TYPE_IMAGE))
            {
                return ResFactory.GetInstance().LoadImage(path);                
            }
            
            //if (type.Equals(TYPE_IMAGE)) 
            //{
            //    Image image = ResFactory.GetInstance().CreateImage(path);
            //    if (image == null) 
            //    {
            //        sbyte[] imageData = LoadBinary(path);
            //        ByteArrayInputStream stream = new ByteArrayInputStream(imageData);
            //        image = ResFactory.GetInstance().CreateImage(stream, imageData.Length);
            //    } 
            //    Debug.Assert(image != null);
            //    AddRes(path, image);
            //    Debug.WriteLine(("Image loaded: " + path));
            //    return image;
            //} 
            //else if (type.Equals(TYPE_FONT)) 
            //{
            //    InputStream _is = ResFactory.GetInstance().GetResourceAsStream(path);
            //    BitmapFont font = new BitmapFont(_is);
            //    AddRes(path, font);
            //    _is.Close();
            //    Debug.WriteLine(("Font loaded: " + path));
            //    return font;
            //} 
            //else if (type.Equals(TYPE_STRINGS_PACK)) 
            //{
            //    Debug.Assert((StrRes.GetInstance()) != null, "StrRes is not created!");
            //    InputStream _is = ResFactory.GetInstance().GetResourceAsStream(path);
            //    StringsPack strPack = new StringsPack();
            //    strPack.Load(_is);
            //    _is.Close();
            //    StrRes.GetInstance().AddPack(strPack);
            //    Debug.WriteLine(("Strings pack loaded: " + path));
            //    return strPack;
            //} 
            //else if (type.Equals(TYPE_ANIMATION)) 
            //{
            //    sbyte[] animData = LoadBinary(path);
            //    Animation anim = new Animation(new ByteArrayInputStream(animData) , null);
            //    AddRes(path, anim);
            //    Debug.WriteLine(("Animation loaded: " + path));
            //    return anim;
            //} 
            //else if (type.Equals(TYPE_PARTSET)) 
            //{
            //    InputStream stream = ResFactory.GetInstance().GetResourceAsStream(path);
            //    PartSet partset = new PartSet(stream , null);
            //    stream.Close();
            //    AddRes(path, partset);
            //    Debug.WriteLine(("Partset loaded: " + path));
            //    return partset;
            //} 
            return base.Load(path, callback);            
        }
        
        public override void Unload(String path)
        {
            if (_getType(path).Equals(TYPE_STRINGS_PACK)) 
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
        
        public virtual Animation GetAnimation(String path)
        {
            return ((Animation)(GetRes(path)));
        }
        
        public virtual PartSet GetPartset(String path)
        {
            return ((PartSet)(GetRes(path)));
        }
        
    }
    
    
}