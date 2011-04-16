using System;

using System.Collections.Generic;



namespace asap.ui
{
    public interface ViewComposite
    {
        int GetViewsCount();
        BaseElement GetView(int index);
        int IndexOf(BaseElement view);
        int GetViewX(int index);
        int GetViewY(int index);
    }
    
    
}