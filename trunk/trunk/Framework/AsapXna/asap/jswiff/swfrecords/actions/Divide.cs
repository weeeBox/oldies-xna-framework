using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Divides two numbers.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br><code>pop b<br> pop a<br> push [a / b]</code>
     * </p>
     * 
     * <p>
     * Note: in SWF 5 and later, if <code>a</code> or <code>b</code> are not (or
     * cannot be converted to) floating point numbers, the result is
     * <code>NaN</code> (or <code>Double.NaN</code>); if <code>b</code> is 0, the
     * result is <code>Infinity</code> or <code>-Infinity</code>
     * (<code>Double.POSITIVE_INFINITY</code> or
     * <code>Double.NEGATIVE_INFINITY</code>), depending on <code>a</code>'s sign.
     * Before SWF 5, these results were not IEEE-754 compliant.
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the <code>/</code> operator
     * </p>
     *
     * @since SWF 4
     */
    public class Divide : ActionRecord
    {
        /** 
         * Creates a new Divide action.
         */
        public Divide() 
        {
            code = ActionConstants.DIVIDE;
        }
        
        public override String ToString()
        {
            return "Divide";
        }
    }
}