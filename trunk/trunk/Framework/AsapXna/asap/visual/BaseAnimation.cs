using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.visual;
using Microsoft.Xna.Framework;
using asap.graphics;
using asap.util;

namespace AsapXna.asap.visual
{
    public class BaseAnimation : BaseElement
    {
        public enum Timeline
        {
            NO_LOOP,
            PING_PONG,
            REPLAY
        }

        public interface TimelineDelegate
        {
            void ElementTimelineFinished(BaseElement e);
        }

        public struct KeyFrame
        {
            public float x;
            public float y;
            public float scaleX;
            public float scaleY;
            public ColorTransform ctForm;
            public float rotation;
            public float time;

            public KeyFrame(float x, float y, ColorTransform ctForm, float scaleX, float scaleY, float rotation, float time)
            {
                this.x = x;
                this.y = y;
                this.scaleX = scaleX;
                this.scaleY = scaleY;
                this.rotation = rotation;
                this.ctForm = ctForm;
                this.time = time;
            }            
        }

        private const int UNDEFINED_FRAME = -1;

        protected KeyFrame[] keyFrames;
        protected int nextKeyFrame;
        protected int keyFramesCount;
        protected int keyFramesCapacity;
        protected KeyFrame currentStepPerSecond;
        protected float keyFrameTimeLeft;
        public TimelineDelegate timelineDelegate;
        protected Timeline timelineLoopType;
        protected bool timelineDirReverse;

        private BaseElement target;

        public BaseAnimation(BaseElement target)
        {
            Reset();
            SetTarget(target);
        }

        public void SetTarget(BaseElement target)
        {            
            this.target = target;            
            width = target.width;
            height = target.height;
            target.SetParent(this);
        }

        private void Reset()
        {
            nextKeyFrame = UNDEFINED_FRAME;
            keyFrames = null;
            keyFramesCount = keyFramesCapacity = 0;
        }

        public void SetTimelineDelegate(TimelineDelegate td)
        {
            timelineDelegate = td;
        }

        public override void Draw(Graphics g)
        {
            if (target.visible)
            {
                PreDraw(g);
                target.Draw(g);
                PostDraw(g);
            }
        }        

        public override void Update(float delta)
        {
            if (IsTimelinePlaying())
            {
                UpdateTimeline(delta);
            }

            if (target.updateable)
            {
                target.Update(delta);
            }
        }
        
        public void TurnTimelineSupportWithMaxKeyFrames(int m)
        {
            keyFramesCapacity = m + 1; // 1 for the current state
            keyFrames = new KeyFrame[m + 1];
            timelineLoopType = Timeline.NO_LOOP;
            ResetTimeline();
        }

        public void SetTimelineLoopType(Timeline l)
        {
            timelineLoopType = l;
        }

        public void ResetTimeline()
        {
            keyFramesCount = 1;
            nextKeyFrame = UNDEFINED_FRAME;
            timelineDirReverse = false;
        }

        public void DeleteTimeline()
        {
            ResetTimeline();
            keyFrames = null;
            keyFramesCapacity = 0;
        }

        public void AddKeyFrame(KeyFrame k)
        {
            keyFrames[keyFramesCount++] = k;
        }

        private void UpdateTimeline(float delta)
        {
            keyFrameTimeLeft -= delta;

            ctForm.AddR += currentStepPerSecond.ctForm.AddR;
            ctForm.AddG += currentStepPerSecond.ctForm.AddG;
            ctForm.AddB += currentStepPerSecond.ctForm.AddB;
            ctForm.AddA += currentStepPerSecond.ctForm.AddA;
            rotation += currentStepPerSecond.rotation * delta;
            scaleX += currentStepPerSecond.scaleX * delta;
            scaleY += currentStepPerSecond.scaleY * delta;
            x += currentStepPerSecond.x * delta;
            y += currentStepPerSecond.y * delta;

            if (keyFrameTimeLeft <= 0)
            {
                ctForm.AddR = keyFrames[nextKeyFrame].ctForm.AddR;
                ctForm.AddG = keyFrames[nextKeyFrame].ctForm.AddG;
                ctForm.AddB = keyFrames[nextKeyFrame].ctForm.AddB;
                ctForm.AddA = keyFrames[nextKeyFrame].ctForm.AddA;
                rotation = keyFrames[nextKeyFrame].rotation;
                scaleX = keyFrames[nextKeyFrame].scaleX;
                scaleY = keyFrames[nextKeyFrame].scaleY;
                x = keyFrames[nextKeyFrame].x;
                y = keyFrames[nextKeyFrame].y;

                TimelineKeyFrameFinished();
            }
        }

