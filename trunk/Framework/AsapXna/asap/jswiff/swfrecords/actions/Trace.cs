using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Sends a debugging output string to the Output panel of the Macromedia Flash
     * authoring environment (in test mode).
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop message</code> (message to be displayed)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>trace()</code>
     * </p>
     *
     * @since SWF 5
     */
    public class Trace : ActionRecord
    {
        /** 
         * Creates a new Trace action.
         */
        public Trace() 
        {
            code = ActionConstants.TRACE;
        }
        
        public override String ToString()
        {
            return "Trace";
        }
    }
}