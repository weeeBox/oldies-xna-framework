using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    public class SymbolClass : Tag
    {
        private Dictionary<String, SymbolClass.SymbolClassEntry>  pairs;
        
        public SymbolClass() 
        {
            code = TagConstants.SYMBOL_CLASS;
            pairs = new Dictionary<String, SymbolClass.SymbolClassEntry> ();
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            int numSymbols = inStream.ReadUI16();
            for (int symbolIndex = 0; symbolIndex < numSymbols; symbolIndex++) 
            {
                int tagId = inStream.ReadUI16();
                String className = inStream.ReadString();
                pairs.Add(className, new SymbolClassEntry(tagId , className));
            }
        }
        
        public virtual void AddSymbolPair(int tagId, String className)
        {
            pairs.Add(className, new SymbolClassEntry(tagId , className));
        }
        
        public virtual Dictionary<String, SymbolClass.SymbolClassEntry>  GetPairs()
        {
            return pairs;
        }
        
        public class SymbolClassEntry
        {
            private int tag;
            
            private String name;
            
            public SymbolClassEntry(int tag ,String name) 
            {
                this.tag = tag;
                this.name = name;
            }
            
            public virtual int GetTag()
            {
                return tag;
            }
            
            public virtual String GetName()
            {
                return name;
            }
            
            public override int GetHashCode()
            {
                int prime = 31;
                int result = 1;
                result = (prime * result) + (name.GetHashCode());
                result = (prime * result) + (tag);
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
                
                SymbolClassEntry other = ((SymbolClassEntry)(obj));
                if (!(name.Equals(other.name)))
                    return false;
                
                if ((tag) != (other.tag))
                    return false;
                
                return true;
            }
        }
    }
}