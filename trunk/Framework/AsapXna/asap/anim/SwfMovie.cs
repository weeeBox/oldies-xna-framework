using System;

using System.Collections.Generic;


using asap.resources;
using swiff.com.jswiff.swfrecords.tags;

namespace asap.anim
{
    public class SwfMovie
    {
        private int framesCount;
        private int frameRate;

        private List<Tag> tags;

        public SwfMovie(int framesCount, int frameRate)
        {
        }

        public void SetTags(List<Tag> tags)
        {
            this.tags = tags;
        }
    }
}