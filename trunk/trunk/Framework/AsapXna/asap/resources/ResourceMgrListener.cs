using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace asap.resources
{
    public interface ResourceMgrListener
    {
        void resourceLoaded(ref ResourceLoadInfo res);
        void allResourcesLoaded();
    }
}
