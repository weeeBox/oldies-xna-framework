using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace asap.graphics
{
    public class BaseElementContainer : BaseElement
    {
        public bool passTransformationsToChilds;
        public bool passButtonEventsToAllChilds;

        protected List<BaseElement> childs;

        public BaseElementContainer()
            : this(0, 0, 0, 0)
        {
        }

        public BaseElementContainer(int width, int height)
            : this(0, 0, width, height)
        {
        }

        public BaseElementContainer(float x, float y, int width, int height)
            : base(x, y, width, height)
        {
            childs = new List<BaseElement>();
            passTransformationsToChilds = true;
            passButtonEventsToAllChilds = true;
        }

        public override void update(float delta)
        {
            base.update(delta);

            foreach (BaseElement c in childs)
            {
                if (c != null && c.updateable)
                {
                    c.update(delta);
                }
            }
        }

        public override void postDraw()
        {
            if (!passTransformationsToChilds)
            {
                restoreTransformations();
            }

            foreach (BaseElement c in childs)
            {
                if (c != null && c.visible)
                {
                    c.draw();
                }
            }

            if (passTransformationsToChilds)
            {
                restoreTransformations();
            }
        }        

        public virtual void addChild(BaseElement c)
        {
            c.setParent(this);
            childs.Add(c);            
        }        

        public void removeChild(BaseElement c)
        {
            c.setParent(null);
            childs.Remove(c);
        }

        public void removeAllChilds()
        {
            childs.Clear();
        }        

        public List<BaseElement> getChilds()
        {
            return childs;
        }

        public int childsCount()
        {
            return childs.Count;
        }        
    }
}
