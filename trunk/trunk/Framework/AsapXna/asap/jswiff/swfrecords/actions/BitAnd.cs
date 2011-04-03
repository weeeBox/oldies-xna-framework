using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Performs a bitwise AND (<code>&amp;</code>). The arguments are treated as
     * 32-bit integers (signed integers for negative numbers, otherwise unsigned
     * integers). The result is interpreted as a signed 32-bit integer.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop b<br>
     * pop a<br>
     * push [a &amp; b]</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the <code>&amp;</code> operator
     * </p>
     *
     * @since SWF 5
     */
    public class BitAnd : ActionRecord
    {
        /** 
         * Creates a new BitAnd action.
         */
        public BitAnd() 
        {
            code = ActionConstants.BIT_AND;
        }
        
        public override String ToString()
        {
            return "BitAnd";
        }
    }
}