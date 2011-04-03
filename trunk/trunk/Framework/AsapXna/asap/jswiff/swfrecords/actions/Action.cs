using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * This is the base class for action records.
     */
    abstract public class ActionRecord
    {
        public short code;
        
        private int offset;
        
        private String label;
        
        public virtual short GetCode()
        {
            return code;
        }               
        
        public virtual void SetLabel(String label)
        {
            this.label = label;
        }
        
        public virtual String GetLabel()
        {
            return label;
        }
        
        public virtual int GetOffset()
        {
            return offset;
        }

        public virtual void SetOffset(int offset)
        {
            this.offset = offset;
        }
        
        public virtual int GetSize()
        {
            return 1;
        }        
    }
}