using System;

using System.Collections.Generic;


using swiff.com.jswiff.swfrecords.actions;
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
     * DefineButton also includes an action block which contains actions performed
     * when when the button is clicked and released.
     * </p>
     *
     * @see ButtonRecord
     * @since SWF 1
     */
    public class DefineButton : DefinitionTag
    {
        private ButtonRecord[] characters;
        
        private ActionBlock actionBlock;
        
        /** 
         * Creates a new DefineButton tag.
         *
         * @param characterId the button's character ID
         * @param characters array of button records
         *
         * @throws IllegalArgumentException if button record array is
         * 		   <code>null</code> or empty
         */
        public DefineButton(int characterId ,ButtonRecord[] characters) 
        {
            code = TagConstants.DEFINE_BUTTON;
            if ((characters == null) || ((characters.Length) == 0)) 
            {
                throw new ArgumentOutOfRangeException("At least one button record is needed!");
            } 
            this.characterId = characterId;
            this.characters = characters;
        }
        
        public DefineButton() 
        {
        }
        
        public virtual ActionBlock GetActions()
        {
            if ((actionBlock) == null) 
            {
                actionBlock = new ActionBlock();
            } 
            return actionBlock;
        }
        
        public virtual void SetCharacters(ButtonRecord[] characters)
        {
            this.characters = characters;
        }
        
        public virtual ButtonRecord[] GetCharacters()
        {
            return characters;
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
            List<ButtonRecord> buttonRecords = new List<ButtonRecord>();
            do 
            {
                if ((data[((int)(inStream.GetOffset()))]) == 0) 
                {
                    inStream.ReadUI8();
                    break;
                } 
                buttonRecords.Add(new ButtonRecord(inStream , false));
            } while (true );
            // characters = new ButtonRecord[buttonRecords.Count];
            characters = buttonRecords.ToArray();
            actionBlock = new ActionBlock(inStream);
        }
    }
}