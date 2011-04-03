using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Defines a <code>with</code> action block which lets you specify an object in
     * order to access it's members without having  to repeatedly write the
     * object's name or it's path.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br><code>pop object</code>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>with</code> statement
     * </p>
     *
     * @since SWF 5
     */
    public class With : ActionRecord
    {
        private ActionBlock actionBlock;
        
        /** 
         * Creates a new With action.
         */
        public With() 
        {
            code = ActionConstants.WITH;
            actionBlock = new ActionBlock();
        }
        
        public With(InputBitStream stream ,InputBitStream mainStream) /* throws IOException */ 
        {
            code = ActionConstants.WITH;
            int blockSize = stream.ReadUI16();
            byte[] blockBuffer = mainStream.ReadBytes(blockSize);
            InputBitStream blockStream = new InputBitStream(blockBuffer);
            blockStream.SetANSI(stream.IsANSI());
            blockStream.SetShiftJIS(stream.IsShiftJIS());
            actionBlock = new ActionBlock(blockStream);
        }
        
        public override int GetSize()
        {
            return (actionBlock.GetSize()) + 5;
        }
        
        public virtual ActionBlock GetWithBlock()
        {
            return actionBlock;
        }
        
        public override String ToString()
        {
            return "With";
        }
    }
}