using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * This action mutes all sounds currently playing in the movie.
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>stopAllSounds()</code>
     * </p>
     *
     * @since SWF 3
     */
    public class StopSounds : ActionRecord
    {
        /** 
         * Creates a new StopSounds action.
         */
        public StopSounds() 
        {
            code = ActionConstants.STOP_SOUNDS;
        }
        
        public override String ToString()
        {
            return "StopSounds";
        }
    }
}