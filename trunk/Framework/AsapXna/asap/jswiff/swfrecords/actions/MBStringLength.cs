using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Computes the length of a string, multibyte-aware. Deprecated as of SWF 5,
     * use String.length instead.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop string</code><br>
     * <code>push length</code> (length of <code>string</code>)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>length()</code>
     * </p>
     *
     * @since SWF 4
     */
    public class MBStringLength : ActionRecord
    {
        /** 
         * Creates a new MBStringLength action.
         */
        public MBStringLength() 
        {
            code = ActionConstants.M_B_STRING_LENGTH;
        }
        
        public override String ToString()
        {
            return "MBSrtingLength";
        }
    }
}