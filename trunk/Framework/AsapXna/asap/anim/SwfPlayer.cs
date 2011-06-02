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
            PAUSED
        };        

        private SwfMovie movie;
        
        private float frameElaspedTime;
        private float frameDelay;

        private int currentFrame;
        private int tagPointer;

        private PlayerState state;
        private AnimationType animationType;

        private int framesCount;
        private int frameRate;
        private List<Tag> tags;

        private SwfPlayerCache instanceCache;
        private DisplayObjectContainer displayList;

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
            tags = movie.Tags;
        }
        
        private void Reset()
        {
            state = PlayerState.STOPPED;

            currentFrame = -1;
            frameElaspedTime = 0;
            tagPointer = 0;
            displayList.RemoveAllChilds();
        }                

        public void Tick(float delta)
        {
            if (state == PlayerState.PLAYING)
            {
                frameElaspedTime += delta;                                                               
                if (frameElaspedTime > frameDelay)
                {
                    NextFrame();
                    frameElaspedTime = 0;
                }                
            }    
        }
        
        private bool IsAnimationFinished()
        {
            return tagPointer == Tags.Count;
        }

        protected virtual void OnAnimationFinished()
        {
            Stop();

            switch (animationType)
            {
                case AnimationType.NORMAL:
                    break;
                case AnimationType.LOOP:
                    Play();
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
            displayList.ReplaceChildAt(CharacterInstance.NULL, depth - 1);
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

        public List<Tag> Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        public void GotoAndPlay(int frameIndex)
        {
            throw new NotImplementedException();
        }

        public void GotoAndStop(int frameIndex)
        {
            throw new NotImplementedException();
        }

        public void NextFrame()
        {
            currentFrame++;

            List<Tag> tags = Tags;
            int tagsCount = tags.Count;

            bool breakFlag = false;
            for (; tagPointer < tagsCount && !breakFlag; ++tagPointer)
            {
                Tag tag = tags[tagPointer];
                switch (tag.GetCode())
                {
                    case TagConstants.SHOW_FRAME:
                        breakFlag = true;
                        break;

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

        public void PrevFrame()
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            Reset();
            state = PlayerState.PLAYING;
            NextFrame();
        }

        public void Stop()
        {
            state = PlayerState.STOPPED;
        }
    }
}
