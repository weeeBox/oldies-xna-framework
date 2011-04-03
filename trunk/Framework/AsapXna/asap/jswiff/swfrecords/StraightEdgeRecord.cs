using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used for defining a line between two points. The current
     * drawing position is considered to be the first point. The second point is
     * specified as coordinates relative to the current drawing position.
     */
    public class StraightEdgeRecord : EdgeRecord
    {
        private int deltaX;
        
        private int deltaY;
        
        /** 
         * Creates a new StraightEdgeRecord instance. Specify the point the line is
         * supposed to be drawn to by supplying its coordinates relative to the
         * current drawing position.
         *
         * @param deltaX x coordinate relative to current position (in twips)
         * @param deltaY y coordinate relative to current position (in twips)
         */
        public StraightEdgeRecord(int deltaX ,int deltaY) 
        {
            this.deltaX = deltaX;
            this.deltaY = deltaY;
        }
        
        /** 
         * Creates a new StraightEdgeRecord instance, reading data from a bit stream.
         *
         * @param stream source bit stream
         *
         * @throws IOException if an I/O error occured
         */
        public StraightEdgeRecord(InputBitStream stream) /* throws IOException */ 
        {
            byte numBits = unchecked((byte)(stream.ReadUnsignedBits(4)));
            bool generalLineFlag = stream.ReadBooleanBit();
            if (generalLineFlag) 
            {
                deltaX = ((int)(stream.ReadSignedBits((numBits + 2))));
                deltaY = ((int)(stream.ReadSignedBits((numBits + 2))));
            } 
            else 
            {
                bool vertLineFlag = stream.ReadBooleanBit();
                if (vertLineFlag) 
                {
                    deltaY = ((int)(stream.ReadSignedBits((numBits + 2))));
                } 
                else 
                {
                    deltaX = ((int)(stream.ReadSignedBits((numBits + 2))));
                }
            }
        }
        
        public virtual int GetDeltaX()
        {
            return deltaX;
        }
        
        public virtual int GetDeltaY()
        {
            return deltaY;
        }
    }
}