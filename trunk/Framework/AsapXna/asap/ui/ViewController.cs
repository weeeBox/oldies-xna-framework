using System;

using System.Collections.Generic;

using java.asap.core;
using System.Diagnostics;

namespace java.asap.ui
{
    public class ViewController : KeyListener, PointerListener
     {
        private View root;
        
        private View focusedView;
        
        private ViewIterator focusIterator;
        
        private bool isKeyPressedHandledByFocus;
        
        private View activeView;
        
        private int activeViewX;
        
        private int activeViewY;
        
        private bool isPointerEntered;
        
        private List<FocusListener>  focusListeners = new List<FocusListener> ();
        
        public ViewController(View root) 
        {
            Debug.Assert(root != null);
            this.root = root;
            focusIterator = new ViewIterator(root);
            focusedView = null;
            isKeyPressedHandledByFocus = false;
            activeView = null;
            isPointerEntered = false;
        }
        
        public virtual void AddFocusListener(FocusListener focusListener)
        {
            focusListeners.Add(focusListener);
        }
        
        public virtual void RemoveFocusListener(FocusListener focusListener)
        {
            focusListeners.Remove(focusListener);
        }
        
        public virtual View GetFocus()
        {
            return focusedView;
        }
        
        public virtual bool KeyPressed(int keyCode, int keyAction)
        {
            if ((root) is KeyListener) 
            {
                if (((KeyListener)(root)).KeyPressed(keyCode, keyAction)) 
                {
                    return true;
                } 
            } 
            if (((focusedView) != null) && ((focusedView) is KeyListener)) 
            {
                if (((KeyListener)(focusedView)).KeyPressed(keyCode, keyAction))
                    return true;
                
            } 
            return HandleKeyFocusEvent(keyAction, true);
        }
        
        public virtual bool KeyReleased(int keyCode, int keyAction)
        {
            if ((root) is KeyListener) 
            {
                if (((KeyListener)(root)).KeyReleased(keyCode, keyAction)) 
                {
                    return true;
                } 
            } 
            if (((!(isKeyPressedHandledByFocus)) && ((focusedView) != null)) && ((focusedView) is KeyListener)) 
            {
                if (((KeyListener)(focusedView)).KeyReleased(keyCode, keyAction))
                    return true;
                
            } 
            return HandleKeyFocusEvent(keyAction, false);
        }
        
        public virtual bool KeyRepeated(int keyCode, int keyAction)
        {
            if ((root) is KeyListener) 
            {
                if (((KeyListener)(root)).KeyRepeated(keyCode, keyAction)) 
                {
                    return true;
                } 
            } 
            if (((focusedView) != null) && ((focusedView) is KeyListener)) 
            {
                if (((KeyListener)(focusedView)).KeyRepeated(keyCode, keyAction))
                    return true;
                
            } 
            return HandleKeyFocusEvent(keyAction, true);
        }
        
        private bool HandleKeyFocusEvent(int keyAction, bool pressed)
        {
            switch (keyAction)
            {
                case KeyAction.UP:
                    
                    {
                        if (pressed) 
                        {
                            isKeyPressedHandledByFocus = true;
                            FocusPrevView();
                            return (focusedView) != null;
                        } 
                        else if (isKeyPressedHandledByFocus) 
                        {
                            isKeyPressedHandledByFocus = false;
                            return true;
                        } 
                        break;
                    }
                case KeyAction.DOWN:
                    
                    {
                        if (pressed) 
                        {
                            isKeyPressedHandledByFocus = true;
                            FocusNextView();
                            return (focusedView) != null;
                        } 
                        else if (isKeyPressedHandledByFocus) 
                        {
                            isKeyPressedHandledByFocus = false;
                            return (focusedView) != null;
                        } 
                        break;
                    }
            }
            return false;
        }
        
        public virtual void PointerPressed(int x, int y, int fingerId)
        {
            if ((activeView) != null) 
            {
                ((PointerListener)(activeView)).PointerReleased((x - (activeViewX)), (y - (activeViewY)), fingerId);
                if (isPointerEntered)
                    ((PointerListener)(activeView)).PointerExited((x - (activeViewX)), (y - (activeViewY)), fingerId);
                
                isPointerEntered = false;
                activeView = null;
            } 
            FocusView(null);
            FindPointerListenerAtPoint(x, y);
            if ((activeView) != null) 
            {
                isPointerEntered = true;
                ((PointerListener)(activeView)).PointerEntered((x - (activeViewX)), (y - (activeViewY)), fingerId);
                ((PointerListener)(activeView)).PointerPressed((x - (activeViewX)), (y - (activeViewY)), fingerId);
            } 
        }
        
        public virtual void PointerReleased(int x, int y, int fingerId)
        {
            if ((activeView) != null) 
            {
                ((PointerListener)(activeView)).PointerReleased((x - (activeViewX)), (y - (activeViewY)), fingerId);
                if (isPointerEntered)
                    ((PointerListener)(activeView)).PointerExited((x - (activeViewX)), (y - (activeViewY)), fingerId);
                
                activeView = null;
                isPointerEntered = false;
            } 
        }
        
