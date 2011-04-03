using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Swaps the top two items on the stack.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop item1</code><br>
     * <code>pop item2</code><br>
     * <code>push item1</code><br>
     * <code>push item2</code><br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: none
     * </p>
     *
     * @since SWF 5
     */
    public class StackSwap : ActionRecord
    {
        /** 
         * Creates a new StackSwap action.
         */
        public StackSwap() 
        {
            code = ActionConstants.STACK_SWAP;
        }
        
        public override String ToString()
        {
            return "StackSwap";
        }
    }
}