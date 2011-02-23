using System;

using System.Collections.Generic;



namespace java.asap.core
{
    public interface KeyListener
    {
        bool KeyPressed(int keyCode, int keyAction);
        bool KeyReleased(int keyCode, int keyAction);
        bool KeyRepeated(int keyCode, int keyAction);
    }
    
    
}