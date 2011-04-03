using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Computes the product of two numbers.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br><code>pop b<br> pop a<br> push [a  b]</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the multiplication operator
     * </p>
     *
     * @since SWF 4
     */
    public class Multiply : ActionRecord
    {
        /** 
         * Creates a new Multiply action.
         */
        public Multiply() 
        {
            code = ActionConstants.MULTIPLY;
        }
        
        public override String ToString()
        {
            return "Multiply";
        }
    }
}