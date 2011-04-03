using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * Removes a clone sprite created with <code>CloneSprite</code>.
     * 
     * <p>
     * Performed stack operations:<br>
     * <code> pop name</code> (instance name of the clone to be removed)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>removeMovieClip()</code>
     * </p>
     *
     * @since SWF 4
     */
    public class RemoveSprite : ActionRecord
    {
        /** 
         * Creates a new RemoveSprite action.
         */
        public RemoveSprite() 
        {
            code = ActionConstants.REMOVE_SPRITE;
        }
        
        public override String ToString()
        {
            return "RemoveSprite";
        }
    }
}