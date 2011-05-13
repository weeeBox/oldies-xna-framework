using System;

using System.Collections.Generic;
using System.Diagnostics;



namespace asap.ui
{
    public class ViewIterator
     {
        private const int INITIAL_STACK_SIZE = 4;
        
        private UiComponent[] stack;
        
        private int pos;
        
        private UiComponent root;
        
        public ViewIterator(UiComponent root) 
        {
            this.root = root;
            stack = new UiComponent[INITIAL_STACK_SIZE];
            pos = -1;
        }
        
        public virtual UiComponent[] GetPath()
        {
            UiComponent[] res = new UiComponent[(pos) + 1];
            Array.Copy(stack, 0, res, 0, ((pos) + 1));
            return res;
        }
        
        public virtual UiComponent Last()
        {
            Clean();
            UiComponent v = root;
            while (true) 
            {
                Push(v);
                int childrenCount = GetChildrenCount(v);
                if (childrenCount == 0)
                    return v;
                
                v = GetChild(v, (childrenCount - 1));
            }
        }
        
        public virtual UiComponent First()
        {
            Clean();
            Push(root);
            return root;
        }
        
        public virtual UiComponent Prev()
        {
            if (!(IsEmpty())) 
            {
                UiComponent top = Pop();
                if (!(IsEmpty())) 
                {
                    UiComponent prev = Pop();
                    int index = GetChildIndex(prev, top);
                    if (index > 0) 
                    {
                        index--;
                        Push(prev);
                        while (true) 
                        {
                            UiComponent newTop = GetChild(prev, index);
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
        
        public virtual UiComponent Next()
        {
            if (IsEmpty()) 
            {
                Push(root);
                return root;
            } 
            else 
            {
                UiComponent top = Pop();
                if ((GetChildrenCount(top)) > 0) 
                {
                    Push(top);
                    UiComponent newTop = GetChild(top, 0);
                    Push(newTop);
                    return newTop;
                } 
                else 
                {
                    while (!(IsEmpty())) 
                    {
                        UiComponent prev = Pop();
                        int index = GetChildIndex(prev, top);
                        if ((index + 1) < (GetChildrenCount(prev))) 
                        {
                            index++;
                            Push(prev);
                            UiComponent newTop = GetChild(prev, index);
                            Push(newTop);
                            return newTop;
                        } 
                        top = prev;
                    }
                }
            }
            return null;
        }
        
        private int GetChildIndex(UiComponent component, UiComponent child)
        {
            Debug.Assert(component is ViewComposite);
            return ((ViewComposite)(component)).IndexOf(child);
        }
        
        private int GetChildrenCount(UiComponent component)
        {
            if (component is ViewComposite) 
            {
                ViewComposite composite = ((ViewComposite)(component));
                return composite.GetViewsCount();
            } 
            return 0;
        }
        
        private UiComponent GetChild(UiComponent component, int index)
        {
            if (component is ViewComposite) 
            {
                ViewComposite composite = ((ViewComposite)(component));
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
                UiComponent[] newItems = new UiComponent[(stack.Length) * 2];
                Array.Copy(stack, 0, newItems, 0, stack.Length);
                stack = newItems;
            }
        }
        
        private void Push(UiComponent component)
        {
            (pos)++;
            EnsureCapacity();
            stack[pos] = component;
        }
        
        private UiComponent Pop()
        {
            UiComponent res = stack[pos];
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