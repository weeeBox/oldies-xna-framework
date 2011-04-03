using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag defines a button character. It contains an array of at least one
     * <code>ButtonRecord</code> instance in order to define the button's
     * appearance depending on it's state. See <code>ButtonRecord</code> for
     * details on button states.
     * </p>
     * 
     * <p>
     * DefineButton2 allows actions to be triggered by any state transition. See
     * <code>ButtonCondAction</code> for details on button state transitions.
     * </p>
     *
     * @see ButtonRecord
     * @see ButtonCondAction
     * @since SWF 3
     */
    public class DefineButton2 : DefinitionTag
    {
        private bool trackAsMenu;
        
        private ButtonRecord[] characters;
        
        private ButtonCondAction[] actions;
        
        /** 
         * Creates a new DefineButton2 tag.
         *
         * @param characterId the button's character ID
         * @param characters array of button records
         * @param trackAsMenu if <code>true</code>, button can be influenced by
         *        events started on other buttons
         */
        public DefineButton2(int characterId ,ButtonRecord[] characters ,bool trackAsMenu) 
        {
            code = TagConstants.DEFINE_BUTTON_2;
            this.characterId = characterId;
            this.characters = characters;
            this.trackAsMenu = trackAsMenu;
        }
        
        public DefineButton2() 
        {
        }
        
        public virtual void SetActions(ButtonCondAction[] actions)
        {
            this.actions = actions;
        }
        
        public virtual ButtonCondAction[] GetActions()
        {
            return actions;
        }
        
        public virtual void SetCharacters(ButtonRecord[] characters)
        {
            this.characters = characters;
        }
        
        public virtual ButtonRecord[] GetCharacters()
        {
            return characters;
        }
        
        public virtual void SetTrackAsMenu(bool trackAsMenu)
        {
            this.trackAsMenu = trackAsMenu;
        }
        
        public virtual bool IsTrackAsMenu()
        {
            return trackAsMenu;
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
            characterId = inStream.ReadUI16();
            trackAsMenu = ((inStream.ReadUI8()) & 1) != 0;
            int actionOffset = inStream.ReadUI16();
            List<ButtonRecord> buttonRecords = new List<ButtonRecord>();
            long startOffset = inStream.GetOffset();
            do 
            {
                long remainingBytes = (data.Length) - (inStream.GetOffset());
                if ((actionOffset == 0) && (remainingBytes == 1)) 
                {
                    break;
                } 
                else if (((inStream.GetOffset()) - startOffset) == (actionOffset - 3)) 
                {
                    break;
                } 
                else if (remainingBytes < 6) 
                {
                    break;
                } 
                buttonRecords.Add(new ButtonRecord(inStream , true));
            } while (true );
            inStream.ReadUI8();
            characters = buttonRecords.ToArray();            
            if (actionOffset == 0) 
            {
                return ;
            } 
            List<ButtonCondAction> buttonCondActions = new List<ButtonCondAction>();
            int condActionSize = -1;
            do 
            {
                condActionSize = inStream.ReadUI16();
                buttonCondActions.Add(new ButtonCondAction(inStream));
            } while (condActionSize != 0 );
            actions = buttonCondActions.ToArray();            
        }
    }
}