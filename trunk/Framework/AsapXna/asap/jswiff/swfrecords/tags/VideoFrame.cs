using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag provides a single frame of streaming video data.  The format of the
     * video stream must be defined in a preceding <code>DefineVideoStream</code>
     * tag. The video frame rate is limited by the SWF frame rate, as an SWF frame
     * can contain at most one video frame.
     *
     * @see DefineVideoStream
     * @since SWF 6
     */
    public class VideoFrame : Tag
    {
        private int streamId;
        
        private int frameNum;
        
        private byte[] videoData;
        
        /** 
         * Creates a new VideoFrame tag. Provide the character ID of the video
         * stream, the sequential frame number and the raw video data contained in
         * this frame.
         *
         * @param streamId character ID of video stream
         * @param frameNum frame number
         * @param videoData raw video frame data
         */
        public VideoFrame(int streamId ,int frameNum ,byte[] videoData) 
        {
            code = TagConstants.VIDEO_FRAME;
            this.streamId = streamId;
            this.frameNum = frameNum;
            this.videoData = videoData;
        }
        
        public VideoFrame() 
        {
        }
        
        public virtual void SetFrameNum(int frameNum)
        {
            this.frameNum = frameNum;
        }
        
        public virtual int GetFrameNum()
        {
            return frameNum;
        }
        
        public virtual void SetStreamId(int streamId)
        {
            this.streamId = streamId;
        }
        
        public virtual int GetStreamId()
        {
            return streamId;
        }
        
        public virtual void SetVideoData(byte[] videoData)
        {
            this.videoData = videoData;
        }
        
        public virtual byte[] GetVideoData()
        {
            return videoData;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            streamId = inStream.ReadUI16();
            frameNum = inStream.ReadUI16();
            int videoDataLength = (data.Length) - 4;
            videoData = new byte[videoDataLength];
            Array.Copy(data, 4, videoData, 0, videoDataLength);
        }
    }
}