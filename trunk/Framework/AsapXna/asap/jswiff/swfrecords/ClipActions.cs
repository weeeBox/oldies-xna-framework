using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used for defining event handlers for sprites. Used within the
     * <code>PlaceObject2</code> tag.
     * 
     * @see com.jswiff.swfrecords.tags.PlaceObject2
     */
    public class ClipActions
    {
        private ClipEventFlags eventFlags;
        
        private List<ClipActionRecord>  clipActionRecords = new List<ClipActionRecord> ();
        
        /** 
         * Creates a new ClipActions instance. Supply event flags and handlers.
         * 
         * @param eventFlags
         *            all events used in the clip actions
         * @param clipActionRecords
         *            list of one or more event handlers (
         *            <code>ClipActionRecord</code> instances)
         * 
         * @see ClipActionRecord
         */
        public ClipActions(ClipEventFlags eventFlags ,List<ClipActionRecord>  clipActionRecords) 
        {
            this.eventFlags = eventFlags;
            this.clipActionRecords = clipActionRecords;
        }
        
        /** 
         * Creates a new ClipActions instance, reading data from a bit stream.
         * 
         * @param stream
         *            source bit stream
         * @param swfVersion
         *            swf version used
         * 
         * @throws IOException
         *             if an I/O error has occured
         */
        public ClipActions(InputBitStream stream ,short swfVersion) /* throws IOException */ 
        {
            stream.ReadUI16();
            eventFlags = new ClipEventFlags(stream , swfVersion);
            while (true) 
            {
                int available = stream.Available();
                if (((swfVersion <= 5) && (available == 2)) || ((swfVersion > 5) && (available == 4))) 
                {
                    break;
                } 
                ClipActionRecord record = new ClipActionRecord(stream , swfVersion);
                clipActionRecords.Add(record);
            }
        }
        
        public virtual List<ClipActionRecord>  GetClipActionRecords()
        {
            return clipActionRecords;
        }
        
        public virtual ClipEventFlags GetEventFlags()
        {
            return eventFlags;
        }        
    }
}