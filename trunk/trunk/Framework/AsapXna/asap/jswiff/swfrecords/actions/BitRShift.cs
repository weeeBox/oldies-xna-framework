using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Performs a bitwise right shift (<code>&gt;&gt;</code>). The value argument
     * is interpreted as a 32-bit integer (signed integer for a negative number,
     * otherwise unsigned integer). The shift count is treated as an integer from
     * 0 to 31 (only lower 5 bits are considered). The result is interpreted as
     * signed 32-bit integer.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop count<br>
     * pop value<br>
     * push [value &gt;&gt; count]</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the <code>&gt;&gt;</code> operator
     * </p>
     *
     * @since SWF 5
     */
    public class BitRShift : ActionRecord
    {
        /** 
         * Creates a new BitRShift action.
         */
        public BitRShift() 
        {
            code = ActionConstants.BIT_R_SHIFT;
        }
        
        public override String ToString()
        {
            return "BitRShift";
        }
    }
}