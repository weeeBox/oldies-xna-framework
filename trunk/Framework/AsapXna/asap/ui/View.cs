using System;

using System.Collections.Generic;


using java.asap.graphics;

namespace java.asap.ui
{
    abstract public class View
     {
        private bool visible;
        
        public View() 
        {
            visible = true;
        }
        
        public virtual bool IsVisible()
        {
            return visible;
        }
        
        public virtual View SetVisible(bool visible)
        {
            this.visible = visible;
            return this;
        }
        
        public virtual bool Contains(int x, int y)
        {
            return (((x >= 0) && (x < (GetWidth()))) && (y >= 0)) && (y < (GetHeight()));
        }
        
        public virtual void Draw(Graphics g)
        {
        }
        
        public abstract int GetWidth();
        
        public abstract int GetHeight();
        
    }
    
    
}