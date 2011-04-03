using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag defines the shape outlines of each glyph used in a particular font.
     * Only glyphs used by <code>DefineText</code> tags need to be defined.
     * </p>
     * 
     * <p>
     * Warning: for dynamic text, you have to use the <code>DefineFont2</code> tag.
     * </p>
     *
     * @see Shape
     * @since SWF 1
     */
    public class DefineFont : DefinitionTag
    {
        private Shape[] glyphShapeTable;
        
        /** 
         * Creates a new DefineFont tag.
         *
         * @param characterId the character ID of the font
         * @param glyphShapeTable array of <code>Shape</code> instances
         */
        public DefineFont(int characterId ,Shape[] glyphShapeTable) 
        {
            code = TagConstants.DEFINE_FONT;
            this.characterId = characterId;
            this.glyphShapeTable = glyphShapeTable;
        }
        
        public DefineFont() 
        {
        }
        
        public virtual void SetGlyphShapeTable(Shape[] glyphShapeTable)
        {
            this.glyphShapeTable = glyphShapeTable;
        }
        
        public virtual Shape[] GetGlyphShapeTable()
        {
            return glyphShapeTable;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            int shapeTableOffset = inStream.ReadUI16();
            int tableSize = shapeTableOffset / 2;
            inStream.ReadBytes((shapeTableOffset - 2));
            glyphShapeTable = new Shape[tableSize];
            for (int i = 0; i < tableSize; i++) 
            {
                glyphShapeTable[i] = new Shape(inStream);
            }
        }
    }
}