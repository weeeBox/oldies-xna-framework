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

        private int startFrame;
        private int endFrame;

        private string currentLabel;

        private PlayerState state;
        private AnimationType animationType;

        private int framesCount;
        private int frameRate;
        private SWFFrame[] frames;

        private SwfPlayerCache instanceCache;
        private DisplayObjectContainer displayList;

        public IMovieListener listener;

        public SwfPlayer(DisplayObjectContainer displayList)
        {
            this.displayList = displayList;
            animationType = AnimationType.NORMAL;
            instanceCache = new SwfPlayerCache();            
        }

        public void SetMovie(SwfMovie movie)
        {
            this.movie = movie;

            framesCount = movie.FramesCount;
            FrameRate = movie.FrameRate;
            frames = movie.Frames;
            startFrame = 0;
            endFrame = framesCount - 1;

            Reset();
        }
        
        private void Reset()
        {
            Debug.Assert(startFrame >= 0 && endFrame < framesCount);

            state = PlayerState.STOPPED;
        
            frameElaspedTime = 0;
            currentLabel = null;
            currentFrame = -1;
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

            List<DisplayObject> childs = displayList.GetChilds();
            foreach (DisplayObject c in childs)
            {
                if (c.IsUpdatable())
                {
                    c.Update(delta);
                }
            }
        }

        private void EnterFrame()
        {
            if (endFrame > startFrame)
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

            if (currentFrame == endFrame && startFrame < endFrame)
            {
                OnAnimationFinished();
            }
            else
            {
                // Debug.WriteLine("Next: " + currentFrame);

                Tag[] tags = Frames[currentFrame].Tags;
                for (int i = 0; i < tags.Length; ++i)
                {
                    ProcessTag(tags[i], FRAME_PROCESS_FORWARD);
                }

                if (listener != null)
                    listener.EnterFrame(this);
            }
        }

        private void EnterPrevFrame()
        {            
            if (currentFrame > 0 && currentFrame < FramesCount && Frames[currentFrame].IsDispListChange())
            {
                ClearFrame(currentFrame);
            }

            currentFrame--;

            if (currentFrame == endFrame && startFrame > endFrame)
            {
                OnAnimationFinished();
            }
            else
            {
                // Debug.WriteLine("Prev: " + currentFrame);

                Tag[] tags = Frames[currentFrame].Tags;
                for (int i = tags.Length - 1; i >= 0; --i)
                {
                    ProcessTag(tags[i], FRAME_PROCESS_BACKWARD);
                }

                if (listener != null)
                    listener.EnterFrame(this);
            }
        }

        private void ClearFrame(int frameIndex)
        {
            // Debug.WriteLine(" Clr: " + currentFrame);

            Tag[] tags = Frames[frameIndex].Tags;
            for (int i = tags.Length - 1; i >= 0; --i)
            {
                ProcessTag(tags[i], FRAME_PROCESS_CLEAR);
            }
        }

        protected virtual void OnAnimationFinished()
        {
            if (state == PlayerState.STEP)
            {
                if (currentFrame == -1) // step back performed
                {
                    Reset();
                    GotoAndStop(FramesCount - 1);
                }
                else
                {
                    Reset();
                    EnterNextFrame();
                }
                state = PlayerState.STEP;
                return;
            }

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

            if (listener != null)
                listener.AnimationFinished();
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
                case TagConstants.FRAME_LABEL:
                    {
                        FrameLabel label = (FrameLabel)tag;
                        currentLabel = label.GetName();
                        break;
                    }

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
            bool hasCharacter = placeObject.HasCharacter();
            if (!isMove || hasCharacter)
            {
                if (mode == FRAME_PROCESS_FORWARD || hasCharacter && mode == FRAME_PROCESS_BACKWARD)
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

        public string CurrentLabel
        {
            get { return currentLabel; }
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
            state = PlayerState.PLAYING;
        }

        public void GotoAndStop(int frameIndex)
        {
            GotoHelper(frameIndex);
            state = PlayerState.STOPPED;
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
            GotoAndPlay(startFrame);
        }

        public void Play(int startFrame, int endFrame)
        {
            Debug.Assert(startFrame >= 0 && startFrame < FramesCount);
            Debug.Assert(endFrame >= 0 && endFrame < FramesCount);

            this.startFrame = startFrame;
            this.endFrame = endFrame;

            Play();
        }

        public void Stop()
        {
            state = PlayerState.STOPPED;
        }
    }
}
