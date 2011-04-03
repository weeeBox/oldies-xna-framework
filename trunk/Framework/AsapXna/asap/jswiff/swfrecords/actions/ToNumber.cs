using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Converts an item into a number.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop item</code> (the item to be converted - for objects,
     * <code>valueOf()</code> is used for conversion)<br>
     * <code>push number</code> (the conversion result as number)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>Number()</code>
     * </p>
     *
     * @since SWF 5
     */
    public class ToNumber : ActionRecord
    {
        /** 
         * Creates a new ToNumber action.
         */
        public ToNumber() 
        {
            code = ActionConstants.TO_NUMBER;
        }
        
        public override String ToString()
        {
            return "ToNumber";
        }
    }
}