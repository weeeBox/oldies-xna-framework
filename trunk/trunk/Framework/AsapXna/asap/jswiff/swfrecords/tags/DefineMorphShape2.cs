using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * DOCUMENT ME!
     *
     * @since SWF 8
     */
    public class DefineMorphShape2 : DefinitionTag
    {
        private Rect startShapeBounds;
        
        private Rect endShapeBounds;
        
        private Rect startEdgeBounds;
        
        private Rect endEdgeBounds;
        
        private MorphFillStyles morphFillStyles;
        
        private MorphLineStyles morphLineStyles;
        
        private Shape startShape;
        
        private Shape endShape;
        
        private bool hasNonscalingStrokes;
        
        private bool hasScalingStrokes;
        
        /** 
         * Creates a new DefineMorphShape2 instance.
         *
         * @param characterId TODO: Comments
         * @param startShapeBounds TODO: Comments
         * @param endShapeBounds TODO: Comments
         * @param startEdgeBounds TODO: Comments
         * @param endEdgeBounds TODO: Comments
         * @param morphFillStyles TODO: Comments
         * @param morphLineStyles TODO: Comments
         * @param startShape TODO: Comments
         * @param endShape TODO: Comments
         *
         * @throws IllegalArgumentException TODO: Comments
         */
        public DefineMorphShape2(int characterId ,Rect startShapeBounds ,Rect endShapeBounds ,Rect startEdgeBounds ,Rect endEdgeBounds ,MorphFillStyles morphFillStyles ,MorphLineStyles morphLineStyles ,Shape startShape ,Shape endShape) /* throws ArgumentOutOfRangeException */ 
        {
            code = TagConstants.DEFINE_MORPH_SHAPE_2;
            this.characterId = characterId;
            this.startShapeBounds = startShapeBounds;
            this.endShapeBounds = endShapeBounds;
            this.startEdgeBounds = startEdgeBounds;
            this.endEdgeBounds = endEdgeBounds;
            this.morphFillStyles = morphFillStyles;
            this.morphLineStyles = morphLineStyles;
            CheckEdges(startShape, endShape);
            this.startShape = startShape;
            this.endShape = endShape;
        }
        
        public DefineMorphShape2() 
        {
        }
        
        public virtual void SetEndEdgeBounds(Rect endEdgeBounds)
        {
            this.endEdgeBounds = endEdgeBounds;
        }
        
        public virtual Rect GetEndEdgeBounds()
        {
            return endEdgeBounds;
        }
        
        public virtual void SetEndShape(Shape endShape)
        {
            this.endShape = endShape;
        }
        
        public virtual Shape GetEndShape()
        {
            return endShape;
        }
        
        public virtual void SetEndShapeBounds(Rect endBounds)
        {
            this.endShapeBounds = endBounds;
        }
        
        public virtual Rect GetEndShapeBounds()
        {
            return endShapeBounds;
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
        
        public virtual void SetStartEdgeBounds(Rect startEdgeBounds)
        {
            this.startEdgeBounds = startEdgeBounds;
        }
        
        public virtual Rect GetStartEdgeBounds()
        {
            return startEdgeBounds;
        }
        
        public virtual void SetStartShape(Shape startShape)
        {
            this.startShape = startShape;
        }
        
        public virtual Shape GetStartShape()
        {
            return startShape;
        }
        
        public virtual void SetStartShapeBounds(Rect startBounds)
        {
            this.startShapeBounds = startBounds;
        }
        
        public virtual Rect GetStartShapeBounds()
        {
            return startShapeBounds;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            startShapeBounds = new Rect(inStream);
            endShapeBounds = new Rect(inStream);
            startEdgeBounds = new Rect(inStream);
            endEdgeBounds = new Rect(inStream);
            inStream.ReadUI8();
            long endEdgesOffset = inStream.ReadUI32();
            if (endEdgesOffset == 0) 
            {
                return ;
            } 
            endEdgesOffset += inStream.GetOffset();
            morphFillStyles = new MorphFillStyles(inStream);
            morphLineStyles = new MorphLineStyles(inStream , true);
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
        
        private void CheckStrokeScaling()
        {
            hasNonscalingStrokes = false;
            hasScalingStrokes = false;
            if ((morphLineStyles) == null) 
            {
                return ;
            } 
            for (int i = 1; i <= (morphLineStyles.GetSize()); i++) 
            {
                MorphLineStyle2 style = ((MorphLineStyle2)(morphLineStyles.GetStyle(i)));
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