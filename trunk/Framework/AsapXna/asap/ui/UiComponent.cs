using System;
using System.Diagnostics;
using asap.visual;

namespace asap.ui
{
    public class UiComponent : BaseElementContainer
    {
        private bool enabled;
        private bool focusable;
        private bool traversalKeysEnabled;
        protected FocusTraversalPolicy focusTraversalPolicy;

        public UiComponent() : this(0, 0)
        {
        }

        public UiComponent(float width, float height) : base(width, height)
        {
        }

        public void ResizeToFitChilds()
        {
            ResizeToFitChilds(true, true);
        }
        
        public void AddMargin(int margin)
        {
            AddMargin(margin, margin, margin, margin);
        }
        
        public void AddMargin(int leftMargin, int topMargin, int rightMargin, int bottomMargin)
        {
            float left = -leftMargin;
            float top = -topMargin;
            float right = Width + rightMargin;
            float bottom = Height + bottomMargin;
            foreach (BaseElement child in childs)
            {
                if (child != null)
                {
                    child.x -= left;
                    child.y -= top;                    
                }
            }
            width = right - left;
            height = bottom - top;            
        }
        
        public void ResizeToFitChilds(bool horizontally, bool vertically)
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
        
        public void ArrangeVert(float distance)
        {
            float pos = 0;
            foreach (BaseElement child in childs) 
            {
                child.y = pos;                
                pos += child.Height + distance;
            }            
        }
        
        public void ArrangeHor(float distance)
        {
            float pos = 0;
            foreach (BaseElement child in childs) 
            {
                child.x = pos;                
                pos += child.width + distance;
            }            
        }
        
        public void ArrangeVert()
        {
            ArrangeHelper(false);            
        }
        
        public void ArrangeHor()
        {
            ArrangeHelper(true);            
        }
        
        public void AttachLeft(BaseElement component)
        {
            AttachLeft(component, 0.0f);
        }

        public void AttachLeft(BaseElement component, float indent)
        {
            component.x = indent;
        }
        
        public void AttachHCenter(BaseElement component)
        {
            component.x = 0.5f * (Width - component.Width);            
        }
        
        public void AttachRight(BaseElement component)
        {
            AttachRight(component, 0.0f);
        }

        public void AttachRight(BaseElement component, float indent)
        {
            component.x = Width - component.Width - indent;
        }
        
        public void AttachTop(BaseElement component)
        {
            AttachTop(component, 0);
        }

        public void AttachTop(BaseElement component, float indent)
        {
            component.y = indent;
        }
        
        public void AttachVCenter(BaseElement component)
        {
            component.y = 0.5f * (Height - component.Height);
        }
        
        public void AttachBottom(BaseElement component, float indent)
        {
            component.y = Height - component.height - indent;
        }
        
        public void AttachVert(BaseElement component, float align)
        {
            component.y = align * (Height - component.Height);
        }
        
        public void AttachHor(BaseElement component, float align)
        {
            component.x = align * (Width - component.Width);
        }
        
        public void AttachHCenterAll()
        {
            foreach (BaseElement child in childs)
            {
                AttachHCenter(child);
            }            
        }
        
        public void AttachVCenterAll()
        {
            foreach (BaseElement child in childs)
            {
                AttachVCenter(child);
            }            
        }
        
        private void ArrangeHelper(bool hor)
        {
            if (ChildsCount() == 0)
                return ;
            
            float totalSize = 0;
            foreach (BaseElement child in childs)
            {
                totalSize += hor ? child.width : child.height;
            }

            float size = hor ? Width : Height;
            float dist = 0;
            float pos = 0;
            if (size == totalSize) 
            {
                dist = 0;
                pos = 0;
            } 
            else if ((size >= totalSize) || (ChildsCount() == 1)) 
            {
                dist = (size - totalSize) / (ChildsCount() + 1);
                pos = dist;
            } 
            else 
            {
                dist = (size - totalSize) / (ChildsCount() - 1);
            }
            for (int i = 0; i < ChildsCount(); i++) 
            {
                BaseElement child = GetChild(i);
                if (hor) 
                {                    
                    child.x = pos;
                    pos += child.Width + dist;
                } 
                else 
                {                    
                    child.y = pos;
                    pos += child.Height + dist;
                }
            }
        } 

        public int IndexOf(BaseElement element)
        {
            int childsCount = ChildsCount();
            for (int childIndex = 0; childIndex < childsCount; ++childIndex)
            {
                BaseElement child = childs[childIndex];
                if (child == element)
                {
                    return childIndex;
                }
            }
            return -1;
        }

        public virtual bool Contains(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public virtual bool IsAcceptingFocus()
        {
            return IsFocuable() && IsEnabled() && IsVisible();
        }

        public bool IsEnabled()
        {
            return enabled;
        }

        public void SetEnabled(bool enabled)
        {
            this.enabled = enabled;
        }

        public bool IsFocuable()
        {
            return focusable;
        }

        public void SetFocusable(bool focusable)
        {
            this.focusable = focusable;
        }

        public bool IsTraversalKeysEnabled()
        {
            return traversalKeysEnabled;
        }

        public void SetTraversalKeysEnabled(bool traversalKeysEnabled)
        {
            this.traversalKeysEnabled = traversalKeysEnabled;
        }

        public void SetFocusTraversalPolicy(FocusTraversalPolicy policy)
        {
            focusTraversalPolicy = policy;
        }

        public virtual FocusTraversalPolicy GetFocusTraversalPolicy()
        {
            if (focusTraversalPolicy != null)
                return focusTraversalPolicy;

            BaseElement parent = GetParent();
            if (parent != null && parent is UiComponent)
            {
                return ((UiComponent)parent).GetFocusTraversalPolicy();
            }

            return null;
        }
    }
}