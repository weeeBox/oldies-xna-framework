using asap.core;
using asap.graphics;
using System;
using System.Diagnostics;

namespace asap.visual
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
                if (c != null && c.IsUpdatable())
                {
                    c.Update(delta);
                }
            }
        }

        public override void PostDraw(Graphics g)
        {
            if (!passTransformationsToChilds)
            {
                RestoreDrawState(g);
            }

            foreach (BaseElement c in childs)
            {
                if (c != null && c.IsVisible())
                {
                    c.Draw(g);
                }
            }

            if (passTransformationsToChilds)
            {
                RestoreDrawState(g);
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

        private void ResizeToFitChilds(bool horizontally, bool vertically)
        {
            Debug.Assert(horizontally || vertically);
            if (ChildsCount() == 0)
            {
                width = height = 0;
            }
            else
            {
                BaseElement firtChild = GetChild(0);
                float left = firtChild.x;
                float top = firtChild.y;
                float right = left + firtChild.width;
                float bottom = top + firtChild.height;
                for (int i = 1; i < ChildsCount(); i++)
                {
                    BaseElement child = GetChild(i);
                    left = Math.Min(left, child.x);
                    top = Math.Min(top, child.y);
                    right = Math.Max(right, child.x + child.width);
                    bottom = Math.Max(bottom, child.y + child.height);
                }
                foreach (BaseElement child in childs)
                {
                    child.x = horizontally ? child.x - left : child.x;
                    child.y = vertically ? child.y - top : child.y;
                }
                width = horizontally ? right - left : Width;
                height = vertically ? bottom - top : Height;
            }
        }

        public void ResizeHorizontallyToFitChilds()
        {
            ResizeToFitChilds(true, false);
        }

        public void ResizeVerticallyToFitChilds()
        {
            ResizeToFitChilds(false, true);
        }

        public void ResizeToFitChilds()
        {
            ResizeToFitChilds(true, true);
        }
    }
}
