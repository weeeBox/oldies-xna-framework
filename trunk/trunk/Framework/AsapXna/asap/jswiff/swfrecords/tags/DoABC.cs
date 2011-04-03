using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    public class DoABC : Tag
    {
        private long flags;
        
        private String name;
        
        private byte[] ABCdata;
        
        public DoABC() 
        {
            code = TagConstants.DO_ABC;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            flags = inStream.ReadUI32();
            name = inStream.ReadString();
            ABCdata = inStream.ReadBytes(inStream.Available());
        }
        
        public virtual long GetFlags()
        {
            return flags;
        }
        
        public virtual String GetName()
        {
            return name;
        }
        
        public virtual byte[] GetABCdata()
        {
            return ABCdata;
        }
    }
}