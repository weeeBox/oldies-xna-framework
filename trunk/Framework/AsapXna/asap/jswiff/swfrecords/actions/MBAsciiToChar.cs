using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Converts an ASCII character code to a multibyte character. Deprecated as of
     * SWF 5, use String.fromCharCode() where possible.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop number<br>
     * push char</code> (Character with an ASCII code of <code>number</code>)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>mbchr()</code>
     * </p>
     *
     * @since SWF 4
     */
    public class MBAsciiToChar : ActionRecord
    {
        /** 
         * Creates a new MBAsciiToChar action.
         */
        public MBAsciiToChar() 
        {
            code = ActionConstants.M_B_ASCII_TO_CHAR;
        }
        
        public override String ToString()
        {
            return "MBAsciiToChar";
        }
    }
}