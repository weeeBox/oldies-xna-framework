using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * @since SWF 8
     */
    public class Scale9Grid : Tag
    {
        private int characterId;
        
        private Rect grid;
        
        /** 
         * Creates a new Scale9Grid instance.
         *
         * @param characterId TODO: Comments
         * @param grid TODO: Comments
         */
        public Scale9Grid(int characterId ,Rect grid) 
        {
            code = TagConstants.SCALE_9_GRID;
            this.characterId = characterId;
            this.grid = grid;
        }
        
        public Scale9Grid() 
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
        
        public virtual void SetGrid(Rect grid)
        {
            this.grid = grid;
        }
        
        public virtual Rect GetGrid()
        {
            return grid;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            grid = new Rect(inStream);
        }
    }
}