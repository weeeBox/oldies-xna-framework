using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag is used to define the format of streaming sound data (contained in
     * <code>SoundStreamBlock</code> tags). It extends the
     * <code>SoundStreamHead</code> tag in flexibility, supporting more encoding
     * formats and sample sizes.
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
     * @see SoundStreamBlock
     */
    public class SoundStreamHead2 : Tag
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
        
        private byte playbackRate;
        
        private bool isPlayback16BitSample;
        
        private bool isPlaybackStereo;
        
        private byte streamFormat;
        
        private byte streamRate;
        
        private bool isStream16BitSample;
        
        private bool isStreamStereo;
        
        private int streamSampleCount;
        
        private short latencySeek;
        
        /** 
         * <p>
         * Creates a new SoundStreamHead2 tag. Supply the encoding format of the
         * stream (one of the provided <code>FORMAT_...</code> constants), its
         * sampling rate (use <code>RATE_...</code> constants), specify whether
         * the sample size is 16 bit or 8 bit, and provide the channel count (mono
         * / stereo) and the average number of samples (for stereo sound: sample
         * pairs) per SoundStreamBlock.
         * </p>
         * 
         * <p>
         * The advisory playback parameters (sampling rate, sample size and channel
         * count) are set to be identical to the stream's parameters specified
         * here. Use <code>setPlayback...()</code> methods for changing these
         * values.
         * </p>
         *
         * @param format encoding format (mp3 or ADPCM)
         * @param rate sampling rate
         * @param is16BitSample if <code>true</code>, sample size is 16 bit,
         * 		  otherwise 8 bit
         * @param isStereo if <code>true</code>, sound is stereo, otherwise mono
         * @param sampleCount average number of samples (stereo: sample pairs) per
         * 		  block
         */
        public SoundStreamHead2(byte format ,byte rate ,bool is16BitSample ,bool isStereo ,int sampleCount) 
        {
            code = TagConstants.SOUND_STREAM_HEAD_2;
            this.streamFormat = format;
            this.streamRate = rate;
            this.playbackRate = rate;
            this.isStream16BitSample = is16BitSample;
            this.isPlayback16BitSample = is16BitSample;
            this.isStreamStereo = isStereo;
            this.isPlaybackStereo = isStereo;
            this.streamSampleCount = sampleCount;
        }
        
        public SoundStreamHead2() 
        {
        }
        
        public virtual void SetLatencySeek(short latencySeek)
        {
            this.latencySeek = latencySeek;
        }
        
        public virtual short GetLatencySeek()
        {
            return latencySeek;
        }
        
        public virtual void SetPlayback16BitSample(bool is16BitSample)
        {
            this.isPlayback16BitSample = is16BitSample;
        }
        
        public virtual bool IsPlayback16BitSample()
        {
            return isPlayback16BitSample;
        }
        
        public virtual void SetPlaybackRate(byte rate)
        {
            this.playbackRate = rate;
        }
        
        public virtual byte GetPlaybackRate()
        {
            return playbackRate;
        }
        
        public virtual void SetPlaybackStereo(bool isStereo)
        {
            this.isPlaybackStereo = isStereo;
        }
        
        public virtual bool IsPlaybackStereo()
        {
            return isPlaybackStereo;
        }
        
        public virtual void SetStream16BitSample(bool isStream16BitSample)
        {
            this.isStream16BitSample = isStream16BitSample;
        }
        
        public virtual bool IsStream16BitSample()
        {
            return isStream16BitSample;
        }
        
        public virtual void SetStreamFormat(byte streamFormat)
        {
            this.streamFormat = streamFormat;
        }
        
        public virtual byte GetStreamFormat()
        {
            return streamFormat;
        }
        
        public virtual void SetStreamRate(byte streamRate)
        {
            this.streamRate = streamRate;
        }
        
        public virtual byte GetStreamRate()
        {
            return streamRate;
        }
        
        public virtual void SetStreamSampleCount(int streamSampleCount)
        {
            this.streamSampleCount = streamSampleCount;
        }
        
        public virtual int GetStreamSampleCount()
        {
            return streamSampleCount;
        }
        
        public virtual void SetStreamStereo(bool isStreamStereo)
        {
            this.isStreamStereo = isStreamStereo;
        }
        
        public virtual bool IsStreamStereo()
        {
            return isStreamStereo;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            inStream.ReadUnsignedBits(4);
            playbackRate = unchecked((byte)(inStream.ReadUnsignedBits(2)));
            isPlayback16BitSample = inStream.ReadBooleanBit();
            isPlaybackStereo = inStream.ReadBooleanBit();
            streamFormat = unchecked((byte)(inStream.ReadUnsignedBits(4)));
            streamRate = unchecked((byte)(inStream.ReadUnsignedBits(2)));
            isStream16BitSample = inStream.ReadBooleanBit();
            isStreamStereo = inStream.ReadBooleanBit();
            streamSampleCount = inStream.ReadUI16();
            if (((streamFormat) == (FORMAT_MP3)) && ((data.Length) > 4)) 
            {
                latencySeek = inStream.ReadSI16();
            } 
        }
    }
}