using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag defines a sprite character (i.e. a movie inside the main SWF
     * movie). It consists of a character ID, a frame count and several control
     * tags. Character instances referred to by these control tags in the sprite
     * must have been previously defined.
     * </p>
     * 
     * <p>
     * Once defined, the sprite can be displayed using the
     * <code>PlaceObject2</code> tag.
     * </p>
     *
     * @see PlaceObject2
     * @since SWF 3
     */
    public class DefineSprite : DefinitionTag
    {
        private List<Tag>  controlTags = new List<Tag> ();
        
        /** 
         * Creates a new DefineSprite tag. Supply the character ID of the sprite.
         * After tag creation, use <code>addControlTag()</code> to add tags to the
         * sprite's tag list.
         *
         * @param characterId sprite's character ID
         */
        public DefineSprite(int characterId) 
        {
            code = TagConstants.DEFINE_SPRITE;
            this.characterId = characterId;
        }
        
        public DefineSprite() 
        {
        }
        
        public virtual List<Tag>  GetControlTags()
        {
            return controlTags;
        }
        
        public virtual int GetFrameCount()
        {
            int count = 0;            
            foreach (Tag tag in controlTags)
            {
                if (tag.GetCode() == TagConstants.SHOW_FRAME) 
                {
                    count++;
                } 
            }
            return count;
        }
        
        public virtual void AddControlTag(Tag controlTag)
        {
            controlTags.Add(controlTag);
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            inStream.ReadUI16();
            do 
            {
                Tag tag = TagReader.ReadTag(inStream, GetSWFVersion(), IsJapanese());
                if ((tag.GetCode()) != (TagConstants.END)) 
                {
                    controlTags.Add(tag);
                } 
                else 
                {
                    break;
                }
            } while (true );
        }
    }
}