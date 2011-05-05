using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace asap.resources
{
    public struct ResourceLoadInfo
    {
        public String resName;
        public int resId;
        public int resType;

        public ResourceLoadInfo(String resName, int resId, int resType)
        {
            this.resName = resName;
            this.resId = resId;
            this.resType = resType;
        }
    }
}
