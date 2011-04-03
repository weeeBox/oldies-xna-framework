using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag contains raw streaming sound data. The data format must be defined
     * in a preceding <code>SoundStreamHead</code> or
     * <code>SoundStreamHead2</code> tag. There may only be one
     * <code>SoundStreamBlock</code> tag per SWF frame.
     * </p>
     * 
     * <p>
     * Warning: you are responsible for obtaining technology licenses needed for
     * encoding and decoding sound data (see e.g. <a
     * href="http://mp3licensing.com">mp3licensing.com</a> for details on mp3
     * licensing).
     * </p>
     *
     * @see SoundStreamHead
     * @see SoundStreamHead2
     * @since SWF 1
     */
    public class SoundStreamBlock : Tag
    {
        private byte[] streamSoundData;
        
        /** 
         * Creates a new SoundStreamBlock tag. Supply the sound data as byte array
         *
         * @param streamSoundData raw sound stream data
         */
        public SoundStreamBlock(byte[] streamSoundData) 
        {
            code = TagConstants.SOUND_STREAM_BLOCK;
            this.streamSoundData = streamSoundData;
        }
        
        public SoundStreamBlock() 
        {
        }
        
        public virtual void SetStreamSoundData(byte[] streamSoundData)
        {
            this.streamSoundData = streamSoundData;
        }
        
        public virtual byte[] GetStreamSoundData()
        {
            return streamSoundData;
        }
        
        public override void SetData(byte[] data)
        {
            streamSoundData = data;
        }
    }
}