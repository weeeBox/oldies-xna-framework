using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Increments a number by one.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br><code>pop value<br>push [value + 1]</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>++</code> operator
     * </p>
     *
     * @since SWF 5
     */
    public class Increment : ActionRecord
    {
        /** 
         * Creates a new Increment action.
         */
        public Increment() 
        {
            code = ActionConstants.INCREMENT;
        }
        
        public override String ToString()
        {
            return "Increment";
        }
    }
}