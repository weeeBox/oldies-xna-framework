using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag is used to define the start and end states of a morph sequence.
     * After definition, a snapshot of the sequence can be displayed with the
     * <code>PlaceObject2</code> tag. Use several <code>PlaceObject2</code> tags
     * with <code>ratio</code> value increasing from 0 to 65535 to achieve a
     * smooth rendering of the morph. This is all you have to do to define and
     * render a morph. Flash Player is resposible for generating intermediary
     * states through interpolation.
     * </p>
     * 
     * <p>
     * Shapes belonging to a morph sequence are defined within a single
     * <code>DefineMorphShape</code> tag and are independent of previously defined
     * shapes. Accordingly, character definitions preceding this tag cannot be
     * used.
     * </p>
     *
     * @see PlaceObject2
     * @since SWF 3
     */
    public class DefineMorphShape : DefinitionTag
    {
        private Rect startBounds;
        
        private Rect endBounds;
        
        private MorphFillStyles morphFillStyles;
        
        private MorphLineStyles morphLineStyles;
        
        private Shape startShape;
        
        private Shape endShape;
        
        /** 
         * <p>
         * Creates a new DefineMorphShape tag. Supply the character ID of the morph
         * sequence, bounding boxes for the shapes at start and end of morph, and
         * morph fill and line styles. Finally, provide the start and the end
         * shape.
         * </p>
         * 
         * <p>
         * The shapes must have identical structures, i.e. a style change record in
         * the start shape must have a corresponding style change record in the
         * end shape. Edge records in the start shape must have matching edge
         * records in the end shape. The edge record type does not matter, since
         * straight edge records can be regarded as special cases of curved edge
         * records.
         * </p>
         *
         * @param characterId character ID if morph sequence
         * @param startBounds bounding box at morph start
         * @param endBounds bounding box at morph end
         * @param morphFillStyles array of fill styles used in morph sequence
         * @param morphLineStyles array of line styles used in morph sequence
         * @param startShape start shape
         * @param endShape end shape
         *
         * @throws IllegalArgumentException if start and end shapes are differently
         * 		   structured
         */
        public DefineMorphShape(int characterId ,Rect startBounds ,Rect endBounds ,MorphFillStyles morphFillStyles ,MorphLineStyles morphLineStyles ,Shape startShape ,Shape endShape) /* throws ArgumentOutOfRangeException */ 
        {
            code = TagConstants.DEFINE_MORPH_SHAPE;
            this.characterId = characterId;
            this.startBounds = startBounds;
            this.endBounds = endBounds;
            this.morphFillStyles = morphFillStyles;
            this.morphLineStyles = morphLineStyles;
            CheckEdges(startShape, endShape);
            this.startShape = startShape;
            this.endShape = endShape;
        }
        
        public DefineMorphShape() 
        {
        }
        
        public virtual void SetEndBounds(Rect endBounds)
        {
            this.endBounds = endBounds;
        }
        
        public virtual Rect GetEndBounds()
        {
            return endBounds;
        }
        
        public virtual void SetEndShape(Shape endShape)
        {
            this.endShape = endShape;
        }
        
        public virtual Shape GetEndShape()
        {
            return endShape;
        }
        
        public virtual void SetMorphFillStyles(MorphFillStyles morphFillStyles)
        {
            this.morphFillStyles = morphFillStyles;
        }
        
        public virtual MorphFillStyles GetMorphFillStyles()
        {
            return morphFillStyles;
        }
        
        public virtual void SetMorphLineStyles(MorphLineStyles morphLineStyles)
        {
            this.morphLineStyles = morphLineStyles;
        }
        
        public virtual MorphLineStyles GetMorphLineStyles()
        {
            return morphLineStyles;
        }
        
        public virtual void SetStartBounds(Rect startBounds)
        {
            this.startBounds = startBounds;
        }
        
        public virtual Rect GetStartBounds()
        {
            return startBounds;
        }
        
        public virtual void SetStartShape(Shape startShape)
        {
            this.startShape = startShape;
        }
        
        public virtual Shape GetStartShape()
        {
            return startShape;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            startBounds = new Rect(inStream);
            endBounds = new Rect(inStream);
            long endEdgesOffset = inStream.ReadUI32();
            if (endEdgesOffset == 0) 
            {
                return ;
            } 
            endEdgesOffset += inStream.GetOffset();
            morphFillStyles = new MorphFillStyles(inStream);
            morphLineStyles = new MorphLineStyles(inStream , false);
            long startEdgesOffset = inStream.GetOffset();
            byte[] startEdgesBuffer = new byte[((int)(endEdgesOffset - startEdgesOffset))];
            Array.Copy(data, ((int)(startEdgesOffset)), startEdgesBuffer, 0, startEdgesBuffer.Length);
            startShape = new Shape(new InputBitStream(startEdgesBuffer));
            byte[] endEdgesBuffer = new byte[((int)((data.Length) - endEdgesOffset))];
            Array.Copy(data, ((int)(endEdgesOffset)), endEdgesBuffer, 0, endEdgesBuffer.Length);
            endShape = new Shape(new InputBitStream(endEdgesBuffer));
        }
        
        private void CheckEdges(Shape edges1, Shape edges2)
        {
            if ((edges1 == null) || (edges2 == null)) 
            {
                return ;
            } 
            ShapeRecord[] startShapeRecs = edges1.GetShapeRecords();
            ShapeRecord[] endShapeRecs = edges1.GetShapeRecords();
            if ((startShapeRecs.Length) != (endShapeRecs.Length)) 
            {
                throw new ArgumentOutOfRangeException("Start and end shapes must have the same number of shape records!");
            } 
            for (int i = 0; i < (startShapeRecs.Length); i++) 
            {
                ShapeRecord startRec = startShapeRecs[i];
                ShapeRecord endRec = endShapeRecs[i];
                if (startRec is EdgeRecord) 
                {
                    if (endRec is EdgeRecord) 
                    {
                        continue;
                    } 
                    throw new ArgumentOutOfRangeException("Edge record in start shape must have corresponding record in end shape!");
                } 
                if (!(endRec is EdgeRecord)) 
                {
                    continue;
                } 
                throw new ArgumentOutOfRangeException("Style change record in start shape must have corresponding record in end shape!");
            }
        }
    }
}