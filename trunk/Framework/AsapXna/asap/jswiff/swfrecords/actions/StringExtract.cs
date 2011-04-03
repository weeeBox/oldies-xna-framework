using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Extracts a substring from a string. Deprecated as of SWF 5, use
     * String.substr() instead.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop count</code> (number of characters to extract)<br>
     * <code>pop index</code> (index of first character to extract)<br>
     * <code>pop string</code> (string to extract from)<br>
     * <code>push substr</code> (extracted substring)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>substring()</code>
     * </p>
     *
     * @since SWF 4
     */
    public class StringExtract : ActionRecord
    {
        /** 
         * Creates a new StringExtract action.
         */
        public StringExtract() 
        {
            code = ActionConstants.STRING_EXTRACT;
        }
        
        public override String ToString()
        {
            return "StringExtract";
        }
    }
}