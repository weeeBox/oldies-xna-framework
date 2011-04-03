using System;

using System.Collections.Generic;


using swiff.com.jswiff.swfrecords.actions;
using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * <p>
     * This record is used to define the button's event handlers. An event handler
     * assigns an action block to a mouse or key event.
     * </p>
     * 
     * <p>
     * Mouse events are transitions between mouse states relative to the button.
     * There are four states:
     * 
     * <ul>
     * <li>
     * idle = mouse pointer outside button area, mouse key up
     * </li>
     * <li>
     * outDown = mouse pointer outside button area, mouse key down
     * </li>
     * <li>
     * overUp = mouse pointer inside button area, mouse key up
     * </li>
     * <li>
     * overDown = mouse pointer inside button area, mouse key down
     * </li>
     * </ul>
     * </p>
     * 
     * <p>
     * An event handler reacts to one or more mouse events if the corresponding
     * event flags are set with the appropriate setter methods (e.g. with
     * <code>setIdleToOverUp(true);</code>)
     * </p>
     * 
     * <p>
     * A key event occurs when a certain specified key is pressed. The key the
     * handler is supposed to react to is specified through
     * <code>setKeyPress()</code>. For special keys (like insert or escape), use
     * the <code>KEY_...</code> constants. For ASCII keys, use their ASCII codes.
     * </p>
     * 
     * <p>
     * A particular event handler can be defined to react to more than one mouse
     * event and to (at most) one key event.
     * </p>
     */
    public class ButtonCondAction
    {
        private bool outDownToIdle;
        
        private bool outDownToOverDown;
        
        private bool idleToOverDown;
        
        private bool overDownToOutDown;
        
        private bool overDownToIdle;
        
        private bool overUpToOverDown;
        
        private bool overDownToOverUp;
        
        private bool overUpToIdle;
        
        private bool idleToOverUp;
        
        private byte keyPress;
        
        private ActionBlock actions;
        
        /** 
         * Creates a new ButtonCondAction instance.
         */
        public ButtonCondAction() 
        {
            actions = new ActionBlock();
        }
        
        /** 
         * Reads a ButtonCondAction instance from a bit stream.
         *
         * @param stream the input bit stream
         *
         * @throws IOException if an I/O error has occured
         */
        public ButtonCondAction(InputBitStream stream) /* throws IOException */ 
        {
            idleToOverDown = stream.ReadBooleanBit();
            outDownToIdle = stream.ReadBooleanBit();
            outDownToOverDown = stream.ReadBooleanBit();
            overDownToOutDown = stream.ReadBooleanBit();
            overDownToOverUp = stream.ReadBooleanBit();
            overUpToOverDown = stream.ReadBooleanBit();
            overUpToIdle = stream.ReadBooleanBit();
            idleToOverUp = stream.ReadBooleanBit();
            keyPress = unchecked((byte)(stream.ReadUnsignedBits(7)));
            overDownToIdle = stream.ReadBooleanBit();
            actions = new ActionBlock(stream);
        }
        
        public virtual ActionBlock GetActions()
        {
            return actions;
        }
        
        public virtual void SetIdleToOverDown()
        {
            this.idleToOverDown = true;
        }
        
        public virtual bool IsIdleToOverDown()
        {
            return idleToOverDown;
        }
        
        public virtual void SetIdleToOverUp()
        {
            this.idleToOverUp = true;
        }
        
        public virtual bool IsIdleToOverUp()
        {
            return idleToOverUp;
        }
        
        public virtual void SetKeyPress(byte keyPress)
        {
            this.keyPress = keyPress;
        }
        
        public virtual byte GetKeyPress()
        {
            return keyPress;
        }
        
        public virtual void SetOutDownToIdle()
        {
            this.outDownToIdle = true;
        }
        
        public virtual bool IsOutDownToIdle()
        {
            return outDownToIdle;
        }
        
        public virtual void SetOutDownToOverDown()
        {
            this.outDownToOverDown = true;
        }
        
        public virtual bool IsOutDownToOverDown()
        {
            return outDownToOverDown;
        }
        
        public virtual void SetOverDownToIdle()
        {
            this.overDownToIdle = true;
        }
        
        public virtual bool IsOverDownToIdle()
        {
            return overDownToIdle;
        }
        
        public virtual void SetOverDownToOutDown()
        {
            this.overDownToOutDown = true;
        }
        
        public virtual bool IsOverDownToOutDown()
        {
            return overDownToOutDown;
        }
        
        public virtual void SetOverDownToOverUp()
        {
            this.overDownToOverUp = true;
        }
        
        public virtual bool IsOverDownToOverUp()
        {
            return overDownToOverUp;
        }
        
        public virtual void SetOverUpToIdle()
        {
            this.overUpToIdle = true;
        }
        
        public virtual bool IsOverUpToIdle()
        {
            return overUpToIdle;
        }
        
        public virtual void SetOverUpToOverDown()
        {
            this.overUpToOverDown = true;
        }
        
        public virtual bool IsOverUpToOverDown()
        {
            return overUpToOverDown;
        }        
    }
}