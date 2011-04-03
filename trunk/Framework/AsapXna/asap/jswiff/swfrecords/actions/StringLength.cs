using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Computes the length of a string. Deprecated as of SWF 5, use String.length
     * instead.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop string</code><br>
     * <code>push length</code> (length of <code>string</code>)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>length()</code>
     * </p>
     *
     * @since SWF 4
     */
    public class StringLength : ActionRecord
    {
        /** 
         * Creates a new StringLength action.
         */
        public StringLength() 
        {
            code = ActionConstants.STRING_LENGTH;
        }
        
        public override String ToString()
        {
            return "StringLength";
        }
    }
}