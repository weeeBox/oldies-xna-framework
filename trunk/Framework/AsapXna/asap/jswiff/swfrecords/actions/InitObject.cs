using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Creates an object and initializes it with values from the stack.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code> pop n</code> (property number)<br>
     * <code> pop value_1</code> (1st property value)<br>
     * <code> pop name_1</code> (1st property name)<br>
     * <code> pop value_2</code> (2nd property value)<br>
     * <code> pop name_2</code> (2nd property name)<br>
     * <code> ...<br>
     * pop value_n</code> (n-th property value)<br>
     * <code> pop name_n</code> (n-th property name)<br>
     * <code> push obj</code> (the new object)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: object literal (e.g. <code>{width: 150, height:
     * 100}</code>)
     * </p>
     *
     * @since SWF 5
     */
    public class InitObject : ActionRecord
    {
        /** 
         * Creates a new InitObject action.
         */
        public InitObject() 
        {
            code = ActionConstants.INIT_OBJECT;
        }
        
        public override String ToString()
        {
            return "InitObject";
        }
    }
}