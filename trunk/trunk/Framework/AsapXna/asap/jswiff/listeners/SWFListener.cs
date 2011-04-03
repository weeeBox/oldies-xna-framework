using System;

using System.Collections.Generic;


using swiff.com.jswiff.swfrecords.tags;
using swiff.com.jswiff.swfrecords;
using System.Diagnostics;

namespace swiff.com.jswiff.listeners
{
    /** 
     * Base class for SWF listeners, which can be passed to an
     * <code>SWFReader</code>, offering a flexible way to define it's behaviour
     * before, during and after the parsing process.
     * 
     * @see com.jswiff.SWFReader
     */
    abstract public class SWFListener
    {
        public virtual void PostProcess()
        {
        }
        
        public virtual void PreProcess()
        {
        }
        
        public virtual void ProcessHeader(SWFHeader header)
        {
        }
        
        public virtual void ProcessHeaderReadError(Exception e)
        {
            Debug.WriteLine(e.Message + "\n" + e.StackTrace);
        }
        
        public virtual void ProcessTag(Tag tag, long streamOffset)
        {
        }
        
        public virtual void ProcessTagHeader(TagHeader tagHeader)
        {
        }
        
        public virtual void ProcessTagHeaderReadError(Exception e)
        {
            Debug.WriteLine(e.Message + "\n" + e.StackTrace);
        }
        
        public virtual bool ProcessTagReadError(TagHeader tagHeader, byte[] tagData, Exception e)
        {
            Debug.WriteLine((((("Malformed tag (code: " + (tagHeader.GetCode())) + ", length: ") + (tagHeader.GetLength())) + ")"));
            Debug.WriteLine(e.Message + "\n" + e.StackTrace);
            return true;
        }
    }
}