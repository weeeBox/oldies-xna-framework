using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * The RemoveObject2 tag removes the character instance at the specified depth
     * from the display list.
     *
     * @since SWF 3
     */
    public class RemoveObject2 : Tag
    {
        private int depth;
        
        /** 
         * Creates a new RemoveObject2 tag. Supply the depth of the character
         * instance to be removed.
         *
         * @param depth depth of instance to be removed
         */
        public RemoveObject2(int depth) 
        {
            code = TagConstants.REMOVE_OBJECT_2;
            this.depth = depth;
        }
        
        public RemoveObject2() 
        {
        }
        
        public virtual void SetDepth(int depth)
        {
            this.depth = depth;
        }
        
        public virtual int GetDepth()
        {
            return depth;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            depth = inStream.ReadUI16();
        }
    }
}