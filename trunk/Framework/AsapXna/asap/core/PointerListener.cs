using System;

using System.Collections.Generic;

namespace asap.core
{
    public interface PointerListener
    {
        void PointerPressed(int x, int y, int fingerId);
        void PointerReleased(int x, int y, int fingerId);
        void PointerDragged(int x, int y, int fingerId);
        void PointerExited(int x, int y, int fingerId);
        void PointerEntered(int x, int y, int fingerId);
    }    
}