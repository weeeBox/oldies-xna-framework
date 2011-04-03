using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Extracts a substring from a string, multibyte-aware. Deprecated as of SWF 5,
     * use String.substr() instead.
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
     * ActionScript equivalent: <code>mbsubstring()</code>
     * </p>
     *
     * @since SWF 4
     */
    public class MBStringExtract : ActionRecord
    {
        /** 
         * Creates a new MBStringExtract action.
         */
        public MBStringExtract() 
        {
            code = ActionConstants.M_B_STRING_EXTRACT;
        }
        
        public override String ToString()
        {
            return "MBStringExtract";
        }
    }
}