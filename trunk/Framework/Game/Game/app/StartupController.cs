using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.core;
using asap.resources;
using System.Diagnostics;

namespace app
{
    public class StartupController : Controller, ResourceMgrListener
    {
        public StartupController()
        {

        }

        public override void Start(int param)
        {
            base.Start(param);
            Application.sharedResourceMgr.LoadPacks(this, ResPacks.PACK_COMMON, ResPacks.PACK_MENU);            
        }

        public void resourceLoaded(ResourceLoadInfo res)
        {
            Debug.WriteLine("Resource loaded: " + res.fileName);
        }

        public void allResourcesLoaded()
        {
            Debug.WriteLine("Resources loaded");
            AppRootController.StartController(AppRootController.menuController, 0);
        }
    }
}
