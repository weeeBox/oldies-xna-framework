using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using System.IO;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * Instances of this class represent a header of an SWF file.
     */
    public class SWFHeader
    {
        private bool compressed;
        
        private short version;
        
        private long fileLength;
        
        private Rect frameSize;
        
        private short frameRate;
        
        private int frameCount;
        
        /** 
         * Creates a new SWFHeader instance.
         */
        public SWFHeader() 
        {
        }
        
        /** 
         * Reads an SWF header from a bit stream.
         *
         * @param stream a bit stream
         *
         * @throws IOException if an I/O error has occurred
         */
        public SWFHeader(InputBitStream stream) /* throws IOException */ 
        {
            Read(stream);
        }
        
        public virtual void SetCompressed(bool compressed)
        {
            this.compressed = compressed;
        }
        
        public virtual bool IsCompressed()
        {
            return compressed;
        }
        
        public virtual void SetFileLength(long fileLength)
        {
            this.fileLength = fileLength;
        }
        
        public virtual long GetFileLength()
        {
            return fileLength;
        }
        
        public virtual void SetFrameCount(int frameCount)
        {
            this.frameCount = frameCount;
        }
        
        public virtual int GetFrameCount()
        {
            return frameCount;
        }
        
        public virtual void SetFrameRate(short frameRate)
        {
            this.frameRate = frameRate;
        }
        
        public virtual short GetFrameRate()
        {
            return frameRate;
        }
        
        public virtual void SetFrameSize(Rect frameSize)
        {
            this.frameSize = frameSize;
        }
        
        public virtual Rect GetFrameSize()
        {
            return frameSize;
        }
        
        public virtual void SetVersion(short version)
        {
            this.version = version;
        }
        
        public virtual short GetVersion()
        {
            return version;
        }
        
        private void Read(InputBitStream stream) /* throws IOException */
        {
            short compressionByte = stream.ReadUI8();
            if ((((compressionByte != 67) && (compressionByte != 70)) || ((stream.ReadUI8()) != 87)) || ((stream.ReadUI8()) != 83)) 
            {
                throw new IOException("Invalid SWF file signature!");
            } 
            if (compressionByte == 67) 
            {
                compressed = true;
            } 
            version = unchecked((byte)(stream.ReadUI8()));
            fileLength = stream.ReadUI32();
            if (compressed) 
            {
                throw new NotImplementedException();
            } 
            frameSize = new Rect(stream);
            stream.ReadUI8();
            frameRate = stream.ReadUI8();
            frameCount = stream.ReadUI16();
        }
    }
}