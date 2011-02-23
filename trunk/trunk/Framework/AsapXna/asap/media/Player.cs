using System;

using System.Collections.Generic;



namespace java.asap.media
{
    abstract public class Player
     {
        public const int UNREALIZED = 100;
        
        public const int REALIZED = 200;
        
        public const int PREFETCHED = 300;
        
        public const int STARTED = 400;
        
        public const int CLOSED = 0;
        
        public const long TIME_UNKNOWN = -1;
        
        public virtual void Realize()
        {
        }
        
        public virtual void Prefetch()
        {
        }
        
        public virtual void Start()
        {
        }
        
        public virtual void Stop()
        {
        }
        
        public virtual void SetLoopCount(int count)
        {
        }
        
        public virtual void Deallocate()
        {
        }
        
        public virtual void Close()
        {
        }
        
    }
    
    
}