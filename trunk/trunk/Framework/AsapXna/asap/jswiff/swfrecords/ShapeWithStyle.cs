using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class extends the <code>Shape</code> class by including fill and line
     * styles. Like the <code>Shape</code> class, <code>ShapeWithStyle</code> also
     * contains one or more <code>ShapeRecord</code> instances which define style
     * changes and primitives as lines and curves. Used within
     * <code>DefineShape</code>, <code>DefineShape2</code> and
     * <code>DefineShape3</code>.
     *
     * @see com.jswiff.swfrecords.tags.DefineShape
     * @see com.jswiff.swfrecords.tags.DefineShape2
     * @see com.jswiff.swfrecords.tags.DefineShape3
     */
    public class ShapeWithStyle : Shape
    {
        private FillStyleArray fillStyles;
        
        private LineStyleArray lineStyles;
        
        /** 
         * Creates a new ShapeWithStyle instance. Supply a fill and line style array
         * and an array of shape records. The style arrays must contain less than
         * 256 styles when used within a <code>DefineShape</code> tag.
         *
         * @param fillStyles fill style array
         * @param lineStyles line style array
         * @param shapeRecords shape record array
         */
        public ShapeWithStyle(FillStyleArray fillStyles ,LineStyleArray lineStyles ,ShapeRecord[] shapeRecords) 
         : base(shapeRecords)
        {
            this.fillStyles = fillStyles;
            this.lineStyles = lineStyles;
        }
        
        /** 
         * Creates a new ShapeWithStyle instance, reading data from a bit stream.
         *
         * @param stream source bit stream
         * @param hasAlpha whether transparency is supported
         *
         * @throws IOException if an I/O error occured
         */
        public ShapeWithStyle(InputBitStream stream ,bool hasAlpha) /* throws IOException */ 
        {
            fillStyles = new FillStyleArray(stream , hasAlpha);
            lineStyles = new LineStyleArray(stream , hasAlpha);
            Read(stream, false, hasAlpha);
        }
        
        public ShapeWithStyle(InputBitStream stream) /* throws IOException */ 
        {
            fillStyles = new FillStyleArray(stream , true);
            lineStyles = new LineStyleArray(stream);
            Read(stream, true, true);
        }
        
        public virtual FillStyleArray GetFillStyles()
        {
            return fillStyles;
        }
        
        public virtual LineStyleArray GetLineStyles()
        {
            return lineStyles;
        }
    }
}