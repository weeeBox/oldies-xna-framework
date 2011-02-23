using System;

using System.Collections.Generic;
using System.Diagnostics;



namespace asap.sound
{
    public class SoundPlayer
     {
        private Object annotation = null;
        
        private String filename;
        
        public SoundPlayer(String filename) 
        {
            this.filename = filename;
        }
        
        public virtual void Annotate(Object annotation)
        {
            Debug.Assert(annotation != null);
            this.annotation = annotation;
        }
        
        public virtual void ResetAnnotation()
        {
            this.annotation = null;
        }
        
        public virtual Object GetAnnotation()
        {
            return annotation;
        }
        
        public virtual String GetFilename()
        {
            return filename;
        }
        
        public virtual void Play(bool looped)
        {
            throw new Exception("implement me");
        }
        
        public virtual void Pause()
        {
            throw new Exception("implement me");
        }
        
        public virtual void Resume()
        {
            throw new Exception("implement me");
        }
        
        public virtual void Stop()
        {
            throw new Exception("implement me");
        }
        
        public virtual bool IsPaused()
        {
            throw new Exception("implement me");
        }
        
        public virtual bool IsPlaying()
        {
            throw new Exception("implement me");
        }
        
        private SoundManager GetManager()
        {
            return SoundManager.GetInstance();
        }
        
    }
    
    
}