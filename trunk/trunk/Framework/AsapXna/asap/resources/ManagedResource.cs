using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace asap.resources
{
    public abstract class ManagedResource : IDisposable
    {
        public virtual void Dispose()
        {

        }
    }
}
