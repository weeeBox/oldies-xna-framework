using System;
using System.Collections.Generic;
using System.Diagnostics;
using asap.anim.objects;
using asap.core;
using asap.graphics;
using Microsoft.Xna.Framework;
using swiff.com.jswiff.swfrecords;
using swiff.com.jswiff.swfrecords.tags;
using asap.visual;
using swiff.com.jswiff;

namespace asap.anim
{
    public enum AnimationType
    {
        NORMAL,
        LOOP,
        PING_PONG
    }

    public class SwfPlayer : TickListener, IMovieControl
    {
        private enum PlayerState
        {
            STOPPED,
            PLAYING,
            STEP
        };        

        private SwfMovie movie;
        
        private float frameElaspedTime;
        private float frameDelay;

        private int currentFrame;        

        private PlayerState state;
        private AnimationType animationType;

        private int framesCount;
        private int frameRate;
        private SWFFrame[] frames;

        private bool forward;

        private SwfPlayerCache instanceCache;
        private DisplayObjectContainer displayList;

        public SwfPlayer(DisplayObjectContainer displayList)
        {
            this.displayList = displayList;
            animationType = AnimationType.NORMAL;
            instanceCache = new SwfPlayerCache();
            forward = true;
        }

        public void SetMovie(SwfMovie movie)
        {
            this.movie = movie;

            framesCount = movie.FramesCount;
            FrameRate = movie.FrameRate;
            frames = movie.Frames;

            Reset();
        }
        
        private void Reset()
        {
            state = PlayerState.STOPPED;
            
            currentFrame = forward ? -1 : framesCount;
            frameElaspedTime = 0;            
            displayList.RemoveAllChilds();
        }                

        public void Tick(float delta)
        {
            if (state == PlayerState.PLAYING)
            {
                frameElaspedTime += delta;                                                               
                if (frameElaspedTime > frameDelay)
                {
                    EnterFrame();
                    frameElaspedTime = 0;
                }                
            }    
        }

        private void EnterFrame()
        {
            if (forward)
            {
                EnterNextFrame();
            }
            else
            {
                EnterPrevFrame();
            }
        }

        private void EnterNextFrame()
        {
            currentFrame++;

            // Debug.WriteLine("Next: " + currentFrame);

            Tag[] tags = Frames[currentFrame].Tags;
            for (int i = 0; i < tags.Length; ++i)
            {
                ProcessTag(tags[i], FRAME_PROCESS_FORWARD);
            }

            if (currentFrame == framesCount - 1)
            {
                OnAnimationFinished();
            }
        }

        private void EnterPrevFrame()
        {            
            Tag[] tags;
            if (currentFrame < FramesCount && Frames[currentFrame].IsDispListChange())
            {
                tags = Frames[currentFrame].Tags;
                for (int i = tags.Length - 1; i >= 0; --i)
                {
                    ProcessTag(tags[i], FRAME_PROCESS_CLEAR);
                }
            }

            currentFrame--;

            // Debug.WriteLine("Prev: " + currentFrame);

            tags = Frames[currentFrame].Tags;
            for (int i = tags.Length - 1; i >= 0; --i)
            {
                ProcessTag(tags[i], FRAME_PROCESS_BACKWARD);
            }

            if (currentFrame == 0)
            {
                OnAnimationFinished();
            }
        }

