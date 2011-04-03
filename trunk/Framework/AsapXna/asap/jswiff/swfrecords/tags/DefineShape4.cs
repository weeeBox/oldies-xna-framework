using System;

using System.Collections.Generic;


using swiff.com.jswiff.swfrecords.tags.interfaces;
using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * Used to define shapes, similar to <code>DefineShape</code>,
     * <code>DefineShape2</code> and <code>DefineShape3</code>. Unlike older shape
     * definition tags, <code>DefineShape4</code> can contain
     * <code>LineStyle2</code> structures. Additionally, it allows to define edge
     * bounds and to specify flags for stroke hinting.
     *
     * @see com.jswiff.swfrecords.tags.DefineShape
     * @see com.jswiff.swfrecords.tags.DefineShape2
     * @see com.jswiff.swfrecords.tags.DefineShape3
     * @see com.jswiff.swfrecords.LineStyle2
     * @since SWF 8
     */
    public class DefineShape4 : DefinitionTag, IDefineShape
    {
        private Rect shapeBounds;
        
        private Rect edgeBounds;
        
        private ShapeWithStyle shapes;
        
        private bool hasScalingStrokes;
        
        private bool hasNonscalingStrokes;
        
        /** 
         * Creates a new DefineShape4 tag. Supply the character ID of the shape, its
         * shape and edge bounding box and its primitives and styles.
         *
         * @param characterId character ID of shape
         * @param shapeBounds bounding box of shape
         * @param edgeBounds edge bounding box
         * @param shapes shape's primitives and styles
         */
        public DefineShape4(int characterId ,Rect shapeBounds ,Rect edgeBounds ,ShapeWithStyle shapes) 
        {
            code = TagConstants.DEFINE_SHAPE_4;
            this.characterId = characterId;
            this.shapeBounds = shapeBounds;
            this.edgeBounds = edgeBounds;
            this.shapes = shapes;
        }
        
        public DefineShape4() 
        {
        }
        
        public virtual void SetEdgeBounds(Rect edgeBounds)
        {
            this.edgeBounds = edgeBounds;
        }
        
        public virtual Rect GetEdgeBounds()
        {
            return edgeBounds;
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
            edgeBounds = new Rect(inStream);
            inStream.ReadUI8();
            shapes = new ShapeWithStyle(inStream);
        }
        
        private void CheckStrokeScaling()
        {
            if ((shapes) == null) 
            {
                return ;
            } 
            hasNonscalingStrokes = false;
            hasScalingStrokes = false;
            LineStyleArray lineStyles = shapes.GetLineStyles();
            for (int i = 1; i <= (lineStyles.GetSize()); i++) 
            {
                LineStyle2 style = ((LineStyle2)(lineStyles.GetStyle(i)));
                if ((style.GetScaleStroke()) == (EnhancedStrokeStyle.SCALE_NONE)) 
                {
                    hasNonscalingStrokes = true;
                } 
                else 
                {
                    hasScalingStrokes = true;
                }
                if ((hasNonscalingStrokes) && (hasScalingStrokes)) 
                {
                    break;
                } 
            }
        }
    }
}