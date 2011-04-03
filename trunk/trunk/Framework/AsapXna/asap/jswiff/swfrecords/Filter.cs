using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using System.IO;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * TODO: Comments
     */
    abstract public class Filter
    {
        /** 
         *
         */
        public const int DROP_SHADOW = 0;
        
        /** 
         *
         */
        public const int BLUR = 1;
        
        /** 
         *
         */
        public const int GLOW = 2;
        
        /** 
         *
         */
        public const int BEVEL = 3;
        
        /** 
         *
         */
        public const int GRADIENT_GLOW = 4;
        
        /** 
         *
         */
        public const int CONVOLUTION = 5;
        
        /** 
         *
         */
        public const int COLOR_MATRIX = 6;
        
        /** 
         *
         */
        public const int GRADIENT_BEVEL = 7;
        
        public static List<Filter>  ReadFilters(InputBitStream stream) /* throws IOException */
        {
            int count = stream.ReadUI8();
            List<Filter>  filters = new List<Filter> (count);
            for (int i = 0; i < count; i++) 
            {
                int filterType = stream.ReadUI8();
                Filter filter;
                switch (filterType)
                {
                    case BEVEL:
                        filter = new BevelFilter(stream);
                        break;
                    case BLUR:
                        filter = new BlurFilter(stream);
                        break;
                    case COLOR_MATRIX:
                        filter = new ColorMatrixFilter(stream);
                        break;
                    case CONVOLUTION:
                        filter = new ConvolutionFilter(stream);
                        break;
                    case DROP_SHADOW:
                        filter = new DropShadowFilter(stream);
                        break;
                    case GLOW:
                        filter = new GlowFilter(stream);
                        break;
                    case GRADIENT_BEVEL:
                        filter = new GradientBevelFilter(stream);
                        break;
                    case GRADIENT_GLOW:
                        filter = new GradientGlowFilter(stream);
                        break;
                    default:
                        throw new IOException(("Unknown filter type: " + filterType));
                        break;
                }
                filters.Add(filter);
            }
            return filters;
        }        
    }
}