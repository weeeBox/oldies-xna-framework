using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Computes the difference between two numbers.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br><code>pop b<br> pop a<br> push [a - b]</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the <code>-</code> operator
     * </p>
     *
     * @since SWF 4
     */
    public class Subtract : ActionRecord
    {
        /** 
         * Creates a new Subtract action.
         */
        public Subtract() 
        {
            code = ActionConstants.SUBTRACT;
        }
        
        public override String ToString()
        {
            return "Subtract";
        }
    }
}