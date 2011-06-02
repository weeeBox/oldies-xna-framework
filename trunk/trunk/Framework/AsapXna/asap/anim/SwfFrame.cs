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
                i++;
            }

            return frame;
        }

        public Tag[] Tags
        {
            get { return tags; }
        }
    }
}