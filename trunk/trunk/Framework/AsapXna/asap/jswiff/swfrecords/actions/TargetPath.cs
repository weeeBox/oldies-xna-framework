using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * This action returns a string containing the target path of a clip in dot
     * notation.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop clip</code><br>
     * <code>push targetPath</code> (path in dot notation)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>targetPath()</code>
     * </p>
     *
     * @since SWF 5
     */
    public class TargetPath : ActionRecord
    {
        /** 
         * Creates a new TargetPath action.
         */
        public TargetPath() 
        {
            code = ActionConstants.TARGET_PATH;
        }
        
        public override String ToString()
        {
            return "TargetPath";
        }
    }
}