using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Deletes an object's property. Can be used to free memory. After deletion,
     * the property has the value <code>undefined</code>.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop ref</code> (reference to object to be altered)<br>
     * <code>pop prop</code> (property to be deleted)<br>
     * <code>push success</code> (<code>true</code> if the operation succeeded,
     * otherwise <code>false</code>)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the delete operator (e.g. <code>delete
     * ref.prop;</code>)
     * </p>
     *
     * @since SWF 5
     */
    public class Delete : ActionRecord
    {
        /** 
         * Creates a new Delete action.
         */
        public Delete() 
        {
            code = ActionConstants.DELETE;
        }
        
        public override String ToString()
        {
            return "Delete";
        }
    }
}