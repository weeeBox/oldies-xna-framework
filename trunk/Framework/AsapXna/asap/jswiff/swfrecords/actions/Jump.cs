using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Creates an unconditional branch. The execution continues at the action with
     * the specified label.
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: none (used internally)
     * </p>
     *
     * @see ActionBlock
     * @since SWF 4
     */
    public class Jump : Branch
    {
        private short branchOffset;
        
        private String branchLabel;
        
        /** 
         * Creates a new Jump action.<br>
         * The <code>branchLabel</code> parameter specifies the target of the
         * branch. This label must be identical to the one assigned to the action
         * record the execution is supposed to continue at. Assign
         * <code>ActionBlock.LABEL_END</code> in order to jump to the end of the
         * action block.
         *
         * @param branchLabel label of the action the execution is supposed to
         *        continue at
         */
        public Jump(String branchLabel) 
        {
            code = ActionConstants.JUMP;
            this.branchLabel = branchLabel;
        }
        
        public Jump(short branchOffset) 
        {
            code = ActionConstants.JUMP;
            this.branchOffset = branchOffset;
        }
        
        public Jump(InputBitStream stream) /* throws IOException */ 
        {
            code = ActionConstants.JUMP;
            branchOffset = stream.ReadSI16();
        }
        
        public override String GetBranchLabel()
        {
            return branchLabel;
        }
        
        public override short GetBranchOffset()
        {
            return branchOffset;
        }
        
        public override int GetSize()
        {
            return 5;
        }
        
        public override String ToString()
        {
            return "Jump branchLabel: " + (branchLabel);
        }
        
        public override void SetBranchLabel(String branchLabel)
        {
            this.branchLabel = branchLabel;
        }
        
        public override void SetBranchOffset(short branchOffset)
        {
            this.branchOffset = branchOffset;
        }
    }
}