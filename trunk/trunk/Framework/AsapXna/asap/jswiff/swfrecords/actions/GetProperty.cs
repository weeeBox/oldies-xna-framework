using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Returns the value of a movie property.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop property</code> (property index - see <code>MovieProperties</code>)<br>
     * <code>pop movie</code> (reference to the clip)<br>
     * <code>push value</code> (property value)
     * </p>
     * 
     * <p>
     * ActionScript equivalents: <code>getProperty()</code>
     * </p>
     *
     * @see MovieProperties
     * @since SWF 4
     */
    public class GetProperty : ActionRecord
    {
        /** 
         * Creates a new GetProperty action.
         */
        public GetProperty() 
        {
            code = ActionConstants.GET_PROPERTY;
        }
        
        public override String ToString()
        {
            return "GetProperty";
        }
    }
}