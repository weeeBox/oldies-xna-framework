using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Iterates over all properties of an object and pushes their names to the
     * stack. The difference to <code>Enumerate</code> is that
     * <code>Enumerate2</code> uses a stack argument of object type rather than
     * the object's name as string.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop obj</code> (object instance)<br>
     * <code>push null</code> (indicates the end of the property list)<br>
     * <code>push prop1</code> (1st property name)<br>
     * <code>push prop2</code> (2nd property name)<br>
     * <code>...</code><br>
     * <code>push propn</code> (n-th property name)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>for..in</code> loop
     * </p>
     *
     * @since SWF 6
     */
    public class Enumerate2 : ActionRecord
    {
        /** 
         * Creates a new Enumerate2 action.
         */
        public Enumerate2() 
        {
            code = ActionConstants.ENUMERATE_2;
        }
        
        public override String ToString()
        {
            return "Enumerate2";
        }
    }
}