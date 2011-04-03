using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag enables debugging within the Macromedia Flash authoring tool. A
     * password encrypted with the MD5 algorithm has to be supplied.
     * </p>
     * 
     * <p>
     * Note: the format of the debugging information required in the ActionScript
     * debugger has changed with version 6. In SWF 6 or later, this tag is used
     * instead of <code>EnableDebugger</code>.
     * </p>
     *
     * @since SWF 6
     */
    public class EnableDebugger2 : Tag
    {
        private String password;
        
        /** 
         * Creates a new EnableDebugger instance. Supply a password encrypted with
         * the MD5 algorithm.
         *
         * @param password MD5 encrypted password
         */
        public EnableDebugger2(String password) 
        {
            code = TagConstants.ENABLE_DEBUGGER_2;
            this.password = password;
        }
        
        public EnableDebugger2() 
        {
        }
        
        public virtual void SetPassword(String password)
        {
            this.password = password;
        }
        
        public virtual String GetPassword()
        {
            return password;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            if ((data.Length) > 3) 
            {
                password = System.Text.Encoding.UTF8.GetString(data, 2, data.Length - 3);                
            } 
        }
    }
}