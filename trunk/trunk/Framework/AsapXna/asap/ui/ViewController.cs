using System;

using System.Collections.Generic;

using asap.core;
using System.Diagnostics;

namespace asap.ui
{
    public class ViewController : KeyListener, PointerListener
     {
        private UiComponent root;
        
        private UiComponent focusedView;
        
        private ViewIterator focusIterator;
        
        private bool isKeyPressedHandledByFocus;
        
        private UiComponent activeView;
        
        private int activeViewX;
        
        private int activeViewY;
        
        private bool isPointerEntered;
        
        private List<FocusListener>  focusListeners = new List<FocusListener> ();
        
        public ViewController(UiComponent root) 
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
        
        public virtual UiComponent GetFocus()
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
            for (UiComponent component = iterator.Last(); component != null; component = iterator.Prev()) 
            {
                if (component is PointerListener) 
                {
                    UiComponent[] path = iterator.GetPath();
                    Debug.Assert(component == (path[((path.Length) - 1)]));
                    int absX = 0;
                    int absY = 0;
                    for (int i = 0; i < ((path.Length) - 1); i++) 
                    {
                        ViewComposite composite = ((ViewComposite)(path[i]));
                        int index = composite.IndexOf(path[(i + 1)]);
                        absX += composite.GetViewX(index);
                        absY += composite.GetViewY(index);
                    }
                    if (component.Contains((x - absX), (y - absY))) 
                    {
                        activeView = component;
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
            for (UiComponent component = focusIterator.First(); component != null; component = focusIterator.Next()) 
            {
                if ((component is Focusable) && (((Focusable)(component)).CanAcceptFocus(FocusType.FORWARD))) 
                {
                    SetViewFocused(component, FocusType.FORWARD);
                    return ;
                } 
            }
        }
        
        public virtual void FocusLastView()
        {
            for (UiComponent component = focusIterator.Last(); component != null; component = focusIterator.Prev()) 
            {
                if ((component is Focusable) && (((Focusable)(component)).CanAcceptFocus(FocusType.BACKWARD))) 
                {
                    SetViewFocused(component, FocusType.BACKWARD);
                    return ;
                } 
            }
        }
        
        public virtual void FocusNextView()
        {
            UiComponent next = FindNextFocus(true);
            SetViewFocused(next, FocusType.FORWARD);
        }
        
        public virtual void FocusPrevView()
        {
            UiComponent next = FindNextFocus(false);
            SetViewFocused(next, FocusType.BACKWARD);
        }
        
        public virtual void FocusView(UiComponent component)
        {
            if (component == (focusedView))
                return ;
            
            if (component == null) 
            {
                SetViewFocused(component, FocusType.DIRECT);
                return ;
            } 
            Debug.Assert(component is Focusable);
            for (UiComponent candidate = focusIterator.First(); candidate != null; candidate = focusIterator.Next()) 
            {
                if (candidate == component) 
                {
                    SetViewFocused(component, FocusType.DIRECT);
                    return ;
                } 
            }
            Debug.Assert(false);
        }
        
        private void SetViewFocused(UiComponent component, FocusType focusType)
        {
            if (component != (focusedView)) 
            {
                UiComponent prev = focusedView;
                if ((focusedView) != null)
                    ((Focusable)(focusedView)).Blur();
                
                focusedView = component;
                if ((focusedView) != null)
                    ((Focusable)(focusedView)).Focus(focusType);
                
                FireFocusChanged(focusType, prev, component);
            } 
        }
        
        private UiComponent FindNextFocus(bool forward)
        {
            UiComponent component;
            while ((component = forward ? focusIterator.Next() : focusIterator.Prev()) != null) 
            {
                if ((component is Focusable) && (((Focusable)(component)).CanAcceptFocus((forward ? FocusType.FORWARD : FocusType.BACKWARD))))
                    return component;
                
            }
            return null;
        }
        
        private void FireFocusChanged(FocusType focusType, UiComponent prev, UiComponent current)
        {
            for (int i = 0; i < focusListeners.Count; i++)
                focusListeners[i].FocusChanged(focusType, prev, current);
        }
        
    }
    
    
}