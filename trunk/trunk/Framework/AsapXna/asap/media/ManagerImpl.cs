using System;

using System.Collections.Generic;
using System.Diagnostics;



namespace java.asap.media
{
    abstract public class ManagerImpl
    {
        private static ManagerImpl instance;

        public ManagerImpl()
        {
            Debug.Assert((ManagerImpl.instance) == null);
            ManagerImpl.instance = this;
        }

        public static ManagerImpl GetInstance()
        {
            return ManagerImpl.instance;
        }

        public abstract Player CreatePlayer(String locator);
    }
}