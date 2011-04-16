
using asap.graphics;
using Microsoft.Xna.Framework;

namespace asap.ui
{
    public abstract class BaseElement
    {
        private float x;

        private float y;

        private float scaleX;

        private float scaleY;

        private Color color;

        private float rotation;

        private float rotationOffsetX;

        private float rotationOffsetY;

        private bool visible;

        private bool enabled;

        public BaseElement() 
        {
            visible = true;
        }
        
        public virtual bool IsVisible()
        {
            return visible;
        }
        
        public virtual BaseElement SetVisible(bool visible)
        {
            this.visible = visible;
            return this;
        }
        
        public virtual bool Contains(int x, int y)
        {
            return x >= 0 && x < GetWidth() && y >= 0 && y < GetHeight();
        }
        
        public virtual void Draw(Graphics g)
        {
        }
        
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public abstract int GetWidth();
        
        public abstract int GetHeight();        
    }    
}