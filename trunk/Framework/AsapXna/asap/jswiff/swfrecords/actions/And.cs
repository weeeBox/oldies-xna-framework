using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Performs a boolean AND (<code>&&</code>) operation.
     * </p>
     * 
     * <p>
     * Note: Before SWF 5, 1 was used instead of true and 0 instead of false.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop b<br>
     * pop a<br>
     * push [a && b]</code> (<code>true</code> (with SWF 4: 1) if both
     * <code>a</code> and <code>b</code> are <code>true</code> (with SWF 4: 1))
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the <code>&&</code> operator
     * </p>
     *
     * @since SWF 4
     */
    public class And : ActionRecord
    {
        /** 
         * Creates a new And action.
         */
        public And() 
        {
            code = ActionConstants.AND;
        }
        
        public override String ToString()
        {
            return "And";
        }
    }
}