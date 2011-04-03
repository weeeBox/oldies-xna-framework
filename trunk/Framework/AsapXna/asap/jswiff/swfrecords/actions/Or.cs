using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Performs a boolean OR (<code>||</code>) operation.
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
     * push [a || b]</code> (<code>true</code> (with SWF 4: 1) if either
     * <code>a</code> or <code>b</code> is <code>true</code> (1) )
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the <code>||</code> operator
     * </p>
     *
     * @since SWF 4
     */
    public class Or : ActionRecord
    {
        /** 
         * Creates a new Or action.
         */
        public Or() 
        {
            code = ActionConstants.OR;
        }
        
        public override String ToString()
        {
            return "Or";
        }
    }
}