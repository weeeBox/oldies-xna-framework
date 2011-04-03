using System;

using System.Collections.Generic;


using swiff.com.jswiff;
using swiff.com.jswiff.swfrecords.tags;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.listeners
{
    /** 
     * Simple implementation of an <code>SWFListener</code>, can be used to create
     * a <code>SWFDocument</code>. During file parsing, the header and the tags
     * are stored in this document and some properties (e.g. background color,
     * metadata, access mode) are set. Usage:
     * <pre>
     * <code>
     * SWFReader reader            = new SWFReader(inputStream);
     * SWFDocumentReader docReader = new SWFDocumentReader();
     * reader.addListener(docReader);
     * reader.read();
     * SWFDocument doc             = docReader.getDocument();
     * </code>
     * </pre>
     */
    public class SWFDocumentReader : SWFListener
    {
        private SWFDocument document = new SWFDocument();
        
        public virtual SWFDocument GetDocument()
        {
            return document;
        }
        
        public override void ProcessHeader(SWFHeader header)
        {
            document.SetFrameRate(header.GetFrameRate());
            document.SetFrameSize(header.GetFrameSize());
            document.SetVersion(header.GetVersion());
            document.SetFileLength(header.GetFileLength());
            document.SetFrameCount(header.GetFrameCount());
            document.SetCompressed(header.IsCompressed());
        }
        
        public override void ProcessTag(Tag tag, long streamOffset)
        {
            switch (tag.GetCode())
            {
                case TagConstants.SET_BACKGROUND_COLOR:
                    document.SetBackgroundColor(((SetBackgroundColor)(tag)).GetColor());
                    break;
                case TagConstants.FILE_ATTRIBUTES:
                    SetFileAttributes(((FileAttributes)(tag)));
                    break;
                case TagConstants.METADATA:
                    SetMetadata(((Metadata)(tag)));
                    break;
            }
            document.AddTag(tag);
        }
        
        private void SetFileAttributes(FileAttributes attributes)
        {
            if (attributes.IsAllowNetworkAccess()) 
            {
                document.SetAccessMode(SWFDocument.ACCESS_MODE_NETWORK);
            } 
        }
        
        private void SetMetadata(Metadata metadata)
        {
            document.SetMetadata(metadata.GetDataString());
        }
    }
}