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
            if (IsPlayingBackward())
            {
                EnterPrevFrame();
            }
            else
            {
                EnterNextFrame();                
            }
        }

        private void EnterNextFrame()
        {
            if (currentFrame == endFrame)
            {
                OnAnimationFinished();
            }
            else
            {
                currentFrame++;
                // Debug.WriteLine("Next: " + currentFrame);
                ProcessFrame(currentFrame, FRAME_PROCESS_FORWARD);
            }
        }

        private void EnterPrevFrame()
        {            
            if (NeedCleanFrame(currentFrame))
            {
                // Debug.WriteLine(" Clr: " + currentFrame);                
                ProcessFrame(currentFrame, FRAME_PROCESS_CLEAR);
            }            

            if (currentFrame == endFrame && IsPlayingBackward())
            {
                OnAnimationFinished();
            }
            else
            {
                currentFrame--;
                // Debug.WriteLine("Prev: " + currentFrame);
                ProcessFrame(currentFrame, FRAME_PROCESS_BACKWARD);
            }
        }

        private bool NeedCleanFrame(int frameIndex)
        {
            return frameIndex >= startFrame && frameIndex <= endFrame && Frames[frameIndex].IsDispListChange();
        }

        private void ProcessFrame(int frameIndex, int mode)
        {
            Tag[] tags = Frames[frameIndex].Tags;
            for (int i = 0; i < tags.Length; ++i)
            {
                ProcessTag(tags[i], mode);
            }
        }        

        protected virtual void OnAnimationFinished()        
        {
            Debug.Assert(state == PlayerState.PLAYING);            

            switch (animationType)
            {
                case AnimationType.NORMAL:                
                {
                    Stop();
                    break;
                }
                case AnimationType.LOOP:
                {
                    GotoAndPlay(startFrame);
                    break;
                }
                case AnimationType.PING_PONG:
                {                    
                    SwitchPlaybackDirection();
                    GotoAndPlay(IsPlayingBackward() ? (startFrame - 1) : (startFrame + 1));
                    break;
                }
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
                        if (listener != null)
                            listener.EnterLabelFrame(this, currentLabel);
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
                    
                    int characterId = placeObject.GetCharacterId();
                    CharacterInstance instance = CreateInstance(characterId, depth);
                    if (placeObject.HasMatrix())
                    {
                        instance.SetSwfMatrix(placeObject.GetMatrix());
                    }
                    else
                    {
                        Debug.Assert(index >= 0 && index < displayList.ChildsCount());
                        CharacterInstance oldInstance = (CharacterInstance)displayList.GetChildAt(index);
                        instance.SetMatrix(oldInstance.GetMatrix());                        
                    }
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
                    CharacterInstance instance = (CharacterInstance)displayList.GetChildAt(index);
                    instance.SetEnabled(false);
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
            else
            {
                instance.SetEnabled(true);
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

        private bool IsPlayingBackward()
        {
            return startFrame > endFrame;
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
                {
                    EnterNextFrame();
                }
            }
            else if (frameIndex < currentFrame)
            {
                int numBackSteps = currentFrame - frameIndex;
                int forwardSteps = frameIndex;

                if (numBackSteps <= forwardSteps)
                {
                    while (currentFrame > frameIndex)
                    {
                        EnterPrevFrame();
                    }
                }
                else
                {
                    Reset();
                    GotoHelper(frameIndex);
                }                
            }
        }

        public void NextFrame()
        {
            state = PlayerState.STEP;
            if (currentFrame == endFrame && !IsPlayingBackward() || currentFrame == startFrame && IsPlayingBackward())
            {
                Debug.WriteLine("Can't move next frame");
            }
            else
            {
                EnterNextFrame();
            }
        }

        public void PrevFrame()
        {
            state = PlayerState.STEP;
            if (currentFrame == endFrame && IsPlayingBackward() || currentFrame == startFrame && !IsPlayingBackward())
            {
                Debug.WriteLine("Can't move prev frame");
            }
            else
            {
                EnterPrevFrame();
            }            
        }

        public void Play()
        {
            Play(0, FramesCount - 1);
        }

        public void Play(int startFrame, int endFrame)
        {
            Debug.Assert(startFrame >= 0 && startFrame < FramesCount);
            Debug.Assert(endFrame >= 0 && endFrame < FramesCount);

            this.startFrame = startFrame;
            this.endFrame = endFrame;

            GotoAndPlay(startFrame);
        }

        public void Stop()
        {
            state = PlayerState.STOPPED;
        }

        private void SwitchPlaybackDirection()
        {
            int temp = startFrame;
            startFrame = endFrame;
            endFrame = temp;
        }
    }
}
