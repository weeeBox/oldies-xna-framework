using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Performs a bitwise left shift (<code>&lt;&lt;</code>). The value argument is
     * interpreted as a 32-bit integer (signed integer for a negative number,
     * otherwise unsigned integer). The shift count is treated as an integer from
     * 0 to 31 (only lower 5 bits are considered). The result is interpreted as
     * signed 32-bit integer.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop b<br>
     * pop a<br>
     * push [a &lt;&lt; b]</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the <code>&lt;&lt;</code> operator
     * </p>
     *
     * @since SWF 5
     */
    public class BitLShift : ActionRecord
    {
        /** 
         * Creates a new BitLShift action.
         */
        public BitLShift() 
        {
            code = ActionConstants.BIT_L_SHIFT;
        }
        
        public override String ToString()
        {
            return "BitLShift";
        }
    }
}