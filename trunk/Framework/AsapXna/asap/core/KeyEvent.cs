using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace asap.core
{
    public struct KeyEvent
    {
        public KeyCode code;
        public KeyAction action;
        public int playerIndex;

        public KeyEvent(int playerIndex, KeyCode code, KeyAction action)
        {
            this.playerIndex = playerIndex;
            this.code = code;
            this.action = action;
        }
    }
}
