using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * Instructs Flash Player to advance to the next frame in the current movie.
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>nextFrame()</code>
     * </p>
     *
     * @since SWF 3
     */
    public class NextFrame : ActionRecord
    {
        /** 
         * Creates a new NextFrame action.
         */
        public NextFrame() 
        {
            code = ActionConstants.NEXT_FRAME;
        }
        
        public override String ToString()
        {
            return "NextFrame";
        }
    }
}