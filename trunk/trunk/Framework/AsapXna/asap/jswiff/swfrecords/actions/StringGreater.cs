using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Tests whether a string is greater than another.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop b</code><br>
     * <code>pop a</code><br>
     * <code>push [a &gt; b]</code> (1 (<code>true</code> in SWF 5 and higher) if
     * a&gt;b, otherwise 0 (<code>false</code>) )<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>&gt;</code> operator
     * </p>
     *
     * @since SWF 6
     */
    public class StringGreater : ActionRecord
    {
        /** 
         * Creates a new StringGreater action.
         */
        public StringGreater() 
        {
            code = ActionConstants.STRING_GREATER;
        }
        
        public override String ToString()
        {
            return "StringGreater";
        }
    }
}