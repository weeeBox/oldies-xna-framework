using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Decrements a number by one.
     * </p>
     * Pops a value from the stack, converts it to number type, decrements it by 1,
     * and pushes it back to the stack.
     * 
     * <p>
     * Performed stack operations:<br><code>pop value<br>push [value - 1]</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>--</code> (decrement) operator
     * </p>
     *
     * @since SWF 5
     */
    public class Decrement : ActionRecord
    {
        /** 
         * Creates a new Decrement action.
         */
        public Decrement() 
        {
            code = ActionConstants.DECREMENT;
        }
        
        public override String ToString()
        {
            return "Decrement";
        }
    }
}