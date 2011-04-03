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
     * Performed stack operations:<br>
     * <code>pop frame</code> (frame number or label)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>ifFrameLoaded()</code>
     * </p>
     *
     * @since SWF 4
     */
    public class WaitForFrame2 : ActionRecord
    {
        private short skipCount;
        
        /** 
         * Creates a new WaitForFrame2 action.
         *
         * @param skipCount number of actions to be skipped if the frame isn't loaded
         *        yet
         */
        public WaitForFrame2(short skipCount) 
        {
            code = ActionConstants.WAIT_FOR_FRAME_2;
            this.skipCount = skipCount;
        }
        
        public WaitForFrame2(InputBitStream stream) /* throws IOException */ 
        {
            code = ActionConstants.WAIT_FOR_FRAME_2;
            skipCount = stream.ReadUI8();
        }
        
        public override int GetSize()
        {
            return 4;
        }
        
        public virtual short GetSkipCount()
        {
            return skipCount;
        }
        
        public override String ToString()
        {
            return "WaitForFrame2 skipCount: " + (skipCount);
        }
    }
}