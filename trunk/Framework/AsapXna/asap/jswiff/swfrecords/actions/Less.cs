using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Tests whether a number is less than another number
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop b</code><br>
     * <code>pop a</code><br>
     * <code>push [a &lt; b]</code> (in SWF 4: 1 if a&lt;b, otherwise 0 - as of SWF
     * 5, <code>true</code> / <code>false</code>)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>&lt;</code> operator
     * </p>
     *
     * @since SWF 4
     */
    public class Less : ActionRecord
    {
        /** 
         * Creates a new Less action.
         */
        public Less() 
        {
            code = ActionConstants.LESS;
        }
        
        public override String ToString()
        {
            return "Less";
        }
    }
}