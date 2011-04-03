using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Sets the value of a movie property.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop value</code> (property value)<br>
     * <code>pop property</code> (property index - see <code>MovieProperties</code>)<br>
     * <code>pop movie</code> (reference to the clip)
     * </p>
     * 
     * <p>
     * ActionScript equivalents: <code>setProperty()</code>
     * </p>
     *
     * @see MovieProperties
     * @since SWF 4
     */
    public class SetProperty : ActionRecord
    {
        /** 
         * Creates a new SetProperty action.
         */
        public SetProperty() 
        {
            code = ActionConstants.SET_PROPERTY;
        }
        
        public override String ToString()
        {
            return "SetProperty";
        }
    }
}