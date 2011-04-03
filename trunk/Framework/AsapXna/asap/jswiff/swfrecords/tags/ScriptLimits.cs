using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag is used to override the default values for maximum recursion depth
     * (i.e. how often a function can call itself) and timeout regarding the
     * execution of actions.
     *
     * @since SWF 7
     */
    public class ScriptLimits : Tag
    {
        private int maxRecursionDepth;
        
        private int scriptTimeoutSeconds;
        
        /** 
         * Creates a new ScriptLimits tag. Supply the maximum recursion depth and
         * the timeout in seconds.
         *
         * @param maxRecursionDepth maximum recursion depth (at most 65535)
         * @param scriptTimeoutSeconds timeout in seconds
         */
        public ScriptLimits(int maxRecursionDepth ,int scriptTimeoutSeconds) 
        {
            code = TagConstants.SCRIPT_LIMITS;
            this.maxRecursionDepth = maxRecursionDepth;
            this.scriptTimeoutSeconds = scriptTimeoutSeconds;
        }
        
        public ScriptLimits() 
        {
        }
        
        public virtual void SetMaxRecursionDepth(int maxRecursionDepth)
        {
            this.maxRecursionDepth = maxRecursionDepth;
        }
        
        public virtual int GetMaxRecursionDepth()
        {
            return maxRecursionDepth;
        }
        
        public virtual void SetScriptTimeoutSeconds(int scriptTimeoutSeconds)
        {
            this.scriptTimeoutSeconds = scriptTimeoutSeconds;
        }
        
        public virtual int GetScriptTimeoutSeconds()
        {
            return scriptTimeoutSeconds;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            maxRecursionDepth = inStream.ReadUI16();
            scriptTimeoutSeconds = inStream.ReadUI16();
        }
    }
}