        public void PlayTimeline()
        {
            nextKeyFrame = 1;
            KeyFrame currentState = new KeyFrame(x, y, ctForm, scaleY, scaleY, rotation, 0);
            keyFrames[0] = currentState;
            InitKeyFrameStep(ref keyFrames[0], ref keyFrames[nextKeyFrame], keyFrames[nextKeyFrame].time);            
        }

        public void StopTimeline()
        {
            nextKeyFrame = UNDEFINED_FRAME;
        }

        public bool IsTimelinePlaying()
        {
            return nextKeyFrame != UNDEFINED_FRAME;
        }

        private void InitKeyFrameStep(ref KeyFrame src, ref KeyFrame dst, float t)
        {
            keyFrameTimeLeft = t;
            currentStepPerSecond.ctForm.AddR = ((dst.ctForm.AddR - src.ctForm.AddR) / keyFrameTimeLeft);
            currentStepPerSecond.ctForm.AddG = ((dst.ctForm.AddG - src.ctForm.AddG) / keyFrameTimeLeft);
            currentStepPerSecond.ctForm.AddB = ((dst.ctForm.AddB - src.ctForm.AddB) / keyFrameTimeLeft);
            currentStepPerSecond.ctForm.AddA = ((dst.ctForm.AddA - src.ctForm.AddA) / keyFrameTimeLeft);
            currentStepPerSecond.rotation = (dst.rotation - src.rotation) / keyFrameTimeLeft;
            currentStepPerSecond.scaleX = (dst.scaleX - src.scaleX) / keyFrameTimeLeft;
            currentStepPerSecond.scaleY = (dst.scaleY - src.scaleY) / keyFrameTimeLeft;
            currentStepPerSecond.x = (dst.x - src.x) / keyFrameTimeLeft;
            currentStepPerSecond.y = (dst.y - src.y) / keyFrameTimeLeft;
        }

        private void TimelineKeyFrameFinished()
        {
            switch (timelineLoopType)
            {
                case Timeline.PING_PONG:
                    if (timelineDirReverse && nextKeyFrame == 0)
                    {
                        timelineDirReverse = false;
                    }
                    else if (!timelineDirReverse && nextKeyFrame == keyFramesCount - 1)
                    {
                        timelineDirReverse = true;
                    }

                    if (timelineDirReverse)
                    {
                        InitKeyFrameStep(ref keyFrames[nextKeyFrame], ref keyFrames[nextKeyFrame - 1], keyFrames[nextKeyFrame].time);                        
                        nextKeyFrame--;
                    }
                    else
                    {
                        InitKeyFrameStep(ref keyFrames[nextKeyFrame], ref keyFrames[nextKeyFrame + 1], keyFrames[nextKeyFrame + 1].time);                        
                        nextKeyFrame++;
                    }
                    break;

                case Timeline.REPLAY:
                case Timeline.NO_LOOP:
                    if (nextKeyFrame < keyFramesCount - 1)
                    {
                        InitKeyFrameStep(ref keyFrames[nextKeyFrame], ref keyFrames[nextKeyFrame + 1], keyFrames[nextKeyFrame + 1].time);                        
                        nextKeyFrame++;
                    }
                    else
                    {
                        if (timelineLoopType == Timeline.REPLAY)
                        {
                            nextKeyFrame = 0;
                            InitKeyFrameStep(ref keyFrames[nextKeyFrame], ref keyFrames[nextKeyFrame + 1], keyFrames[nextKeyFrame + 1].time);                            
                            nextKeyFrame++;
                        }
                        else
                        {
                            StopTimeline();
                            
                            if (timelineDelegate != null)
                            {
                                timelineDelegate.ElementTimelineFinished(this);                            
                            }
                        }
                    }
                    break;
            }
        }
    }
}
