using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * This action allows casting from one data type to another.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop Type<br>
     * pop obj<br>
     * push [(Type)obj] </code> (object <code>obj</code> cast to type
     * <code>Type</code>)
     * </p>
     * 
     * <p>
     * Note: push <code>Type</code> this way to the stack:<br>
     * <code>push 'Type'<br>
     * GetVar </code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: type cast (<code>Type(obj)</code>)
     * </p>
     *
     * @since SWF 6
     */
    public class CastOp : ActionRecord
    {
        /** 
         * Creates a new CastOp action.
         */
        public CastOp() 
        {
            code = ActionConstants.CAST_OP;
        }
        
        public override String ToString()
        {
            return "CastOp";
        }
    }
}