        protected virtual void OnAnimationFinished()
        {
            if (state == PlayerState.STEP)
                return;

            Stop();

            switch (animationType)
            {
                case AnimationType.NORMAL:                    
                    break;
                case AnimationType.LOOP:
                {
                    Reset();
                    Play();
                    break;
                }
                case AnimationType.PING_PONG:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        // Tags logic
        /////////////////////////////////////////////////////////////////////////////

        private const int FRAME_PROCESS_FORWARD = 0;
        private const int FRAME_PROCESS_BACKWARD = 1;
        private const int FRAME_PROCESS_CLEAR = 2;

        private void ProcessTag(Tag tag, int mode)
        {
            // Debug.WriteLine("\t" + tag);

            switch (tag.GetCode())
            {
                case TagConstants.PLACE_OBJECT:
                case TagConstants.PLACE_OBJECT_3:
                    {
                        PlaceObject3 placeObject = (PlaceObject3)tag;
                        throw new NotImplementedException();
                    }
                case TagConstants.PLACE_OBJECT_2:
                    {
                        doPlaceObject2(tag, mode);
                        break;
                    }
                case TagConstants.REMOVE_OBJECT:
                case TagConstants.REMOVE_OBJECT_2:
                    {                    
                        DoRemoveObject(tag, mode);                 
                        break;
                    }
            }
        }        

        private void doPlaceObject2(Tag tag, int mode)
        {
            PlaceObject2 placeObject = (PlaceObject2)tag;
            bool isMove = placeObject.IsMove();
            int depth = placeObject.GetDepth();
            int index = depth - 1;
            if (isMove)
            {
                bool hasCharacter = placeObject.HasCharacter();
                if (hasCharacter)
                {
                    throw new NotImplementedException(placeObject.ToString());
                }
                else
                {
                    CharacterInstance instance = (CharacterInstance)displayList.GetChildAt(index);
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
                if (mode == FRAME_PROCESS_FORWARD)
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
                        instance.name = placeObject.GetName();
                    }
                    if (index < displayList.ChildsCount())
                    {
                        displayList.ReplaceChildAt(instance, index);
                    }
                    else
                    {
                        while (index > displayList.ChildsCount())
                        {
                            displayList.AddChild(CharacterInstance.NULL);
                        }
                        displayList.AddChild(instance);
                    }
                }
                else if (mode == FRAME_PROCESS_CLEAR)
                {
                    displayList.ReplaceChildAt(CharacterInstance.NULL, index);
                }
            }
        }
        
        private void DoRemoveObject(Tag tag, int mode)
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
            int index = depth - 1;
            if (mode == FRAME_PROCESS_FORWARD)
            {
                displayList.ReplaceChildAt(CharacterInstance.NULL, index);
            }
            else if (mode == FRAME_PROCESS_CLEAR)
            {
                CharacterInstance cachedInstance = instanceCache.FindAddedAtDepthBeforeFrame(depth, currentFrame);
                Debug.Assert(cachedInstance != null);
                displayList.ReplaceChildAt(cachedInstance, index);
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        // Helpers
        /////////////////////////////////////////////////////////////////////////////

        private CharacterInstance CreateInstance(int characterId, int depth)
        {
            CharacterInstance instance = instanceCache.FindCached(depth, currentFrame);            
            Debug.Assert(instance == null || instance.GetCharacterId() == characterId);
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
            set 
            { 
                frameRate = value;
                frameDelay = 1.0f / frameRate;
            }
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

        public void GotoAndPlay(int frameIndex)
        {
            GotoHelper(frameIndex);
            Play();
        }

        public void GotoAndStop(int frameIndex)
        {
            GotoHelper(frameIndex);
            Stop();
        }

        private void GotoHelper(int frameIndex)
        {
            Debug.Assert(frameIndex >= 0 && frameIndex < FramesCount);

            if (frameIndex > currentFrame)
            {
                while (currentFrame < frameIndex)
                    EnterNextFrame();
            }
            else if (frameIndex < currentFrame)
            {
                while (currentFrame > frameIndex)
                    EnterPrevFrame();
            }
        }

        public void NextFrame()
        {
            state = PlayerState.STEP;
            EnterNextFrame();
        }

        public void PrevFrame()
        {
            state = PlayerState.STEP;
            EnterPrevFrame();
        }

        public void Play()
        {            
            state = PlayerState.PLAYING;
            EnterFrame();
        }

        public void Stop()
        {
            state = PlayerState.STOPPED;
        }
    }
}
