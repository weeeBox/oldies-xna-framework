using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Sets a variable.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop value</code> (the new value)<br>
     * <code>pop name</code> (the variable's name)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: conventional variable assignment (e.g. <code>x =
     * 2;</code>)
     * </p>
     *
     * @since SWF 4
     */
    public class SetVariable : ActionRecord
    {
        /** 
         * Creates a new SetVariable action.
         */
        public SetVariable() 
        {
            code = ActionConstants.SET_VARIABLE;
        }
        
        public override String ToString()
        {
            return "SetVariable";
        }
    }
}