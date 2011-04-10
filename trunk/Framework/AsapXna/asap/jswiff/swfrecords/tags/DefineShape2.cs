using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag is used to define shapes, just like <code>DefineShape</code>.
     * Unlike <code>DefineShape</code>, <code>DefineShape2</code> supports more
     * than 255 styles in the line and fill style arrays and supports the
     * definition of new style arrays within contained
     * <code>StyleChangeRecord</code> instances.
     *
     * @see DefineShape
     * @see ShapeWithStyle
     * @see com.jswiff.swfrecords.StyleChangeRecord#setNewStyles(LineStyleArray,
     * 		FillStyleArray)
     * @since SWF 2
     */
    public class DefineShape2 : DefinitionTag
    {
        private Rect shapeBounds;
        
        private ShapeWithStyle shapes;
        
        /** 
         * Creates a new DefineShape2 tag. Supply the character ID of the shape,
         * its bounding box and its primitives and styles.
         *
         * @param characterId character ID of shape
         * @param shapeBounds bounding box of shape
         * @param shapes shape's primitives and styles
         */
        public DefineShape2(int characterId ,Rect shapeBounds ,ShapeWithStyle shapes) 
        {
            code = TagConstants.DEFINE_SHAPE_2;
            this.characterId = characterId;
            this.shapeBounds = shapeBounds;
            this.shapes = shapes;
        }
        
        public DefineShape2() 
        {
        }
        
        public virtual void SetShapeBounds(Rect shapeBounds)
        {
            this.shapeBounds = shapeBounds;
        }
        
        public virtual Rect GetShapeBounds()
        {
            return shapeBounds;
        }
        
        public virtual void SetShapes(ShapeWithStyle shapes)
        {
            this.shapes = shapes;
        }
        
        public virtual ShapeWithStyle GetShapes()
        {
            return shapes;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            shapeBounds = new Rect(inStream);
            shapes = new ShapeWithStyle(inStream , false);
        }
    }
}