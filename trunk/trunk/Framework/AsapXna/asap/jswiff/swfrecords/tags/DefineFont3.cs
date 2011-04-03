using System;

using System.Collections.Generic;


using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag's structure is identical to <code>DefineFont2</code>. The
     * difference is in the precision of the glyph definition within
     * glyphShapeTable: all EM square coordinates are multiplied by 20 before
     * being added to the tag, thus enabling substantial resolution increase.
     *
     * @since SWF 8.
     */
    public class DefineFont3 : DefineFont2
    {
        /** 
         * @see DefineFont2#DefineFont2(int, String, Shape[], char[])
         */
        public DefineFont3(int characterId ,String fontName ,Shape[] glyphShapeTable ,char[] codeTable) 
         : base(characterId, fontName, glyphShapeTable, codeTable)
        {
            code = TagConstants.DEFINE_FONT_3;
        }
        
        public DefineFont3() 
        {
        }
    }
}