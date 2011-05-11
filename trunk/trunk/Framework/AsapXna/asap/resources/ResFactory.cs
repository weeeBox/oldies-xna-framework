using System;
using System.Collections.Generic;
using System.Diagnostics;
using asap.graphics;
using asap.sound;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using asap.anim;
using Microsoft.Xna.Framework.Graphics;
using AsapXna.asap.resources.types;

namespace asap.resources
{
    public class ResFactory
    {
        private ContentManager content;
        private Dictionary<string, Song> songs;
        private Dictionary<string, SoundEffect> effects;
        private Dictionary<string, object> usedReferences;

        private static ResFactory instance;

        public ResFactory(ContentManager content)
        {
            this.content = content;
            songs = new Dictionary<string, Song>();
            effects = new Dictionary<string, SoundEffect>();
            usedReferences = new Dictionary<string, object>();

            instance = this;
            EmbededRes.Load(content);
        }

        public static ResFactory GetInstance()
        {
            return instance;
        }       
        
        public BitmapFont LoadFont(string path)
        {
            using (ContentManager manager = new ContentManager(content.ServiceProvider, "Content"))
            {
                return manager.Load<BitmapFont>(path);
            }
        }

        public GameTexture LoadImage(string resname)
        {
            Texture2D texture = content.Load<Texture2D>(resname);
            return new GameTexture(texture);
        }

        public GameTexture LoadManagedImage(string path)
        {            
            GameTexture instance = FindUsedReference<GameTexture>(path);
            if (instance == null)
            {
                instance = LoadImage(path);
                AddReference(path, instance);
            }            
            return instance;
        }

        public AtlasRes loadAtlas(string resName)
        {
            return content.Load<AtlasRes>(resName);
        }

        public SwfMovie LoadSwfMovie(string path)
        {
            using (ContentManager manager = new ContentManager(content.ServiceProvider, "Content"))
            {
                return manager.Load<SwfMovie>(path);
            }
        }

        public StringsPack LoadStrings(string path)
        {
            return content.Load<StringsPack>(path);
        }       

        public void Dispose()
        {
            EmbededRes.Dispose();
        }

        public T FindUsedReference<T>(string name)
        {
            if (!usedReferences.ContainsKey(name))
                return default(T);
            return (T)usedReferences[name];
        }

        public void AddReference(string name, object obj)
        {
            Debug.Assert(!usedReferences.ContainsKey(name), name);
            usedReferences.Add(name, obj);
        }        
    }
}
