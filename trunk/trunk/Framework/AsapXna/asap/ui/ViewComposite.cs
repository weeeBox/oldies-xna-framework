using System;

using System.Collections.Generic;

namespace asap.ui
{
    public interface ViewComposite
    {
        int GetViewsCount();
        View GetView(int index);
        int IndexOf(View view);
        int GetViewX(int index);
        int GetViewY(int index);
    }    
}