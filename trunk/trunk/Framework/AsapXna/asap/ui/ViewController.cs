using System;

using System.Collections.Generic;

using asap.core;
using System.Diagnostics;
using asap.visual;

namespace asap.ui
{
    public class ViewController : KeyListener, PointerListener
    {
        private UiComponent root;
        
        private UiComponent focusedComponent;
                
        private bool isKeyPressedHandledByFocus;               
        
        private int activeViewX;
        
        private int activeViewY;
        
        private bool isPointerEntered;        
        
        public ViewController(UiComponent root) 
        {
            Debug.Assert(root != null);
            this.root = root;
                    
            focusedComponent = null;
            isKeyPressedHandledByFocus = false;            
            isPointerEntered = false;
        }        
        
        public virtual UiComponent GetFocusedComponent()
        {
            return focusedComponent;
        }

        public virtual bool KeyPressed(KeyEvent evt)
        {
            if (root is KeyListener) 
            {
                if (((KeyListener)root).KeyPressed(evt)) 
                {
                    return true;
                } 
            } 
            if (focusedComponent != null && focusedComponent is KeyListener) 
            {
                if (focusedComponent.IsTraversalKeysEnabled())
                {
                    bool focusNext = false;
                    bool focusPrev = false;

                    if ((focusNext = ContainsKeyCode(focusedComponent.GetNextFocusKeyCodes(), evt.code)) || 
                        (focusPrev = ContainsKeyCode(focusedComponent.GetPrevFocusKeyCodes(), evt.code)))
                    {
                        FocusTraversalPolicy traversal = focusedComponent.GetFocusTraversalPolicy();
                        Debug.Assert(traversal != null);
                                                
                        UiComponent nextFocusedComponent = null;
                        UiComponent container = (UiComponent)focusedComponent.GetParent();
                        if (focusNext)
                        {
                            nextFocusedComponent = traversal.GetComponentAfter(container, focusedComponent);
                            if (nextFocusedComponent == null)
                            {
                                nextFocusedComponent = traversal.GetFirstComponent(root);
                            }
                        }
                        else if (focusPrev)
                        {
                            nextFocusedComponent = traversal.GetComponentBefore(container, focusedComponent);
                            if (nextFocusedComponent == null)
                            {
                                nextFocusedComponent = traversal.GetLastComponent(root);
                            }
                        }

                        if (nextFocusedComponent != null)
                        {
                            FocusComponent(nextFocusedComponent);
                            return true;
                        }
                    }                    
                }

                if (((KeyListener)(focusedComponent)).KeyPressed(evt))
                    return true;
                
            }
            return false;
        }

        public virtual bool KeyReleased(KeyEvent evt)
        {
            if ((root) is KeyListener) 
            {
                if (((KeyListener)(root)).KeyReleased(evt)) 
                {
                    return true;
                } 
            } 
            if (((!(isKeyPressedHandledByFocus)) && ((focusedComponent) != null)) && ((focusedComponent) is KeyListener)) 
            {
                if (((KeyListener)(focusedComponent)).KeyReleased(evt))
                    return true;
                
            }
            return false;
        }        
        
        public virtual void PointerPressed(int x, int y, int fingerId)
        {
            if ((focusedComponent) != null) 
            {
                ((PointerListener)(focusedComponent)).PointerReleased((x - (activeViewX)), (y - (activeViewY)), fingerId);
                if (isPointerEntered)
                    ((PointerListener)(focusedComponent)).PointerExited((x - (activeViewX)), (y - (activeViewY)), fingerId);
                
                isPointerEntered = false;
                focusedComponent = null;
            } 
            FocusComponent(null);
            FindPointerListenerAtPoint(x, y);
            if ((focusedComponent) != null) 
            {
                isPointerEntered = true;
                ((PointerListener)(focusedComponent)).PointerEntered((x - (activeViewX)), (y - (activeViewY)), fingerId);
                ((PointerListener)(focusedComponent)).PointerPressed((x - (activeViewX)), (y - (activeViewY)), fingerId);
            } 
        }
        
