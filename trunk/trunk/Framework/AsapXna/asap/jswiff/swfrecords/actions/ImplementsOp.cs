using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * This action specifies the interfaces a class implements.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop constructor</code> (the constructor of the new class)<br>
     * <code>pop n</code> (number of interfaces this class implements)<br>
     * <code>pop i1</code> (1st interface)<br>
     * <code>pop i2</code> (2nd interface)<br>
     * <code>...<br>
     * pop in</code> (n-th interface)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>implements</code> keyword
     * </p>
     *
     * @since SWF 7
     */
    public class ImplementsOp : ActionRecord
    {
        /** 
         * Creates a new ImplementsOp action.
         */
        public ImplementsOp() 
        {
            code = ActionConstants.IMPLEMENTS_OP;
        }
        
        public override String ToString()
        {
            return "ImplementsOp";
        }
    }
}