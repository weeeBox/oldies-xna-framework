using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * This action is used to implement the addition operator according to the
     * ECMAScript specification, i.e. it performs either string concatenation or
     * numeric addition, depending on the data types of the operands.
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
     * @since SWF 5
     */
    public class Add2 : ActionRecord
    {
        /** 
         * Creates a new Add2 action.
         */
        public Add2() 
        {
            code = ActionConstants.ADD_2;
        }
        
        public override String ToString()
        {
            return "Add2";
        }
    }
}