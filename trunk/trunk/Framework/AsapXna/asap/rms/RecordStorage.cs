using System;

using System.Collections.Generic;
using System.Diagnostics;



namespace java.asap.rms
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
        
        public abstract bool Save(String name, sbyte[] data);
        
        public abstract sbyte[] Load(String name);
        
    }
    
    
}