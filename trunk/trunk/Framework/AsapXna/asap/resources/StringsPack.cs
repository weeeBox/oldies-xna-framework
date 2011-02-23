using System;

using System.Collections.Generic;
using System.Diagnostics;



namespace asap.resources
{
    /** 
     * Provides strings set that can be loaded and accessed by id.
     * @see StrRes - container for strings packs.
     */
    public class StringsPack
     {
        private String[] strings;
        
        private int idBase;
        
        //public virtual void Load(InputStream _is)
        //{
        //    try 
        //    {
        //        DataInputStream dis = new DataInputStream(_is);
        //        int strCount = dis.ReadShort();
        //        strings = new String[strCount];
        //        idBase = dis.ReadShort();
        //        for (int i = 0; i < strCount; i++) 
        //        {
        //            strings[i] = dis.ReadUTF();
        //        }
        //        dis.Close();
        //    }
        //    catch (Exception e) 
        //    {
        //        Debug.Assert(false, "Can\'t load strings pack!");
        //    }
        //}
        
        public virtual int GetCount()
        {
            Debug.Assert((strings) != null);
            return strings.Length;
        }
        
        public virtual int GetIdBase()
        {
            Debug.Assert((strings) != null);
            return idBase;
        }
        
        public virtual String GetStr(int strId)
        {
            Debug.Assert((strings) != null);
            Debug.Assert((strId >= (idBase)) && (strId < ((idBase) + (strings.Length))));
            return strings[(strId - (idBase))];
        }
        
    }
    
    
}