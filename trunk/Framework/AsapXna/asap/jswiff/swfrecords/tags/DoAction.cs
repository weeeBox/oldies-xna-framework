using System;

using System.Collections.Generic;


using swiff.com.jswiff.swfrecords.actions;
using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag instructs Flash Player to execute a series of actions when
     * encountering the next <code>ShowFrame</code> tag, after all drawing for the
     * current frame has completed.
     *
     * @since SWF 3
     */
    public class DoAction : Tag
    {
        private ActionBlock actions;
        
        /** 
         * Creates a new DoAction instance.
         */
        public DoAction() 
        {
            code = TagConstants.DO_ACTION;
        }
        
        public virtual ActionBlock GetActions()
        {
            if ((actions) == null) 
            {
                actions = new ActionBlock();
            } 
            return actions;
        }
        
        public virtual void AddAction(ActionRecord action)
        {
            GetActions().AddAction(action);
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            if ((GetSWFVersion()) < 6) 
            {
                if (IsJapanese()) 
                {
                    inStream.SetShiftJIS(true);
                } 
                else 
                {
                    inStream.SetANSI(true);
                }
            } 
            actions = new ActionBlock(inStream);
        }
    }
}