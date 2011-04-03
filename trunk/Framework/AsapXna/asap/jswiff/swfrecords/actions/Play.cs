using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Instructs Flash Player to start playing at the current frame.
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>gotoAndPlay()</code> (no 1:1 equivalent
     * available)
     * </p>
     *
     * @since SWF 3
     */
    public class Play : ActionRecord
    {
        /** 
         * Creates a new Play action.
         */
        public Play() 
        {
            code = ActionConstants.PLAY;
        }
        
        public override String ToString()
        {
            return "Play";
        }
    }
}