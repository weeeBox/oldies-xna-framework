using System;

using System.Collections.Generic;


using swiff.com.jswiff.swfrecords.tags;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff
{
    /** 
     * This class represents a Macromedia Flash (SWF) document. Contains an SWF
     * header and a list of tags. Use a <code>SWFReader</code> to parse a file to
     * an <code>SWFDocument</code> instance. Write SWF files using
     * <code>SWFWriter</code>.
     */
    public class SWFDocument
    {
        /** 
         *
         */
        public const String JSWIFF_VERSION = "8.0";
        
        /** 
         * The default value for the compression flag. By default, compression is on
         */
        public const bool DEFAULT_COMPRESSION = true;
        
        /** 
         *
         */
        public const short DEFAULT_SWF_VERSION = 10;
        
        /** 
         * The default frame size (based on the authoring tool from Macromedia: 11000
         * x 8000 twips)
         */
        public static readonly Rect DEFAULT_FRAME_SIZE = new Rect(0 , 11000 , 0 , 8000);
        
        /** 
         * The default frame rate of a (newly created) SWF movie (based on the
         * authoring tool from Macromedia: 12 fps)
         */
        public const short DEFAULT_FRAME_RATE = 12;
        
        /** 
         *
         */
        public const short MAX_SWF_VERSION = 10;
        
        /** 
         *
         */
        public const byte ACCESS_MODE_LOCAL = 0;
        
        /** 
         *
         */
        public const byte ACCESS_MODE_NETWORK = 1;
        
        private SWFHeader header = new SWFHeader();
        
        private int currentCharId;
        
        private List<Tag>  tags = new List<Tag> ();
        
        private RGB backgroundColor = new RGB(((short)(255)) , ((short)(255)) , ((short)(255)));
        
        private byte accessMode = ACCESS_MODE_LOCAL;
        
        private String metadata;
        
        /** 
         * Creates a new SWFDocument instance.
         */
        public SWFDocument() 
        {
            header.SetCompressed(DEFAULT_COMPRESSION);
            header.SetVersion(DEFAULT_SWF_VERSION);
            header.SetFrameSize(DEFAULT_FRAME_SIZE);
            header.SetFrameRate(DEFAULT_FRAME_RATE);
        }
        
        public virtual void SetAccessMode(byte accessMode)
        {
            this.accessMode = accessMode;
        }
        
        public virtual byte GetAccessMode()
        {
            return accessMode;
        }
        
        public virtual bool IsActionScript3()
        {
            return true;
        }
        
        public virtual void SetBackgroundColor(RGB backgroundColor)
        {
            this.backgroundColor = backgroundColor;
        }
        
        public virtual RGB GetBackgroundColor()
        {
            return backgroundColor;
        }
        
        public virtual void SetCompressed(bool compressed)
        {
            header.SetCompressed(compressed);
        }
        
        public virtual bool IsCompressed()
        {
            return header.IsCompressed();
        }
        
        public virtual void SetFileLength(long fileLength)
        {
            header.SetFileLength(fileLength);
        }
        
        public virtual long GetFileLength()
        {
            return header.GetFileLength();
        }
        
        public virtual void SetFrameCount(int frameCount)
        {
            header.SetFrameCount(frameCount);
        }
        
        public virtual int GetFrameCount()
        {
            return header.GetFrameCount();
        }
        
        public virtual void SetFrameRate(short frameRate)
        {
            header.SetFrameRate(frameRate);
        }
        
        public virtual short GetFrameRate()
        {
            return header.GetFrameRate();
        }
        
        public virtual void SetFrameSize(Rect frameSize)
        {
            header.SetFrameSize(frameSize);
        }
        
        public virtual Rect GetFrameSize()
        {
            return header.GetFrameSize();
        }

        public int GetWidth()
        {
            return (int)(GetFrameSize().GetXMax() - GetFrameSize().GetXMin()) / 20;
        }

        public int GetHeight()
        {
            return (int)(GetFrameSize().GetYMax() - GetFrameSize().GetYMin()) / 20;
        }
        
        public virtual void SetMetadata(String metadata)
        {
            this.metadata = metadata;
        }
        
        public virtual String GetMetadata()
        {
            return metadata;
        }
        
        public virtual int GetNewCharacterId()
        {
            return ++(currentCharId);
        }
        
        public virtual List<Tag>  GetTags()
        {
            return tags;
        }
        
        public virtual void SetVersion(short version)
        {
            if (version < 1) 
            {
                throw new ArgumentOutOfRangeException("Flash version must be at least 1!");
            } 
            else if (version > (MAX_SWF_VERSION)) 
            {
                throw new ArgumentOutOfRangeException((("Flash version > " + (MAX_SWF_VERSION)) + " not supported!"));
            } 
            header.SetVersion(version);
        }
        
        public virtual short GetVersion()
        {
            return header.GetVersion();
        }
        
        public virtual void AddTag(Tag tag)
        {
            tags.Add(tag);
        }
        
        public virtual void AddTags(List<Tag>  tagList)
        {
            tags.AddRange(tagList);
        }
        
        public virtual bool RemoveTag(Tag tag)
        {
            return tags.Remove(tag);
        }
        
        public virtual Tag RemoveTag(int index)
        {
            Tag tag = tags[index];
            tags.RemoveAt(index);
            return tag;
        }
    }
}