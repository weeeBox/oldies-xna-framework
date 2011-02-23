using System;

using System.Collections.Generic;
using System.Diagnostics;



namespace java.asap.anim
{
    public class AnimLayer
     {
        private int x;
        
        private int y;
        
        private int width;
        
        private int height;
        
        private int alpha;
        
        /** 
         * странная штука, которая может много значить
         * 255 - флаг маркера, в противном случае это трансформация + номер партсета
         */
        private int partsetId;
        
        private int id;
        
        private int mask;
        
        public AnimLayer(int x ,int y ,int width ,int height ,int alpha ,int partsetId ,int id ,int mask) 
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.alpha = alpha;
            this.partsetId = partsetId;
            this.id = id;
            this.mask = mask;
        }
        
        public virtual int GetX()
        {
            return x;
        }
        
        public virtual int GetY()
        {
            return y;
        }
        
        public virtual int GetWidth()
        {
            return width;
        }
        
        public virtual int GetHeight()
        {
            return height;
        }
        
        public virtual bool IsMarker()
        {
            return (partsetId) == 255;
        }
        
        public virtual int GetPartsetId()
        {
            Debug.Assert(!(IsMarker()));
            return (partsetId) & 63;
        }
        
        public virtual int GetTransform()
        {
            Debug.Assert(!(IsMarker()));
            return ((partsetId) >> 6) & 3;
        }
        
        public virtual int GetId()
        {
            return id;
        }
        
        public virtual int GetCategory()
        {
            return ((mask) >> 28) & 15;
        }
        
        public virtual int GetTags()
        {
            return (mask) & 268435455;
        }
        
        public virtual int GetAlpha()
        {
            return alpha;
        }
        
    }
    
    
}