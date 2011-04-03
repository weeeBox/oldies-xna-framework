using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Converts a character to its ASCII code. Deprecated, use String.charCodeAt()
     * where possible.
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
    public class CharToAscii : ActionRecord
    {
        /** 
         * Creates a new CharToAscii action.
         */
        public CharToAscii() 
        {
            code = ActionConstants.CHAR_TO_ASCII;
        }
        
        public override String ToString()
        {
            return "CharToAscii";
        }
    }
}