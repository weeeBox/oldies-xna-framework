using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Creates a new object, invoking a constructor.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop className</code> (name of the class to be instantiated)<br>
     * <code>pop n</code> (number of parameters passed to constructor)<br>
     * <code>pop param1</code> (1st parameter)<br>
     * <code>pop param2</code> (2nd parameter)<br>
     * <code>...</code><br>
     * <code>pop paramn</code> (n-th parameter)<br>
     * <code>push obj</code> (the newly constructed object)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalents: constructor invocation, e.g. <code>new
     * Car("BMW");</code>
     * </p>
     *
     * @since SWF 5
     */
    public class NewObject : ActionRecord
    {
        /** 
         * Creates a new NewObject action.
         */
        public NewObject() 
        {
            code = ActionConstants.NEW_OBJECT;
        }
        
        public override String ToString()
        {
            return "NewObject";
        }
    }
}