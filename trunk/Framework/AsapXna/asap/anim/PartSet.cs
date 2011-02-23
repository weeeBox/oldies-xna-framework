using System;

using System.Collections.Generic;


using asap.resources;
using asap.graphics;
using System.Diagnostics;

namespace asap.anim
{
    public class PartSet
     {
        public Image image;
        
        private const int PART_X = 0;
        
        private const int PART_Y = 1;
        
        private const int PART_WIDTH = 2;
        
        private const int PART_HEIGHT = 3;
        
        private const int PART_SIZE = 4;
        
        public short[] partData;
        
        public int partsCount;        
        
        public PartSet() /* throws Exception */ 
        {
            //DataInputStream dis = new DataInputStream(_is);
            //partsCount = dis.ReadShort();
            //partData = new short[(partsCount) * (PART_SIZE)];
            //int size = (partsCount) * (PART_SIZE);
            //for (int i = 0; i < size; i++)
            //    partData[i] = dis.ReadShort();
            //int imageSize = dis.ReadInt();
            //image = ResFactory.GetInstance().CreateImage(_is, imageSize);
            throw new NotImplementedException();
        }
        
        public virtual void SetPalette(int paletteId)
        {
        }
        
        public virtual void Draw(Graphics gr, int x, int y, int partId, int transform)
        {
            DrawRegion(gr, x, y, partId, transform, 0, 0, -1, -1);
        }
        
        public virtual void DrawRegion(Graphics gr, int x, int y, int partId, int transform, int partXOffset, int partYOffset, int partWidth, int partHeight)
        {
            if ((partId >= 0) && (partId < (partsCount))) 
            {
                int offset = partId << 2;
                int partX = partData[(offset + (PART_X))];
                int partY = partData[(offset + (PART_Y))];
                int width = partWidth == (-1) ? (partData[(offset + (PART_WIDTH))]) - partXOffset : partWidth;
                int height = partHeight == (-1) ? (partData[(offset + (PART_HEIGHT))]) - partYOffset : partHeight;
                Debug.Assert((((partXOffset >= 0) && (partYOffset >= 0)) && (width >= 0)) && (height >= 0), (((((partXOffset + " ") + partYOffset) + " ") + width) + " ") + height);
                gr.DrawRegion(image, (partX + partXOffset), (partY + partYOffset), width, height, transform, x, y, ((Graphics.LEFT) | (Graphics.TOP)));
            } 
            else 
            {
                Debug.Assert(false, (("partset: unknown partid: " + partId) + ". parts count: ") + (partsCount));
            }
        }
        
        public virtual int GetPartsCount()
        {
            return partsCount;
        }
        
        public virtual int GetPartWidth(int partId)
        {
            return partData[((partId << 2) + (PART_WIDTH))];
        }
        
        public virtual int GetPartHeight(int partId)
        {
            return partData[((partId << 2) + (PART_HEIGHT))];
        }
        
    }
    
    
}