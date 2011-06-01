using System;
using System.Diagnostics;
using asap.visual;
using asap.core;
using System.Collections.Generic;

namespace asap.ui
{
    public class UiComponent : DisplayObjectContainer
    {
        private bool enabled;        
        private bool traversalKeysEnabled;
        protected FocusTraversalPolicy focusTraversalPolicy;
        protected List<KeyCode> nextFocusKeyCodes;
        protected List<KeyCode> prevFocusKeyCodes;

        public UiComponent() : this(0, 0)
        {
        }

        public UiComponent(float width, float height) : base(width, height)
        {
            enabled = true;
            traversalKeysEnabled = true;
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
            foreach (DisplayObject child in childs)
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
        
        public void ArrangeVert(float distance)
        {
            float pos = 0;
            foreach (DisplayObject child in childs) 
            {
                child.y = pos;                
                pos += child.Height + distance;
            }            
        }
        
        public void ArrangeHor(float distance)
        {
            float pos = 0;
            foreach (DisplayObject child in childs) 
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
        
        public void AttachLeft(DisplayObject component)
        {
            AttachLeft(component, 0.0f);
        }

        public void AttachLeft(DisplayObject component, float indent)
        {
            component.x = indent;
        }
        
        public void AttachHCenter(DisplayObject component)
        {
            component.x = 0.5f * (Width - component.Width);            
        }
        
        public void AttachRight(DisplayObject component)
        {
            AttachRight(component, 0.0f);
        }

        public void AttachRight(DisplayObject component, float indent)
        {
            component.x = Width - component.Width - indent;
        }
        
        public void AttachTop(DisplayObject component)
        {
            AttachTop(component, 0);
        }

        public void AttachTop(DisplayObject component, float indent)
        {
            component.y = indent;
        }
        
        public void AttachVCenter(DisplayObject component)
        {
            component.y = 0.5f * (Height - component.Height);
        }
        
        public void AttachBottom(DisplayObject component, float indent)
        {
            component.y = Height - component.height - indent;
        }
        
        public void AttachVert(DisplayObject component, float align)
        {
            component.y = align * (Height - component.Height);
        }
        
        public void AttachHor(DisplayObject component, float align)
        {
            component.x = align * (Width - component.Width);
        }
        
        public void AttachCenter(DisplayObject component)
        {
            AttachHCenter(component);
            AttachVCenter(component);
        }

        public void AttachCenterAll()
        {
            foreach (DisplayObject child in childs)
            {
                AttachCenter(child);
            }            
        }

        public void AttachHCenterAll()
        {
            foreach (DisplayObject child in childs)
            {
                AttachHCenter(child);
            }            
        }
        
        public void AttachVCenterAll()
        {
            foreach (DisplayObject child in childs)
            {
                AttachVCenter(child);
            }            
        }
        
        private void ArrangeHelper(bool hor)
        {
            if (ChildsCount() == 0)
                return ;
            
            float totalSize = 0;
            foreach (DisplayObject child in childs)
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
                DisplayObject child = GetChildAt(i);
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

        public int IndexOf(DisplayObject element)
        {
            int childsCount = ChildsCount();
            for (int childIndex = 0; childIndex < childsCount; ++childIndex)
            {
                DisplayObject child = childs[childIndex];
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

        public bool IsEnabled()
        {
            return enabled;
        }

        public void SetEnabled(bool enabled)
        {
            this.enabled = enabled;
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

            DisplayObject parent = GetParent();
            if (parent != null && parent is UiComponent)
            {
                return ((UiComponent)parent).GetFocusTraversalPolicy();
            }

            return null;
        }

        public virtual List<KeyCode> GetNextFocusKeyCodes()
        {
            if (nextFocusKeyCodes != null)
                return nextFocusKeyCodes;

            DisplayObject parent = GetParent();
            if (parent != null && parent is UiComponent)
            {
                return ((UiComponent)parent).GetNextFocusKeyCodes();
            }

            return null;
        }

        public virtual List<KeyCode> GetPrevFocusKeyCodes()
        {
            if (prevFocusKeyCodes != null)
                return prevFocusKeyCodes;

            DisplayObject parent = GetParent();
            if (parent != null && parent is UiComponent)
            {
                return ((UiComponent)parent).GetPrevFocusKeyCodes();
            }

            return null;
        }
    }
}