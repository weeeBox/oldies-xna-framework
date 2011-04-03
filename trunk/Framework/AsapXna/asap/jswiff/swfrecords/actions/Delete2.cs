using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Destroys an object reference. Can be used for freeing memory. After deleting
     * the reference, an internal reference counter is decremented. When the
     * counter of an object has reached zero, Flash Player will mark that object
     * for garbage collection.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop ref</code> (reference to be deleted)<br>
     * <code>push success</code> (<code>true</code> if the operation succeeded,
     * otherwise <code>false</code>)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the delete operator (e.g. <code>delete ref;</code>)
     * </p>
     *
     * @since SWF 5
     */
    public class Delete2 : ActionRecord
    {
        /** 
         * Creates a new Delete2 action.
         */
        public Delete2() 
        {
            code = ActionConstants.DELETE_2;
        }
        
        public override String ToString()
        {
            return "Delete2";
        }
    }
}