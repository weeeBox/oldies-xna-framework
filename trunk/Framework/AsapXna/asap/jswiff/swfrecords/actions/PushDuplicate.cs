using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Duplicates the value on top of the stack.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop value<br>
     * push value<br>
     * push value</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: none
     * </p>
     *
     * @since SWF 5
     */
    public class PushDuplicate : ActionRecord
    {
        /** 
         * Creates a new PushDuplicate action.
         */
        public PushDuplicate() 
        {
            code = ActionConstants.PUSH_DUPLICATE;
        }
        
        public override String ToString()
        {
            return "PushDuplicate";
        }
    }
}