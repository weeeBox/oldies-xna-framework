using System;
using System.Collections.Generic;
using System.Diagnostics;
using asap.anim.objects;
using asap.core;
using asap.graphics;
using Microsoft.Xna.Framework;
using swiff.com.jswiff.swfrecords;
using swiff.com.jswiff.swfrecords.tags;
using swiff.com.jswiff;

namespace asap.anim
{
    public enum AnimationType
    {
        NORMAL,
        LOOP,
        PING_PONG
    }

    public class SwfPlayer : TickListener
    {
        private enum PlayerState
        {
            STOPPED,
            PLAYING,
            PAUSED
        };        

        private SwfMovie movie;
        private SwfDisplayList displayList;        

        private float delay;
        private float elaspedTime;

        private int currentFrame;
        private int tagPointer;

        private PlayerState state;
        private AnimationType animationType;

        private int framesCount;
        private int frameRate;
        private SWFFrame[] frames;

        private SwfPlayerCache instanceCache;

        public SwfPlayer()
        {            
            displayList = new SwfDisplayList();
            animationType = AnimationType.NORMAL;
            instanceCache = new SwfPlayerCache();
        }

        public void SetMovie(SwfMovie movie)
        {
            this.movie = movie;

            framesCount = movie.FramesCount;
            frameRate = movie.FrameRate;
            frames = movie.Frames;
        }
        
        private void Reset()
        {
            state = PlayerState.STOPPED;

            currentFrame = -1;
            elaspedTime = 0;
            tagPointer = 0;
            displayList.Clear();
        }
   
        public void Start()
        {
            Reset();            
            delay = 1.0f / FrameRate;
            state = PlayerState.PLAYING;
            Tick(delay); // force the first frame to be shown
        }

        public void Stop()
        {
            state = PlayerState.STOPPED;
        }

        public void Pause()
        {
            state = PlayerState.PAUSED;
        }

        public void Resume()
        {
            state = PlayerState.PLAYING;
        }

        public void Draw(Graphics g)
        {
            int maxDepth = displayList.Size;
            for (int depth = 1; depth <= maxDepth; ++depth)
            {
                CharacterInstance inst = displayList[depth];
                if (inst != null)
                {                    
                    inst.Draw(g);                    
                }
            }            
        }

        private void AddTransform(Graphics g, SwfMatrix swfMatrix)
        {
            Matrix m = Matrix.Identity;            
            if (swfMatrix.HasScale())
            {
                m.M11 = (float)swfMatrix.GetScaleX();
                m.M22 = (float)swfMatrix.GetScaleY();
            }
            if (swfMatrix.HasRotateSkew())
            {
                m.M12 = (float)swfMatrix.GetRotateSkew0();
                m.M21 = (float)swfMatrix.GetRotateSkew1();
            }            
            m.M41 = swfMatrix.GetTranslateX() / 20.0f;
            m.M42 = swfMatrix.GetTranslateY() / 20.0f;
            g.AddTransform(ref m);
        }        

        public void Tick(float delta)
        {
            if (state == PlayerState.PLAYING)
            {
                elaspedTime += delta;                
                                
                int frame = (int)(FrameRate * elaspedTime);
                if (frame != currentFrame)
                {                    
                    ProcessFrame(currentFrame + 1);
                }                
            }    
        
            if (state != PlayerState.PAUSED)
            {
                int maxDepth = displayList.Size;
                for (int depth = 1; depth <= maxDepth; ++depth)
                {
                    CharacterInstance inst = displayList[depth];
                    if (inst != null && inst.IsUpdatable())
                    {
                        inst.Update(delta);
                    }
                }                
            }
        }

