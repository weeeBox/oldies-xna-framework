using System;
using System.Collections.Generic;
using System.Diagnostics;
using asap.core;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using asap.graphics;

namespace asap.resources
{
    public abstract class ResourceMgr : TimerListener
    {
        private const float LOADING_TIME_INTERVAL = 1.0f / 20.0f;

        private TimerController timerController;
        private Timer timer;

        private ManagedResource[] resources;
        private List<ResourceLoadInfo> loadQueue;                
        private int loaded;        

        public ResourceMgrDelegate resourcesDelegate;

        public ResourceMgr(int capacity)
        {
            timerController = new TimerController();                       
            resources = new ManagedResource[capacity];
            loadQueue = new List<ResourceLoadInfo>(capacity);            
        }

        public void initLoading()
        {
            loadQueue.Clear();
            loaded = 0;
            CancelTimer();          
        }

        public void addResourceToLoadQueue(ref ResourceLoadInfo info)
        {            
            loadQueue.Add(info);
        }

        public void startLoading()
        {
            GC.Collect();

            timer = App.CreateTimer(this);
            timer.Schedule(LOADING_TIME_INTERVAL, true);
        }

        public void loadImmediately()
        {
            foreach (ResourceLoadInfo info in loadQueue)
            {
                if (loadResource(info) != null)
                {
                    loaded++;
                }
                else
                {
                    //TODO: handle resource load error
                    //ASSERT(FALSE);
                }
            }
        }

        public bool isBusy()
        {
            return timer.IsScheduled();
        }

        public int getPercentLoaded()
        {
            if (loadQueue.Count == 0)
            {
                return 100;
            }
            else
            {
                return ((100 * loaded) / loadQueue.Count);
            }
        }

        public object getResource(int resName)
        {
            return resources[resName];
        }

        public void freeResource(int resName)
        {
            Debug.Assert(resources[resName] != null);
            resources[resName].Dispose();
            resources[resName] = null;         
        }        

        public ManagedResource loadResource(ResourceLoadInfo r)
        {
            ManagedResource res = Load(r.resType, r.fileName);
            if (r.resId >= 0)
            {
                resources[r.resId] = res;
            }
            return res;
        }                               
        
        protected ManagedResource Load(int resType, string resName)
        {
            switch (resType)
            {
                case ResType.IMAGE:
                    return ResFactory.GetInstance().LoadImage(resName);
            }

            Debug.Assert(false, "Can't load resource: " + resName);
            return null;
        }


        public void Tick(float delta)
        {
            timerController.Tick(delta);
        }

        public void OnTimer(Timer timer)
        {
            ResourceLoadInfo r = loadQueue[loaded];
            if (loadResource(r) != null)
            {
                loaded++;
                if (resourcesDelegate != null)
                {
                    resourcesDelegate.resourceLoaded(r);
                }

                if (loaded == loadQueue.Count)
                {
                    if (resourcesDelegate != null)
                    {
                        GC.Collect();
                        resourcesDelegate.allResourcesLoaded();
                    }
                    CancelTimer();
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public TimerController GetTimerController()
        {
            return timerController;
        }        

        private void CancelTimer()
        {
            if (timer != null)
            {
                timer.Cancel();
                timer = null;
            }
        }
    }
}