using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class identifies a spoken language that applies to text. Used by Flash
     * Player for line breaking of dynamic text and for choosing fallback fonts.
     */
    public class LangCode
    {
        /** 
         *
         */
        public const byte UNDEFINED = 0;
        
        /** 
         *
         */
        public const byte LATIN = 1;
        
        /** 
         *
         */
        public const byte JAPANESE = 2;
        
        /** 
         *
         */
        public const byte KOREAN = 3;
        
        /** 
         *
         */
        public const byte SIMPLIFIED_CHINESE = 4;
        
        /** 
         *
         */
        public const byte TRADITIONAL_CHINESE = 5;
        
        private byte languageCode;
        
        /** 
         * Creates a new LangCode instance. Specify one of the supplied constants
         * (<code>UNDEFINED</code>, <code>LATIN</code>, <code>JAPANESE</code>,
         * <code>KOREAN</code>, <code>SIMPLIFIED_CHINESE</code> or
         * <code>TRADITIONAL_CHINESE</code>).
         *
         * @param languageCode language code.
         */
        public LangCode(byte languageCode) 
        {
            this.languageCode = languageCode;
        }
        
        public virtual byte GetLanguageCode()
        {
            return languageCode;
        }
        
        public override String ToString()
        {
            switch (languageCode)
            {
                case UNDEFINED:
                    return "Undefined";
                case LATIN:
                    return "Latin";
                case JAPANESE:
                    return "Japanese";
                case KOREAN:
                    return "Korean";
                case SIMPLIFIED_CHINESE:
                    return "Simplified Chinese";
                case TRADITIONAL_CHINESE:
                    return "Traditional Chinese";
                default:
                    return ("Unknown (" + (languageCode)) + ")";                    
            }
        }
    }
}