using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Iterates over all properties of an object and pushes their names to the
     * stack.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop objName</code> (object name, can be slash path or dot path syntax)<br>
     * <code>push null</code> (indicates the end of the property list)<br>
     * <code>push prop1</code> (1st property name)<br>
     * <code>push prop2</code> (2nd property name)<br>
     * <code>...</code><br>
     * <code>push propn</code> (n-th property name)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>for..in</code> loop
     * </p>
     *
     * @since SWF 5
     */
    public class Enumerate : ActionRecord
    {
        /** 
         * Creates a new Enumerate action.
         */
        public Enumerate() 
        {
            code = ActionConstants.ENUMERATE;
        }
        
        public override String ToString()
        {
            return "Enumerate";
        }
    }
}