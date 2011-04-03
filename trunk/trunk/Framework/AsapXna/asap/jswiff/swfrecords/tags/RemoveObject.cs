using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * The RemoveObject tag removes the instance of a particular character at the
     * specified depth from the display list.
     *
     * @since SWF 1
     */
    public class RemoveObject : Tag
    {
        private int characterId;
        
        private int depth;
        
        /** 
         * Creates a new RemoveObject tag. Specify character ID and depth of the
         * instance to be removed.
         *
         * @param characterId character ID of instance to remove
         * @param depth depth of instance to remove
         */
        public RemoveObject(int characterId ,int depth) 
        {
            code = TagConstants.REMOVE_OBJECT;
            this.characterId = characterId;
            this.depth = depth;
        }
        
        public RemoveObject() 
        {
        }
        
        public virtual void SetCharacterId(int characterId)
        {
            this.characterId = characterId;
        }
        
        public virtual int GetCharacterId()
        {
            return characterId;
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
            characterId = inStream.ReadUI16();
            depth = inStream.ReadUI16();
        }
    }
}