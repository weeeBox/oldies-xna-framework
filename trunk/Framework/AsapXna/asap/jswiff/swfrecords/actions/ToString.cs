using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Converts an item to a string.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop item</code> (the item to be converted)<br>
     * <code>push str</code> (the conversion result as string)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>String()</code>
     * </p>
     *
     * @since SWF 5
     */
    public class ToStringAction : ActionRecord
    {
        /** 
         * Creates a new ToString action.
         */
        public ToStringAction() 
        {
            code = ActionConstants.TO_STRING;
        }
        
        public override String ToString()
        {
            return "ToString";
        }
    }
}