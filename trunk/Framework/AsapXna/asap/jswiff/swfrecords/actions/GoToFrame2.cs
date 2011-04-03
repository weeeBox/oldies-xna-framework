using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Instructs Flash Player to go to the specified frame.
     * </p>
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop frame</code> (frame number or label)<br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>gotoAndPlay(), gotoAndStop()</code>
     * </p>
     *
     * @since SWF 4
     */
    public class GoToFrame2 : ActionRecord
    {
        private bool play;
        
        private int sceneBias;
        
        /** 
         * Creates a new GoToFrame2 action. If the <code>play</code> flag is set, the
         * movie starts playing at the specified frame, otherwise it stops at that
         * frame. The <code>sceneBias</code> parameter is a non-negative offset to
         * be added to the specified frame.
         *
         * @param play play flag
         * @param sceneBias offset added to target frame
         */
        public GoToFrame2(bool play ,int sceneBias) 
        {
            code = ActionConstants.GO_TO_FRAME_2;
            this.play = play;
            this.sceneBias = sceneBias;
        }
        
        public GoToFrame2(InputBitStream stream) /* throws IOException */ 
        {
            code = ActionConstants.GO_TO_FRAME_2;
            short bits = stream.ReadUI8();
            bool sceneBiasFlag = (bits & 2) != 0;
            play = (bits & 1) != 0;
            if (sceneBiasFlag) 
            {
                sceneBias = stream.ReadUI16();
            } 
        }
        
        public virtual int GetSceneBias()
        {
            return sceneBias;
        }
        
        public override int GetSize()
        {
            int size = 4;
            if ((sceneBias) > 0) 
            {
                size += 2;
            } 
            return size;
        }
        
        public virtual bool Play()
        {
            return play;
        }
        
        public override String ToString()
        {
            String result = "GoToFrame2  play: " + (play);
            if ((sceneBias) > 0) 
            {
                result += " sceneBias: " + (sceneBias);
            } 
            return result;
        }
    }
}