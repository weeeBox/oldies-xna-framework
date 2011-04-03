using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Instructs Flash Player to stop playing at the current frame.
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>gotoAndStop()</code> (no 1:1 equivalent
     * available)
     * </p>
     *
     * @since SWF 3
     */
    public class Stop : ActionRecord
    {
        /** 
         * Creates a new Stop action.
         */
        public Stop() 
        {
            code = ActionConstants.STOP;
        }
        
        public override String ToString()
        {
            return "Stop";
        }
    }
}