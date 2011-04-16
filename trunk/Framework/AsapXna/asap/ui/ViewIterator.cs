using System;

using System.Collections.Generic;
using System.Diagnostics;



namespace asap.ui
{
    public class ViewIterator
     {
        private const int INITIAL_STACK_SIZE = 4;
        
        private BaseElement[] stack;
        
        private int pos;
        
        private BaseElement root;
        
        public ViewIterator(BaseElement root) 
        {
            this.root = root;
            stack = new BaseElement[INITIAL_STACK_SIZE];
            pos = -1;
        }
        
        public virtual BaseElement[] GetPath()
        {
            BaseElement[] res = new BaseElement[(pos) + 1];
            Array.Copy(stack, 0, res, 0, ((pos) + 1));
            return res;
        }
        
        public virtual BaseElement Last()
        {
            Clean();
            BaseElement v = root;
            while (true) 
            {
                Push(v);
                int childrenCount = GetChildrenCount(v);
                if (childrenCount == 0)
                    return v;
                
                v = GetChild(v, (childrenCount - 1));
            }
        }
        
        public virtual BaseElement First()
        {
            Clean();
            Push(root);
            return root;
        }
        
        public virtual BaseElement Prev()
        {
            if (!(IsEmpty())) 
            {
                BaseElement top = Pop();
                if (!(IsEmpty())) 
                {
                    BaseElement prev = Pop();
                    int index = GetChildIndex(prev, top);
                    if (index > 0) 
                    {
                        index--;
                        Push(prev);
                        while (true) 
                        {
                            BaseElement newTop = GetChild(prev, index);
                            Push(newTop);
                            if ((GetChildrenCount(newTop)) == 0)
                                return newTop;
                            
                            index = (GetChildrenCount(newTop)) - 1;
                            prev = newTop;
                        }
                    } 
                    else 
                    {
                        Push(prev);
                        return prev;
                    }
                } 
            } 
            return null;
        }
        
        public virtual BaseElement Next()
        {
            if (IsEmpty()) 
            {
                Push(root);
                return root;
            } 
            else 
            {
                BaseElement top = Pop();
                if ((GetChildrenCount(top)) > 0) 
                {
                    Push(top);
                    BaseElement newTop = GetChild(top, 0);
                    Push(newTop);
                    return newTop;
                } 
                else 
                {
                    while (!(IsEmpty())) 
                    {
                        BaseElement prev = Pop();
                        int index = GetChildIndex(prev, top);
                        if ((index + 1) < (GetChildrenCount(prev))) 
                        {
                            index++;
                            Push(prev);
                            BaseElement newTop = GetChild(prev, index);
                            Push(newTop);
                            return newTop;
                        } 
                        top = prev;
                    }
                }
            }
            return null;
        }
        
        private int GetChildIndex(BaseElement view, BaseElement child)
        {
            Debug.Assert(view is ViewComposite);
            return ((ViewComposite)(view)).IndexOf(child);
        }
        
        private int GetChildrenCount(BaseElement view)
        {
            if (view is ViewComposite) 
            {
                ViewComposite composite = ((ViewComposite)(view));
                return composite.GetViewsCount();
            } 
            return 0;
        }
        
        private BaseElement GetChild(BaseElement view, int index)
        {
            if (view is ViewComposite) 
            {
                ViewComposite composite = ((ViewComposite)(view));
                Debug.Assert((index >= 0) && (index < (composite.GetViewsCount())));
                return composite.GetView(index);
            } 
            Debug.Assert(false);
            return null;
        }
        
        private void Clean()
        {
            for (int i = 0; i < (stack.Length); i++)
                stack[i] = null;
            pos = -1;
        }
        
        private void EnsureCapacity()
        {
            while ((pos) >= (stack.Length)) 
            {
                BaseElement[] newItems = new BaseElement[(stack.Length) * 2];
                Array.Copy(stack, 0, newItems, 0, stack.Length);
                stack = newItems;
            }
        }
        
        private void Push(BaseElement view)
        {
            (pos)++;
            EnsureCapacity();
            stack[pos] = view;
        }
        
        private BaseElement Pop()
        {
            BaseElement res = stack[pos];
            stack[pos] = null;
            (pos)--;
            return res;
        }
        
        private bool IsEmpty()
        {
            return (pos) < 0;
        }
        
    }
    
    
}