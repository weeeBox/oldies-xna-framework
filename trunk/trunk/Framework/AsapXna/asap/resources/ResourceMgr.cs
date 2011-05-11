using System;
using System.Collections.Generic;
using System.Diagnostics;
using asap.app;
using asap.core;
using asap.graphics;
using AsapXna.asap.resources.types;

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

        public ResourceMgrListener listener;

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

        public void AddResourceToLoadQueue(ref ResourceLoadInfo info)
        {            
            loadQueue.Add(info);
        }

        public void StartLoading()
        {
            StartLoading(null);
        }

        public void StartLoading(ResourceMgrListener listener)
        {
            GC.Collect();

            this.listener = listener;
            timer = BaseApp.CreateTimer(this);
            timer.Schedule(LOADING_TIME_INTERVAL, true);
        }

        public void loadImmediately()
        {
            for (int resIndex = 0; resIndex < loadQueue.Count; ++resIndex)
            {
                ResourceLoadInfo info = loadQueue[resIndex];
                if (loadResource(ref info) != null)
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

        protected ManagedResource getResource(int resName)
        {
            ManagedResource resource = resources[resName];
            Debug.Assert(resource != null, "Resource not loaded: " + resName);
            return resource;
        }        

        public void freeResource(int resName)
        {
            Debug.Assert(resources[resName] != null);
            resources[resName].Dispose();
            resources[resName] = null;         
        }        

        public ManagedResource loadResource(ref ResourceLoadInfo info)
        {
            ManagedResource res = Load(ref info);
            if (info.resId >= 0)
            {
                resources[info.resId] = res;
            }
            return res;
        }                               
        
        protected virtual ManagedResource Load(ref ResourceLoadInfo info)
        {
            int resType = info.resType;
            string resName = info.resName;

            switch (resType)
            {
                case ResType.IMAGE:
                    return ResFactory.GetInstance().LoadImage(resName);

                case ResType.ATLAS:
                {
                    AtlasRes atlas = ResFactory.GetInstance().loadAtlas(resName);
                    
                    int resId = info.resId;                    
                    foreach (AtlasPartInfo part in atlas.Parts)
                    {
                        GameTexture tex = new GameTexture();
                        tex.setTexture(atlas, part.x, part.y, part.w, part.h);
                        resources[++resId] = tex;
                    }                    

                    return atlas;
                }

                case ResType.BITMAP_FONT:
                    return ResFactory.GetInstance().LoadFont(resName);

                case ResType.SWF:
                    return ResFactory.GetInstance().LoadSwfMovie(resName);
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
            ResourceLoadInfo info = loadQueue[loaded];
            if (loadResource(ref info) != null)
            {
                loaded++;
                if (listener != null)
                {
                    listener.resourceLoaded(ref info);
                }

                if (loaded == loadQueue.Count)
                {
                    if (listener != null)
                    {
                        GC.Collect();
                        listener.allResourcesLoaded();
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