using System;

using System.Collections.Generic;



namespace asap.ui
{
    public interface Focusable
    {
        bool CanAcceptFocus(FocusType focusType);
        void Focus(FocusType focusType);
        void Blur();
    }
    
    
}