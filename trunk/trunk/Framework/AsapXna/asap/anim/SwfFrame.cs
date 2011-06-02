using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using swiff.com.jswiff.swfrecords.tags;

namespace swiff.com.jswiff
{
    public class SWFFrame
    {
        private Tag[] tags;

        private bool dispListChange;

        private static SWFFrame EMPTY = new SWFFrame(0);

        private SWFFrame(int tagsCount)
        {
            tags = new Tag[tagsCount];
        }

        public static SWFFrame Create(List<Tag> tagsList)
        {
            if (tagsList.Count == 0)
            {
                return EMPTY;
            }

            SWFFrame frame = new SWFFrame(tagsList.Count);
            int i = 0;
            foreach (Tag t in tagsList)
            {
                frame.tags[i] = t;                
                frame.processTag(t);
                i++;
            }

            return frame;
        }

        public Tag[] Tags
        {
            get { return tags; }
        }

        public bool IsDispListChange()
        {
            return dispListChange;
        }

        private void processTag(Tag t)
        {
            switch (t.GetCode())
            {
                case TagConstants.REMOVE_OBJECT:
                case TagConstants.REMOVE_OBJECT_2:
                {
                    dispListChange = true;
                    break;
                }
                case TagConstants.PLACE_OBJECT:
                {
                    throw new NotImplementedException("The tag too old");
                }
                case TagConstants.PLACE_OBJECT_2:
                {
                    PlaceObject2 p = (PlaceObject2)t;
                    if (!p.IsMove() || p.HasCharacter())
                    {
                        dispListChange = true;
                    }
                    break;
                }
                case TagConstants.PLACE_OBJECT_3:
                {
                    PlaceObject3 p = (PlaceObject3)t;
                    if (!p.IsMove() || p.HasCharacter())
                    {
                        dispListChange = true;
                    }
                }
                break;   
            }
        }
    }
}