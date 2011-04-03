using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Converts an ASCII character code to a character. Deprecated as of SWF 5, use
     * String.fromCharCode() where possible.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop code<br>
     * push char</code> (Character with the ASCII code <code>code</code>)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>chr()</code>
     * </p>
     *
     * @since SWF 4
     */
    public class AsciiToChar : ActionRecord
    {
        /** 
         * Creates a new AsciiToChar action.
         */
        public AsciiToChar() 
        {
            code = ActionConstants.ASCII_TO_CHAR;
        }
        
        public override String ToString()
        {
            return "AsciiToChar";
        }
    }
}