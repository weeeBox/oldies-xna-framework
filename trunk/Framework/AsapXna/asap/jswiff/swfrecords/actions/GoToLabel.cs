using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Instructs Flash Player to go to a frame associated with the specified label.
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>gotoAndPlay(), gotoAndStop()</code> (no 1:1
     * equivalent available)
     * </p>
     *
     * @see com.jswiff.swfrecords.tags.FrameLabel
     * @since SWF 3
     */
    public class GoToLabel : ActionRecord
    {
        private String frameLabel;
        
        /** 
         * Creates a new GoToLabel action.
         *
         * @param frameLabel the label of the target frame
         */
        public GoToLabel(String frameLabel) 
        {
            code = ActionConstants.GO_TO_LABEL;
            this.frameLabel = frameLabel;
        }
        
        public GoToLabel(InputBitStream stream) /* throws IOException */ 
        {
            code = ActionConstants.GO_TO_LABEL;
            frameLabel = stream.ReadString();
        }
        
        public virtual String GetFrameLabel()
        {
            return frameLabel;
        }
        
        public override int GetSize()
        {
            int size = 4;            
            size += System.Text.Encoding.UTF8.GetBytes(frameLabel).Length;            
            return size;
        }
        
        public override String ToString()
        {
            return "GoToLabel " + (frameLabel);
        }
    }
}