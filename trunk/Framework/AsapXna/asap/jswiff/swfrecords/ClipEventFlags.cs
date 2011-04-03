using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class implements a container for event flags. Used in
     * <code>ClipActions</code> and <code>ClipActionRecord</code> to indicate
     * which events to react upon.
     *
     * @see ClipActions
     * @see ClipActionRecord
     */
    public class ClipEventFlags
    {
        private bool keyUp;
        
        private bool keyDown;
        
        private bool mouseUp;
        
        private bool mouseDown;
        
        private bool mouseMove;
        
        private bool unload;
        
        private bool enterFrame;
        
        private bool load;
        
        private bool dragOver;
        
        private bool rollOut;
        
        private bool rollOver;
        
        private bool releaseOutside;
        
        private bool release;
        
        private bool press;
        
        private bool initialize;
        
        private bool data;
        
        private bool construct;
        
        private bool keyPress;
        
        private bool dragOut;
        
        /** 
         * Creates a new ClipEventFlags instance.
         */
        public ClipEventFlags() 
        {
        }
        
        /** 
         * Creates a new ClipEventFlags instance, reading data from a bit stream.
         *
         * @param stream source bit stream
         * @param swfVersion SWF version
         *
         * @throws IOException if an I/O error has occured
         */
        public ClipEventFlags(InputBitStream stream ,short swfVersion) /* throws IOException */ 
        {
            keyUp = stream.ReadBooleanBit();
            keyDown = stream.ReadBooleanBit();
            mouseUp = stream.ReadBooleanBit();
            mouseDown = stream.ReadBooleanBit();
            mouseMove = stream.ReadBooleanBit();
            unload = stream.ReadBooleanBit();
            enterFrame = stream.ReadBooleanBit();
            load = stream.ReadBooleanBit();
            dragOver = stream.ReadBooleanBit();
            rollOut = stream.ReadBooleanBit();
            rollOver = stream.ReadBooleanBit();
            releaseOutside = stream.ReadBooleanBit();
            release = stream.ReadBooleanBit();
            press = stream.ReadBooleanBit();
            initialize = stream.ReadBooleanBit();
            data = stream.ReadBooleanBit();
            if (swfVersion >= 6) 
            {
                stream.ReadUnsignedBits(5);
                construct = stream.ReadBooleanBit();
                keyPress = stream.ReadBooleanBit();
                dragOut = stream.ReadBooleanBit();
                stream.ReadUnsignedBits(8);
            } 
        }
        
        public virtual void SetConstruct()
        {
            construct = true;
        }
        
        public virtual bool IsConstruct()
        {
            return construct;
        }
        
        public virtual void SetData()
        {
            data = true;
        }
        
        public virtual bool IsData()
        {
            return data;
        }
        
        public virtual void SetDragOut()
        {
            dragOut = true;
        }
        
        public virtual bool IsDragOut()
        {
            return dragOut;
        }
        
        public virtual void SetDragOver()
        {
            dragOver = true;
        }
        
        public virtual bool IsDragOver()
        {
            return dragOver;
        }
        
        public virtual void SetEnterFrame()
        {
            enterFrame = true;
        }
        
        public virtual bool IsEnterFrame()
        {
            return enterFrame;
        }
        
        public virtual void SetInitialize()
        {
            initialize = true;
        }
        
        public virtual bool IsInitialize()
        {
            return initialize;
        }
        
        public virtual void SetKeyDown()
        {
            keyDown = true;
        }
        
        public virtual bool IsKeyDown()
        {
            return keyDown;
        }
        
        public virtual void SetKeyPress()
        {
            keyPress = true;
        }
        
        public virtual bool IsKeyPress()
        {
            return keyPress;
        }
        
        public virtual void SetKeyUp()
        {
            keyUp = true;
        }
        
        public virtual bool IsKeyUp()
        {
            return keyUp;
        }
        
        public virtual void SetLoad()
        {
            load = true;
        }
        
        public virtual bool IsLoad()
        {
            return load;
        }
        
        public virtual void SetMouseDown()
        {
            mouseDown = true;
        }
        
        public virtual bool IsMouseDown()
        {
            return mouseDown;
        }
        
        public virtual void SetMouseMove()
        {
            mouseMove = true;
        }
        
        public virtual bool IsMouseMove()
        {
            return mouseMove;
        }
        
        public virtual void SetMouseUp()
        {
            mouseUp = true;
        }
        
        public virtual bool IsMouseUp()
        {
            return mouseUp;
        }
        
        public virtual void SetPress()
        {
            press = true;
        }
        
        public virtual bool IsPress()
        {
            return press;
        }
        
        public virtual void SetRelease()
        {
            release = true;
        }
        
        public virtual bool IsRelease()
        {
            return release;
        }
        
        public virtual void SetReleaseOutside()
        {
            releaseOutside = true;
        }
        
        public virtual bool IsReleaseOutside()
        {
            return releaseOutside;
        }
        
        public virtual void SetRollOut()
        {
            rollOut = true;
        }
        
        public virtual bool IsRollOut()
        {
            return rollOut;
        }
        
        public virtual void SetRollOver()
        {
            rollOver = true;
        }
        
        public virtual bool IsRollOver()
        {
            return rollOver;
        }
        
        public virtual void SetUnload()
        {
            unload = true;
        }
        
        public virtual bool IsUnload()
        {
            return unload;
        }        
    }
}