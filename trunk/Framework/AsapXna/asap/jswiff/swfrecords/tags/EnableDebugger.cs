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
     * Note: Flash Player 6 or later will ignore this tag, since the format of the
     * debugging information required in the ActionScript debugger has changed
     * with version 6. In SWF 6 or later, <code>EnableDebugger2</code> is used
     * instead.
     * </p>
     *
     * @since SWF 5 (used only in SWF 5)
     */
    public class EnableDebugger : Tag
    {
        private String password;
        
        /** 
         * Creates a new EnableDebugger instance. Supply a password encrypted with
         * the MD5 algorithm.
         *
         * @param password MD5 encrypted password
         */
        public EnableDebugger(String password) 
        {
            code = TagConstants.ENABLE_DEBUGGER;
            this.password = password;
        }
        
        public EnableDebugger() 
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
            if ((data.Length) > 0) 
            {
                password = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
            } 
        }
    }
}