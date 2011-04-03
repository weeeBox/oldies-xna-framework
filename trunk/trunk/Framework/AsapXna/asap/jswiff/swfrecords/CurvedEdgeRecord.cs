using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used for defining quadratic Bezier curves. A Bezier curve is
     * defined by three points: two on-curve anchor points and one off-curve
     * control point. The current drawing position is considered to be the first
     * anchor point, i.e. for a curve definition it is sufficient to specify one
     * control and one (single) anchor point.
     */
    public class CurvedEdgeRecord : EdgeRecord
    {
        private int controlDeltaX;
        
        private int controlDeltaY;
        
        private int anchorDeltaX;
        
        private int anchorDeltaY;
        
        /** 
         * Creates a new CurvedEdgeRecord instance. Supply the control point
         * (relative to the current drawing position) and the anchor point (relative
         * to the specified control point).
         *
         * @param controlDeltaX x coordinate of control point (relative to current
         *        position, in twips)
         * @param controlDeltaY y coordinate of control point (relative to current
         *        position, in twips)
         * @param anchorDeltaX x coordinate of anchor point (relative to control
         *        point, in twips)
         * @param anchorDeltaY y coordinate of anchor point (relative to control
         *        point, in twips)
         */
        public CurvedEdgeRecord(int controlDeltaX ,int controlDeltaY ,int anchorDeltaX ,int anchorDeltaY) 
        {
            this.controlDeltaX = controlDeltaX;
            this.controlDeltaY = controlDeltaY;
            this.anchorDeltaX = anchorDeltaX;
            this.anchorDeltaY = anchorDeltaY;
        }
        
        /** 
         * Creates a new CurvedEdgeRecord instance, reading data from a bit stream.
         *
         * @param stream source bit stream
         *
         * @throws IOException if an I/O error occured
         */
        public CurvedEdgeRecord(InputBitStream stream) /* throws IOException */ 
        {
            int numBits = ((int)(stream.ReadUnsignedBits(4))) + 2;
            controlDeltaX = ((int)(stream.ReadSignedBits(numBits)));
            controlDeltaY = ((int)(stream.ReadSignedBits(numBits)));
            anchorDeltaX = ((int)(stream.ReadSignedBits(numBits)));
            anchorDeltaY = ((int)(stream.ReadSignedBits(numBits)));
        }
        
        public virtual int GetAnchorDeltaX()
        {
            return anchorDeltaX;
        }
        
        public virtual int GetAnchorDeltaY()
        {
            return anchorDeltaY;
        }
        
        public virtual int GetControlDeltaX()
        {
            return controlDeltaX;
        }
        
        public virtual int GetControlDeltaY()
        {
            return controlDeltaY;
        }        
    }
}