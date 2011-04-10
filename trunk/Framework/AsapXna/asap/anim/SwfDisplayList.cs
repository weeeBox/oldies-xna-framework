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
                if (depth > Size)
                {
                    Realloc(depth);
                }
                int index = depth - 1;
                objects[index] = value;
            }
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
