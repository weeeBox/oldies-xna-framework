using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * This action defines a function.
     * </p>
     * 
     * <p>
     * Note: DefineFunction is rarely used as of SWF 7 and later; it has been
     * superseded by DefineFunction2.
     * </p>
     * 
     * <p>
     * Performed stack operations:
     * 
     * <ul>
     * <li>
     * standard function declarations do not touch the stack
     * </li>
     * <li>
     * when a function name is not specified, it is assumed that a function literal
     * (anonymous function) is declared. In this case, the declared function is
     * pushed to the stack so it can either be assigned or invoked:<br>
     * </li>
     * </ul>
     * </p>
     * 
     * <p>
     * Note: Use <code>Return</code> to declare the function's result. Otherwise
     * the function has no result, and <code>undefined</code> is pushed to stack
     * upon invocation.
     * </p>
     * 
     * <p>
     * ActionScript equivalents:
     * 
     * <ul>
     * <li>
     * standard function declaration, e.g.<br>
     * <code>myFunction(x) {<br>
     * return (x + 3);<br>}</code><br>
     * </li>
     * <li>
     * anonymous function declaration, e.g.<br>
     * <code>function (x) { x + 3 };</code><br>
     * </li>
     * <li>
     * anonymous function invocation, e.g.<br>
     * <code>function (x) { x + 3 } (1);</code><br>
     * </li>
     * <li>
     * method declaration
     * </li>
     * </ul>
     * </p>
     *
     * @see DefineFunction2
     * @since SWF 5
     */
    public class DefineFunction : ActionRecord
    {
        private String name;
        
        private String[] parameters;
        
        private ActionBlock body;
        
        /** 
         * Creates a new DefineFunction action. Use the empty string ("") as function
         * name for anonymous functions.
         *
         * @param functionName name of the function
         * @param parameters array of parameter names
         */
        public DefineFunction(String functionName ,String[] parameters) 
        {
            code = ActionConstants.DEFINE_FUNCTION;
            this.name = functionName;
            this.parameters = parameters;
            body = new ActionBlock();
        }
        
        public DefineFunction(InputBitStream stream ,InputBitStream mainStream) /* throws IOException */ 
        {
            code = ActionConstants.DEFINE_FUNCTION;
            name = stream.ReadString();
            int numParams = stream.ReadUI16();
            if (numParams >= 0) 
            {
                parameters = new String[numParams];
                for (int i = 0; i < numParams; i++) 
                {
                    parameters[i] = stream.ReadString();
                }
            } 
            int codeSize = stream.ReadUI16();
            byte[] blockBuffer = mainStream.ReadBytes(codeSize);
            InputBitStream blockStream = new InputBitStream(blockBuffer);
            blockStream.SetANSI(stream.IsANSI());
            blockStream.SetShiftJIS(stream.IsShiftJIS());
            body = new ActionBlock(blockStream);
        }
        
        public virtual ActionBlock GetBody()
        {
            return body;
        }
        
        public virtual String GetName()
        {
            return name;
        }
        
        public virtual String[] GetParameters()
        {
            return parameters;
        }
        
        public override int GetSize()
        {
            int size = 8 + (body.GetSize());            
            int paramLength = (parameters) == null ? 0 : parameters.Length;
            size += (System.Text.Encoding.UTF8.GetBytes(name).Length) + paramLength;
            for (int i = 0; i < paramLength; i++) 
            {
                size += System.Text.Encoding.UTF8.GetBytes(parameters[i]).Length;
            }            
            return size;
        }
        
        public virtual void AddAction(ActionRecord action)
        {
            body.AddAction(action);
        }
        
        public override String ToString()
        {
            return "DefineFunction " + (name);
        }        
    }
}