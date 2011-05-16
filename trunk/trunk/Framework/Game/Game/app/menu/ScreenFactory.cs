using System;

using System.Collections.Generic;


using app;
using asap.ui;
using asap.core;
using asap.graphics;
using asap.app;
using Game.app.menu;

namespace app.menu
{
    /** 
     * Factory for screens and screen elements.
     */
    public class ScreenFactory
    {
        public static Screen CreateMainMenu(ButtonListener listener)
        {
            Screen screen = new MainScreen();
            return screen;
        }             
    }    
}