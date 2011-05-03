using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.core;
using System.Diagnostics;
using Game.app.menu;

namespace app
{
    public class MenuController : Controller
    {
        public override void Start(int param)
        {
            base.Start(param);

            Application.sharedScreensView.StartScreen(new MainScreen());
        }
    }
}
