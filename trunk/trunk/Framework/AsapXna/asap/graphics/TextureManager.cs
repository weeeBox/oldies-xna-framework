using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace asap.graphics
{
    public class TexData
    {
        public String id;
        public Texture2D tex;
        public int refCount;
    }

    public class SubTexData
    {
        public String texId;
        public int x;
        public int y;
        public int w;
        public int h;

        public TexData parent;
    }

    public class TextureManager : ITextureManager
    {
        private ContentManager contentManager;        

        private Dictionary<String, SubTexData> subTextures = new Dictionary<string, SubTexData>();
        private Dictionary<String, TexData> textures = new Dictionary<string, TexData>();
        private Dictionary<Texture2D, TexData> texturesB = new Dictionary<Texture2D, TexData>();

        private static TextureManager instance;

        public TextureManager(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            instance = this;
        }

        public void AddPackAtlas(Atlas atlas)
        {
            TexData texData = new TexData();
            texData.id = atlas.TextureName;
            textures.Add(texData.id, texData);

            Debug.WriteLine("Add atlas: " + texData.id);

            List<SubTexData> parts = atlas.Parts;
            foreach (SubTexData part in parts)
            {
                part.parent = texData;
                subTextures.Add(part.texId, part);
            }            
        }        

        public Image LoadTexture(String name, Image existingInstance)
        {
            SubTexData subData = null;

            TexData texData; 
            if (subTextures.ContainsKey(name))
            {
                subData = subTextures[name];
                texData = subData.parent;                
            }
            else
            {
                Debug.Assert(textures.ContainsKey(name), name);
                texData = textures[name];
            }            

            if (texData.refCount == 0)
            {
                Debug.WriteLine("Texture manager load: " + texData.id);

                ContentManager cm = new ContentManager(contentManager.ServiceProvider, "Content");
                Texture2D t = cm.Load<Texture2D>(texData.id);
                cm = null;

                texData.tex = t;                
                texturesB.Add(t, texData);
            }

            texData.refCount++;
            Debug.WriteLine("\tsubtexture: " + name + " - " + texData.id + ": " + texData.refCount);

            Debug.Assert(existingInstance != null, name);
            if (subData != null)
            {
                existingInstance.setTexture(this, texData.tex, subData.x, subData.y, subData.w, subData.h);
            }
            else
            {
                Texture2D tex2D = texData.tex;
                existingInstance.setTexture(this, tex2D, 0, 0, tex2D.Width, tex2D.Height);
            }

            return existingInstance;
        }

        public void UnloadTexture(string name)
        {
            TexData texData;
            if (subTextures.ContainsKey(name))
            {
                SubTexData subData = subTextures[name];
                texData = subData.parent;
            }
            else
            {
                Debug.Assert(textures.ContainsKey(name), name);
                texData = textures[name];
            }

            UnloadTexture(texData);
        }

        public void UnloadTexture(Texture2D tex)
        {
            if (!texturesB.ContainsKey(tex))
                return;

            TexData td = texturesB[tex];
            UnloadTexture(td);
        }

        private void UnloadTexture(TexData td)
        {
            td.refCount--;
            Debug.WriteLine("Texture manager unload subtexture - " + td.id + ":" + td.refCount);
            if (td.refCount <= 0)
            {
                Debug.WriteLine("Texture manager dispose: " + td.id);

                texturesB.Remove(td.tex);
                td.refCount = 0;
                td.tex.Dispose();
                td.tex = null;
            }
        }

        public static TextureManager Instance
        {
            get { return instance; }
        }
    }
}
