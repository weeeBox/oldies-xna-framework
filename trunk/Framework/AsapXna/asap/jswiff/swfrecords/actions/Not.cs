using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Performs a boolean NOT (<code>!</code>) operation.
     * </p>
     * 
     * <p>
     * Note: Before SWF 5, 1 was used instead of true and 0 instead of false.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop value</code><br>
     * <code>push result</code> (1 if value is 0, otherwise 0. SWF 5 and newer:
     * <code>true</code> if <code>value</code> is <code>true</code>, otherwise
     * <code>false</code>)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>!</code> operator
     * </p>
     *
     * @since SWF 4
     */
    public class Not : ActionRecord
    {
        /** 
         * Creates a new Not action.
         */
        public Not() 
        {
            code = ActionConstants.NOT;
        }
        
        public override String ToString()
        {
            return "Not";
        }
    }
}