using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using swiff.com.jswiff.swfrecords.tags;
using System.Diagnostics;
using asap.graphics;

namespace asap.anim
{
    public class SwfLibrary : IDisposable
    {
        private Dictionary<int, DefinitionTag> tags;
        private List<SwfPartset> partsets;

        public SwfLibrary()
        {
            tags = new Dictionary<int, DefinitionTag>();
            partsets = new List<SwfPartset>();
        }

        public void Add(DefinitionTag tag)
        {
            Debug.Assert(!tags.ContainsKey(tag.GetCharacterId()), "Library duplicate: " + tag.GetCharacterId() + " " + tag);
            tags.Add(tag.GetCharacterId(), tag);
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

        public Image GetImage(int imageId)
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
