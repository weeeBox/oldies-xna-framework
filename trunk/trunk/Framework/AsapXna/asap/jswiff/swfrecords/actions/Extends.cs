using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Extends creates an inheritance relationship between two classes (instead of
     * classes, interfaces can also be used, since inheritance between interfaces
     * is also possible).
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop superClass</code> (the class to be inherited)<br>
     * <code>pop subClassConstructor</code> (the constructor of the new class)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>extends</code> keyword
     * </p>
     *
     * @since SWF 7
     */
    public class Extends : ActionRecord
    {
        /** 
         * Creates a new Extends action.
         */
        public Extends() 
        {
            code = ActionConstants.EXTENDS;
        }
        
        public override String ToString()
        {
            return "Extends";
        }
    }
}