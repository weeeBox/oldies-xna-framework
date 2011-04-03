using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Checks whether the specified frame is loaded. If not, the specified number
     * of actions is skipped. As of SWF 5, this action is deprecated. Macromedia
     * recommends to use <code>MovieClip._framesLoaded</code> instead.
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>ifFrameLoaded()</code>
     * </p>
     *
     * @since SWF 3
     */
    public class WaitForFrame : ActionRecord
    {
        private int frame;
        
        private short skipCount;
        
        /** 
         * Creates a new WaitForFrame action.
         *
         * @param frame frame number to be loaded
         * @param skipCount number of actions to be skipped if the frame isn't loaded
         *        yet
         */
        public WaitForFrame(int frame ,short skipCount) 
        {
            code = ActionConstants.WAIT_FOR_FRAME;
            this.frame = frame;
            this.skipCount = skipCount;
        }
        
        public WaitForFrame(InputBitStream stream) /* throws IOException */ 
        {
            code = ActionConstants.WAIT_FOR_FRAME;
            frame = stream.ReadUI16();
            skipCount = stream.ReadUI8();
        }
        
        public virtual int GetFrame()
        {
            return frame;
        }
        
        public override int GetSize()
        {
            return 6;
        }
        
        public virtual short GetSkipCount()
        {
            return skipCount;
        }
        
        public override String ToString()
        {
            return (("WaitForFrame frame: " + (frame)) + " skipCount: ") + (skipCount);
        }
    }
}