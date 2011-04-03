using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Performs a bitwise OR (<code>|</code>). The arguments are treated as 32-bit
     * integers (signed integers for negative numbers, otherwise unsigned
     * integers). The result is interpreted as a signed 32-bit integer.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br><code>pop b<br>pop a<br>push [a | b]</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the <code>|</code> operator
     * </p>
     *
     * @since SWF 5
     */
    public class BitOr : ActionRecord
    {
        /** 
         * Creates a new BitOr action.
         */
        public BitOr() 
        {
            code = ActionConstants.BIT_OR;
        }
        
        public override String ToString()
        {
            return "BitOr";
        }
    }
}