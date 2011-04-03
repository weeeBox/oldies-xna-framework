using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Executes the script attached to a specified frame. The argument can be a
     * frame number or a frame label.<br>
     * This action is deprecated since SWF 5. Use <code>CallFunction</code> where
     * possible.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br><code>pop frame</code> (number or label)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: the <code>&&</code> operator
     * </p>
     *
     * @since SWF 4
     */
    public class Call : ActionRecord
    {
        /** 
         * Creates a new Call action.
         */
        public Call() 
        {
            code = ActionConstants.CALL;
        }
        
        public override int GetSize()
        {
            return 3;
        }
        
        public override String ToString()
        {
            return "Call";
        }        
    }
}