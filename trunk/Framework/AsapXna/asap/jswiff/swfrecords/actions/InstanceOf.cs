using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * This action determines if an object is an instance of a specified class (or
     * interface as of SWF 7).
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop class</code><br>
     * <code>pop ref</code> (reference to the object to be checked)<br>
     * <code>push result</code> (<code>true</code> or <code>false</code>)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>instanceof</code> operator
     * </p>
     *
     * @since SWF 6
     */
    public class InstanceOf : ActionRecord
    {
        /** 
         * Creates a new InstanceOf action.
         */
        public InstanceOf() 
        {
            code = ActionConstants.INSTANCE_OF;
        }
        
        public override String ToString()
        {
            return "InstanceOf";
        }
    }
}