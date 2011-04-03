using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Calculates a random non-negative integer less than a specified maximum
     * number. Deprecated since SWF 5, use <code>Math.random()</code> instead.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop max</code><br>
     * <code>push rand</code> (random number in the range [0, max) )
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>random()</code>
     * </p>
     *
     * @since SWF 4
     */
    public class RandomNumber : ActionRecord
    {
        /** 
         * Creates a new RandomNumber action.
         */
        public RandomNumber() 
        {
            code = ActionConstants.RANDOM_NUMBER;
        }
        
        public override String ToString()
        {
            return "RandomNumber";
        }
    }
}