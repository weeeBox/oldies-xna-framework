﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using asap.anim.objects;
using asap.core;
using asap.graphics;
using Microsoft.Xna.Framework;
using swiff.com.jswiff.swfrecords;
using swiff.com.jswiff.swfrecords.tags;

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
        private List<Tag> tags;

        public SwfPlayer()
        {            
            displayList = new SwfDisplayList();
            animationType = AnimationType.NORMAL;
        }

        public void SetMovie(SwfMovie movie)
        {
            this.movie = movie;

            framesCount = movie.FramesCount;
            frameRate = movie.FrameRate;
            tags = movie.Tags;
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
                    g.PushTransform();
                    AddTransform(g, inst.Matrix);
                    inst.Draw(g);
                    g.PopTransform();
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

                int oldFrame = currentFrame;
                currentFrame = (int)(FrameRate * elaspedTime);
                if (currentFrame != oldFrame)
                {                    
                    ProcessFrame(currentFrame);
                }

                int maxDepth = displayList.Size;
                for (int depth = 1; depth <= maxDepth; ++depth)
                {
                    CharacterInstance inst = displayList[depth];
                    if (inst != null && inst.updateable)
                    {
                        inst.Update(delta);
                    }
                }                
            }
        }

        private void ProcessFrame(int currentFrame)
        {
            List<Tag> tags = Tags;
            int tagsCount = tags.Count;

            bool breakFlag = false;
            for (;tagPointer < tagsCount && !breakFlag; ++tagPointer)
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
                        throw new NotImplementedException();                        
                    }
                    case TagConstants.PLACE_OBJECT_2:                    
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
                                if (placeObject.HasMatrix())
                                {                                    
                                    CharacterInstance instance = displayList[depth];
                                    Debug.Assert(placeObject.GetMatrix() != null);
                                    instance.Matrix = placeObject.GetMatrix();                                    
                                }
                            }
                        }
                        else
                        {
                            Debug.Assert(placeObject.HasCharacter());
                            Debug.Assert(placeObject.GetMatrix() != null);
                            int characterId = placeObject.GetCharacterId();
                            CharacterInstance instance = movie.CreateInstance(characterId);
                            instance.Matrix = placeObject.GetMatrix();
                            displayList[depth] = instance;                           
                        }                        
                        
                        break;
                    }                        
                }
            }            

            if (tagPointer == tagsCount)
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

        public List<Tag> Tags
        {
            get { return tags; }
            set { tags = value; }
        }
    }
}