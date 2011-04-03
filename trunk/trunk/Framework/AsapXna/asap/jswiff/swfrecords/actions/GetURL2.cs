using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * Gets contents from an URL or exchanges data with a server.
     * 
     * <p>
     * Performed stack operations:<br>
     * <code>pop target</code> (see <code>GetURL</code>)<br>
     * <code>pop url</code><br>
     * </p>
     * 
     * <p>
     * ActionScript equivalent: <code>getURL(), loadMovie(), loadMovieNum(),
     * loadVariables()</code>
     * </p>
     *
     * @see GetURL
     * @since SWF 4
     */
    public class GetURL2 : ActionRecord
    {
        /** 
         *
         */
        public const byte METHOD_NONE = 0;
        
        /** 
         *
         */
        public const byte METHOD_GET = 1;
        
        /** 
         *
         */
        public const byte METHOD_POST = 2;
        
        private byte sendVarsMethod;
        
        private bool loadTarget;
        
        private bool loadVariables;
        
        /** 
         * Creates a new GetURL2 action.
         *
         * @param sendVarsMethod HTTP request method (<code>METHOD_NONE,
         *        METHOD_GET</code> or <code>METHOD_POST</code>)
         * @param loadTarget <code>false</code> if target is a browser frame,
         *        <code>true</code> if it is a path to a clip (in slash syntax -
         *        /parentClip/childClip - or dot syntax - parentClip.childClip)
         * @param loadVariables if <code>true</code>, the server is expected to
         *        respond with an url encoded set of variables
         */
        public GetURL2(byte sendVarsMethod ,bool loadTarget ,bool loadVariables) 
        {
            code = ActionConstants.GET_URL_2;
            this.sendVarsMethod = sendVarsMethod;
            this.loadTarget = loadTarget;
            this.loadVariables = loadVariables;
        }
        
        public GetURL2(InputBitStream stream) /* throws IOException */ 
        {
            code = ActionConstants.GET_URL_2;
            loadVariables = stream.ReadBooleanBit();
            loadTarget = stream.ReadBooleanBit();
            stream.ReadUnsignedBits(4);
            sendVarsMethod = unchecked((byte)(stream.ReadUnsignedBits(2)));
        }
        
        public virtual bool IsLoadTarget()
        {
            return loadTarget;
        }
        
        public virtual bool IsLoadVariables()
        {
            return loadVariables;
        }
        
        public virtual byte GetSendVarsMethod()
        {
            return sendVarsMethod;
        }
        
        public override int GetSize()
        {
            return 4;
        }
        
        public override String ToString()
        {
            String result = "GetURL2 sendVarsMethod: ";
            switch (sendVarsMethod)
            {
                case METHOD_GET:
                    result += "GET";
                    break;
                case METHOD_POST:
                    result += "POST";
                    break;
                default:
                    result += "none";
                    break;
            }
            result += ((" loadTarget: " + (loadTarget)) + " loadVariables: ") + (loadVariables);
            return result;
        }        
    }
}