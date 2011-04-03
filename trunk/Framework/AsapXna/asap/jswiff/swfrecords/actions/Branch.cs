using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * Base class for branch actions (<code>Jump</code>, <code>If</code>).
     */
    abstract public class Branch : ActionRecord
    {
        public abstract String GetBranchLabel();
        
        public abstract void SetBranchLabel(String branchLabel);
        
        public abstract void SetBranchOffset(short branchOffset);
        
        public abstract short GetBranchOffset();
    }
}