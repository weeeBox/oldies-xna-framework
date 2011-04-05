using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.anim;

namespace AsapXna.asap.anim
{
    public class SwfPlayer
    {
        private SwfMovie movie;
        private List<DisplayObject> displayList;

        public SwfPlayer()
        {
        }

        public void SetMovie(SwfMovie movie)
        {
            this.movie = movie;
        }
    }
}
