using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * <p>
     * Instructs Flash Player to get a specified URL (e.g. an HTML file, an image
     * or another SWF movie) and to display it using a particular target (either a
     * browser frame or a level in Flash Player).
     * </p>
     * 
     * <p>
     * Several protocols are supported:
     * 
     * <ul>
     * <li>
     * conventional internet protocols (<code>http, https, ftp, mailto,
     * telnet</code>)
     * </li>
     * <li>
     * <code>file:///drive:/filename</code> for local file access
     * </li>
     * <li>
     * <code>print</code> used for printing a movie clip
     * </li>
     * <li>
     * <code>javascript</code> and  <code>vbscript</code> to execute script code in
     * the browser
     * </li>
     * <li>
     * <code>event</code> and <code>lingo</code> for Macromedia Director
     * interaction
     * </li>
     * </ul>
     * </p>
     * 
     * <p>
     * Usually, the specified target directs the URL content to a particular
     * browser frame (e.g. <code>_self</code>, <code>_parent</code>,
     * <code>_blank</code>). If the URL points to an SWF, the target can be a
     * string specifying the name of a movie clip instance or a document level
     * (e.g. <code>_level1</code>).
     * </p>
     * 
     * <p>
     * Performed stack operations: none
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>getURL(), loadMovie()</code> operator
     * </p>
     *
     * @since SWF 3
     */
    public class GetURL : ActionRecord
    {
        private String url;
        
        private String target;
        
        /** 
         * Creates a new GetURL action. The <code>url</code> content will be
         * displayed at the specified <code>target</code>.
         *
         * @param url the URL to be loaded
         * @param target the target used to display the URL
         */
        public GetURL(String url ,String target) 
        {
            code = ActionConstants.GET_URL;
            this.url = url;
            this.target = target;
        }
        
        public GetURL(InputBitStream stream) /* throws IOException */ 
        {
            code = ActionConstants.GET_URL;
            url = stream.ReadString();
            target = stream.ReadString();
        }
        
        public override int GetSize()
        {
            int size = 5;            
            size += (System.Text.Encoding.UTF8.GetBytes(url).Length) + (System.Text.Encoding.UTF8.GetBytes(target).Length);            
            return size;
        }
        
        public virtual String GetTarget()
        {
            return target;
        }
        
        public virtual String GetURLString()
        {
            return url;
        }
        
        public override String ToString()
        {
            return ((("GetURL url: \'" + (url)) + "\' target: \'") + (target)) + "\'";
        }        
    }
}