using System;

using System.Collections.Generic;



namespace java.asap.ui
{
    public interface Focusable
    {
        bool CanAcceptFocus(FocusType focusType);
        void Focus(FocusType focusType);
        void Blur();
    }
    
    
}