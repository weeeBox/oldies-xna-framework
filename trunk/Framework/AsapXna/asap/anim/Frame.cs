using System;

using System.Collections.Generic;
using System.Diagnostics;



namespace asap.anim
{
    public class Frame
     {
        private int delay;
        
        private AnimLayer[] layers;
        
        public Frame(int delay) 
        {
            this.delay = delay;
            layers = null;
        }
        
        public virtual void AddLayer(AnimLayer layer)
        {
            if ((layers) == null) 
            {
                layers = new AnimLayer[1];
            } 
            else 
            {
                AnimLayer[] newLayers = new AnimLayer[(layers.Length) + 1];
                Array.Copy(layers, 0, newLayers, 0, layers.Length);
                layers = newLayers;
            }
            layers[((layers.Length) - 1)] = layer;
        }
        
        public virtual int GetDelay()
        {
            return delay;
        }
        
        public virtual int GetLayersCount()
        {
            return (layers) == null ? 0 : layers.Length;
        }
        
        public virtual AnimLayer GetLayer(int index)
        {
            Debug.Assert(((layers) != null) && ((0 <= index) && (index < (layers.Length))));
            return layers[index];
        }
        
    }
    
    
}