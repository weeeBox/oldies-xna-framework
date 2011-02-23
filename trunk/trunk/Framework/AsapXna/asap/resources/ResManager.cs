using System;

using System.Collections.Generic;
using System.Diagnostics;



namespace asap.resources
{
    public class ResManager
     {
        private Dictionary<string, object> resources;
        
        private ResCallback callback;
        
        public static ResManager instance;
        
        public String TYPE_PACK = ".pak";
        
        public String TYPE_BINARY = ".bin";
        
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
        
        public virtual Object Load(String path, ResCallback callback)
        {
            this.callback = callback;
            
            if (resources.ContainsKey(path))
                return resources[path];

            throw new NotImplementedException();            
        }
        
        public virtual Object Load(String path)
        {
            return Load(path, null);
        }
        
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
        
        public virtual String _getType(String path)
        {
            int k = path.LastIndexOf('.');
            if (k != -1)
                return path.Substring(0, k);
            
            else
                return null;
            
        }        
    }    
}