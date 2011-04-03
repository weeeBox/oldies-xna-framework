using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Instructs Flash Player to go to the specified frame in the current movie.
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>gotoAndPlay(), gotoAndStop()</code>
     * </p>
     *
     * @since SWF 3
     */
    public class GoToFrame : ActionRecord
    {
        private int frame;
        
        /** 
         * Creates a new GoToFrame action. The target frame number is passed as a
         * integer.
         *
         * @param frame a frame number
         */
        public GoToFrame(int frame) 
        {
            code = ActionConstants.GO_TO_FRAME;
            this.frame = frame;
        }
        
        public GoToFrame(InputBitStream stream) /* throws IOException */ 
        {
            code = ActionConstants.GO_TO_FRAME;
            frame = stream.ReadUI16();
        }
        
        public virtual int GetFrame()
        {
            return frame;
        }
        
        public override int GetSize()
        {
            return 5;
        }
        
        public override String ToString()
        {
            return "GoToFrame " + (frame);
        }        
    }
}