using System;
using System.Collections.Generic;
using System.Diagnostics;
using asap.graphics;
using swiff.com.jswiff.swfrecords.tags;

namespace asap.anim
{
    public class SwfLibrary : IDisposable
    {
        private Dictionary<int, DefinitionTag> tags;
        private List<SwfPartset> partsets;
        private Dictionary<string, DefineSprite> namedSymbols;

        public SwfLibrary()
        {
            tags = new Dictionary<int, DefinitionTag>();
            namedSymbols = new Dictionary<string, DefineSprite>();
            partsets = new List<SwfPartset>();
        }

        public void AddNamedSymbol(string name, int characterId)
        {
            Debug.Assert(tags.ContainsKey(characterId), "Can't find library tag: " + characterId);
            DefinitionTag tag = tags[characterId];
            Debug.Assert(tag != null, "Can't find character: " + characterId);
            Debug.Assert(tag is DefineSprite, "Can't cast tag to DefineSprite: " + tag.ToString());
            namedSymbols.Add(name, (DefineSprite)tag);
        }

        public void Add(DefinitionTag tag)
        {
            Debug.Assert(!tags.ContainsKey(tag.GetCharacterId()), "Library duplicate: " + tag.GetCharacterId() + " " + tag);
            tags.Add(tag.GetCharacterId(), tag);
        }

        public DefineSprite getNamedSymbol(string name)
        {
            if (namedSymbols.ContainsKey(name))
            {
                return namedSymbols[name];
            }

            return null;
        }

        public DefinitionTag this[int characterId]
        {
            get 
            {
                Debug.Assert(tags.ContainsKey(characterId), "Library tag not found: " + characterId);
                return tags[characterId]; 
            }
        }

        public void AddPartset(SwfPartset partset)
        {
            partsets.Add(partset);
        }

        public GameTexture GetImage(int imageId)
        {
            int partsetIndex = (imageId >> 16) & 0xffff;
            int imageIndex = imageId & 0xffff;

            Debug.Assert(partsetIndex >= 0 && partsetIndex < partsets.Count);
            SwfPartset partset = partsets[partsetIndex];
            return partset[imageIndex];
        }

        public void Dispose()
        {
            if (partsets != null)
            {
                foreach (SwfPartset partset in partsets)
                {
                    partset.Dispose();
                }
                partsets.Clear();
                partsets = null;
            }
        }        
    }
}
