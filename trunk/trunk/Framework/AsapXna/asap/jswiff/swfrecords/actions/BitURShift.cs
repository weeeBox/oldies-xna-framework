using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Performs a bitwise unsigned right shift (<code>&gt;&gt;&gt;</code>). For
     * negative numbers, this operation doesn't preserve the sign, as the bits on
     * the left are filled with 0. The value argument is interpreted as 32-bit
     * integer (signed integer for a negative number, otherwise unsigned integer).
     * The shift count is treated as an integer from 0 to 31 (only lower 5 bits
     * are considered). The result is interpreted as unsigned 32-bit integer.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop count<br>
     * pop value<br>
     * push [value &gt;&gt;&gt; count]</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the <code>&gt;&gt;&gt;</code> operator
     * </p>
     *
     * @since SWF 5
     */
    public class BitURShift : ActionRecord
    {
        /** 
         * Creates a new BitURShift action.
         */
        public BitURShift() 
        {
            code = ActionConstants.BIT_U_R_SHIFT;
        }
        
        public override String ToString()
        {
            return "BitURShift";
        }
    }
}