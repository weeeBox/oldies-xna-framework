using System;

using System.Collections.Generic;


using java.asap.graphics;
using System.Diagnostics;

namespace java.asap.anim
{
    public class AnimationPlayer
     {
        private Animation animation;
        
        private int frame;
        
        private int[] tags;
        
        private bool started;
        
        private bool repeated;
        
        private bool inverted;
        
        private int frameTime;
        
        private int transform;
        
        private int startFrame;
        
        private int endFrame;
        
        public AnimationPlayer(int categoriesCount) 
        {
            tags = new int[categoriesCount];
            SetAnimation(null);
        }
        
        public virtual void SetAnimation(Animation animation)
        {
            SetAnimation(animation, true);
        }
        
        public virtual void SetFrame(int frame)
        {
            this.frame = frame;
        }
        
        public virtual void SetAnimation(Animation animation, bool resetParams)
        {
            this.animation = animation;
            this.frameTime = 0;
            this.frame = 0;
            if (animation != null) 
            {
                startFrame = 0;
                endFrame = (animation.GetFramesCount()) - 1;
            } 
            if (resetParams) 
            {
                for (int i = 0; i < (tags.Length); i++) 
                {
                    this.tags[i] = -1;
                }
                this.started = false;
                this.repeated = false;
                this.inverted = false;
            } 
        }
        
        public virtual Animation GetAnimation()
        {
            return animation;
        }
        
        public virtual void SetTransform(int transform)
        {
            this.transform = transform;
        }
        
        public virtual bool IsValid()
        {
            for (int frameNumber = 0; frameNumber < (animation.GetFramesCount()); ++frameNumber) 
            {
                Frame frame = animation.GetFrame(frameNumber);
                for (int i = 0; i < (frame.GetLayersCount()); ++i) 
                {
                    AnimLayer layer = frame.GetLayer(i);
                    if (layer.IsMarker())
                        continue;
                    
                    if ((animation.GetPartset(layer.GetPartsetId())) == null)
                        return false;
                    
                }
            }
            return true;
        }
        
        public virtual void Draw(Graphics g, int x, int y)
        {
            DrawRegion(g, x, y, 0, 0, -1, -1);
        }
        
        public virtual void DrawRegion(Graphics g, int x, int y, int xOffset, int yOffset, int width, int height)
        {
            Debug.Assert((animation) != null);
            Frame frame = animation.GetFrame(this.frame);
            for (int i = 0; i < (frame.GetLayersCount()); i++) 
            {
                AnimLayer layer = frame.GetLayer(i);
                if (((tags[layer.GetCategory()]) & (layer.GetTags())) != 0) 
                {
                    if (!(layer.IsMarker())) 
                    {
                        int layerX = x + (layer.GetX());
                        int layerY = y + (layer.GetY());
                        int partId = layer.GetId();
                        int layerTransform = layer.GetTransform();
                        PartSet ps = animation.GetPartset(layer.GetPartsetId());
                        int srcX = xOffset;
                        int srcY = yOffset;
                        int srcWidth = width;
                        int srcHeight = height;
                        int destX = layerX;
                        int destY = layerY;
                        if (srcX < 0) 
                        {
                            destX -= srcX;
                            srcX = 0;
                        } 
                        if (srcY < 0) 
                        {
                            destY -= srcY;
                            srcY = 0;
                        } 
                        int pw = ps.GetPartWidth(partId);
                        int ph = ps.GetPartHeight(partId);
                        if ((srcX < pw) && (srcY < ph)) 
                        {
                            if ((srcX + srcWidth) > pw) 
                            {
                                srcWidth = pw - srcX;
                            } 
                            if ((srcY + srcHeight) > ph) 
                            {
                                srcHeight = ph - srcY;
                            } 
                            ps.DrawRegion(g, destX, destY, partId, ((transform) ^ layerTransform), srcX, srcY, srcWidth, srcHeight);
                        } 
                    } 
                } 
            }
        }
        
        public virtual void Update(int deltaTime)
        {
            if ((started) && ((animation) != null)) 
            {
                frameTime += deltaTime;
                int frameDelay = animation.GetFrame(frame).GetDelay();
                if ((frameTime) > frameDelay) 
                {
                    frameTime -= frameDelay;
                    if (!(inverted)) 
                    {
                        (frame)++;
                        if ((frame) > (endFrame)) 
                        {
                            if (repeated) 
                            {
                                frame = startFrame;
                            } 
                            else 
                            {
                                frame = endFrame;
                                Stop();
                            }
                        } 
                    } 
                    else 
                    {
                        (frame)--;
                        if ((frame) < (startFrame)) 
                        {
                            if (repeated) 
                            {
                                frame = endFrame;
                            } 
                            else 
                            {
                                frame = startFrame;
                                Stop();
                            }
                        } 
                    }
                } 
            } 
        }
        
        public virtual int GetFramesCount()
        {
            return animation.GetFramesCount();
        }
        
        public virtual void Start()
        {
            Start(false, false);
        }
        
        public virtual void Start(int startFrame, int endFrame)
        {
            if (startFrame <= endFrame)
                Start(false, false, startFrame, endFrame);
            
            else
                Start(false, true, endFrame, startFrame);
            
        }
        
        public virtual void Start(bool repeated, bool inverted, int startFrame, int endFrame)
        {
            if ((animation) != null) 
            {
                this.startFrame = Math.Max(0, Math.Min(((animation.GetFramesCount()) - 1), startFrame));
                this.endFrame = Math.Max(0, Math.Min(((animation.GetFramesCount()) - 1), endFrame));
                started = true;
                this.repeated = repeated;
                this.inverted = inverted;
                if (inverted) 
                {
                    frame = this.endFrame;
                } 
                else 
                {
                    frame = this.startFrame;
                }
                frameTime = 0;
            } 
            else 
            {
                Debug.Assert(false);
            }
        }
        
        public virtual void Start(bool repeated, bool inverted)
        {
            Start(repeated, inverted, 0, ((GetFramesCount()) - 1));
        }
        
        public virtual void Start(bool repeated, bool inverted, int frame)
        {
            Start(repeated, inverted, frame, ((GetFramesCount()) - 1));
        }
        
        public virtual void Stop()
        {
            if ((animation) != null) 
            {
                started = false;
            } 
            else 
            {
                Debug.Assert(false);
            }
        }
        
        public virtual bool IsStarted()
        {
            return started;
        }
        
        public virtual bool IsRepeated()
        {
            return repeated;
        }
        
        public virtual void SetTags(int category, int mask)
        {
            tags[category] = mask;
        }
        
        public virtual void SetTags(int[] tags)
        {
            Debug.Assert((tags.Length) == (this.tags.Length));
            for (int i = 0; i < (tags.Length); i++) 
            {
                this.tags[i] = tags[i];
            }
        }
        
        public virtual int GetTags(int category)
        {
            return tags[category];
        }
        
        public virtual int[] GetTags()
        {
            return tags;
        }
        
        public virtual int GetFrame()
        {
            return frame;
        }
        
        public virtual int GetTransform()
        {
            return transform;
        }
        
        public virtual bool IsInverted()
        {
            return inverted;
        }
        
    }
    
    
}