using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Defines a local variable and sets its value. If the variable already exists,
     * the value is set to the newly specified value.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code> pop value</code> (initial value)<br>
     * <code> pop varName</code> (variable name)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: variable declaration with initialization (e.g.
     * <code>var a = 2;)</code>
     * </p>
     *
     * @since SWF 5
     */
    public class DefineLocal : ActionRecord
    {
        /** 
         * Creates a new DefineLocal action.
         */
        public DefineLocal() 
        {
            code = ActionConstants.DEFINE_LOCAL;
        }
        
        public override String ToString()
        {
            return "DefineLocal";
        }
    }
}