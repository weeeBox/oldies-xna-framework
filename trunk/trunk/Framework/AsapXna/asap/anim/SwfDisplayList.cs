using System;
using System.Collections.Generic;
using System.Diagnostics;
using asap.anim.objects;

namespace asap.anim
{
    public class SwfDisplayList
    {
        private List<CharacterInstance> objects;

        public SwfDisplayList()
        {
            objects = new List<CharacterInstance>();
        }

        public CharacterInstance this[int depth]
        {
            get 
            {
                int index = depth - 1;
                Debug.Assert(index >= 0 && index < Size);
                return objects[index];
            }
            set
            {
                int index = depth - 1;                
                if (value == null)
                {
                    RemoveAt(index);
                }
                else
                {
                    if (depth > Size)
                    {
                        Realloc(depth);
                    }
                    else
                    {
                        RemoveAt(index);
                    }                    
                    objects[index] = value;
                }
            }
        }        

        private void RemoveAt(int index)
        {            
            CharacterInstance instance = objects[index];
            if (instance != null)
            {
                instance.Dispose();
                objects[index] = null;
            }
        }

        public SpriteInstance FindInstance(string name)
        {
            foreach (CharacterInstance obj in objects)
            {                
                if (obj.GetCode() == CharacterConstansts.SPRITE)
                {
                    SpriteInstance sprite = (SpriteInstance)obj;                    
                    if (name.Equals(sprite.Name))
                    {
                        return sprite;
                    }
                }
            }
            return null;
        }

        public List<CharacterInstance> FindInstancesOf(Type type)
        {
            List<CharacterInstance> instances = new List<CharacterInstance>();
            foreach (CharacterInstance obj in objects)
            {
                if (obj.GetType() == type)
                {
                    instances.Add(obj);
                }
            }
            return instances;
        }

        public List<CharacterInstance> FindInstances(int characterId)
        {            
            List<CharacterInstance> instances = new List<CharacterInstance>();

            foreach (CharacterInstance obj in objects)
            {
                if (obj.GetCharacterId() == characterId)
                {
                    instances.Add(obj);
                }
            }

            return instances;
        }

        private void Realloc(int newSize)
        {            
            while (Size < newSize)
            {
                objects.Add(null);
            }
        }

        public int Size
        {
            get { return objects.Count; }
        }

        public void Clear()
        {
            objects.Clear();
        }        
    }
}
