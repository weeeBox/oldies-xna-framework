using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Tests whether a number is greater than another number, taking account of
     * data types (according to ECMA-262 spec).
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop b</code><br>
     * <code>pop a</code><br>
     * <code>push [a &gt; b]</code> (<code>true</code> if a&gt;b, otherwise
     * <code>false</code>)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>&gt;</code> operator
     * </p>
     *
     * @since SWF 6
     */
    public class Greater : ActionRecord
    {
        /** 
         * Creates a new Greater action.
         */
        public Greater() 
        {
            code = ActionConstants.GREATER;
        }
        
        public override String ToString()
        {
            return "Greater";
        }
    }
}