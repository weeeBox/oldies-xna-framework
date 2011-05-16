using System;

using System.Collections.Generic;



namespace asap.ui
{
    public interface Focusable
    {
        bool CanAcceptFocus();
        bool IsFocused();
        void FocusGained();
        void FocusLost();        
    }    
}