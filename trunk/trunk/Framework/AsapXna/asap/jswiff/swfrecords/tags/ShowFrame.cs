using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag instructs Flash Player to display all characters added  (either
     * with <code>PlaceObject</code> or <code>PlaceObject2</code>) to the display
     * list. The display list is cleared, and the movie is paused for the duration
     * of a single frame (which is the reciprocal of the SWF frame rate).
     *
     * @since SWF 1
     */
    public class ShowFrame : Tag
    {
        /** 
         * Creates a new ShowFrame tag.
         */
        public ShowFrame() 
        {
            code = TagConstants.SHOW_FRAME;
        }
        
        public override void SetData(byte[] data)
        {
        }
    }
}