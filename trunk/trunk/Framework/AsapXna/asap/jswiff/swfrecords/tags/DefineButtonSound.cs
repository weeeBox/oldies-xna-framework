using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag determines the Flash Player to play sounds on a button's state
     * transitions. Consult <code>ButtonCondAction</code> for more details on
     * state transitions.
     * </p>
     * 
     * <p>
     * Note: despite its name, this tag isn't a definition tag. It doesn't define a
     * new character, it specifies attributes for an existing character.
     * </p>
     *
     * @see com.jswiff.swfrecords.ButtonCondAction
     * @since SWF 2
     */
    public class DefineButtonSound : Tag
    {
        private int buttonId;
        
        private int overUpToIdleSoundId;
        
        private SoundInfo overUpToIdleSoundInfo;
        
        private int idleToOverUpSoundId;
        
        private SoundInfo idleToOverUpSoundInfo;
        
        private int overUpToOverDownSoundId;
        
        private SoundInfo overUpToOverDownSoundInfo;
        
        private int overDownToOverUpSoundId;
        
        private SoundInfo overDownToOverUpSoundInfo;
        
        /** 
         * Creates a new DefineButtonSound tag.
         *
         * @param buttonId character ID of the button
         */
        public DefineButtonSound(int buttonId) 
        {
            code = TagConstants.DEFINE_BUTTON_SOUND;
            this.buttonId = buttonId;
        }
        
        public DefineButtonSound() 
        {
        }
        
        public virtual void SetButtonId(int buttonId)
        {
            this.buttonId = buttonId;
        }
        
        public virtual int GetButtonId()
        {
            return buttonId;
        }
        
        public virtual void SetIdleToOverUpSoundId(int soundId)
        {
            this.idleToOverUpSoundId = soundId;
        }
        
        public virtual int GetIdleToOverUpSoundId()
        {
            return idleToOverUpSoundId;
        }
        
        public virtual void SetIdleToOverUpSoundInfo(SoundInfo soundInfo)
        {
            this.idleToOverUpSoundInfo = soundInfo;
        }
        
        public virtual SoundInfo GetIdleToOverUpSoundInfo()
        {
            return idleToOverUpSoundInfo;
        }
        
        public virtual void SetOverDownToOverUpSoundId(int soundId)
        {
            this.overDownToOverUpSoundId = soundId;
        }
        
        public virtual int GetOverDownToOverUpSoundId()
        {
            return overDownToOverUpSoundId;
        }
        
        public virtual void SetOverDownToOverUpSoundInfo(SoundInfo soundInfo)
        {
            this.overDownToOverUpSoundInfo = soundInfo;
        }
        
        public virtual SoundInfo GetOverDownToOverUpSoundInfo()
        {
            return overDownToOverUpSoundInfo;
        }
        
        public virtual void SetOverUpToIdleSoundId(int soundId)
        {
            this.overUpToIdleSoundId = soundId;
        }
        
        public virtual int GetOverUpToIdleSoundId()
        {
            return overUpToIdleSoundId;
        }
        
        public virtual void SetOverUpToIdleSoundInfo(SoundInfo soundInfo)
        {
            this.overUpToIdleSoundInfo = soundInfo;
        }
        
        public virtual SoundInfo GetOverUpToIdleSoundInfo()
        {
            return overUpToIdleSoundInfo;
        }
        
        public virtual void SetOverUpToOverDownSoundId(int soundId)
        {
            this.overUpToOverDownSoundId = soundId;
        }
        
        public virtual int GetOverUpToOverDownSoundId()
        {
            return overUpToOverDownSoundId;
        }
        
        public virtual void SetOverUpToOverDownSoundInfo(SoundInfo soundInfo)
        {
            this.overUpToOverDownSoundInfo = soundInfo;
        }
        
        public virtual SoundInfo GetOverUpToOverDownSoundInfo()
        {
            return overUpToOverDownSoundInfo;
        }
                
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            buttonId = inStream.ReadUI16();
            overUpToIdleSoundId = inStream.ReadUI16();
            if ((overUpToIdleSoundId) != 0) 
            {
                overUpToIdleSoundInfo = new SoundInfo(inStream);
            } 
            idleToOverUpSoundId = inStream.ReadUI16();
            if ((idleToOverUpSoundId) != 0) 
            {
                idleToOverUpSoundInfo = new SoundInfo(inStream);
            } 
            overUpToOverDownSoundId = inStream.ReadUI16();
            if ((overUpToOverDownSoundId) != 0) 
            {
                overUpToOverDownSoundInfo = new SoundInfo(inStream);
            } 
            overDownToOverUpSoundId = inStream.ReadUI16();
            if ((overDownToOverUpSoundId) != 0) 
            {
                overDownToOverUpSoundInfo = new SoundInfo(inStream);
            } 
        }
    }
}