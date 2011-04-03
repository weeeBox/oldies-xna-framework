using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag is used to define the format of streaming sound data (contained in
     * <code>SoundStreamBlock</code> tags). Streaming sounds defined with this tag
     * are always compressed and their sample size is always 16 bit. For more
     * flexibility, use <code>SoundStreamHead2</code>.
     * </p>
     * 
     * <p>
     * Warning: you are responsible for obtaining technology licenses needed for
     * encoding and decoding sound data (see e.g. <a
     * href="http://mp3licensing.com">mp3licensing.com</a> for details on mp3
     * licensing).
     * </p>
     *
     * @see SoundStreamBlock
     * @see SoundStreamHead2
     * @since SWF 1
     */
    public class SoundStreamHead : Tag
    {
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
        
        private bool playbackStereo;
        
        private byte streamFormat;
        
        private byte streamRate;
        
        private bool streamStereo;
        
        private int streamSampleCount;
        
        private short latencySeek;
        
        /** 
         * <p>
         * Creates a new SoundStreamHead tag. Supply the encoding format of the
         * stream (one of the provided <code>FORMAT_...</code> constants), its
         * sampling rate (use <code>RATE_...</code> constants), and provide the
         * channel count (mono / stereo) and the average number of samples (for
         * stereo sound: sample pairs) per SoundStreamBlock.
         * </p>
         * 
         * <p>
         * The advisory playback parameters (sampling rate and channel count) are
         * set to be identical to the stream's parameters specified here. Use
         * <code>setPlayback...()</code> methods for changing these values.
         * </p>
         *
         * @param streamFormat encoding format (mp3 or ADPCM)
         * @param rate sampling rate
         * @param stereo if <code>true</code>, sound is stereo, otherwise mono
         * @param sampleCount average number of samples (stereo: sample pairs) per
         * 		  block
         */
        public SoundStreamHead(byte streamFormat ,byte rate ,bool stereo ,int sampleCount) 
        {
            code = TagConstants.SOUND_STREAM_HEAD;
            this.streamFormat = streamFormat;
            this.streamRate = rate;
            this.playbackRate = rate;
            this.streamStereo = stereo;
            this.playbackStereo = stereo;
            this.streamSampleCount = sampleCount;
        }
        
        public SoundStreamHead() 
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
            this.playbackStereo = isStereo;
        }
        
        public virtual bool IsPlaybackStereo()
        {
            return playbackStereo;
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
        
        public virtual void SetStreamStereo(bool streamStereo)
        {
            this.streamStereo = streamStereo;
        }
        
        public virtual bool IsStreamStereo()
        {
            return streamStereo;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            inStream.ReadUnsignedBits(4);
            playbackRate = unchecked((byte)(inStream.ReadUnsignedBits(2)));
            inStream.ReadUnsignedBits(1);
            playbackStereo = inStream.ReadBooleanBit();
            streamFormat = unchecked((byte)(inStream.ReadUnsignedBits(4)));
            streamRate = unchecked((byte)(inStream.ReadUnsignedBits(2)));
            inStream.ReadUnsignedBits(1);
            streamStereo = inStream.ReadBooleanBit();
            streamSampleCount = inStream.ReadUI16();
            if (((streamFormat) == (FORMAT_MP3)) && ((data.Length) > 4)) 
            {
                latencySeek = inStream.ReadSI16();
            } 
        }
    }
}