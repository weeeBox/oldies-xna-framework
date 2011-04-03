using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used for time-based volume control within
     * <code>SoundInfo</code>. Defines the volume level for the left and right
     * channel, starting at a certain sound sample called the 'envelope point'.
     *
     * @see SoundInfo
     */
    public class SoundEnvelope
    {
        private long pos44;
        
        private int leftLevel;
        
        private int rightLevel;
        
        /** 
         * Creates a new SoundEnvelope instance. Specify the position of the envelope
         * point within the sound as a number of 44 kHz samples (multiply
         * accordingly when using a lower sampling rate). Then supply the volume
         * level for the left and right channel. For mono sounds, use identical
         * values.
         *
         * @param pos44 envelope point in number of 44 kHz samples
         * @param leftLevel left volume level (between 0 and 32768)
         * @param rightLevel right volume level (between 0 and 32768)
         */
        public SoundEnvelope(long pos44 ,int leftLevel ,int rightLevel) 
        {
            this.pos44 = pos44;
            this.leftLevel = leftLevel;
            this.rightLevel = rightLevel;
        }
        
        /** 
         * Creates a new SoundEnvelope instance, reading data from a bit stream.
         *
         * @param stream source bit stream
         *
         * @throws IOException if an I/O error occured
         */
        public SoundEnvelope(InputBitStream stream) /* throws IOException */ 
        {
            pos44 = stream.ReadUI32();
            leftLevel = stream.ReadUI16();
            rightLevel = stream.ReadUI16();
        }
        
        public virtual int GetLeftLevel()
        {
            return leftLevel;
        }
        
        public virtual long GetPos44()
        {
            return pos44;
        }
        
        public virtual int GetRightLevel()
        {
            return rightLevel;
        }
    }
}