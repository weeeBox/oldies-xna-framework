using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Tests two strings for equality. Replaced by <code>Equals2</code> as of SWF
     * 5.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop str1</code> (first string)<br>
     * <code>pop str2</code> (second number)<br>
     * <code>push [str2 == str1]</code> (1 if equal, else 0)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>eq</code> operator
     * </p>
     *
     * @since SWF 4
     */
    public class StringEquals : ActionRecord
    {
        /** 
         * Creates a new StringEquals action.
         */
        public StringEquals() 
        {
            code = ActionConstants.STRING_EQUALS;
        }
        
        public override String ToString()
        {
            return "StringEquals";
        }
    }
}