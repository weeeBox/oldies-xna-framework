using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace asap.resources
{
    public interface ResourceMgrListener
    {
        void resourceLoaded(ResourceLoadInfo res);
        void allResourcesLoaded();
    }
}
