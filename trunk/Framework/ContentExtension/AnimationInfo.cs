using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ContentExtension
{
    public struct Transform
    {
        public float scaleX;
        public float scaleY;
        public float skewX;
        public float skewY;
        public float transX;
        public float transY;
    }

    public class AnimationInfo
    {
        private float width;
        private float height;
        private int framesCount;
        private int frameRate;

        private List<Transform> transforms;

        public AnimationInfo(float width, float height, int framesCount, int frameRate)
        {
            this.width = width;
            this.height = height;
            this.frameRate = frameRate;
            this.framesCount = framesCount;
            transforms = new List<Transform>(framesCount);
        }

        public void AddTransform(Transform m)
        {
            transforms.Add(m);
        }

        public List<Transform> Transforms
        {
            get { return transforms; }
        }

        public int FramesCount 
        { 
            get { return framesCount; } 
        }

        public int FrameRate 
        { 
            get { return frameRate; } 
        }

        public float Width 
        { 
            get { return width; } 
        }

        public float Height 
        { 
            get { return height; } 
        }
    }
}
