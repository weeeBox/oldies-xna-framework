using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Flipstones2.res;

using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

using System.Diagnostics;

using asap.resources;
using asap.sound;
using Flipstones2.sfx;
using asap.anim;
using asap.graphics;
using Flipstones2.gfx;

namespace Flipstones2.app
{
    public class XnaResFactory : ResFactory
    {
        private ContentManager content;
        private Dictionary<string, Song> songs;
        private Dictionary<string, SoundEffect> effects;
        private Dictionary<string, object> usedReferences;

        public XnaResFactory(ContentManager content)
        {
            this.content = content;
            songs = new Dictionary<string, Song>();
            effects = new Dictionary<string, SoundEffect>();
            usedReferences = new Dictionary<string, object>();
        }

        public override SoundPlayer CreateSoundPlayer(string fileName, bool streaming)
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

        public override PartSet LoadPartset(string path)
        {
            return content.Load<PartSet>("ps_" + getSimpleName(path));
        }

        public override Animation LoadAnimation(string path, string[] partsetPaths)
        {
            using (ContentManager manager = new ContentManager(content.ServiceProvider, "Content"))
            {
                PartSet[] partsets = new PartSet[partsetPaths.Length];
                for (int i = 0; i < partsets.Length; i++)
                {
                    partsets[i] = (PartSet)(ResManager.GetBaseInstance().GetRes(partsetPaths[i]));
                }
                Animation animation = manager.Load<Animation>("ani_" + getSimpleName(path));
                animation.partsets = partsets;

                return animation;
            }
        }

        public override BitmapFont LoadFont(string path)
        {
            using (ContentManager manager = new ContentManager(content.ServiceProvider, "Content"))
            {
                return manager.Load<BitmapFont>(getSimpleName(path));
            }
        }

        public override Image LoadImage(string path)
        {
            path = getSimpleName(path);
            XnaImage instance = FindUsedReference<XnaImage>(path);
            if (instance == null)
            {
                instance = new XnaImage();
                AddReference(path, instance);
            }
            TextureManager.Instance.LoadTexture(path, instance);
            return instance;
        }

        public override StringsPack LoadStrings(string path)
        {
            return content.Load<StringsPack>(getSimpleName(path));
        }

        public override void UnloadResource(object res)
        {
            if (res is IDisposable)
            {
                IDisposable obj = (IDisposable)res;
                obj.Dispose();
            }
        }

        private string getSimpleName(string name)
        {
            int index = name.LastIndexOf('.');
            if (index == -1)
                return name;

            return name.Substring(0, index);
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
