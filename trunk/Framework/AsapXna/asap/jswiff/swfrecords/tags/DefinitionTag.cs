using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * Base class for definition tags, which define new characters.
     */
    abstract public class DefinitionTag : Tag
    {
        public int characterId;
        
        public virtual void SetCharacterId(int characterId)
        {
            this.characterId = characterId;
        }
        
        public virtual int GetCharacterId()
        {
            return characterId;
        }
        
        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = (prime * result) + (characterId);
            return result;
        }
        
        public override bool Equals(Object obj)
        {
            if ((this) == obj)
                return true;
            
            if (obj == null)
                return false;
            
            if (!(obj is DefinitionTag))
                return false;
            
            DefinitionTag other = ((DefinitionTag)(obj));
            return (characterId) == (other.characterId);
        }
        
        public override String ToString()
        {
            return ((base.ToString()) + ": characterId=") + (characterId);
        }
    }
}