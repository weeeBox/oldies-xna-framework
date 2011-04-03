using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Instructs Flash Player to go back to the previous frame in the current
     * movie.
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>prevFrame()</code>
     * </p>
     *
     * @since SWF 3
     */
    public class PreviousFrame : ActionRecord
    {
        /** 
         * Creates a new PreviousFrame action.
         */
        public PreviousFrame() 
        {
            code = ActionConstants.PREVIOUS_FRAME;
        }
        
        public override String ToString()
        {
            return "PreviousFrame";
        }
    }
}