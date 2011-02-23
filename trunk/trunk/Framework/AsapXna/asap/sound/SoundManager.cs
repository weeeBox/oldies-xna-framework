using System;

using System.Collections.Generic;


using asap.resources;
using System.Diagnostics;

namespace asap.sound
{
    public class SoundManager
     {
        private static SoundManager instance;
        
        private List<SoundPlayer> players;
        
        public static SoundManager GetInstance()
        {
            if ((SoundManager.instance) == null) 
            {
                SoundManager.instance = new SoundManager();
            } 
            return SoundManager.instance;
        }
        
        private SoundManager() 
        {
            players = new List<SoundPlayer>();
        }
        
        public virtual SoundPlayer CreatePlayer(String filename)
        {
            return CreatePlayer(filename, null, false);
        }
        
        public virtual SoundPlayer CreatePlayer(String filename, bool streaming)
        {
            return CreatePlayer(filename, null, streaming);
        }
        
        public virtual SoundPlayer CreatePlayer(String filename, Object annotation, bool streaming)
        {
            SoundPlayer player = ResFactory.GetInstance().CreateSoundPlayer(filename, streaming);
            if (annotation != null)
                player.Annotate(annotation);
            
            players.Add(player);
            return player;
        }
        
        public virtual void DestroyPlayer(SoundPlayer player)
        {
            Debug.Assert(player != null);
            player.Stop();
            players.Remove(player);
        }
        
        public virtual Object[] GetAllAnnotations()
        {
            Object[] seen = new Object[0];
            for (int i = 0; i < players.Count; ++i)
                if (!(OneOf(seen, players[i]))) 
                {
                    Object[] newseen = new Object[(seen.Length) + 1];
                    if ((seen.Length) > 0)
                        Array.Copy(seen, 0, newseen, 0, seen.Length);
                    
                    newseen[seen.Length] = players[i];
                    seen = newseen;
                } 
            return seen;
        }
        
        public virtual void Pause()
        {
            PauseAnnotatedOneOf(null);
        }
        
        public virtual void Resume()
        {
            ResumeAnnotatedOneOf(null);
        }
        
        public virtual void Stop()
        {
            StopAnnotatedOneOf(null);
        }
        
        public virtual void PauseAnnotated(Object annotation)
        {
            PauseAnnotatedOneOf(ArrayBox(annotation));
        }
        
        public virtual void ResumeAnnotated(Object annotation)
        {
            ResumeAnnotatedOneOf(ArrayBox(annotation));
        }
        
        public virtual void StopAnnotated(Object annotation)
        {
            StopAnnotatedOneOf(ArrayBox(annotation));
        }
        
        public virtual void PauseAnnotatedOneOf(Object[] annotations)
        {
            for (int playerIndex = 0; playerIndex < players.Count; ++playerIndex) 
            {
                SoundPlayer player = players[playerIndex];
                if (OneOf(annotations, player.GetAnnotation()))
                    player.Pause();
                
            }
        }
        
        public virtual void ResumeAnnotatedOneOf(Object[] annotations)
        {
            for (int playerIndex = 0; playerIndex < players.Count; ++playerIndex) 
            {
                SoundPlayer player = players[playerIndex];
                if (OneOf(annotations, player))
                    player.Resume();
                
            }
        }
        
        public virtual void StopAnnotatedOneOf(Object[] annotations)
        {
            for (int playerIndex = 0; playerIndex < players.Count; ++playerIndex) 
            {
                SoundPlayer player = players[playerIndex];
                if (OneOf(annotations, player))
                    player.Stop();
                
            }
        }
        
        private bool OneOf(Object[] annotations, Object sample)
        {
            if (annotations == null)
                return true;
            
            for (int i = 0; i < (annotations.Length); ++i)
                if ((annotations[i]) == sample)
                    return true;
                
            return false;
        }
        
        private Object[] ArrayBox(Object box)
        {
            return new Object[]{ box };
        }
        
    }
    
    
}