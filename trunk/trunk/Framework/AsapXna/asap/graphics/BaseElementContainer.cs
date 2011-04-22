using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.core;

namespace asap.graphics
{
    public class BaseElementContainer : BaseElement
    {
        public bool passTransformationsToChilds;
        public bool passButtonEventsToAllChilds;

        protected DynamicArray<BaseElement> childs;

        public BaseElementContainer()
            : this(0, 0, 0, 0)
        {
        }

        public BaseElementContainer(float width, float height)
            : this(0, 0, width, height)
        {
        }

        public BaseElementContainer(float x, float y, float width, float height)
            : base(x, y, width, height)
        {
            childs = new DynamicArray<BaseElement>();
            passTransformationsToChilds = true;
            passButtonEventsToAllChilds = true;
        }

        public override void Update(float delta)
        {
            base.Update(delta);

            foreach (BaseElement c in childs)
            {
                if (c != null && c.updateable)
                {
                    c.Update(delta);
                }
            }
        }

        public override void PostDraw(Graphics g)
        {
            if (!passTransformationsToChilds)
            {
                RestoreTransformations();
            }

            foreach (BaseElement c in childs)
            {
                if (c != null && c.visible)
                {
                    c.Draw(g);
                }
            }

            if (passTransformationsToChilds)
            {
                RestoreTransformations();
            }
        }

        public virtual void AddChild(BaseElement c, int index)
        {
            c.SetParent(this);
            childs[index] = c;
        }

        public virtual int AddChild(BaseElement c)
        {
            int index = childs.getFirstEmptyIndex();
            AddChild(c, index);
            return index;
        }

        public void RemoveChildWithId(int i)
        {
            BaseElement c = childs[i];
            c.SetParent(null);
            childs[i] = null;
        }

        public void RemoveChild(BaseElement c)
        {
            int index = childs.getObjectIndex(c);
            RemoveChildWithId(index);
        }

        public void RemoveAllChilds()
        {
            childs = new DynamicArray<BaseElement>();
        }

        public BaseElement GetChild(int i)
        {
            return childs[i];
        }

        public DynamicArray<BaseElement> GetChilds()
        {
            return childs;
        }

        public int ChildsCount()
        {
            return childs.count();
        }        
    }
}
