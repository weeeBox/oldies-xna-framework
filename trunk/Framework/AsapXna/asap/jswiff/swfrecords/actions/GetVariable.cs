using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Gets a variable's value.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop name</code> (the variable's name)<br>
     * <code>push value</code> (the value of the variable)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: conventional variable access
     * </p>
     *
     * @since SWF 4
     */
    public class GetVariable : ActionRecord
    {
        /** 
         * Creates a new GetVariable action.
         */
        public GetVariable() 
        {
            code = ActionConstants.GET_VARIABLE;
        }
        
        public override String ToString()
        {
            return "GetVariable";
        }
    }
}