using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * This action makes the target sprite draggable, i.e. it makes sure users can
     * drag it to another location.
     * </p>
     * 
     * <p>
     * Note: Only one sprite can be dragged at a time. The sprite remains draggable
     * until it is explicitly stopped by <code>ActionStopDrag</code> or until
     * <code>StartDrag</code> is called for another sprite.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop sprite</code> (the sprite to be dragged)<br>
     * <code>pop lockCenter</code> (if nonzero, mouse is locked to sprite center,
     * otherwise it is locked to the mouse position at the time the dragging started)<br>
     * <code>pop constrain</code> (if nonzero, four values which define a
     * constraint window are popped off the stack)<br>
     * <code>pop y2</code> (bottom constraint coordinate)<br>
     * <code>pop x2</code> (right constraint coordinate)<br>
     * <code>pop y1</code> (top constraint coordinate)<br>
     * <code>pop x1</code> (left constraint coordinate)<br>
     * Constraint values are relative to the coordinates of the sprite's parent.
     * </p>
     * 
     * <p>
     * ActionScript equivalents: <code>startDrag()</code>,
     * <code>MovieClip.startDrag()</code>
     * </p>
     *
     * @since SWF 5
     */
    public class StartDrag : ActionRecord
    {
        /** 
         * Creates a new StartDrag action.
         */
        public StartDrag() 
        {
            code = ActionConstants.START_DRAG;
        }
        
        public override String ToString()
        {
            return "StartDrag";
        }
    }
}