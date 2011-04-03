using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Tests two items for equality. Unlike <code>Equals</code>,
     * <code>Equals2</code> takes account of data types.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop item1</code> (first item)<br>
     * <code>pop item2</code> (second item)<br>
     * <code>push [item2 == item1]</code> (<code>true</code> if equal, else
     * <code>false</code>)
     * </p>
     * 
     * <p>
     * ActionScript equivalents: <code>==</code> operator
     * </p>
     *
     * @since SWF 5
     */
    public class Equals2 : ActionRecord
    {
        /** 
         * Creates a new Equals2 actions.
         */
        public Equals2() 
        {
            code = ActionConstants.EQUALS_2;
        }
        
        public override String ToString()
        {
            return "Equals2";
        }
    }
}