using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Tests whether a number is less than another number, taking account of data
     * types (according to ECMA-262 spec).
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop b</code><br>
     * <code>pop a</code><br>
     * <code>push [a &lt; b]</code> (<code>true</code> if a&lt;b, otherwise
     * <code>false</code>)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>&lt;</code> operator
     * </p>
     *
     * @since SWF 5
     */
    public class Less2 : ActionRecord
    {
        /** 
         * Creates a new Less2 action.
         */
        public Less2() 
        {
            code = ActionConstants.LESS_2;
        }
        
        public override String ToString()
        {
            return "Less2";
        }
    }
}