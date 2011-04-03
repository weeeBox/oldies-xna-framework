using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag defines an event sound.
     * </p>
     * 
     * <p>
     * Warning: you are responsible for obtaining technology licenses needed for
     * encoding and decoding sound data (see e.g. <a
     * href="http://mp3licensing.com">mp3licensing.com</a> for details on mp3
     * licensing).
     * </p>
     *
     * @since SWF 1
     */
    public class DefineSound : DefinitionTag
    {
        /** 
         * Uncompressed sound format. For 16-bit samples, native byte ordering
         * (little-endian or big-endian) is used. Warning: this introduces
         * platform dependency!
         */
        public const byte FORMAT_UNCOMPRESSED = 0;
        
        /** 
         * ADPCM compressed sound format (simple compression algorithm without
         * licensing issues).
         */
        public const byte FORMAT_ADPCM = 1;
        
        /** 
         * mp3 compressed sound format (for high-quality sound encoding) (since SWF
         * 4).
         */
        public const byte FORMAT_MP3 = 2;
        
        /** 
         * Uncompressed little-endian sound format, i.e. 16-bit samples are decoded
         * using little-endian byte ordering (platform-independent format) (since
         * SWF 4).
         */
        public const byte FORMAT_UNCOMPRESSED_LITTLE_ENDIAN = 3;
        
        /** 
         * Nellymoser Asao compressed sound format (optimized for low-bitrate mono
         * speech transmission) (since SWF 6).
         */
        public const byte FORMAT_NELLYMOSER = 6;
        
        /** 
         *
         */
        public const byte RATE_5500_HZ = 0;
        
        /** 
         *
         */
        public const byte RATE_11000_HZ = 1;
        
        /** 
         *
         */
        public const byte RATE_22000_HZ = 2;
        
        /** 
         *
         */
        public const byte RATE_44000_HZ = 3;
        
        private byte format;
        
        private byte rate;
        
        private bool is16BitSample;
        
        private bool isStereo;
        
        private long sampleCount;
        
        private byte[] soundData;
        
        /** 
         * Creates a new DefineSound instance. Supply the character ID of the
         * sound, the encoding format (one of the provided <code>FORMAT_...</code>
         * constants), the sampling rate (use <code>RATE_...</code> constants),
         * specify whether 8-bit or 16-bit samples are used (8-bit samples are
         * allowed only for uncompressed formats) and whether the sound is mono or
         * stereo (Nellymoser merely supports mono). Provide the number of samples
         * (for stereo sound: sample pairs) and, finally, the actual sound data as
         * raw data.
         *
         * @param characterId character ID of sound
         * @param format encoding format (use provided constants)
         * @param rate sampling rate (use provided constants)
         * @param is16BitSample if <code>true</code>, 16-bit samples are used
         * 		  (otherwise 8-bit, for uncompressed formats only)
         * @param isStereo if <code>true</code>, sound is stereo, otherwise mono
         * @param sampleCount number of samples (stereo: sample pairs)
         * @param soundData raw sound data
         */
        public DefineSound(int characterId ,byte format ,byte rate ,bool is16BitSample ,bool isStereo ,long sampleCount ,byte[] soundData) 
        {
            code = TagConstants.DEFINE_SOUND;
            this.characterId = characterId;
            this.format = format;
            this.rate = rate;
            this.is16BitSample = is16BitSample;
            this.isStereo = isStereo;
            this.sampleCount = sampleCount;
            this.soundData = soundData;
        }
        
        public DefineSound() 
        {
        }
        
        public virtual void SetFormat(byte format)
        {
            this.format = format;
        }
        
        public virtual byte GetFormat()
        {
            return format;
        }
        
        public virtual byte GetRate()
        {
            return rate;
        }
        
        public virtual void SetSampleCount(long sampleCount)
        {
            this.sampleCount = sampleCount;
        }
        
        public virtual long GetSampleCount()
        {
            return sampleCount;
        }
        
        public virtual void SetSoundData(byte[] soundData)
        {
            this.soundData = soundData;
        }
        
        public virtual byte[] GetSoundData()
        {
            return soundData;
        }
        
        public virtual void SetStereo(bool isStereo)
        {
            this.isStereo = isStereo;
        }
        
        public virtual bool IsStereo()
        {
            return isStereo;
        }
        
        public virtual bool Is16BitSample()
        {
            return is16BitSample;
        }
        
        public virtual void Set16BitSample(bool is16BitSample)
        {
            this.is16BitSample = is16BitSample;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            format = unchecked((byte)(inStream.ReadUnsignedBits(4)));
            rate = unchecked((byte)(inStream.ReadUnsignedBits(2)));
            is16BitSample = inStream.ReadBooleanBit();
            isStereo = inStream.ReadBooleanBit();
            sampleCount = inStream.ReadUI32();
            soundData = new byte[(data.Length) - 7];
            Array.Copy(data, 7, soundData, 0, ((data.Length) - 7));
        }
    }
}