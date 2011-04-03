using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Computes the sum of two numbers. Deprecated since SWF 5. If possible, use
     * <code>Add2</code> instead.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br><code>pop b<br> pop a<br> push [a + b]</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the <code>+</code> operator
     * </p>
     *
     * @since SWF 4
     */
    public class Add : ActionRecord
    {
        /** 
         * Creates a new Add action.
         */
        public Add() 
        {
            code = ActionConstants.ADD;
        }
        
        public override String ToString()
        {
            return "Add";
        }
    }
}