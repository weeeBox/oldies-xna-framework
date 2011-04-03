using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag is used as of SWF 8 to define SWF properties like access mode and
     * the presence of metadata. Do NOT add this tag to your
     * <code>SWFDocument</code>, use its <code>setAccessMode</code> and
     * <code>setMetadata</code> methods instead!
     *
     * @see com.jswiff.SWFDocument#setAccessMode(byte)
     * @see com.jswiff.SWFDocument#setMetadata(String)
     * @since SWF 8
     */
    public class FileAttributes : Tag
    {
        /** 
         *
         */
        private const int FLAG_RESERVED0 = 0;
        
        /** 
         * where such acceleration is available. If 0, the SWF file will not use hardware
         */
        private const int FLAG_USE_DIRECT_BLIT = 1;
        
        /** 
         * acceleration is available. If 0, the SWF file will not use hardware accelerated 
         */
        private const int FLAG_USE_USE_GPU = 2;
        
        /** 
         *
         */
        private const int FLAG_HAS_METADATA = 3;
        
        /** 
         *
         */
        private const int FLAG_ACTION_SCRIPT3 = 4;
        
        /** 
         *
         */
        private const int FLAG_RESERVED1 = 5;
        
        /** 
         *
         */
        private const int FLAG_RESERVED2 = 6;
        
        /** 
         *
         */
        private const int FLAG_USE_NETWORK = 7;
        
        /** 
         *
         */
        private const int FLAGS_COUNT = 8;
        
        private bool[] flagsArray;
        
        /** 
         * Creates a new FileAttributes instance.
         */
        public FileAttributes() 
        {
            code = TagConstants.FILE_ATTRIBUTES;
            flagsArray = new bool[FLAGS_COUNT];
        }
        
        public virtual void SetAllowNetworkAccess(bool allowNetworkAccess)
        {
            SetFlag(FLAG_USE_NETWORK, allowNetworkAccess);
        }
        
        public virtual bool IsAllowNetworkAccess()
        {
            return HasFlag(FLAG_USE_NETWORK);
        }
        
        public virtual void SetHasMetadata(bool hasMetadata)
        {
            SetFlag(FLAG_HAS_METADATA, hasMetadata);
        }
        
        public virtual bool HasMetadata()
        {
            return HasFlag(FLAG_HAS_METADATA);
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            int flags = inStream.ReadSI32();
            for (int flagIndex = 0; flagIndex < (FLAGS_COUNT); flagIndex++) 
            {
                flagsArray[flagIndex] = (flags & (1 << flagIndex)) != 0;
            }
        }
        
        public virtual bool IsUseDirectBlit()
        {
            return HasFlag(FLAG_USE_DIRECT_BLIT);
        }
        
        public virtual void SetUseDirectBlit(bool flag)
        {
            SetFlag(FLAG_USE_DIRECT_BLIT, flag);
        }
        
        public virtual bool IsUseGPU()
        {
            return HasFlag(FLAG_USE_USE_GPU);
        }
        
        public virtual void SetUseGPU(bool flag)
        {
            SetFlag(FLAG_USE_USE_GPU, flag);
        }
        
        public virtual bool IsActionScript3()
        {
            return HasFlag(FLAG_ACTION_SCRIPT3);
        }
        
        public virtual void SetActionScript3(bool flag)
        {
            SetFlag(FLAG_ACTION_SCRIPT3, flag);
        }
        
        private bool HasFlag(int flagIndex)
        {
            System.Diagnostics.Debug.Assert((flagIndex >= 0) && (flagIndex < (FLAGS_COUNT)), (flagIndex + "<") + (FLAGS_COUNT));
            return flagsArray[flagIndex];
        }
        
        private void SetFlag(int flagIndex, bool value)
        {
            System.Diagnostics.Debug.Assert((flagIndex >= 0) && (flagIndex < (FLAGS_COUNT)), (flagIndex + "<") + (FLAGS_COUNT));
            flagsArray[flagIndex] = value;
        }
    }
}