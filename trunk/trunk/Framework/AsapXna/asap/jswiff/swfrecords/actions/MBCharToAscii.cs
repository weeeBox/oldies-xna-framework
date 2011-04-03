using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Converts a multibyte character to its ASCII code. Deprecated as of SWF 5,
     * use String.charCodeAt() where possible.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop char<br>
     * push code</code> (ASCII code of <code>char</code>)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>ord()</code>
     * </p>
     *
     * @since SWF 4
     */
    public class MBCharToAscii : ActionRecord
    {
        /** 
         * Creates a new MBCharToAscii action.
         */
        public MBCharToAscii() 
        {
            code = ActionConstants.M_B_CHAR_TO_ASCII;
        }
        
        public override String ToString()
        {
            return "MBCharToAscii";
        }
    }
}