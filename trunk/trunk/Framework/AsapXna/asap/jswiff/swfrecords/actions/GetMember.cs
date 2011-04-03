using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Retrieves a member value from an object. Can be either a property or a
     * method.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop name</code> (the member's name)<br>
     * <code>pop ref</code> (reference to the object to be accessed)<br>
     * <code>push value</code> (the value of the member)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalents:<br>
     * 
     * <ul>
     * <li>
     * member access (with or without dot operator), e.g. <code>speed</code> or
     * <code>car.speed</code>
     * </li>
     * <li>
     * internally used by the AS compiler to implement various constructs
     * </li>
     * </ul>
     * </p>
     *
     * @since SWF 5
     */
    public class GetMember : ActionRecord
    {
        /** 
         * Creates a new GetMember action.
         */
        public GetMember() 
        {
            code = ActionConstants.GET_MEMBER;
        }
        
        public override String ToString()
        {
            return "GetMember";
        }
    }
}