        private void ProcessFrame(int currentFrame)
        {
            this.currentFrame = currentFrame;            
            Tag[] tags = Frames[currentFrame].Tags;
                        
            foreach (Tag tag in tags)
            {
                switch (tag.GetCode())
                {
                    case TagConstants.PLACE_OBJECT:
                    case TagConstants.PLACE_OBJECT_3:
                    {
                        PlaceObject3 placeObject = (PlaceObject3)tag;
                        throw new NotImplementedException();                        
                    }
                    case TagConstants.PLACE_OBJECT_2:                   
                        doPlaceObject2(tag);                        
                        break;
                    case TagConstants.REMOVE_OBJECT:
                    case TagConstants.REMOVE_OBJECT_2:
                        DoRemoveObject(tag);
                        break;                    
                }
            }            

            if (IsAnimationFinished())
            {
                OnAnimationFinished();
            }
        }        
        
        private bool IsAnimationFinished()
        {
            return currentFrame == Frames.Length - 1;
        }

        protected virtual void OnAnimationFinished()
        {
            Stop();

            switch (animationType)
            {
                case AnimationType.NORMAL:
                    break;
                case AnimationType.LOOP:
                    Start();
                    break;
                case AnimationType.PING_PONG:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        // Tags logic
        /////////////////////////////////////////////////////////////////////////////

        private void doPlaceObject2(Tag tag)
        {
            PlaceObject2 placeObject = (PlaceObject2)tag;
            bool isMove = placeObject.IsMove();
            int depth = placeObject.GetDepth();
            if (isMove)
            {
                bool hasCharacter = placeObject.HasCharacter();
                if (hasCharacter)
                {
                    throw new NotImplementedException(placeObject.ToString());
                }
                else
                {
                    CharacterInstance instance = displayList[depth];
                    if (placeObject.HasMatrix())
                    {
                        instance.SetSwfMatrix(placeObject.GetMatrix());
                    }
                    if (placeObject.HasColorTransform())
                    {
                        instance.SetSwfColorTransform(placeObject.GetColorTransform());
                    }
                }
            }
            else
            {
                Debug.Assert(placeObject.HasCharacter());
                Debug.Assert(placeObject.GetMatrix() != null);
                int characterId = placeObject.GetCharacterId();
                CharacterInstance instance = CreateInstance(characterId, depth);
                instance.SetSwfMatrix(placeObject.GetMatrix());
                if (placeObject.HasColorTransform())
                {
                    instance.SetSwfColorTransform(placeObject.GetColorTransform());
                }
                if (placeObject.HasName())
                {
                    Debug.Assert(instance is SpriteInstance);
                    ((SpriteInstance)instance).Name = placeObject.GetName();
                }
                displayList[depth] = instance;
            }
        }
        
        private void DoRemoveObject(Tag tag)
        {
            int depth;
            switch (tag.GetCode())
            {
                case TagConstants.REMOVE_OBJECT_2:
                    depth = ((RemoveObject2)tag).GetDepth();
                    break;
                case TagConstants.REMOVE_OBJECT:
                    depth = ((RemoveObject)tag).GetDepth();
                    break;
                default:
                    throw new NotImplementedException(tag.GetCode().ToString());
            }            
            displayList[depth] = null;
        }

        /////////////////////////////////////////////////////////////////////////////
        // Helpers
        /////////////////////////////////////////////////////////////////////////////
        
        public SpriteInstance FindInstance(string name)
        {
            return displayList.FindInstance(name);
        }

        public List<CharacterInstance> FindInstances(int characterId)
        {
            return displayList.FindInstances(characterId);
        }

        public List<CharacterInstance> FindInstancesOf(Type type)
        {
            return displayList.FindInstancesOf(type);
        }

        private CharacterInstance CreateInstance(int characterId, int depth)
        {
            CharacterInstance instance = instanceCache.FindCached(characterId, depth, currentFrame);
            if (instance == null)
            {
                instance = movie.CreateInstance(characterId);
                instanceCache.AddCached(instance, depth, currentFrame);
            }
            return instance;
        }

        public int FramesCount
        {
            get { return framesCount; }
            set { framesCount = value; }
        }

        public int FrameRate
        {
            get { return frameRate; }
            set { frameRate = value; }
        }
        
        public AnimationType AnimationType
        {
            get { return animationType; }
            set { animationType = value; }
        }

        public SWFFrame[] Frames
        {
            get { return frames; }
            set { frames = value; }
        }
    }
}
