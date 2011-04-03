using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag removes a character with a given ID, thereby freeing memory from
     * Flash Player.
     */
    public class FreeCharacter : Tag
    {
        private int characterId;
        
        /** 
         * Creates a new FreeCharacter tag. Supply the ID of the character to be
         * removed.
         *
         * @param characterId character ID to be removed
         */
        public FreeCharacter(int characterId) 
        {
            code = TagConstants.FREE_CHARACTER;
            this.characterId = characterId;
        }
        
        public FreeCharacter() 
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
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
        }
    }
}