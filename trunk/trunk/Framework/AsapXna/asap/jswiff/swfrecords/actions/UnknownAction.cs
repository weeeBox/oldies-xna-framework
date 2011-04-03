using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.actions
{
    /** 
     * This class implements a placeholder for unknown action records.
     */
    public class UnknownAction : ActionRecord
    {
        private InputBitStream inStream;
        
        private byte[] actionData;
        
        /** 
         * Creates a new UnknownAction instance.
         *
         * @param code action code indicationg the action type
         * @param data data contained in the action record
         */
        public UnknownAction(short code ,byte[] data) 
        {
            this.code = code;
            actionData = data;
        }
        
        public UnknownAction(InputBitStream stream ,short code) /* throws IOException */ 
        {
            inStream = stream;
            this.code = code;
            if ((inStream) != null) 
            {
                actionData = inStream.ReadBytes(inStream.Available());
            } 
            else 
            {
                actionData = new byte[0];
            }
        }
        
        public virtual byte[] GetData()
        {
            return actionData;
        }
        
        public override String ToString()
        {
            return ((("Unknown action (code: " + (GetCode())) + ", length: ") + (actionData.Length)) + ")";
        }
    }
}