using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag assigns a certain name to the current frame. This name can be used
     * by <code>GoToLabel</code> to identify this frame.
     * </p>
     * 
     * <p>
     * As of SWF 6, labels can additionally be defined as named anchors for better
     * browser integration. Named anchors are similar to HTML anchors (i.e.
     * fragment identifiers, as specified in RFC 2396). If the named anchor is
     * supplied at the end of the SWF file's URL (like
     * <code>http://servername/filename.swf#named_anchor</code>) in the browser,
     * the Flash Player plugin starts playback at the frame labeled as
     * <code>named_anchor</code>. Additionally, if the Flash Player plugin
     * encounters a frame containing a named anchor during playback of an SWF, it
     * adds the anchor to the URL of the HTML page embedding the SWF in the
     * address bar (or updates it if an anchor is already there), so the frame can
     * be bookmarked and the browser's "back" and "forward" buttons can be used
     * for navigation.
     * </p>
     *
     * @since SWF 3 (named anchors since SWF 6)
     */
    public class FrameLabel : Tag
    {
        private String name;
        
        private bool isNamedAnchor;
        
        /** 
         * Creates a new FrameLabel tag.
         *
         * @param name label name
         * @param isNamedAnchor set to <code>true</code> if label is named anchor,
         * 		  otherwise <code>false</code>
         */
        public FrameLabel(String name ,bool isNamedAnchor) 
        {
            code = TagConstants.FRAME_LABEL;
            this.name = name;
            this.isNamedAnchor = isNamedAnchor;
        }
        
        public FrameLabel() 
        {
        }
        
        public virtual void SetName(String name)
        {
            this.name = name;
        }
        
        public virtual String GetName()
        {
            return name;
        }
        
        public virtual void SetNamedAnchor(bool isNamedAnchor)
        {
            this.isNamedAnchor = isNamedAnchor;
        }
        
        public virtual bool IsNamedAnchor()
        {
            return isNamedAnchor;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            if ((GetSWFVersion()) < 6) 
            {
                if (IsJapanese()) 
                {
                    inStream.SetShiftJIS(true);
                } 
                else 
                {
                    inStream.SetANSI(true);
                }
            } 
            name = inStream.ReadString();
            if (((GetSWFVersion()) >= 6) && ((inStream.Available()) > 0)) 
            {
                isNamedAnchor = (inStream.ReadUI8()) != 0;
            } 
        }
    }
}