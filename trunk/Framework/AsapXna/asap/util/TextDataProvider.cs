using System;

using System.Collections.Generic;



namespace java.asap.util
{
    /** 
     * Класс для предоставления информации о тексте, который мы форматируем в StringWrapper.
     */
    abstract public class TextDataProvider
     {
        public abstract int Length();
        
        public abstract char GetCharAt(int index);
        
        public abstract int WidthOfCharAt(int index);
        
        public abstract int CharDistanceOfCharAt(int index);
        
    }
    
    
}