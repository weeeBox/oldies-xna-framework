using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag instructs the Flash Player to play a sound previously defined with
     * the <code>DefineSound</code> tag. Playback parameters can be defined by
     * supplying a <code>SoundInfo</code> instance.
     *
     * @see SoundInfo
     * @since SWF 1
     */
    public class StartSound : Tag
    {
        private int soundId;
        
        private SoundInfo soundInfo;
        
        /** 
         * Creates a new StartSound tag. Supply the character ID of the sound to be
         * played, and playback otions in an <code>SoundInfo</code> instance.
         *
         * @param soundId the sound's character ID
         * @param soundInfo playback options
         */
        public StartSound(int soundId ,SoundInfo soundInfo) 
        {
            code = TagConstants.START_SOUND;
            this.soundId = soundId;
            this.soundInfo = soundInfo;
        }
        
        public StartSound() 
        {
        }
        
        public virtual void SetSoundId(int soundId)
        {
            this.soundId = soundId;
        }
        
        public virtual int GetSoundId()
        {
            return soundId;
        }
        
        public virtual void SetSoundInfo(SoundInfo soundInfo)
        {
            this.soundInfo = soundInfo;
        }
        
        public virtual SoundInfo GetSoundInfo()
        {
            return soundInfo;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            soundId = inStream.ReadUI16();
            soundInfo = new SoundInfo(inStream);
        }
    }
}