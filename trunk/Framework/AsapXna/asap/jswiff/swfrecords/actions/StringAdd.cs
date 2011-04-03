using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Concatenates two strings. Replaced by <code>Add2</code> as of SWF 5.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop str1<br>
     * pop str2<br>
     * push [str2 & str1]</code> (concatenation of str2 and str1)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>&</code> operator in SWF 4 (as of SWF 5, this
     * operator has a different meaning!)
     * </p>
     *
     * @since SWF 4
     */
    public class StringAdd : ActionRecord
    {
        /** 
         * Creates a new StringAdd action.
         */
        public StringAdd() 
        {
            code = ActionConstants.STRING_ADD;
        }
        
        public override String ToString()
        {
            return "StringAdd";
        }
    }
}