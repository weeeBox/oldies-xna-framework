using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.anim;
using asap.graphics;

namespace AsapXna.asap.anim
{
    public class SwfPlayer
    {
        private SwfMovie movie;
        private List<DisplayObject> displayList;

        private int totalFrames;
        private int currentFrame;

        private float elapsed;

        public SwfPlayer()
        {
        }

        public void SetMovie(SwfMovie movie)
        {
            this.movie = movie;
            Reset();
        }

        private void Reset()
        {
            
        }

        public void Update(float dt)
        {
        }

        public void Draw(Graphics g)
        {
        }
    }
}
