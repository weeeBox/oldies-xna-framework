using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Reads the next item from the stack and stores it in the register with the
     * specified number. Up to 4 registers can be used (within
     * <code>DefineFunction2</code> up to 256).
     * </p>
     * 
     * <p>
     * Performed stack operations: none (item is read without being removed from
     * stack)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: none
     * </p>
     *
     * @since SWF 5
     */
    public class StoreRegister : ActionRecord
    {
        private short number;
        
        /** 
         * Creates a new StoreRegister action. Up to 4 registers can be used (within
         * <code>DefineFunction2</code> up to 256).
         *
         * @param number a register number.
         */
        public StoreRegister(short number) 
        {
            code = ActionConstants.STORE_REGISTER;
            this.number = number;
        }
        
        public StoreRegister(InputBitStream stream) /* throws IOException */ 
        {
            code = ActionConstants.STORE_REGISTER;
            number = stream.ReadUI8();
        }
        
        public virtual short GetNumber()
        {
            return number;
        }
        
        public override int GetSize()
        {
            return 4;
        }
        
        public override String ToString()
        {
            return "StoreRegister " + (number);
        }
    }
}