using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace asap.ui
{
    public interface FocusTraversalPolicy
    {
        UiComponent GetComponentAfter(UiComponent container, UiComponent component);
        UiComponent GetComponentBefore(UiComponent container, UiComponent component);
        UiComponent GetFirstComponent(UiComponent container);
        UiComponent GetLastComponent(UiComponent container);
        UiComponent GetDefaultComponent(UiComponent container);
    }
}
