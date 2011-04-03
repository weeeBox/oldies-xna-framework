using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Defines a local variable without setting its value. The initial value is
     * <code>undefined</code>. If the variable already exists, nothing happens.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code> pop varName</code> (the new variable's name)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: variable declaration without initialization (e.g.
     * <code>var a;)</code>
     * </p>
     *
     * @since SWF 5
     */
    public class DefineLocal2 : ActionRecord
    {
        /** 
         * Creates a new DefineLocal2 action.
         */
        public DefineLocal2() 
        {
            code = ActionConstants.DEFINE_LOCAL_2;
        }
        
        public override String ToString()
        {
            return "DefineLocal2";
        }
    }
}