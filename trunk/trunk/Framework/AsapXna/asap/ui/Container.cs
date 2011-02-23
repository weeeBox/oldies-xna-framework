using System;

using System.Collections.Generic;


using java.asap.graphics;
using System.Diagnostics;

namespace java.asap.ui
{
    public class Container : View, ViewComposite
     {
        public const int INITIAL_VIEWS_COUNT = 8;
        
        private int viewsCount = 0;
        
        private View[] views;
        
        private int[] xPositions;
        
        private int[] yPositions;
        
        private int width;
        
        private int height;
        
        public Container() 
        {
            width = 0;
            height = 0;
            viewsCount = 0;
            views = new View[INITIAL_VIEWS_COUNT];
            xPositions = new int[INITIAL_VIEWS_COUNT];
            yPositions = new int[INITIAL_VIEWS_COUNT];
        }
        
        public override void Draw(Graphics g)
        {
            int clipLeft = g.GetClipX();
            int clipTop = g.GetClipY();
            int clipRight = clipLeft + (g.GetClipWidth());
            int clipBottom = clipTop + (g.GetClipHeight());
            for (int i = 0; i < (viewsCount); i++) 
            {
                View view = views[i];
                int left = GetViewX(i);
                int top = GetViewY(i);
                int right = left + (view.GetWidth());
                int bottom = top + (view.GetHeight());
                if (((((view.IsVisible()) && (clipLeft <= right)) && (clipRight > left)) && (clipTop <= bottom)) && (clipBottom > top)) 
                {
                    g.Translate(left, top);
                    view.Draw(g);
                    g.Translate(-left, -top);
                } 
            }
            base.Draw(g);
        }
        
        public virtual int GetViewsCount()
        {
            return viewsCount;
        }
        
        public virtual View GetView(int index)
        {
            return views[index];
        }
        
        public virtual int IndexOf(View view)
        {
            for (int i = 0; i < (viewsCount); i++) 
            {
                if ((views[i]) == view)
                    return i;
                
            }
            return -1;
        }
        
        public virtual int GetViewX(int index)
        {
            return xPositions[index];
        }
        
        public virtual Container SetViewX(int index, int x)
        {
            xPositions[index] = x;
            return this;
        }
        
        public virtual int GetViewY(int index)
        {
            return yPositions[index];
        }
        
        public virtual Container SetViewY(int index, int y)
        {
            yPositions[index] = y;
            return this;
        }
        
        public override int GetWidth()
        {
            return width;
        }
        
        public override int GetHeight()
        {
            return height;
        }
        
        public virtual void SetWidth(int width)
        {
            this.width = width;
        }
        
        public virtual void SetHeight(int height)
        {
            this.height = height;
        }
        
        public virtual Container SetSize(int width, int height)
        {
            SetWidth(width);
            SetHeight(height);
            return this;
        }
        
        public virtual int AddView(View view)
        {
            Debug.Assert(view != null);
            EnsureCapacity();
            views[viewsCount] = view;
            xPositions[viewsCount] = 0;
            yPositions[viewsCount] = 0;
            (viewsCount)++;
            return (viewsCount) - 1;
        }
        
        public virtual Container ReplaceView(int index, View view)
        {
            Debug.Assert((index >= 0) && (index < (viewsCount)));
            views[index] = view;
            return this;
        }
        
        public virtual Container ReplaceView(View original, View replaced)
        {
            for (int i = 0; i < (viewsCount); ++i) 
            {
                if ((views[i]) == original) 
                {
                    views[i] = replaced;
                    return this;
                } 
            }
            Debug.Assert(false, "no such view");
            return this;
        }
        
        public virtual Container RemoveView(View view)
        {
            int index = -1;
            for (int i = 0; i < (viewsCount); i++) 
            {
                if (view == (views[i])) 
                {
                    index = i;
                    break;
                } 
            }
            Debug.Assert(index != (-1));
            for (int i = index; i < ((viewsCount) - 1); i++) 
            {
                views[i] = views[(i + 1)];
            }
            (viewsCount)--;
            return this;
        }
        
        public virtual Container Clean()
        {
            for (int i = 0; i < (viewsCount); i++)
                views[i] = null;
            viewsCount = 0;
            return this;
        }
        
        public virtual bool Contains(View view)
        {
            for (int i = 0; i < (views.Length); i++) 
            {
                if ((views[i]) == view)
                    return true;
                
            }
            return false;
        }
        
        public virtual Container MoveToFront(View view)
        {
            RemoveView(view);
            AddView(view);
            return this;
        }
        
        public virtual Container ResizeToFitViews()
        {
            return ResizeToFitViews(true, true);
        }
        
        public virtual Container AddMargin(int margin)
        {
            return AddMargin(margin, margin, margin, margin);
        }
        
        public virtual Container AddMargin(int leftMargin, int topMargin, int rightMargin, int bottomMargin)
        {
            int left = -leftMargin;
            int top = -topMargin;
            int right = (GetWidth()) + rightMargin;
            int bottom = (GetHeight()) + bottomMargin;
            for (int i = 0; i < (GetViewsCount()); i++) 
            {
                SetViewX(i, ((GetViewX(i)) - left));
                SetViewY(i, ((GetViewY(i)) - top));
            }
            width = right - left;
            height = bottom - top;
            return this;
        }
        
