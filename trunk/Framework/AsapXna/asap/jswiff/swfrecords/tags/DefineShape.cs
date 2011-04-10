using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag defines a shape, assigning it a character ID. After definition,
     * this shape can be displayed on the screen (e.g. with
     * <code>PlaceObject</code>) or referenced from within other tags.  The
     * shape's primitives (i.e. lines and curves) and its styles are contained in
     * a <code>ShapeWithStyle</code> instance.
     *
     * @see ShapeWithStyle
     * @see DefineShape2
     * @see DefineShape3
     * @since SWF 1
     */
    public class DefineShape : DefinitionTag
    {
        private Rect shapeBounds;
        
        private ShapeWithStyle shapes;
        
        /** 
         * Creates a new DefineShape tag. Supply the character ID of the shape, its
         * bounding box and its primitives and styles.
         *
         * @param characterId character ID of shape
         * @param shapeBounds bounding box of shape
         * @param shapes shape's primitives and styles
         */
        public DefineShape(int characterId ,Rect shapeBounds ,ShapeWithStyle shapes) 
        {
            code = TagConstants.DEFINE_SHAPE;
            this.characterId = characterId;
            this.shapeBounds = shapeBounds;
            this.shapes = shapes;
        }
        
        public DefineShape() 
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