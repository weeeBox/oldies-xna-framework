using System;

using System.Collections.Generic;


using swiff.com.jswiff.swfrecords.tags.interfaces;
using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag is used to define shapes, just like <code>DefineShape</code> and
     * <code>DefineShape2</code>. Unlike older shape definition tags,
     * <code>DefineShape3</code> adds transparency support, i.e. colors within
     * this tag are <code>RGBA</code> (not <code>RGB</code>) instances.
     *
     * @see com.jswiff.swfrecords.tags.DefineShape
     * @see com.jswiff.swfrecords.tags.DefineShape2
     * @see com.jswiff.swfrecords.RGBA
     * @since SWF 3
     */
    public class DefineShape3 : DefinitionTag, IDefineShape
    {
        private Rect shapeBounds;
        
        private ShapeWithStyle shapes;
        
        /** 
         * Creates a new DefineShape3 tag. Supply the character ID of the shape,
         * its bounding box and its primitives and styles.
         *
         * @param characterId character ID of shape
         * @param shapeBounds bounding box of shape
         * @param shapes shape's primitives and styles
         */
        public DefineShape3(int characterId ,Rect shapeBounds ,ShapeWithStyle shapes) 
        {
            code = TagConstants.DEFINE_SHAPE_3;
            this.characterId = characterId;
            this.shapeBounds = shapeBounds;
            this.shapes = shapes;
        }
        
        public DefineShape3() 
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
            shapes = new ShapeWithStyle(inStream , true);
        }
    }
}