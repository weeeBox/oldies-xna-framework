using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Similar to <code>Equals2</code>, but the two arguments must be of the same
     * type in order to be considered equal (i.e. data types are not converted).
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop item2</code> (first number)<br>
     * <code>pop item1</code> (second number)<br>
     * <code>push [item1 === item2]</code> (<code>true</code> if items and types
     * are equal, else <code>false</code>)
     * </p>
     * 
     * <p>
     * ActionScript equivalents: <code>===</code> operator
     * </p>
     *
     * @since SWF 6
     */
    public class StrictEquals : ActionRecord
    {
        /** 
         * Creates a new StrictEquals action.
         */
        public StrictEquals() 
        {
            code = ActionConstants.STRICT_EQUALS;
        }
        
        public override String ToString()
        {
            return "StrictEquals";
        }
    }
}