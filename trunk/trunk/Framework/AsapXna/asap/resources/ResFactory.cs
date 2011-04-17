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
        }

        public static ResFactory GetInstance()
        {
            return instance;
        }

        public SoundPlayer CreateSoundPlayer(string fileName, bool streaming)
        {
            int dotIndex = fileName.LastIndexOf(".");
            string assetName = dotIndex != 0 ? fileName.Substring(0, dotIndex) : fileName;

            if (fileName.StartsWith("song_"))
            {
                Song song;
                if (songs.ContainsKey(assetName))
                {
                    song = songs[assetName];
                }
                else
                {
                    song = content.Load<Song>(assetName);
                    songs.Add(assetName, song);
                }
                return new Mp3SoundPlayer(song, fileName);
            }
            else
            {
                SoundEffect effect;
                if (effects.ContainsKey(assetName))
                {
                    effect = effects[assetName];
                }
                else
                {
                    effect = content.Load<SoundEffect>(assetName);
                    effects.Add(assetName, effect);
                }
                return new WaveSoundPlayer(effect, fileName);
            }
        }
        
        public BitmapFont LoadFont(string path)
        {
            using (ContentManager manager = new ContentManager(content.ServiceProvider, "Content"))
            {
                return manager.Load<BitmapFont>(path);
            }
        }

        public Image LoadImage(string resname)
        {
            Texture2D texture = content.Load<Texture2D>(resname);
            return new Image(texture);
        }

        public Image LoadManagedImage(string path)
        {            
            Image instance = FindUsedReference<Image>(path);
            if (instance == null)
            {
                instance = new Image();
                AddReference(path, instance);
            }
            TextureManager.Instance.LoadTexture(path, instance);
            return instance;
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

        public void UnloadResource(object res)
        {
            if (res is IDisposable)
            {
                IDisposable obj = (IDisposable)res;
                obj.Dispose();
            }
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
