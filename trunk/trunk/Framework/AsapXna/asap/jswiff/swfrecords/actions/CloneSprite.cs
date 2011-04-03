using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Duplicates a sprite, creating a new sprite instance at a given depth.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code> pop depth </code>(depth at which the clone sprite will be created)<br>
     * <code> pop name</code> (instance name of the clone)<br>
     * <code> pop sprite</code> (sprite to be cloned)<br>
     * </p>
     * 
     * <p>
     * Note: use values between 16384 and 1064959 for <code>depth</code>, as this
     * range is reserved for dynamic use (otherwise - among other problems - you
     * won't be able to remove the created sprite).
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>duplicateMovieClip()</code>. The Macromedia
     * Flash compiler internally adds 16384 for convenience to the depth passed as
     * parameter.
     * </p>
     *
     * @since SWF 4
     */
    public class CloneSprite : ActionRecord
    {
        /** 
         * Creates a new CloneSprite action.
         */
        public CloneSprite() 
        {
            code = ActionConstants.CLONE_SPRITE;
        }
        
        public override String ToString()
        {
            return "CloneSprite";
        }
    }
}