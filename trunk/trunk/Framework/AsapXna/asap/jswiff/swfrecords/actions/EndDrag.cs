using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Ends the drag operation in progress, if any.
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalents: <code>stopDrag()</code>,
     * <code>MovieClip.stopDrag()</code>
     * </p>
     *
     * @since SWF 5
     */
    public class EndDrag : ActionRecord
    {
        /** 
         * Creates a new EndDrag action.
         */
        public EndDrag() 
        {
            code = ActionConstants.END_DRAG;
        }
        
        public override String ToString()
        {
            return "EndDrag";
        }
    }
}