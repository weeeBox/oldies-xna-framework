using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using System.IO;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used to define fill styles used in a morph sequence (as array
     * within a <code>DefineMorphShape</code> tag. Three basic types of shape
     * fills are available:
     * 
     * <ul>
     * <li>
     * solid fill (with / without transparency)
     * </li>
     * <li>
     * gradient fill (linear / radial)
     * </li>
     * <li>
     * bitmap fill (clipped / tiled, smoothed / non-smoothed)
     * </li>
     * </ul>
     * 
     *
     * @see MorphFillStyles
     * @see com.jswiff.swfrecords.tags.DefineMorphShape
     * @see FillStyle
     */
    public class MorphFillStyle
    {
        /** 
         *
         */
        public const short TYPE_SOLID = 0;
        
        /** 
         *
         */
        public const short TYPE_LINEAR_GRADIENT = 16;
        
        /** 
         *
         */
        public const short TYPE_RADIAL_GRADIENT = 18;
        
        /** 
         *
         */
        public const short TYPE_FOCAL_RADIAL_GRADIENT = 19;
        
        /** 
         *
         */
        public const short TYPE_TILED_BITMAP = 64;
        
        /** 
         *
         */
        public const short TYPE_CLIPPED_BITMAP = 65;
        
        /** 
         *
         */
        public const short TYPE_NONSMOOTHED_TILED_BITMAP = 66;
        
        /** 
         *
         */
        public const short TYPE_NONSMOOTHED_CLIPPED_BITMAP = 67;
        
        private short type;
        
        private RGBA startColor;
        
        private RGBA endColor;
        
        private SwfMatrix startGradientMatrix;
        
        private SwfMatrix endGradientMatrix;
        
        private MorphGradient gradient;
        
        private int bitmapId;
        
        private SwfMatrix startBitmapMatrix;
        
        private SwfMatrix endBitmapMatrix;
        
        /** 
         * Creates a new solid morph fill style. Specify fill colors with
         * transparency (RGBA) for start and end state.
         *
         * @param startColor start color
         * @param endColor end color
         */
        public MorphFillStyle(RGBA startColor ,RGBA endColor) 
        {
            this.startColor = startColor;
            this.endColor = endColor;
            type = TYPE_SOLID;
        }
        
        /** 
         * <p>
         * Creates a new gradient morph fill style.
         * </p>
         * 
         * <p>
         * Shapes can be filled with different gradients in the morph's start and end
         * state. A gradient contains several control points; the fill color is
         * interpolated between these point's colors. Start and end gradients must
         * have the same number of control points. Control points of start and end
         * gradients as well as their colors are defined in a
         * <code>MorphGradient</code> instance.
         * </p>
         * 
         * <p>
         * Gradients are defined in the <i>gradient square</i>: (-16384, -16384,
         * 16384, 16384). The gradient matrix is used to map the gradient from the
         * gradient square to the display surface. Supply gradient matrices for
         * start and end state.
         * </p>
         * 
         * <p>
         * Linear and circular gradients are supported. Use either
         * <code>TYPE_LINEAR_GRADIENT</code> or <code>TYPE_CIRCULAR_GRADIENT</code>
         * as gradient type.
         * </p>
         *
         * @param gradient a morph gradient
         * @param startGradientMatrix start gradient matrix
         * @param endGradientMatrix end gradient matrix
         * @param type gradient type
         *
         * @throws IllegalArgumentException if specified gradient type is not
         *         supported
         */
        public MorphFillStyle(MorphGradient gradient ,SwfMatrix startGradientMatrix ,SwfMatrix endGradientMatrix ,short type) 
        {
            if (((type != (TYPE_LINEAR_GRADIENT)) && (type != (TYPE_RADIAL_GRADIENT))) && (type != (TYPE_FOCAL_RADIAL_GRADIENT))) 
            {
                throw new ArgumentOutOfRangeException("Illegal gradient type!");
            } 
            this.gradient = gradient;
            this.startGradientMatrix = startGradientMatrix;
            this.endGradientMatrix = endGradientMatrix;
            this.type = type;
        }
        
        /** 
         * Creates a new bitmap morph fill style. You have to specify the character
         * ID of a previously defined bitmap, two transform matrices used for
         * mapping the bitmap to the filled (start and end) shapes, and the bitmap
         * type (one of the constants <code>TYPE_TILED_BITMAP</code>,
         * <code>TYPE_CLIPPED_BITMAP</code>,
         * <code>TYPE_NONSMOOTHED_TILED_BITMAP</code> or
         * <code>TYPE_NONSMOOTHED_CLIPPED_BITMAP</code>).
         *
         * @param bitmapId character ID of the bitmap
         * @param startBitmapMatrix transform matrix for start state
         * @param endBitmapMatrix transform matrix for end state
         * @param type bitmap type
         *
         * @throws IllegalArgumentException if an illegal bitmap type has been
         *         specified
         */
        public MorphFillStyle(int bitmapId ,SwfMatrix startBitmapMatrix ,SwfMatrix endBitmapMatrix ,short type) 
        {
            if ((((type != (TYPE_TILED_BITMAP)) && (type != (TYPE_CLIPPED_BITMAP))) && (type != (TYPE_NONSMOOTHED_TILED_BITMAP))) && (type != (TYPE_NONSMOOTHED_CLIPPED_BITMAP))) 
            {
                throw new ArgumentOutOfRangeException("Illegal bitmap type");
            } 
            this.bitmapId = bitmapId;
            this.startBitmapMatrix = startBitmapMatrix;
            this.endBitmapMatrix = endBitmapMatrix;
            this.type = type;
        }
        
        public MorphFillStyle(InputBitStream stream) /* throws IOException */ 
        {
            type = stream.ReadUI8();
            switch (type)
            {
                case TYPE_SOLID:
                    startColor = new RGBA(stream);
                    endColor = new RGBA(stream);
                    break;
                case TYPE_LINEAR_GRADIENT:
                case TYPE_RADIAL_GRADIENT:
                    startGradientMatrix = new SwfMatrix(stream);
                    endGradientMatrix = new SwfMatrix(stream);
                    gradient = new MorphGradient(stream);
                    break;
                case TYPE_FOCAL_RADIAL_GRADIENT:
                    startGradientMatrix = new SwfMatrix(stream);
                    endGradientMatrix = new SwfMatrix(stream);
                    gradient = new FocalMorphGradient(stream);
                    break;
                case TYPE_TILED_BITMAP:
                case TYPE_CLIPPED_BITMAP:
                case TYPE_NONSMOOTHED_TILED_BITMAP:
                case TYPE_NONSMOOTHED_CLIPPED_BITMAP:
                    bitmapId = stream.ReadUI16();
                    startBitmapMatrix = new SwfMatrix(stream);
                    endBitmapMatrix = new SwfMatrix(stream);
                    break;
                default:
                    throw new IOException(("Illegal morph fill type: " + (type)));                    
            }
        }
        
        public virtual int GetBitmapId()
        {
            return bitmapId;
        }
        
        public virtual SwfMatrix GetEndBitmapMatrix()
        {
            return endBitmapMatrix;
        }
        
        public virtual RGBA GetEndColor()
        {
            return endColor;
        }
        
        public virtual SwfMatrix GetEndGradientMatrix()
        {
            return endGradientMatrix;
        }
        
        public virtual MorphGradient GetGradient()
        {
            return gradient;
        }
        
        public virtual SwfMatrix GetStartBitmapMatrix()
        {
            return startBitmapMatrix;
        }
        
        public virtual RGBA GetStartColor()
        {
            return startColor;
        }
        
        public virtual SwfMatrix GetStartGradientMatrix()
        {
            return startGradientMatrix;
        }
        
        public virtual short _getType()
        {
            return type;
        }
    }
}