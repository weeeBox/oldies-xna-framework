using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used by <code>DefineFunction2</code> to specify the function's
     * parameters. These can be either variables or registers.
     */
    public class RegisterParam
    {
        private short register;
        
        private String paramName;
        
        /** 
         * Creates a new RegisterParam instance. If you use 0 as register number, the
         * parameter can be referenced as a variable within the function (this
         * variable's name is contained in <code>paramName</code>). If the register
         * number is greater than 0, the parameter is copied into the corresponding
         * register.
         *
         * @param register register number
         * @param paramName variable name
         */
        public RegisterParam(short register ,String paramName) 
        {
            this.register = register;
            this.paramName = paramName;
        }
        
        /** 
         * Reads an instance from a bit stream.
         *
         * @param stream source bit stream
         *
         * @throws IOException if an I/O error has occured
         */
        public RegisterParam(InputBitStream stream) /* throws IOException */ 
        {
            register = stream.ReadUI8();
            paramName = stream.ReadString();
        }
        
        public virtual String GetParamName()
        {
            return paramName;
        }
        
        public virtual short GetRegister()
        {
            return register;
        }
        
        public virtual int GetSize()
        {
            int size = 2;            
            size += System.Text.Encoding.UTF8.GetBytes(paramName).Length;            
            return size;
        }
    }
}