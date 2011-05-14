using System;

using System.Collections.Generic;



namespace asap.core
{
    public interface KeyListener
    {
        bool KeyPressed(KeyEvent evt);
        bool KeyReleased(KeyEvent evt);        
    }    
}