        public virtual void PointerDragged(int x, int y, int fingerId)
        {
            if ((activeView) != null) 
            {
                if ((isPointerEntered) && (!(activeView.Contains((x - (activeViewX)), (y - (activeViewY)))))) 
                {
                    ((PointerListener)(activeView)).PointerExited((x - (activeViewX)), (y - (activeViewY)), fingerId);
                    isPointerEntered = false;
                } 
                else if ((!(isPointerEntered)) && (activeView.Contains((x - (activeViewX)), (y - (activeViewY))))) 
                {
                    ((PointerListener)(activeView)).PointerEntered((x - (activeViewX)), (y - (activeViewY)), fingerId);
                    isPointerEntered = true;
                } 
                ((PointerListener)(activeView)).PointerDragged((x - (activeViewX)), (y - (activeViewY)), fingerId);
            } 
        }
        
        public virtual void PointerEntered(int x, int y, int fingerId)
        {
            if ((((activeView) != null) && (!(isPointerEntered))) && (activeView.Contains((x - (activeViewX)), (y - (activeViewY))))) 
            {
                isPointerEntered = true;
                ((PointerListener)(activeView)).PointerEntered((x - (activeViewX)), (y - (activeViewY)), fingerId);
            } 
        }
        
        public virtual void PointerExited(int x, int y, int fingerId)
        {
            if (((activeView) != null) && (isPointerEntered)) 
            {
                isPointerEntered = false;
                ((PointerListener)(activeView)).PointerExited((x - (activeViewX)), (y - (activeViewY)), fingerId);
            } 
        }
        
        private void FindPointerListenerAtPoint(int x, int y)
        {
            Debug.Assert((activeView) == null);
            ViewIterator iterator = new ViewIterator(root);
            for (View view = iterator.Last(); view != null; view = iterator.Prev()) 
            {
                if (view is PointerListener) 
                {
                    View[] path = iterator.GetPath();
                    Debug.Assert(view == (path[((path.Length) - 1)]));
                    int absX = 0;
                    int absY = 0;
                    for (int i = 0; i < ((path.Length) - 1); i++) 
                    {
                        ViewComposite composite = ((ViewComposite)(path[i]));
                        int index = composite.IndexOf(path[(i + 1)]);
                        absX += composite.GetViewX(index);
                        absY += composite.GetViewY(index);
                    }
                    if (view.Contains((x - absX), (y - absY))) 
                    {
                        activeView = view;
                        activeViewX = absX;
                        activeViewY = absY;
                        return ;
                    } 
                } 
            }
        }
        
        public virtual void BlurFocusedView()
        {
            if ((focusedView) != null) 
            {
                ((Focusable)(focusedView)).Blur();
                focusedView = null;
            } 
        }
        
        public virtual void FocusFirstView()
        {
            for (View view = focusIterator.First(); view != null; view = focusIterator.Next()) 
            {
                if ((view is Focusable) && (((Focusable)(view)).CanAcceptFocus(FocusType.FORWARD))) 
                {
                    SetViewFocused(view, FocusType.FORWARD);
                    return ;
                } 
            }
        }
        
        public virtual void FocusLastView()
        {
            for (View view = focusIterator.Last(); view != null; view = focusIterator.Prev()) 
            {
                if ((view is Focusable) && (((Focusable)(view)).CanAcceptFocus(FocusType.BACKWARD))) 
                {
                    SetViewFocused(view, FocusType.BACKWARD);
                    return ;
                } 
            }
        }
        
        public virtual void FocusNextView()
        {
            View next = FindNextFocus(true);
            SetViewFocused(next, FocusType.FORWARD);
        }
        
        public virtual void FocusPrevView()
        {
            View next = FindNextFocus(false);
            SetViewFocused(next, FocusType.BACKWARD);
        }
        
        public virtual void FocusView(View view)
        {
            if (view == (focusedView))
                return ;
            
            if (view == null) 
            {
                SetViewFocused(view, FocusType.DIRECT);
                return ;
            } 
            Debug.Assert(view is Focusable);
            for (View candidate = focusIterator.First(); candidate != null; candidate = focusIterator.Next()) 
            {
                if (candidate == view) 
                {
                    SetViewFocused(view, FocusType.DIRECT);
                    return ;
                } 
            }
            Debug.Assert(false);
        }
        
        private void SetViewFocused(View view, FocusType focusType)
        {
            if (view != (focusedView)) 
            {
                View prev = focusedView;
                if ((focusedView) != null)
                    ((Focusable)(focusedView)).Blur();
                
                focusedView = view;
                if ((focusedView) != null)
                    ((Focusable)(focusedView)).Focus(focusType);
                
                FireFocusChanged(focusType, prev, view);
            } 
        }
        
        private View FindNextFocus(bool forward)
        {
            View view;
            while ((view = forward ? focusIterator.Next() : focusIterator.Prev()) != null) 
            {
                if ((view is Focusable) && (((Focusable)(view)).CanAcceptFocus((forward ? FocusType.FORWARD : FocusType.BACKWARD))))
                    return view;
                
            }
            return null;
        }
        
        private void FireFocusChanged(FocusType focusType, View prev, View current)
        {
            for (int i = 0; i < focusListeners.Count; i++)
                focusListeners[i].FocusChanged(focusType, prev, current);
        }
        
    }
    
    
}