using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.visual;
using Microsoft.Xna.Framework;
using asap.graphics;

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
            public Vector4 color;
            public float rotation;
            public float time;

            public KeyFrame(float x, float y, Color color, float scaleX, float scaleY, float rotation, float time)
            {
                this.x = x;
                this.y = y;
                this.scaleX = scaleX;
                this.scaleY = scaleY;
                this.rotation = rotation;
                this.color = new Vector4(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
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

            color.R += (byte)(255.0f * currentStepPerSecond.color.X * delta);
            color.G += (byte)(255.0f * currentStepPerSecond.color.Y * delta);
            color.B += (byte)(255.0f * currentStepPerSecond.color.Z * delta);
            color.A += (byte)(255.0f * currentStepPerSecond.color.W * delta);
            rotation += currentStepPerSecond.rotation * delta;
            scaleX += currentStepPerSecond.scaleX * delta;
            scaleY += currentStepPerSecond.scaleY * delta;
            x += currentStepPerSecond.x * delta;
            y += currentStepPerSecond.y * delta;

            if (keyFrameTimeLeft <= 0)
            {
                color.R = (byte)(keyFrames[nextKeyFrame].color.X * 255);
                color.G = (byte)(keyFrames[nextKeyFrame].color.Y * 255);
                color.B = (byte)(keyFrames[nextKeyFrame].color.Z * 255);
                color.A = (byte)(keyFrames[nextKeyFrame].color.W * 255);
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
            KeyFrame currentState = new KeyFrame(x, y, color, scaleY, scaleY, rotation, 0);
            keyFrames[0] = currentState;
            InitKeyFrameStep(keyFrames[0], keyFrames[nextKeyFrame], keyFrames[nextKeyFrame].time);
            //[self initKeyFrameStepFrom:&keyFrames[0] To:&keyFrames[nextKeyFrame] withTime:keyFrames[nextKeyFrame].time];
        }

        public void StopTimeline()
        {
            nextKeyFrame = UNDEFINED_FRAME;
        }

        public bool IsTimelinePlaying()
        {
            return nextKeyFrame != UNDEFINED_FRAME;
        }

        private void InitKeyFrameStep(KeyFrame src, KeyFrame dst, float t)
        {
            keyFrameTimeLeft = t;
            currentStepPerSecond.color.X = ((dst.color.X - src.color.X) / keyFrameTimeLeft);
            currentStepPerSecond.color.Y = ((dst.color.Y - src.color.Y) / keyFrameTimeLeft);
            currentStepPerSecond.color.Z = ((dst.color.Z - src.color.Z) / keyFrameTimeLeft);
            currentStepPerSecond.color.W = ((dst.color.W - src.color.W) / keyFrameTimeLeft);
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
                        InitKeyFrameStep(keyFrames[nextKeyFrame], keyFrames[nextKeyFrame - 1], keyFrames[nextKeyFrame].time);                        
                        nextKeyFrame--;
                    }
                    else
                    {
                        InitKeyFrameStep(keyFrames[nextKeyFrame], keyFrames[nextKeyFrame + 1], keyFrames[nextKeyFrame + 1].time);                        
                        nextKeyFrame++;
                    }
                    break;

                case Timeline.REPLAY:
                case Timeline.NO_LOOP:
                    if (nextKeyFrame < keyFramesCount - 1)
                    {
                        InitKeyFrameStep(keyFrames[nextKeyFrame], keyFrames[nextKeyFrame + 1], keyFrames[nextKeyFrame + 1].time);                        
                        nextKeyFrame++;
                    }
                    else
                    {
                        if (timelineLoopType == Timeline.REPLAY)
                        {
                            nextKeyFrame = 0;
                            InitKeyFrameStep(keyFrames[nextKeyFrame], keyFrames[nextKeyFrame + 1], keyFrames[nextKeyFrame + 1].time);                            
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
