using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace asap.resources
{
    public class ResourceLoadInfo
    {
        public String fileName;
        public int resId;
        public int resType;

        public ResourceLoadInfo(String fileName, int resId, int resType)
        {
            this.fileName = fileName;
            this.resId = resId;
            this.resType = resType;
        }
    }
}
