using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Calculates the remainder of the division between two numbers.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop b</code><br>
     * <code>pop a</code><br>
     * <code>push [a % b]</code> (remainder of division between a and b)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>%</code> operator
     * </p>
     *
     * @since SWF 4
     */
    public class Modulo : ActionRecord
    {
        /** 
         * Creates a new Modulo action.
         */
        public Modulo() 
        {
            code = ActionConstants.MODULO;
        }
        
        public override String ToString()
        {
            return "Modulo";
        }
    }
}