using System;

using System.Collections.Generic;

namespace asap.ui
{
    public interface ViewComposite
    {
        int GetViewsCount();
        UiComponent GetView(int index);
        int IndexOf(UiComponent component);
        int GetViewX(int index);
        int GetViewY(int index);
    }    
}