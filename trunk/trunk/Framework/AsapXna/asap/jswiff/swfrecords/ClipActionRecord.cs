using System;

using System.Collections.Generic;


using swiff.com.jswiff.swfrecords.actions;
using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class defines an event handler for a sprite. Used within
     * <code>ClipActions</code>.
     *
     * @see ClipActions
     */
    public class ClipActionRecord
    {
        private ClipEventFlags eventFlags;
        
        private short keyCode;
        
        private ActionBlock actions;
        
        /** 
         * Creates a new ClipActionRecord instance.
         *
         * @param eventFlags event flags defining the events this handler is supposed
         *        to react upon.
         */
        public ClipActionRecord(ClipEventFlags eventFlags) 
        {
            this.eventFlags = eventFlags;
            actions = new ActionBlock();
        }
        
        /** 
         * Creates a new ClipActionRecord instance, reading data from a bit stream.
         *
         * @param stream source bit stream
         * @param swfVersion SWF version
         *
         * @throws IOException if an I/O error has occured
         */
        public ClipActionRecord(InputBitStream stream ,short swfVersion) /* throws IOException */ 
        {
            eventFlags = new ClipEventFlags(stream , swfVersion);
            int actionRecordSize = ((int)(stream.ReadUI32()));
            if ((swfVersion >= 6) && (eventFlags.IsKeyPress())) 
            {
                keyCode = stream.ReadUI8();
                actionRecordSize--;
            } 
            InputBitStream actionStream = new InputBitStream(stream.ReadBytes(actionRecordSize));
            actionStream.SetANSI(stream.IsANSI());
            actionStream.SetShiftJIS(stream.IsShiftJIS());
            actions = new ActionBlock(actionStream);
        }
        
        public virtual ActionBlock GetActions()
        {
            return actions;
        }
        
        public virtual ClipEventFlags GetEventFlags()
        {
            return eventFlags;
        }
        
        public virtual void SetKeyCode(short keyCode)
        {
            this.keyCode = keyCode;
        }
        
        public virtual short GetKeyCode()
        {
            return keyCode;
        }        
    }
}