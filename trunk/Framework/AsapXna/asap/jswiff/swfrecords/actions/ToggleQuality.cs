using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Toggles the display between high and low quality. Since SWF 5, this action
     * is deprecated. Use the global property <code>_quality</code> instead.
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>toggleHighQuality()</code>
     * </p>
     *
     * @since SWF 3
     */
    public class ToggleQuality : ActionRecord
    {
        /** 
         * Creates a new ToggleQuality action.
         */
        public ToggleQuality() 
        {
            code = ActionConstants.TOGGLE_QUALITY;
        }
        
        public override String ToString()
        {
            return "ToggleQuality";
        }
    }
}