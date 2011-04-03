using System;

using System.Collections.Generic;
using System.Diagnostics;



namespace asap.rms
{
    abstract public class RecordStorage
     {
        private static RecordStorage instance;
        
        public static RecordStorage GetInstance()
        {
            return RecordStorage.instance;
        }
        
        public RecordStorage() 
        {
            Debug.Assert((RecordStorage.instance) == null);
            RecordStorage.instance = this;
        }
        
        public abstract bool Save(String name, byte[] data);
        
        public abstract byte[] Load(String name);
        
    }
    
    
}