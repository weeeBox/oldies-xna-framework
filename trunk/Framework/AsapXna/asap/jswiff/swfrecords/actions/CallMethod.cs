using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Invokes a method and pushes its result to the stack.
     * </p>
     * 
     * <p>
     * Performed stack operations (e.g. for a method <code>myObject.myMethod(arg1,
     * arg2, ..., argn))</code>:<br>
     * <code>pop instance</code> (the instance this method is invoked on, e.g. <code>myObject</code>)<br>
     * <code>pop name</code> (the method name, e.g. <code>"myMethod"</code>)<br>
     * <code>pop n</code> (the number of arguments as an integer)<br>
     * <code>pop arg1</code> (first argument)<br>
     * <code>pop arg2</code> (second argument)<br>
     * <code>...<br>
     * pop argn</code> (nth argument)<br>
     * <code> push result<br>
     * </code> The instance the method is invoked on has to be pushed to the stack
     * before method invocation e.g. like this:<br>
     * <code>push &quot;myObject&quot;<br>
     * GetVariable<br>
     * </code> If the method has no result, <code>undefined</code> is pushed. In
     * this case, use <code>Pop</code> to discard it.
     * </p>
     * 
     * <p>
     * ActionScript equivalent: method invocation using dot syntax
     * </p>
     *
     * @since SWF 5
     */
    public class CallMethod : ActionRecord
    {
        /** 
         * Creates a new CallMethod action.
         */
        public CallMethod() 
        {
            code = ActionConstants.CALL_METHOD;
        }
        
        public override String ToString()
        {
            return "CallMethod";
        }
    }
}