        public virtual Container ResizeToFitViews(bool horizontally, bool vertically)
        {
            Debug.Assert(horizontally || vertically);
            if ((GetViewsCount()) == 0) 
            {
                width = height = 0;
                return this;
            } 
            int left = GetViewX(0);
            int top = GetViewY(0);
            int right = (GetViewX(0)) + (GetView(0).GetWidth());
            int bottom = (GetViewY(0)) + (GetView(0).GetHeight());
            for (int i = 1; i < (GetViewsCount()); i++) 
            {
                View view = GetView(i);
                left = Math.Min(left, GetViewX(i));
                top = Math.Min(top, GetViewY(i));
                right = Math.Max(right, ((GetViewX(i)) + (view.GetWidth())));
                bottom = Math.Max(bottom, ((GetViewY(i)) + (view.GetHeight())));
            }
            for (int i = 0; i < (GetViewsCount()); i++) 
            {
                SetViewX(i, (horizontally ? (GetViewX(i)) - left : GetViewX(i)));
                SetViewY(i, (vertically ? (GetViewY(i)) - top : GetViewY(i)));
            }
            width = horizontally ? right - left : GetWidth();
            height = vertically ? bottom - top : GetHeight();
            return this;
        }
        
        public virtual Container ResizeHorizontallyToFitViews()
        {
            ResizeToFitViews(true, false);
            return this;
        }
        
        public virtual Container ResizeVerticallyToFitViews()
        {
            ResizeToFitViews(false, true);
            return this;
        }
        
        public virtual Container SpreadVertically(int distance)
        {
            int pos = 0;
            for (int i = 0; i < (GetViewsCount()); i++) 
            {
                SetViewY(i, pos);
                pos += (views[i].GetHeight()) + distance;
            }
            return this;
        }
        
        public virtual Container SpreadHorizontally(int distance)
        {
            int pos = 0;
            for (int i = 0; i < (GetViewsCount()); i++) 
            {
                SetViewX(i, pos);
                pos += (views[i].GetWidth()) + distance;
            }
            return this;
        }
        
        public virtual Container SpreadVertically()
        {
            SpreadHelper(false);
            return this;
        }
        
        public virtual Container SpreadHorizontally()
        {
            SpreadHelper(true);
            return this;
        }
        
        public virtual Container AlignLeft(View view)
        {
            SetViewX(IndexOf(view), 0);
            return this;
        }
        
        public virtual Container AlignCenter(View view)
        {
            SetViewX(IndexOf(view), (((GetWidth()) - (view.GetWidth())) / 2));
            return this;
        }
        
        public virtual Container AlignRight(View view)
        {
            SetViewX(IndexOf(view), ((GetWidth()) - (view.GetWidth())));
            return this;
        }
        
        public virtual Container AlignTop(View view)
        {
            SetViewY(IndexOf(view), 0);
            return this;
        }
        
        public virtual Container AlignMiddle(View view)
        {
            SetViewY(IndexOf(view), (((GetHeight()) - (view.GetHeight())) / 2));
            return this;
        }
        
        public virtual Container AlignBottom(View view)
        {
            SetViewY(IndexOf(view), ((GetHeight()) - (view.GetHeight())));
            return this;
        }
        
        public virtual Container AlignVertically(View view, int percent)
        {
            SetViewY(IndexOf(view), ((((GetHeight()) - (view.GetHeight())) * percent) / 100));
            return this;
        }
        
        public virtual Container AlignHorizontally(View view, int percent)
        {
            SetViewX(IndexOf(view), ((((GetWidth()) - (view.GetWidth())) * percent) / 100));
            return this;
        }
        
        public virtual Container AlignCenterAll()
        {
            for (int i = 0; i < (GetViewsCount()); i++)
                AlignCenter(GetView(i));
            return this;
        }
        
        public virtual Container AlignMiddleAll()
        {
            for (int i = 0; i < (GetViewsCount()); i++)
                AlignMiddle(GetView(i));
            return this;
        }
        
        private void SpreadHelper(bool hor)
        {
            if ((GetViewsCount()) == 0)
                return ;
            
            int totalSize = 0;
            for (int i = 0; i < (GetViewsCount()); i++)
                totalSize += hor ? GetView(i).GetWidth() : GetView(i).GetHeight();
            int size = hor ? GetWidth() : GetHeight();
            int dist = 0;
            int pos = 0;
            if (size == totalSize) 
            {
                dist = 0;
                pos = 0;
            } 
            else if ((size >= totalSize) || ((GetViewsCount()) == 1)) 
            {
                dist = (size - totalSize) / ((GetViewsCount()) + 1);
                pos = dist;
            } 
            else 
            {
                dist = (size - totalSize) / ((GetViewsCount()) - 1);
            }
            for (int i = 0; i < (GetViewsCount()); i++) 
            {
                View view = GetView(i);
                if (hor) 
                {
                    SetViewX(i, pos);
                    pos += (view.GetWidth()) + dist;
                } 
                else 
                {
                    SetViewY(i, pos);
                    pos += (view.GetHeight()) + dist;
                }
            }
        }
        
        private void EnsureCapacity()
        {
            if ((viewsCount) == (views.Length)) 
            {
                View[] newViews = new View[(views.Length) * 2];
                Array.Copy(views, 0, newViews, 0, views.Length);
                views = newViews;
                int[] newXPositions = new int[(views.Length) * 2];
                Array.Copy(xPositions, 0, newXPositions, 0, xPositions.Length);
                xPositions = newXPositions;
                int[] newYPositions = new int[(views.Length) * 2];
                Array.Copy(yPositions, 0, newYPositions, 0, yPositions.Length);
                yPositions = newYPositions;
            } 
        }
        
    }
    
    
}