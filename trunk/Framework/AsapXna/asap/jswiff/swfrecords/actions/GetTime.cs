using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Reports the milliseconds since the SWF started playing.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>push time</code> (ms since the Player started)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>getTimer()</code>
     * </p>
     *
     * @since SWF 5
     */
    public class GetTime : ActionRecord
    {
        /** 
         * Creates a new GetTime action.
         */
        public GetTime() 
        {
            code = ActionConstants.GET_TIME;
        }
        
        public override String ToString()
        {
            return "GetTime";
        }
    }
}