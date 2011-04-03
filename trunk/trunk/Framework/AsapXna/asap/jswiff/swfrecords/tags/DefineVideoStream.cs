using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag defines the format of a streaming video data contained in
     * subsequent <code>VideoFrame</code> tags.
     *
     * @see VideoFrame
     * @since SWF 6
     */
    public class DefineVideoStream : DefinitionTag
    {
        /** 
         *
         */
        public const byte DEBLOCKING_PACKET = 0;
        
        /** 
         *
         */
        public const byte DEBLOCKING_OFF = 1;
        
        /** 
         *
         */
        public const byte DEBLOCKING_ON = 2;
        
        /** 
         * Some movies do not specify a codec ID. Do not use this value when creating
         * movies from scratch.
         */
        public const short CODEC_UNDEFINED = 0;
        
        /** 
         *
         */
        public const short CODEC_SORENSON_H263 = 2;
        
        /** 
         * Use Screen Video codec (optimized for screen captures in motion) (since
         * SWF 7).
         */
        public const short CODEC_SCREEN_VIDEO = 3;
        
        /** 
         *
         */
        public const short CODEC_VP6 = 4;
        
        /** 
         *
         */
        public const short CODEC_VP6_ALPHA = 5;
        
        /** 
         *
         */
        public const short CODEC_SCREEN_VIDEO_V2 = 6;
        
        private int numFrames;
        
        private int width;
        
        private int height;
        
        private byte deblocking;
        
        private bool smoothing;
        
        private short codecId;
        
        /** 
         * Creates a new DefineVideoStream tag. Supply the character ID, the number
         * of frames (i.e. subsequent <code>VideoFrame</code> tags) and the
         * dimensions of the video. Specify if a deblocking filter should be used at
         * playback to reduce blocking artifacts (use <code>DEBLOCKING_...</code>
         * constants) and whether to apply smoothing. Finally, specify which codec
         * is used for video encoding. Supported codecs are Sorenson H.263 (an
         * enhanced subset of ITU H.263v1) and, since SWF 7, Screen Video, a format
         * optimized for screen captures in motion (use either
         * <code>CODEC_SORENSON_H263</code> or <code>CODEC_SCREEN_VIDEO</code>).
         *
         * @param characterId character ID of video
         * @param numFrames number of video frames
         * @param width video width in pixels
         * @param height video height in pixels
         * @param deblocking deblocking setting (on /off / use packet setting - see
         *        <code>DEBLOCKING_...</code> constants)
         * @param smoothing if <code>true</code>, video is smoothed
         * @param codecId video encoding algorithm (<code>CODEC_SORENSON_H263</code>
         *        or <code>CODEC_SCREEN_VIDEO</code>)
         */
        public DefineVideoStream(int characterId ,int numFrames ,int width ,int height ,byte deblocking ,bool smoothing ,short codecId) 
        {
            code = TagConstants.DEFINE_VIDEO_STREAM;
            this.characterId = characterId;
            this.numFrames = numFrames;
            this.width = width;
            this.height = height;
            this.deblocking = deblocking;
            this.smoothing = smoothing;
            this.codecId = codecId;
        }
        
        public DefineVideoStream() 
        {
        }
        
        public virtual void SetCodecId(short codecId)
        {
            this.codecId = codecId;
        }
        
        public virtual short GetCodecId()
        {
            return codecId;
        }
        
        public virtual void SetDeblocking(byte deblocking)
        {
            this.deblocking = deblocking;
        }
        
        public virtual byte GetDeblocking()
        {
            return deblocking;
        }
        
        public virtual void SetHeight(int height)
        {
            this.height = height;
        }
        
        public virtual int GetHeight()
        {
            return height;
        }
        
        public virtual void SetNumFrames(int numFrames)
        {
            this.numFrames = numFrames;
        }
        
        public virtual int GetNumFrames()
        {
            return numFrames;
        }
        
        public virtual void SetSmoothing(bool smoothing)
        {
            this.smoothing = smoothing;
        }
        
        public virtual bool IsSmoothing()
        {
            return smoothing;
        }
        
        public virtual void SetWidth(int width)
        {
            this.width = width;
        }
        
        public virtual int GetWidth()
        {
            return width;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            numFrames = inStream.ReadUI16();
            width = inStream.ReadUI16();
            height = inStream.ReadUI16();
            inStream.ReadUnsignedBits(5);
            deblocking = unchecked((byte)(inStream.ReadUnsignedBits(2)));
            smoothing = inStream.ReadBooleanBit();
            codecId = inStream.ReadUI8();
        }
    }
}