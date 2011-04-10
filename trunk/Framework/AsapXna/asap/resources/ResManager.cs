using System;

using System.Collections.Generic;
using System.Diagnostics;

namespace asap.resources
{
    public abstract class ResManager
    {
        private Dictionary<string, object> resources;
        
        private ResCallback callback;
        
        public static ResManager instance;        
        
        public ResManager() 
        {
            Debug.Assert((ResManager.instance) == null);
            ResManager.instance = this;
            resources = new Dictionary<string, object>();
        }
        
        public static ResManager GetBaseInstance()
        {
            return ResManager.instance;
        }

        public virtual void Load(String path)
        {
            Load(path, null);
        }

        public virtual void Load(String path, ResCallback callback)
        {
            this.callback = callback;

            if (!resources.ContainsKey(path))
            {
                object resource = LoadResource(path);
                if (resource == null)
                {
                    throw new ArgumentOutOfRangeException("Cannot load resource: " + path);
                }
                resources.Add(path, resource);
            }            
        }

        protected abstract object LoadResource(string path);        
        
        public virtual void Unload(String path)
        {
            if (resources.ContainsKey(path)) 
            {                
                resources.Remove(path);
                Debug.WriteLine(("Resource unloaded: " + path));
            } 
        }
        
        public virtual bool IsLoaded(String path)
        {
            return resources.ContainsKey(path);
        }        
        
        public virtual Object GetRes(String path)
        {
            Debug.Assert(resources.ContainsKey(path));
            return resources[path];
        }
        
        public virtual void AddRes(String path, Object res)
        {
            Debug.Assert(res != null);
            resources.Add(path, res);
        }
        
        public virtual String GetExt(String path)
        {
            int dotPos = path.LastIndexOf('.');
            return dotPos == -1 ? null : path.Substring(dotPos, path.Length - dotPos);            
        }        
    }    
}