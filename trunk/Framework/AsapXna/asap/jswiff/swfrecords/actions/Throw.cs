using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * This action is used to signal an exceptional condition which can be handled
     * by exception handlers declared with <code>Try</code>.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop errorObj</code> (the error object to be thrown)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>throw</code> statement
     * </p>
     *
     * @since SWF 7
     */
    public class Throw : ActionRecord
    {
        /** 
         * Creates a new Throw action.
         */
        public Throw() 
        {
            code = ActionConstants.THROW;
        }
        
        public override String ToString()
        {
            return "Throw";
        }
    }
}