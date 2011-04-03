using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag is used as container for malformed tag data which could not be
     * interpreted. The exception thrown while parsing the malformed tag is also
     * contained herein and can be used for error tracing.
     */
    public class MalformedTag : Tag
    {
        private byte[] data;
        
        private TagHeader tagHeader;
        
        private Exception exception;
        
        /** 
         * Creates a new MalformedTag instance. It makes no sense to add MalformedTag
         * instances to a SWF document, as SWF writers don't write their contents.
         *
         * @param tagHeader tag header
         * @param data raw tag data
         * @param exception exception thrown while parsing the tag
         */
        public MalformedTag(TagHeader tagHeader ,byte[] data ,Exception exception) 
        {
            code = TagConstants.MALFORMED;
            this.tagHeader = tagHeader;
            this.data = data;
            this.exception = exception;
        }
        
        public virtual byte[] GetData()
        {
            return data;
        }
        
        public virtual Exception GetException()
        {
            return exception;
        }
        
        public virtual TagHeader GetTagHeader()
        {
            return tagHeader;
        }
        
        public override void SetData(byte[] data)
        {
            this.data = data;
        }
    }
}