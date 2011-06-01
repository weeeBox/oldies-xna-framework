using asap.core;
using asap.graphics;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace asap.visual
{
    public class DisplayObjectContainer : DisplayObject
    {
        public bool passTransformationsToChilds;
        public bool passButtonEventsToAllChilds;

        protected List<DisplayObject> childs;

        public DisplayObjectContainer()
            : this(0, 0, 0, 0)
        {
        }

        public DisplayObjectContainer(float width, float height)
            : this(0, 0, width, height)
        {
        }

        public DisplayObjectContainer(float x, float y, float width, float height)
            : base(x, y, width, height)
        {
            childs = new List<DisplayObject>();
            passTransformationsToChilds = true;
            passButtonEventsToAllChilds = true;
        }

        public override void Update(float delta)
        {
            base.Update(delta);

            foreach (DisplayObject c in childs)
            {
                if (c.IsUpdatable())
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

            foreach (DisplayObject c in childs)
            {
                if (c.IsVisible())
                {
                    c.Draw(g);
                }
            }

            if (passTransformationsToChilds)
            {
                RestoreDrawState(g);
            }
        }

        public virtual void AddChildAt(DisplayObject c, int index)
        {
            Debug.Assert(index >= 0 && index < ChildsCount());            
            c.SetParent(this);
            childs.Insert(index, c);
        }

        public virtual void AddChild(DisplayObject c)
        {
            DisplayObjectContainer oldParent = c.GetParent();            
            c.SetParent(this);
            childs.Add(c);                        
        }

        public virtual bool Contains(DisplayObject obj)
        {
            foreach (DisplayObject child in childs)
            {
                if (child == obj)
                    return true;

                if (child is DisplayObjectContainer)
                    return ((DisplayObjectContainer)child).Contains(obj);
            }

            return false;
        }

        public DisplayObject GetChildAt(int index)
        {
            Debug.Assert(index >= 0 && index < ChildsCount());
            return childs[index];
        }

        public DisplayObject GetChildByName(string name)
        {
            foreach (DisplayObject child in childs)
            {
                if (child.name == name)
                    return child;

                if (child is DisplayObjectContainer)
                    return ((DisplayObjectContainer)child).GetChildByName(name);
            }           

            return null;
        }

        public int GetChildIndex(DisplayObject obj)
        {
            return childs.IndexOf(obj);
        }                        

        public void RemoveChild(DisplayObject c)
        {
            bool childRemoved = childs.Remove(c);
            if (childRemoved)
            {
                c.SetParent(null);
            }
        }

        public void RemoveChildAt(int index)
        {
            RemoveChild(GetChildAt(index));
        }

        public void RemoveAllChilds()
        {
            foreach (DisplayObject child in childs)
            {
                child.SetParent(null);
            }
            childs.Clear();
        }

        public virtual DisplayObject ReplaceChildAt(DisplayObject c, int index)
        {
            Debug.Assert(index >= 0 && index < ChildsCount());
            DisplayObject oldChild = childs[index];
            oldChild.SetParent(null);
            c.SetParent(this);
            childs[index] = c;
            return oldChild;
        }

        public void SetChildIndex(DisplayObject child, int index)
        {
            Debug.Assert(index >= 0 && index < ChildsCount());
            int oldIndex = GetChildIndex(child);
            Debug.Assert(oldIndex != -1);
            if (index > oldIndex)
            {
                for (int i = oldIndex; i < index; ++i)
                {
                    childs[i] = childs[i + 1];
                }
            }
            else if (index < oldIndex)
            {
                for (int i = oldIndex; i > index; --i)
                {
                    childs[i] = childs[i - 1];
                }
            }
            childs[index] = child;
        }

        public void SwapChildren(DisplayObject child1, DisplayObject child2)
        {
            int index1 = GetChildIndex(child1);
            int index2 = GetChildIndex(child2);

            SwapChildrenAt(index1, index2);
        }

        public void SwapChildrenAt(int index1, int index2)
        {
            Debug.Assert(index1 >= 0 && index1 < ChildsCount());
            Debug.Assert(index2 >= 0 && index2 < ChildsCount());
            DisplayObject temp = childs[index1];
            childs[index1] = childs[index2];
            childs[index2] = temp;
        }

        public List<DisplayObject> GetChilds()
        {
            return childs;
        }

        public int ChildsCount()
        {
            return childs.Count;
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
                DisplayObject firtChild = GetChildAt(0);
                float left = firtChild.x;
                float top = firtChild.y;
                float right = left + firtChild.width;
                float bottom = top + firtChild.height;
                for (int i = 1; i < ChildsCount(); i++)
                {
                    DisplayObject child = GetChildAt(i);
                    left = Math.Min(left, child.x);
                    top = Math.Min(top, child.y);
                    right = Math.Max(right, child.x + child.width);
                    bottom = Math.Max(bottom, child.y + child.height);
                }
                foreach (DisplayObject child in childs)
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
