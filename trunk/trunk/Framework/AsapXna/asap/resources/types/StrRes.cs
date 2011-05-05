using System;

using System.Collections.Generic;
using System.Diagnostics;



namespace asap.resources
{
    /** 
     * Singleton to get string resources.
     * String id has format 0xAAAABBBB, where AAAA - id base (pack id) and BBBB - str index inside pack.
     * The class has short name to simplify access to strings.
     */
    public class StrRes
     {
        private static StrRes instance;
        
        private StringsPack[] packs;
        
        public StrRes(int maxIdBase) 
        {
            Debug.Assert((StrRes.instance) == null);
            Debug.Assert(maxIdBase > 0);
            StrRes.instance = this;
            packs = new StringsPack[maxIdBase];
        }
        
        public static StrRes GetInstance()
        {
            return StrRes.instance;
        }
        
        public virtual void AddPack(StringsPack pack)
        {
            int idBase = pack.GetIdBase();
            Debug.Assert((idBase >= 0) && (idBase < (packs.Length)));
            packs[idBase] = pack;
        }
        
        public virtual void DelPack(int idBase)
        {
            Debug.Assert((idBase >= 0) && (idBase < (packs.Length)));
            packs[idBase] = null;
        }
        
        public static String Get(int strId)
        {
            Debug.Assert((StrRes.instance) != null);
            int idBase = StrRes.GetIdBase(strId);
            Debug.Assert((idBase >= 0) && (idBase < (StrRes.instance.packs.Length)));
            return StrRes.instance.packs[idBase].GetStr(strId);
        }
        
        public static String Get(String str)
        {
            return str;
        }
        
        public static int GetIdBase(int strId)
        {
            return strId >> 16;
        }
        
    }
    
    
}