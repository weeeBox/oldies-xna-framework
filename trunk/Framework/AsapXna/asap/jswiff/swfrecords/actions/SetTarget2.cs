using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Instructs Flash Player to change the context of subsequent actions, so they
     * apply to an object with the specified name. This action can be used e.g. to
     * control the timeline of a sprite object. Unlike <code>SetTarget</code>,
     * <code>SetTarget2</code> pops the target off the stack.
     * </p>
     * 
     * <p>
     * Note: as of SWF 5, this action is deprecated. Use <code>With</code> instead.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br><code>pop target</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>tellTarget()</code>
     * </p>
     *
     * @since SWF 4
     */
    public class SetTarget2 : ActionRecord
    {
        /** 
         * Creates a new SetTarget2 action.
         */
        public SetTarget2() 
        {
            code = ActionConstants.SET_TARGET_2;
        }
        
        public override String ToString()
        {
            return "SetTarget2";
        }
    }
}