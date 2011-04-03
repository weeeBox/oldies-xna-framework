using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Evaluates a condition to determine the next action in an SWF file. If the
     * condition is true, the execution continues at the action with the specified
     * label.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop cond</code> (the condition - must evaluate to 0 or 1, or as of SWF
     * 5 to <code>false</code> or <code>true</code>)
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>if</code> statement
     * </p>
     *
     * @see ActionBlock
     * @since SWF 4
     */
    public class If : Branch
    {
        private short branchOffset;
        
        private String branchLabel;
        
        /** 
         * Creates a new If action.<br>
         * The <code>branchLabel</code> parameter specifies the target of the jump
         * in case the condition is fulfilled. This label must be identical to the
         * one assigned to the action record the execution is supposed to continue
         * at. Assign <code>ActionBlock.LABEL_END</code> in order to jump to the end
         * of the action block.
         *
         * @param branchLabel label of the action the execution is supposed to
         *        continue at
         */
        public If(String branchLabel) 
        {
            code = ActionConstants.IF;
            this.branchLabel = branchLabel;
        }
        
        public If(InputBitStream stream) /* throws IOException */ 
        {
            code = ActionConstants.IF;
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
            return "If branchLabel: " + (branchLabel);
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