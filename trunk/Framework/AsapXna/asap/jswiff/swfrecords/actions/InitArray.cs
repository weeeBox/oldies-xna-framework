using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Creates an array and initializes it with values from the stack.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code> pop n</code> (array length)<br>
     * <code> pop value_1</code> (first value)<br>
     * <code> pop value_2</code> (second value)<br>
     * <code> ...<br>
     * pop value_n</code> (n-th value)<br>
     * <code> push array</code> (array object)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: array literal (e.g. <code>[1, 2, 3]</code>)
     * </p>
     *
     * @since SWF 5
     */
    public class InitArray : ActionRecord
    {
        /** 
         * Creates a new InitArray action.
         */
        public InitArray() 
        {
            code = ActionConstants.INIT_ARRAY;
        }
        
        public override String ToString()
        {
            return "InitArray";
        }
    }
}