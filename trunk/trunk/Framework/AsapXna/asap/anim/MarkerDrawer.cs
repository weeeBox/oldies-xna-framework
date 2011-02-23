using System;

using System.Collections.Generic;


using java.asap.graphics;

namespace java.asap.anim
{
    /** 
     * Лучше был бы интерфейсом, но проблемы будут с бравой.
     */
    abstract public class MarkerDrawer
     {
        public abstract void DrawMarker(Graphics gr, int x, int y, int width, int height, int markerId, Animation animation, Object data);
        
    }
    
    
}