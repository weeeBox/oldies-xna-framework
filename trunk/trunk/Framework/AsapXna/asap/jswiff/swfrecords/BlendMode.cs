using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords
{
    /** 
     * TODO: Comments
     */
    public class BlendMode
    {
        /** 
         *
         */
        public const short NORMAL = 1;
        
        /** 
         *
         */
        public const short LAYER = 2;
        
        /** 
         *
         */
        public const short MULTIPLY = 3;
        
        /** 
         *
         */
        public const short SCREEN = 4;
        
        /** 
         *
         */
        public const short LIGHTEN = 5;
        
        /** 
         *
         */
        public const short DARKEN = 6;
        
        /** 
         *
         */
        public const short DIFFERENCE = 7;
        
        /** 
         *
         */
        public const short ADD = 8;
        
        /** 
         *
         */
        public const short SUBTRACT = 9;
        
        /** 
         *
         */
        public const short INVERT = 10;
        
        /** 
         *
         */
        public const short ALPHA = 11;
        
        /** 
         *
         */
        public const short ERASE = 12;
        
        /** 
         *
         */
        public const short OVERLAY = 13;
        
        /** 
         *
         */
        public const short HARD_LIGHT = 14;
        
        public static String GetDescription(short blendMode)
        {
            switch (blendMode)
            {
                case 0:
                case NORMAL:
                    return "normal";
                case LAYER:
                    return "layer";
                case MULTIPLY:
                    return "multiply";
                case SCREEN:
                    return "screen";
                case LIGHTEN:
                    return "lighten";
                case DARKEN:
                    return "darken";
                case DIFFERENCE:
                    return "difference";
                case ADD:
                    return "add";
                case SUBTRACT:
                    return "subtract";
                case INVERT:
                    return "invert";
                case ALPHA:
                    return "alpha";
                case ERASE:
                    return "erase";
                case OVERLAY:
                    return "overlay";
                case HARD_LIGHT:
                    return "hard light";
                default:
                    return "unknown value: " + blendMode;                    
            }
        }
        
        public static short GetFromDescription(String description)
        {
            if (description.Equals("normal")) 
            {
                return NORMAL;
            } 
            else if (description.Equals("layer")) 
            {
                return LAYER;
            } 
            else if (description.Equals("multiply")) 
            {
                return MULTIPLY;
            } 
            else if (description.Equals("screen")) 
            {
                return SCREEN;
            } 
            else if (description.Equals("lighten")) 
            {
                return LIGHTEN;
            } 
            else if (description.Equals("darken")) 
            {
                return DARKEN;
            } 
            else if (description.Equals("difference")) 
            {
                return DIFFERENCE;
            } 
            else if (description.Equals("add")) 
            {
                return ADD;
            } 
            else if (description.Equals("subtract")) 
            {
                return SUBTRACT;
            } 
            else if (description.Equals("invert")) 
            {
                return INVERT;
            } 
            else if (description.Equals("alpha")) 
            {
                return ALPHA;
            } 
            else if (description.Equals("erase")) 
            {
                return ERASE;
            } 
            else if (description.Equals("overlay")) 
            {
                return OVERLAY;
            } 
            else if (description.Equals("hard light")) 
            {
                return HARD_LIGHT;
            } 
            else 
            {
                return -1;
            }
        }
    }
}