using System;

using System.Collections.Generic;



namespace asap.ui
{
    public interface FocusListener
    {
        void FocusChanged(FocusType focusType, BaseElement prev, BaseElement current);
    }
    
    
}