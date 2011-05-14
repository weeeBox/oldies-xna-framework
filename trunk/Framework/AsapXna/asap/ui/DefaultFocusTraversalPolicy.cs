using asap.core;
using asap.visual;

namespace asap.ui
{
    public class DefaultFocusTraversalPolicy : FocusTraversalPolicy
    {
        public UiComponent GetComponentAfter(UiComponent container, UiComponent component)
        {
            return FindNext(container, container.IndexOf(component) + 1);
        }

        public UiComponent GetComponentBefore(UiComponent container, UiComponent component)
        {           
            return FindPrev(container, container.IndexOf(component) - 1);
        }

        public UiComponent GetFirstComponent(UiComponent container)
        {
            return FindNext(container, 0);
        }

        public UiComponent GetastComponent(UiComponent container)
        {
            return FindPrev(container, container.ChildsCount() - 1);
        }

        public UiComponent GetDefaultComponent(UiComponent container)
        {
            return GetFirstComponent(container);
        }

        private UiComponent FindNext(UiComponent container, int fromIndex)
        {
            DynamicArray<BaseElement> childs = container.GetChilds();
            int childsCount = container.ChildsCount();
            for (int childIndex = fromIndex; childIndex < childsCount; ++childIndex)
            {
                BaseElement child = container.GetChild(childIndex);
                if (child != null && child is UiComponent)
                {
                    UiComponent component = (UiComponent)child;
                    if (component.IsAcceptingFocus())
                    {
                        return component;
                    }
                }
            }

            return null;
        }

        private UiComponent FindPrev(UiComponent container, int fromIndex)
        {
            DynamicArray<BaseElement> childs = container.GetChilds();            
            for (int childIndex = fromIndex; childIndex >= 0; --childIndex)
            {
                BaseElement child = container.GetChild(childIndex);
                if (child != null && child is UiComponent)
                {
                    UiComponent component = (UiComponent)child;
                    if (component.IsAcceptingFocus())
                    {
                        return component;
                    }
                }
            }

            return null;
        }                
    }
}
