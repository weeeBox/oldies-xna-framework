using System;

using System.Collections.Generic;


using swiff.com.jswiff.swfrecords.actions;
using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag contains a series of initialization actions for a particular
     * sprite. These actions are executed only once, before the first instatiation
     * of the sprite. Typically used for class definitions.
     * </p>
     * 
     * <p>
     * This tag is used to implement the <code>#initclip</code> ActionScript
     * compiler directive.
     * </p>
     *
     * @since SWF 6
     */
    public class DoInitAction : Tag
    {
        private int spriteId;
        
        private ActionBlock initActions;
        
        /** 
         * Creates a new DoInitAction tag. Supply the character ID of the sprite
         * the initialization actions apply to. After creation, use
         * <code>addAction()</code> to add actions to the contained action block.
         *
         * @param spriteId character ID of sprite to be initialized
         */
        public DoInitAction(int spriteId) 
        {
            code = TagConstants.DO_INIT_ACTION;
            this.spriteId = spriteId;
            initActions = new ActionBlock();
        }
        
        public DoInitAction() 
        {
        }
        
        public virtual ActionBlock GetInitActions()
        {
            return initActions;
        }
        
        public virtual void SetSpriteId(int spriteId)
        {
            this.spriteId = spriteId;
        }
        
        public virtual int GetSpriteId()
        {
            return spriteId;
        }
        
        public virtual void AddAction(ActionRecord action)
        {
            initActions.AddAction(action);
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
            spriteId = inStream.ReadUI16();
            initActions = new ActionBlock(inStream);
        }
    }
}