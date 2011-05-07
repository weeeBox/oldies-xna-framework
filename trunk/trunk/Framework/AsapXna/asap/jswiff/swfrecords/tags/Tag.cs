using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * Base class for SWF tags.
     */
    abstract public class Tag
    {
        protected bool forceLongHeader;
        
        protected short code;
        
        protected int Length;        
        
        private short swfVersion = 7;
        
        protected bool shiftJIS;
        
        public virtual int GetCode()
        {
            return code;
        }        
        
        public virtual void SetCode(short code)
        {
            this.code = code;
        }
        
        public abstract void SetData(byte[] data) /* throws IOException */;
        
        public virtual void SetSWFVersion(short swfVersion)
        {
            this.swfVersion = swfVersion;
        }
        
        public virtual short GetSWFVersion()
        {
            return swfVersion;
        }
        
        public virtual void SetJapanese(bool shiftJIS)
        {
            this.shiftJIS = shiftJIS;
        }
        
        public virtual bool IsJapanese()
        {
            return shiftJIS;
        }
        
        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = (prime * result) + (code);
            result = (prime * result) + (Length);
            return result;
        }
        
        public override bool Equals(Object obj)
        {
            if ((this) == obj)
                return true;
            
            if (obj == null)
                return false;
            
            if ((GetType()) != (obj.GetType()))
                return false;
            
            Tag other = ((Tag)(obj));
            if ((code) != (other.code))
                return false;
            
            if ((Length) != (other.Length))
                return false;
            
            return true;
        }
        
        public override String ToString()
        {
            return GetType().Name;
        }
    }
}