        public virtual void PointerReleased(int x, int y, int fingerId)
        {
            if ((focusedComponent) != null) 
            {
                ((PointerListener)(focusedComponent)).PointerReleased((x - (activeViewX)), (y - (activeViewY)), fingerId);
                if (isPointerEntered)
                    ((PointerListener)(focusedComponent)).PointerExited((x - (activeViewX)), (y - (activeViewY)), fingerId);
                
                focusedComponent = null;
                isPointerEntered = false;
            } 
        }
        
        public virtual void PointerDragged(int x, int y, int fingerId)
        {
            if ((focusedComponent) != null) 
            {
                if ((isPointerEntered) && (!(focusedComponent.Contains((x - (activeViewX)), (y - (activeViewY)))))) 
                {
                    ((PointerListener)(focusedComponent)).PointerExited((x - (activeViewX)), (y - (activeViewY)), fingerId);
                    isPointerEntered = false;
                } 
                else if ((!(isPointerEntered)) && (focusedComponent.Contains((x - (activeViewX)), (y - (activeViewY))))) 
                {
                    ((PointerListener)(focusedComponent)).PointerEntered((x - (activeViewX)), (y - (activeViewY)), fingerId);
                    isPointerEntered = true;
                } 
                ((PointerListener)(focusedComponent)).PointerDragged((x - (activeViewX)), (y - (activeViewY)), fingerId);
            } 
        }
        
        public virtual void PointerEntered(int x, int y, int fingerId)
        {
            if ((((focusedComponent) != null) && (!(isPointerEntered))) && (focusedComponent.Contains((x - (activeViewX)), (y - (activeViewY))))) 
            {
                isPointerEntered = true;
                ((PointerListener)(focusedComponent)).PointerEntered((x - (activeViewX)), (y - (activeViewY)), fingerId);
            } 
        }
        
        public virtual void PointerExited(int x, int y, int fingerId)
        {
            if (((focusedComponent) != null) && (isPointerEntered)) 
            {
                isPointerEntered = false;
                ((PointerListener)(focusedComponent)).PointerExited((x - (activeViewX)), (y - (activeViewY)), fingerId);
            } 
        }
        
        private void FindPointerListenerAtPoint(int x, int y)
        {
            //Debug.Assert((focusedComponent) == null);            
            //foreach (DisplayObject component in root.GetChilds())
            //{
            //    if (component is PointerListener) 
            //    {
            //        UiComponent[] path = iterator.GetPath();
            //        Debug.Assert(component == (path[((path.Length) - 1)]));
            //        int absX = 0;
            //        int absY = 0;
            //        for (int i = 0; i < ((path.Length) - 1); i++) 
            //        {
            //            ViewComposite composite = ((ViewComposite)(path[i]));
            //            int index = composite.IndexOf(path[(i + 1)]);
            //            absX += composite.GetViewX(index);
            //            absY += composite.GetViewY(index);
            //        }
            //        if (component.Contains((x - absX), (y - absY))) 
            //        {
            //            focusedComponent = component;
            //            activeViewX = absX;
            //            activeViewY = absY;
            //            return ;
            //        } 
            //    } 
            //}
            throw new NotImplementedException();
        }        
        
        public virtual void FocusComponent(UiComponent component)
        {
            if (component == focusedComponent)
                return ;
            
            if (component == null) 
            {
                SetComponentFocused(component);
                return ;
            } 
            Debug.Assert(component is Focusable);            
            SetComponentFocused(component);            
        }
        
        private void SetComponentFocused(UiComponent component)
        {
            if (component != focusedComponent) 
            {
                UiComponent prev = focusedComponent;
                if ((focusedComponent) != null)
                    ((Focusable)(focusedComponent)).FocusLost();
                
                focusedComponent = component;
                if ((focusedComponent) != null)
                    ((Focusable)(focusedComponent)).FocusGained();
            } 
        }        

        private bool ContainsKeyCode(List<KeyCode> set, KeyCode code)
        {
            if (set == null)
                return false;

            return set.Contains(code);
        }

        public void FocusDefaultComponent()
        {
            FocusTraversalPolicy traversal = root.GetFocusTraversalPolicy();
            FocusComponent(traversal.GetDefaultComponent(root));
        }

        public void RemoveFocus()
        {
            if (focusedComponent != null)
            {
                ((Focusable)focusedComponent).FocusLost();
                focusedComponent = null;
            }
        }
    }    
}