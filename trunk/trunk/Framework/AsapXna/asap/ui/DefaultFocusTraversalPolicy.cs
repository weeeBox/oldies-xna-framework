using asap.core;
using asap.visual;
using System.Diagnostics;

namespace asap.ui
{
    public class DefaultFocusTraversalPolicy : FocusTraversalPolicy
    {
        private enum SearchType
        {
            After,
            Before,
            First,
            Last,
            Default
        };

        public UiComponent GetComponentAfter(UiComponent container, UiComponent component)
        {
            int componentIndex = container.IndexOf(component);
            Debug.Assert(componentIndex != -1);
                        
            return GetComponentHelper(container, componentIndex + 1, SearchType.After);
        }
                
        public UiComponent GetComponentBefore(UiComponent container, UiComponent component)
        {
            int componentIndex = container.IndexOf(component);
            Debug.Assert(componentIndex != -1);            

            return GetComponentHelper(container, componentIndex - 1, SearchType.Before);
        }
                
        public UiComponent GetFirstComponent(UiComponent container)
        {
            return GetComponentHelper(container, 0, SearchType.First);
        }

        public UiComponent GetLastComponent(UiComponent container)
        {
            return GetComponentHelper(container, container.ChildsCount() - 1, SearchType.Last);
        }

        public UiComponent GetDefaultComponent(UiComponent container)
        {
            return GetComponentHelper(container, 0, SearchType.Default);
        }                               

        private UiComponent GetComponentHelper(UiComponent container, int fromIndex, SearchType searchType)
        {
            DynamicArray<BaseElement> childs = container.GetChilds();
            int childsCount = container.ChildsCount();            

            bool searchForward = searchType == SearchType.After || searchType == SearchType.Default || searchType == SearchType.First;
            for (int childIndex = fromIndex; ; )
            {
                if (searchForward)
                {
                    if (childIndex >= childsCount)
                        break;
                }
                else
                {
                    if (childIndex < 0)
                        break;
                }

                BaseElement child = container.GetChild(childIndex);
                if (child != null && child is UiComponent)
                {
                    UiComponent childComponent = (UiComponent)child;
                    UiComponent focusedChild = null;
                    if (searchType == SearchType.After || searchType == SearchType.First)
                        focusedChild = GetComponentHelper(childComponent, null, SearchType.First);
                    else if (searchType == SearchType.Before || searchType == SearchType.Last)
                        focusedChild = GetComponentHelper(childComponent, null, SearchType.Last);                                        
                    else
                        focusedChild = GetComponentHelper(childComponent, null, SearchType.Default);


                    if (focusedChild != null)
                    {
                        return focusedChild;
                    }                    
                }

                if (searchForward)
                    childIndex++;
                else
                    childIndex--;
            }

            if (container is Focusable)
            {
                Focusable focusable = (Focusable)container;
                if (focusable.CanAcceptFocus())
                {
                    return container;
                }
            }

            BaseElement parentElement = container.GetParent();
            if (parentElement != null && parentElement is UiComponent)
            {
                UiComponent parentContainer = (UiComponent)parentElement;
                return GetComponentHelper(parentContainer, container, searchType);
            }

            return null;
        }

        private UiComponent GetComponentHelper(UiComponent container, UiComponent component, SearchType searchType)
        {
            if (container.IsTraversalKeysEnabled())
            {
                FocusTraversalPolicy traversal = container.GetFocusTraversalPolicy();
                Debug.Assert(traversal != null);

                switch (searchType)
                {
                    case SearchType.After:
                        return traversal.GetComponentAfter(container, component);
                    case SearchType.Before:
                        return traversal.GetComponentBefore(container, component);
                    case SearchType.First:
                        return traversal.GetFirstComponent(container);
                    case SearchType.Last:
                        return traversal.GetLastComponent(container);
                    case SearchType.Default:
                        return traversal.GetDefaultComponent(container);
                    default:
                        Debug.Assert(false, "Bad search type: " + searchType);
                        break;
                }                
            }            

            return null;
        }
    }
}
