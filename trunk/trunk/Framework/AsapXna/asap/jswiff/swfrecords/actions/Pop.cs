using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * Pops a value from the stack and discards it.
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop value</code> (value to be removed from stack)
     * </p>
     * 
     * <p>
     * ActionScript equivalents: discarded function result
     * (<code>getName();</code>), discarded expression evaluation (<code>x +
     * 1;</code>);
     * </p>
     *
     * @since SWF 4
     */
    public class Pop : ActionRecord
    {
        /** 
         * Creates a new Pop action.
         */
        public Pop() 
        {
            code = ActionConstants.POP;
        }
        
        public override String ToString()
        {
            return "Pop";
        }
    }
}