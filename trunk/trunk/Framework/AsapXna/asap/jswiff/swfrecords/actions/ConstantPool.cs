using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Creates a new constant pool. The constants defined here can be referenced in
     * <code>Push</code> as <code>constant8</code> if there are less than 256
     * constants in the pool, otherwise as <code>constant16</code>.
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: none
     * </p>
     * 
     * @since SWF 5
     */
    public class ConstantPool : ActionRecord
    {
        private List<String>  constants = new List<String> ();
        
        /** 
         * Creates a new ConstantPool action.
         */
        public ConstantPool() 
        {
            code = ActionConstants.CONSTANT_POOL;
        }
        
        public ConstantPool(InputBitStream stream) /* throws IOException */ 
        {
            code = ActionConstants.CONSTANT_POOL;
            int count = stream.ReadUI16();
            if (count > 0) 
            {
                for (int i = 0; i < count; i++) 
                {
                    constants.Add(stream.ReadString());
                }
            } 
        }
        
        public virtual List<String>  GetConstants()
        {
            return constants;
        }
        
        public override int GetSize()
        {
            int size = 5;            
            foreach (String constant in constants) 
            {
                size += (System.Text.Encoding.UTF8.GetBytes(constant).Length) + 1;
            }            
            return size;
        }
        
        public override String ToString()
        {
            return ("ConstantPool (" + (constants.Count)) + " constants)";
        }        
    }
}