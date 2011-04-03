using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class provides information used for playing an event sound defined with
     * the <code>DefineSound</code> tag.
     *
     * @see com.jswiff.swfrecords.tags.DefineSound
     */
    public class SoundInfo
    {
        private bool syncStop;
        
        private bool syncNoMultiple;
        
        private long inPoint;
        
        private long outPoint;
        
        private int loopCount;
        
        private SoundEnvelope[] envelopeRecords;
        
        private bool hasEnvelope;
        
        private bool hasLoops;
        
        private bool hasOutPoint;
        
        private bool hasInPoint;
        
        /** 
         * Creates a new SoundInfo instance.
         */
        public SoundInfo() 
        {
        }
        
        /** 
         * Creates a new SoundInfo instance, reading data from a bit stream.
         *
         * @param stream target bit stream
         *
         * @throws IOException if an I/O error has occured
         */
        public SoundInfo(InputBitStream stream) /* throws IOException */ 
        {
            stream.ReadUnsignedBits(2);
            syncStop = stream.ReadBooleanBit();
            syncNoMultiple = stream.ReadBooleanBit();
            hasEnvelope = stream.ReadBooleanBit();
            hasLoops = stream.ReadBooleanBit();
            hasOutPoint = stream.ReadBooleanBit();
            hasInPoint = stream.ReadBooleanBit();
            if (hasInPoint) 
            {
                inPoint = stream.ReadUI32();
            } 
            if (hasOutPoint) 
            {
                outPoint = stream.ReadUI32();
            } 
            if (hasLoops) 
            {
                loopCount = stream.ReadUI16();
            } 
            if (hasEnvelope) 
            {
                short envPoints = stream.ReadUI8();
                envelopeRecords = new SoundEnvelope[envPoints];
                for (int i = 0; i < envPoints; i++) 
                {
                    envelopeRecords[i] = new SoundEnvelope(stream);
                }
            } 
        }
        
        public virtual void SetEnvelopeRecords(SoundEnvelope[] envelopeRecords)
        {
            this.envelopeRecords = envelopeRecords;
            if (envelopeRecords != null) 
            {
                hasEnvelope = true;
            } 
        }
        
        public virtual SoundEnvelope[] GetEnvelopeRecords()
        {
            return envelopeRecords;
        }
        
        public virtual void SetInPoint(long inPoint)
        {
            hasInPoint = true;
            this.inPoint = inPoint;
        }
        
        public virtual long GetInPoint()
        {
            return inPoint;
        }
        
        public virtual void SetLoopCount(int loopCount)
        {
            hasLoops = true;
            this.loopCount = loopCount;
        }
        
        public virtual int GetLoopCount()
        {
            return loopCount;
        }
        
        public virtual void SetOutPoint(long outPoint)
        {
            hasOutPoint = true;
            this.outPoint = outPoint;
        }
        
        public virtual long GetOutPoint()
        {
            return outPoint;
        }
        
        public virtual void SetSyncNoMultiple()
        {
            syncNoMultiple = true;
        }
        
        public virtual bool IsSyncNoMultiple()
        {
            return syncNoMultiple;
        }
        
        public virtual void SetSyncStop()
        {
            syncStop = true;
        }
        
        public virtual bool IsSyncStop()
        {
            return syncStop;
        }
        
        public virtual bool HasEnvelope()
        {
            return hasEnvelope;
        }
        
        public virtual bool HasInPoint()
        {
            return hasInPoint;
        }
        
        public virtual bool HasLoops()
        {
            return hasLoops;
        }
        
        public virtual bool HasOutPoint()
        {
            return hasOutPoint;
        }
    }
}