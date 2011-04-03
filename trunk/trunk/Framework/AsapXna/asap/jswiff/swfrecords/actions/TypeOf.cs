using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * This action returns the type of a specified item as a string. The possible
     * types are:
     * 
     * <ul>
     * <li>
     * "number"
     * </li>
     * <li>
     * "boolean"
     * </li>
     * <li>
     * "string"
     * </li>
     * <li>
     * "function"
     * </li>
     * <li>
     * "object"
     * </li>
     * <li>
     * "movieclip"
     * </li>
     * <li>
     * "null"
     * </li>
     * <li>
     * "undefined"
     * </li>
     * </ul>
     * </p>
     * 
     * <p>
     * Performed stack operations:<br><code>pop item<br> push type</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>typeof()</code>
     * </p>
     *
     * @since SWF 5
     */
    public class TypeOf : ActionRecord
    {
        /** 
         * Creates a new TypeOf action.
         */
        public TypeOf() 
        {
            code = ActionConstants.TYPE_OF;
        }
        
        public override String ToString()
        {
            return "TypeOf";
        }
    }
}