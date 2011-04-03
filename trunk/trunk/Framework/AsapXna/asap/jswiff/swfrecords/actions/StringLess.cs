using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Tests whether a string is less than another.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop b</code><br>
     * <code>pop a</code><br>
     * <code>push [a &lt; b]</code> (1 (<code>true</code> in SWF 5 and higher) if
     * a&lt;b, otherwise 0 (<code>false</code>) )<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>lt</code> operator, <code>&lt;</code>
     * operator
     * </p>
     *
     * @since SWF 4
     */
    public class StringLess : ActionRecord
    {
        /** 
         * Creates a new StringLess action.
         */
        public StringLess() 
        {
            code = ActionConstants.STRING_LESS;
        }
        
        public override String ToString()
        {
            return "StringLess";
        }
    }
}