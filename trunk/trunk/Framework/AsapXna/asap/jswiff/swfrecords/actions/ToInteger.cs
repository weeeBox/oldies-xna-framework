using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Converts an item to an integer. Deprecated in SWF 5, use
     * <code>Math.round()</code>, <code>Math.floor()</code> or
     * <code>parseInt()</code>instead.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop item</code> (the item to be converted)<br>
     * <code>push int</code> (the conversion result as integer)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>int()</code>
     * </p>
     *
     * @since SWF 4
     */
    public class ToInteger : ActionRecord
    {
        /** 
         * Creates a new ToInteger action.
         */
        public ToInteger() 
        {
            code = ActionConstants.TO_INTEGER;
        }
        
        public override String ToString()
        {
            return "ToInteger";
        }
    }
}