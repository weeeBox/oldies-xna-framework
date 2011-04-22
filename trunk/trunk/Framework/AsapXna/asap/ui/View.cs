using System;
using System.Diagnostics;
using asap.visual;

namespace asap.ui
{
    public class View : BaseElementContainer
    {        
        public View() : this(0, 0)
        {
        }

        public View(float width, float height) : base(width, height)
        {
        }

        public void ResizeToFitViews()
        {
            ResizeToFitViews(true, true);
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
        
        public void ResizeToFitViews(bool horizontally, bool vertically)
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
        
        public void ResizeHorizontallyToFitViews()
        {
            ResizeToFitViews(true, false);            
        }
        
        public void ResizeVerticallyToFitViews()
        {
            ResizeToFitViews(false, true);            
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
        
        public void AttachLeft(BaseElement view)
        {
            AttachLeft(view, 0.0f);
        }

        public void AttachLeft(BaseElement view, float indent)
        {
            view.x = indent;
        }
        
        public void AttachHCenter(BaseElement view)
        {
            view.x = 0.5f * (Width - view.Width);            
        }
        
        public void AttachRight(BaseElement view)
        {
            AttachRight(view, 0.0f);
        }

        public void AttachRight(BaseElement view, float indent)
        {
            view.x = Width - view.Width - indent;
        }
        
        public void AttachTop(BaseElement view)
        {
            AttachTop(view, 0);
        }

        public void AttachTop(BaseElement view, float indent)
        {
            view.y = indent;
        }
        
        public void AttachVCenter(BaseElement view)
        {
            view.y = 0.5f * (Height - view.Height);
        }
        
        public void AttachBottom(BaseElement view, float indent)
        {
            view.y = Height - view.height - indent;
        }
        
        public void AttachVert(BaseElement view, float align)
        {
            view.y = align * (Height - view.Height);
        }
        
        public void AttachHor(BaseElement view, float align)
        {
            view.x = align * (Width - view.Width);
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
                BaseElement view = GetChild(i);
                if (hor) 
                {                    
                    view.x = pos;
                    pos += view.Width + dist;
                } 
                else 
                {                    
                    view.y = pos;
                    pos += view.Height + dist;
                }
            }
        } 

        public virtual bool Contains(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
    }
}