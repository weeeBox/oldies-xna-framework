using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag is used to advise SWF editors to restrict edition capabilities for
     * the current SWF ducument. The behavior depends on the editor's
     * implementation.
     * </p>
     * 
     * <p>
     * Upon encountering a <code>Protect</code> tag, the Macromedia Flash authoring
     * environment checks if a password is contained in this tag. If the password
     * is missing, the file import is disallowed. Otherwise, file import is
     * allowed if the correct password is specified.
     * </p>
     * 
     * <p>
     * <b>Warning:</b> This tag is advisory only. Since editors might choose to
     * ignore it, this is not an appropriate way of protecting sensitive data.
     * </p>
     * 
     * <p>
     * <b>Warning:</b> the main purpose of this tag is to mark the file as being
     * copyrighted in a way or another. Importing the file regardless of this tag
     * may be considered by the file's author as reverse engineering and copyright
     * violation.
     * </p>
     *
     * @since SWF 2
     */
    public class Protect : Tag
    {
        private String password;
        
        /** 
         * Creates a new Protect tag. Supply a password encrypted with the MD5
         * algorithm.
         *
         * @param password MD5-encrypted password
         */
        public Protect(String password) 
        {
            code = TagConstants.PROTECT;
            this.password = password;
        }
        
        public Protect() 
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