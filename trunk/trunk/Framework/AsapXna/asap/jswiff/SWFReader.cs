using System;

using System.Collections.Generic;


using swiff.com.jswiff.listeners;
using swiff.com.jswiff.swfrecords.tags;
using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;
using System.IO;

namespace swiff.com.jswiff
{
    /** 
     * This class reads an SWF file from a stream, invoking registered listeners to
     * process the SWF. Use the following code to parse a SWF into a
     * <code>SWFDocument</code> instance:
     * 
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
    public class SWFReader
    {
        private InputBitStream bitStream;
        
        private List<SWFListener>  listeners = new List<SWFListener> ();
        
        private bool japanese;
        
        /** 
         * Creates a new SWF reader which reads from the specified stream.
         * 
         * @param stream
         *            the input stream the SWF file is read from
         */
        public SWFReader(Stream stream) 
        {
            this.bitStream = new InputBitStream(stream);
        }
        
        public virtual void SetJapanese(bool japanese)
        {
            this.japanese = japanese;
        }
        
        public virtual void AddListener(SWFListener listener)
        {
            listeners.Add(listener);
        }
        
        public virtual void Read() /* throws IOException */
        {
            PreProcess();
            SWFHeader header;
            try 
            {
                header = new SWFHeader(bitStream);
            }
            catch (Exception e) 
            {
                ProcessHeaderReadError(e);
                return ;
            }
            ProcessHeader(header);
            do 
            {
                if (((header.GetFileLength()) - (bitStream.GetOffset())) < 2) 
                {
                    break;
                } 
                TagHeader tagHeader = null;
                try 
                {
                    tagHeader = TagReader.ReadTagHeader(bitStream);
                }
                catch (Exception e) 
                {
                    ProcessTagHeaderReadError(e);
                    break;
                }
                ProcessTagHeader(tagHeader);
                Tag tag = null;
                byte[] tagData = null;
                try 
                {
                    tagData = TagReader.ReadTagData(bitStream, tagHeader);
                    tag = TagReader.ReadTag(tagHeader, tagData);
                    if ((tag.GetCode()) == (TagConstants.END)) 
                    {
                        break;
                    } 
                }
                catch (Exception e) 
                {
                    if (ProcessTagReadError(tagHeader, tagData, e)) 
                    {
                        IOException ioe = new IOException("Error while parsing SWF: " + e.Message);                        
                        throw ioe;
                    } 
                    tag = new MalformedTag(tagHeader , tagData , e);
                }
                ProcessTag(tag, bitStream.GetOffset());
            } while (true );
            PostProcess();
            try 
            {
                bitStream.Close();
            }
            catch (Exception) 
            {
            }
        }
        
        private void PostProcess()
        {
            foreach (SWFListener listener in listeners) 
            {
                listener.PostProcess();
            }
        }
        
        private void PreProcess()
        {
            foreach (SWFListener listener in listeners) 
            {
                listener.PreProcess();
            }
        }
        
        private void ProcessHeader(SWFHeader header)
        {
            foreach (SWFListener listener in listeners) 
            {
                listener.ProcessHeader(header);
            }
        }
        
        private void ProcessHeaderReadError(Exception e)
        {
            foreach (SWFListener listener in listeners) 
            {
                listener.ProcessHeaderReadError(e);
            }
        }
        
        private void ProcessTag(Tag tag, long streamOffset)
        {
            foreach (SWFListener listener in listeners) 
            {
                listener.ProcessTag(tag, streamOffset);
            }
        }
        
        private void ProcessTagHeader(TagHeader tagHeader)
        {
            foreach (SWFListener listener in listeners) 
            {
                listener.ProcessTagHeader(tagHeader);
            }
        }
        
        private void ProcessTagHeaderReadError(Exception e)
        {
            foreach (SWFListener listener in listeners) 
            {
                listener.ProcessTagHeaderReadError(e);
            }
        }
        
        private bool ProcessTagReadError(TagHeader tagHeader, byte[] tagData, Exception e)
        {
            bool result = false;
            foreach (SWFListener listener in listeners) 
            {
                result = (listener.ProcessTagReadError(tagHeader, tagData, e)) || result;
            }
            return result;
        }
    }
}