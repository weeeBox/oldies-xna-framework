using asap.core;
using asap.visual;
using System.Diagnostics;

namespace asap.ui
{
    public class DefaultFocusTraversalPolicy : FocusTraversalPolicy
    {
        public UiComponent GetComponentAfter(UiComponent container, UiComponent component)
        {
            int startIndex = component == null ? 0 : container.IndexOf(component) + 1;           
                        
            DynamicArray<BaseElement> childs = container.GetChilds();
            int childsCount = container.ChildsCount();
                        
            for (int childIndex = startIndex; childIndex < childsCount; ++childIndex)
            {
                BaseElement child = container.GetChild(childIndex);
                if (child != null && child is UiComponent)
                {
                    UiComponent childComponent = (UiComponent)child;
                    UiComponent focusedChild = childComponent.GetFocusTraversalPolicy().GetComponentAfter(childComponent, null);

                    if (focusedChild != null)
                    {
                        return focusedChild;
                    }

                    if (childComponent is Focusable && ((Focusable)childComponent).CanAcceptFocus())
                    {                        
                        return childComponent;                     
                    }
                }                
            }

            if (childsCount > 0)
            {
                UiComponent containerParent = (UiComponent)container.GetParent();
                if (containerParent != null)
                {
                    return containerParent.GetFocusTraversalPolicy().GetComponentAfter(containerParent, container);
                }
            }

            return null;
        }
                
        public UiComponent GetComponentBefore(UiComponent container, UiComponent component)
        {
            DynamicArray<BaseElement> childs = container.GetChilds();
            int childsCount = container.ChildsCount();

            int startIndex = component == null ? childsCount - 1 : container.IndexOf(component) - 1;            

            for (int childIndex = startIndex; childIndex >= 0; --childIndex)
            {
                BaseElement child = container.GetChild(childIndex);
                if (child != null && child is UiComponent)
                {
                    UiComponent childComponent = (UiComponent)child;
                    UiComponent focusedChild = childComponent.GetFocusTraversalPolicy().GetComponentBefore(childComponent, null);

                    if (focusedChild != null)
                    {
                        return focusedChild;
                    }

                    if (childComponent is Focusable && ((Focusable)childComponent).CanAcceptFocus())
                    {
                        return childComponent;
                    }
                }
            }

            if (childsCount > 0)
            {
                UiComponent containerParent = (UiComponent)container.GetParent();
                if (containerParent != null)
                {
                    return containerParent.GetFocusTraversalPolicy().GetComponentBefore(containerParent, container);
                }
            }

            return null;
        }
                
        public UiComponent GetFirstComponent(UiComponent container)
        {
            DynamicArray<BaseElement> childs = container.GetChilds();
            int childsCount = container.ChildsCount();

            for (int childIndex = 0; childIndex < childsCount; ++childIndex)
            {
                BaseElement child = container.GetChild(childIndex);
                if (child != null && child is UiComponent)
                {
                    UiComponent childComponent = (UiComponent)child;
                    UiComponent focusedChild = childComponent.GetFocusTraversalPolicy().GetFirstComponent(childComponent);

                    if (focusedChild != null)
                    {
                        return focusedChild;
                    }

                    if (childComponent is Focusable && ((Focusable)childComponent).CanAcceptFocus())
                    {
                        return childComponent;
                    }
                }
            }

            return null;
        }

        public UiComponent GetLastComponent(UiComponent container)
        {
            DynamicArray<BaseElement> childs = container.GetChilds();
            int childsCount = container.ChildsCount();

            for (int childIndex = childsCount - 1; childIndex >= 0; --childIndex)
            {
                BaseElement child = container.GetChild(childIndex);
                if (child != null && child is UiComponent)
                {
                    UiComponent childComponent = (UiComponent)child;
                    UiComponent focusedChild = childComponent.GetFocusTraversalPolicy().GetLastComponent(childComponent);

                    if (focusedChild != null)
                    {
                        return focusedChild;
                    }

                    if (childComponent is Focusable && ((Focusable)childComponent).CanAcceptFocus())
                    {
                        return childComponent;
                    }
                }
            }

            return null;
        }

        public UiComponent GetDefaultComponent(UiComponent container)
        {
            DynamicArray<BaseElement> childs = container.GetChilds();
            int childsCount = container.ChildsCount();
                        
            for (int childIndex = 0; childIndex < childsCount; ++childIndex)
            {
                BaseElement child = container.GetChild(childIndex);
                if (child != null && child is UiComponent)
                {
                    UiComponent childComponent = (UiComponent)child;
                    UiComponent focusedChild = childComponent.GetFocusTraversalPolicy().GetDefaultComponent(childComponent);

                    if (focusedChild != null)
                    {
                        return focusedChild;
                    }

                    if (childComponent is Focusable && ((Focusable)childComponent).CanAcceptFocus())
                    {
                        return childComponent;
                    }
                }                
            }            

            return null;
        }                                      
    }
}
