using System;
using System.Collections.Generic;
using System.Diagnostics;
using asap.app;
using asap.core;
using asap.graphics;
using AsapXna.asap.resources.types;
using asap.anim;

namespace asap.resources
{
    public abstract class ResourceMgr : TimerListener
    {
        private const float LOADING_TIME_INTERVAL = 1.0f / 20.0f;

        private TimerController timerController;
        private Timer timer;

        private ResourceLoadInfo[][] resourcesData;
        private ManagedResource[] resources;
        private List<ResourceLoadInfo> loadQueue;                
        private int loaded;        

        public ResourceMgrListener listener;

        public ResourceMgr(int capacity, ResourceLoadInfo[][] resourcesData)
        {
            this.resourcesData = resourcesData;
            timerController = new TimerController();                       
            resources = new ManagedResource[capacity];
            loadQueue = new List<ResourceLoadInfo>(capacity);            
        }

        public void InitLoading()
        {
            loadQueue.Clear();
            loaded = 0;
            CancelTimer();          
        }

        public void AddResourceToLoadQueue(ref ResourceLoadInfo info)
        {            
            loadQueue.Add(info);
        }

        public void LoadPacks(ResourceMgrListener listener, params int[] packs)
        {
            InitLoading();
            foreach (int packIndex in packs)
            {
                AddPackToLoad(packIndex);
            }
            StartLoading(listener);
        }

        public void AddPackToLoad(int packIndex)
        {
            Debug.Assert(packIndex >= 0 && packIndex < resourcesData.Length);

            ResourceLoadInfo[] infos = resourcesData[packIndex];
            for (int resIndex = 0; resIndex < infos.Length; ++resIndex)
            {
                AddResourceToLoadQueue(ref infos[resIndex]);
            }
        }

        public void UnloadPack(int packIndex)
        {
            Debug.Assert(packIndex >= 0 && packIndex < resourcesData.Length);
            ResourceLoadInfo[] infos = resourcesData[packIndex];
            for (int resIndex = 0; resIndex < infos.Length; ++resIndex)
            {
                FreeResource(infos[resIndex].resId);
            }
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

        public void LoadImmediately()
        {
            for (int resIndex = 0; resIndex < loadQueue.Count; ++resIndex)
            {
                ResourceLoadInfo info = loadQueue[resIndex];
                if (LoadResource(ref info) != null)
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

        public bool IsBusy()
        {
            return timer.IsScheduled();
        }

        public int GetPercentLoaded()
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

        public ManagedResource GetResource(int resName)
        {
            ManagedResource resource = resources[resName];
            Debug.Assert(resource != null, "Resource not loaded: " + resName);
            return resource;
        }        

        public void FreeResource(int resName)
        {
            Debug.Assert(resources[resName] != null);
            resources[resName].Dispose();
            resources[resName] = null;         
        }        

        public ManagedResource LoadResource(ref ResourceLoadInfo info)
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

                case ResType.MUSIC:
                    return ResFactory.GetInstance().LoadMusic(resName);

                case ResType.SOUND:
                    return ResFactory.GetInstance().LoadSound(resName);
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
            if (LoadResource(ref info) != null)
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

        public GameTexture GetTexture(int id)
        {
            return (GameTexture)GetResource(id);
        }

        public SwfMovie GetMovie(int id)
        {
            return (SwfMovie)GetResource(id);
        }

        public BaseFont GetFont(int id)
        {
            return (BaseFont)GetResource(id);
        }

        public GameSound GetSound(int id)
        {
            return (GameSound)GetResource(id);
        }        

        public GameMusic GetMusic(int id)
        {
            return (GameMusic)GetResource(id);
        }
    }
}