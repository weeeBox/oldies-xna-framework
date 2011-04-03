using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Instructs Flash Player to change the context of subsequent actions, so they
     * apply to an object with the specified name. This action can be used e.g. to
     * control the timeline of a sprite object.
     * </p>
     * 
     * <p>
     * Note: as of SWF 5, this action is deprecated. Use <code>With</code> instead.
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>tellTarget()</code>
     * </p>
     *
     * @since SWF 3
     */
    public class SetTarget : ActionRecord
    {
        private String name;
        
        /** 
         * Creates a new SetTarget action. The target object's name is passed as a
         * string.
         *
         * @param name target object name
         */
        public SetTarget(String name) 
        {
            code = ActionConstants.SET_TARGET;
            this.name = name;
        }
        
        public SetTarget(InputBitStream stream) /* throws IOException */ 
        {
            code = ActionConstants.SET_TARGET;
            name = stream.ReadString();
        }
        
        public virtual String GetName()
        {
            return name;
        }
        
        public override int GetSize()
        {
            int size = 4;            
            size += System.Text.Encoding.UTF8.GetBytes(name).Length;            
            return size;
        }
        
        public override String ToString()
        {
            return "SetTarget " + (name);
        }
    }
}