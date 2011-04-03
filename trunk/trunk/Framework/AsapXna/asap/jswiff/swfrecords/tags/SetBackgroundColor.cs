using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag contains the background color of the SWF. Do NOT add this tag to
     * your <code>SWFDocument</code>, use its <code>setBackgroundColor</code>
     * method instead!
     *
     * @see com.jswiff.SWFDocument#setBackgroundColor(RGB)
     * @since SWF 1
     */
    public class SetBackgroundColor : Tag
    {
        private RGB color;
        
        /** 
         * Creates a new SetBackgroundColor tag.
         *
         * @param color background color
         */
        public SetBackgroundColor(RGB color) 
        {
            code = TagConstants.SET_BACKGROUND_COLOR;
            this.color = color;
        }
        
        public SetBackgroundColor() 
        {
        }
        
        public virtual RGB GetColor()
        {
            return color;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            color = new RGB(inStream);
        }
    }
}