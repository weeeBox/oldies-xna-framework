using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * This action is used to declare a function. It supersedes
     * <code>DefineFunction</code> since SWF 7.<br>
     * With DefineFunction2, a function may allocate its own private set of up to
     * 256 registers, which can be used as parameters or local variables.<br>
     * For performance improvement, you can specify if "common variables"
     * (<code>_parent, _root, super, arguments, this,</code> or
     * <code>_global</code>) are supposed to be preloaded into registers before
     * execution. Additionally, the Flash Player can be instructed to suppress
     * unused variables. (Only <code>super, arguments</code> and <code>this</code>
     * can be suppressed. Naturally, you can either preload or suppress a
     * variable, not both).
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
     * @see DefineFunction
     * @since SWF 7
     */
    public class DefineFunction2 : ActionRecord
    {
        private String name;
        
        private short registerCount;
        
        private bool preloadParent;
        
        private bool preloadRoot;
        
        private bool suppressSuper;
        
        private bool preloadSuper;
        
        private bool suppressArguments;
        
        private bool preloadArguments;
        
        private bool suppressThis;
        
        private bool preloadThis;
        
        private bool preloadGlobal;
        
        private RegisterParam[] parameters;
        
        private ActionBlock body;
        
        /** 
         * Creates a new DefineFunction2 action. Use the empty string ("") as
         * function name for anonymous functions.
         *
         * @param name name of the function
         * @param registerCount number of used registers
         * @param parameters the function's parameters
         */
        public DefineFunction2(String name ,short registerCount ,RegisterParam[] parameters) 
        {
            code = ActionConstants.DEFINE_FUNCTION_2;
            this.name = name;
            this.registerCount = registerCount;
            this.parameters = parameters;
            body = new ActionBlock();
        }
        
        public DefineFunction2(InputBitStream stream ,InputBitStream mainStream) /* throws IOException */ 
        {
            code = ActionConstants.DEFINE_FUNCTION_2;
            name = stream.ReadString();
            int numParams = stream.ReadUI16();
            registerCount = stream.ReadUI8();
            preloadParent = stream.ReadBooleanBit();
            preloadRoot = stream.ReadBooleanBit();
            suppressSuper = stream.ReadBooleanBit();
            preloadSuper = stream.ReadBooleanBit();
            suppressArguments = stream.ReadBooleanBit();
            preloadArguments = stream.ReadBooleanBit();
            suppressThis = stream.ReadBooleanBit();
            preloadThis = stream.ReadBooleanBit();
            preloadGlobal = ((stream.ReadUI8()) & 1) != 0;
            parameters = new RegisterParam[numParams];
            for (int i = 0; i < numParams; i++) 
            {
                parameters[i] = new RegisterParam(stream);
            }
            int codeSize = stream.ReadUI16();
            byte[] blockBuffer = mainStream.ReadBytes(codeSize);
            InputBitStream blockStream = new InputBitStream(blockBuffer);
            blockStream.SetANSI(mainStream.IsANSI());
            blockStream.SetShiftJIS(mainStream.IsShiftJIS());
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
        
        public virtual RegisterParam[] GetParameters()
        {
            return parameters;
        }
        
        public virtual short GetRegisterCount()
        {
            return registerCount;
        }
        
        public override int GetSize()
        {
            int size = 11 + (body.GetSize());            
            size += System.Text.Encoding.UTF8.GetBytes(name).Length;            
            
            if ((parameters) != null) 
            {
                for (int i = 0; i < (parameters.Length); i++) 
                {
                    size += parameters[i].GetSize();
                }
            } 
            return size;
        }
        
        public virtual void AddAction(ActionRecord action)
        {
            body.AddAction(action);
        }
        
        public virtual void PreloadArguments()
        {
            preloadArguments = true;
        }
        
        public virtual void PreloadGlobal()
        {
            preloadGlobal = true;
        }
        
        public virtual void PreloadParent()
        {
            preloadParent = true;
        }
        
        public virtual void PreloadRoot()
        {
            preloadRoot = true;
        }
        
        public virtual void PreloadSuper()
        {
            preloadSuper = true;
        }
        
        public virtual void PreloadThis()
        {
            preloadThis = true;
        }
        
        public virtual bool PreloadsArguments()
        {
            return preloadArguments;
        }
        
        public virtual bool PreloadsGlobal()
        {
            return preloadGlobal;
        }
        
        public virtual bool PreloadsParent()
        {
            return preloadParent;
        }
        
        public virtual bool PreloadsRoot()
        {
            return preloadRoot;
        }
        
        public virtual bool PreloadsSuper()
        {
            return preloadSuper;
        }
        
        public virtual bool PreloadsThis()
        {
            return preloadThis;
        }
        
        public virtual void SuppressArguments()
        {
            suppressArguments = true;
        }
        
        public virtual void SuppressSuper()
        {
            suppressSuper = true;
        }
        
        public virtual void SuppressThis()
        {
            suppressThis = true;
        }
        
        public virtual bool SuppressesArguments()
        {
            return suppressArguments;
        }
        
        public virtual bool SuppressesSuper()
        {
            return suppressSuper;
        }
        
        public virtual bool SuppressesThis()
        {
            return suppressThis;
        }
        
        public override String ToString()
        {
            return "DefineFunction2 " + (name);
        }        
    }
}