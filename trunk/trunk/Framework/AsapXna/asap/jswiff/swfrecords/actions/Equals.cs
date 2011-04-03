using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Tests two numbers for equality.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop num1</code> (first number)<br>
     * <code>pop num2</code> (second number)<br>
     * <code>push [num2 == num1]</code> (1 if equal, else 0; as of SWF 5,
     * <code>true</code> instead of 1 and <code>false</code> instead of 0)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>==</code> operator
     * </p>
     *
     * @since SWF 4
     */
    public class Equals : ActionRecord
    {
        /** 
         * Creates a new Equals action.
         */
        public Equals() 
        {
            code = ActionConstants.EQUALS;
        }
        
        public override String ToString()
        {
            return "Equals";
        }
    }
}