using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords
{
    /** 
     * Base class for the new enhanced stroke line styles introduced in SWF 8.
     */
    abstract public class EnhancedStrokeStyle
    {
        /** 
         *
         */
        public const byte SCALE_NONE = 0;
        
        /** 
         *
         */
        public const byte SCALE_VERTICAL = 1;
        
        /** 
         *
         */
        public const byte SCALE_HORIZONTAL = 2;
        
        /** 
         *
         */
        public const byte SCALE_BOTH = 3;
        
        /** 
         *
         */
        public const byte CAPS_ROUND = 0;
        
        /** 
         *
         */
        public const byte CAPS_NONE = 1;
        
        /** 
         *
         */
        public const byte CAPS_SQUARE = 2;
        
        /** 
         *
         */
        public const byte JOINT_ROUND = 0;
        
        /** 
         *
         */
        public const byte JOINT_BEVEL = 1;
        
        /** 
         *
         */
        public const byte JOINT_MITER = 2;
    }
}