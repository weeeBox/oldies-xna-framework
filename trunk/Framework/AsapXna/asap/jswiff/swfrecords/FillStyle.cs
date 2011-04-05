using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using System.IO;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used to define a fill style. Three basic types of shape fills
     * are available:
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
     */
    public class FillStyle
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
        
        private Color color;
        
        private SwfMatrix gradientMatrix;
        
        private Gradient gradient;
        
        private int bitmapId;
        
        private SwfMatrix bitmapMatrix;
        
        /** 
         * <p>
         * Creates a new gradient fill style. You have to specify a gradient, a
         * gradient matrix and a gradient type.
         * </p>
         * 
         * <p>
         * The gradient contains several control points. The fill color is
         * interpolated between these point's colors.
         * </p>
         * 
         * <p>
         * Gradients are defined in the <i>gradient square</i>: (-16384, -16384,
         * 16384, 16384). The gradient matrix is used to map the gradient from the
         * gradient square to the display surface.
         * </p>
         * 
         * <p>
         * Linear and circular gradients are supported. Use either
         * <code>TYPE_LINEAR_GRADIENT</code> or <code>TYPE_CIRCULAR_GRADIENT</code>
         * as gradient type.
         * </p>
         *
         * @param gradient a gradient
         * @param gradientMatrix gradient matrix
         * @param type gradient type
         *
         * @throws IllegalArgumentException if specified gradient type is not
         *         supported
         */
        public FillStyle(Gradient gradient ,SwfMatrix gradientMatrix ,short type) 
        {
            if (((type != (TYPE_LINEAR_GRADIENT)) && (type != (TYPE_RADIAL_GRADIENT))) && (type != (TYPE_FOCAL_RADIAL_GRADIENT))) 
            {
                throw new ArgumentOutOfRangeException("Illegal gradient type!");
            } 
            this.type = type;
            this.gradient = gradient;
            this.gradientMatrix = gradientMatrix;
        }
        
        /** 
         * Creates a new solid fill style. Specify a fill color. This can be either
         * an <code>RGBA</code> or an <code>RGB instance</code>, depending on
         * whether the tag the style is contained in supports transparency or not.
         * <code>DefineShape3</code> supports transparency.
         *
         * @param color fill color
         *
         * @see com.jswiff.swfrecords.tags.DefineShape3
         */
        public FillStyle(Color color) 
        {
            this.color = color;
            type = TYPE_SOLID;
        }
        
        /** 
         * Creates a new bitmap fill style. You have to specify the character ID of a
         * previously defined bitmap, a transform matrix used for mapping the bitmap
         * to the filled shape, and the bitmap type (one of the constants
         * <code>TYPE_TILED_BITMAP</code>, <code>TYPE_CLIPPED_BITMAP</code>,
         * <code>TYPE_NONSMOOTHED_TILED_BITMAP</code> or
         * <code>TYPE_NONSMOOTHED_CLIPPED_BITMAP</code>).
         *
         * @param bitmapId character ID of the bitmap
         * @param bitmapMatrix transform matrix
         * @param type bitmap type
         *
         * @throws IllegalArgumentException if an illegal bitmap type has been
         *         specified
         */
        public FillStyle(int bitmapId ,SwfMatrix bitmapMatrix ,short type) 
        {
            if ((((type != (TYPE_TILED_BITMAP)) && (type != (TYPE_CLIPPED_BITMAP))) && (type != (TYPE_NONSMOOTHED_TILED_BITMAP))) && (type != (TYPE_NONSMOOTHED_CLIPPED_BITMAP))) 
            {
                throw new ArgumentOutOfRangeException("Illegal bitmap type");
            } 
            this.bitmapId = bitmapId;
            this.bitmapMatrix = bitmapMatrix;
            this.type = type;
        }
        
        public FillStyle(InputBitStream stream ,bool hasAlpha) /* throws IOException */ 
        {
            type = stream.ReadUI8();
            switch (type)
            {
                case TYPE_SOLID:
                    if (hasAlpha) 
                    {
                        color = new RGBA(stream);
                    } 
                    else 
                    {
                        color = new RGB(stream);
                    }
                    break;
                case TYPE_LINEAR_GRADIENT:
                case TYPE_RADIAL_GRADIENT:
                    gradientMatrix = new SwfMatrix(stream);
                    gradient = new Gradient(stream , hasAlpha);
                    break;
                case TYPE_FOCAL_RADIAL_GRADIENT:
                    gradientMatrix = new SwfMatrix(stream);
                    gradient = new FocalGradient(stream);
                    break;
                case TYPE_TILED_BITMAP:
                case TYPE_CLIPPED_BITMAP:
                case TYPE_NONSMOOTHED_TILED_BITMAP:
                case TYPE_NONSMOOTHED_CLIPPED_BITMAP:
                    bitmapId = stream.ReadUI16();
                    bitmapMatrix = new SwfMatrix(stream);
                    break;
                default:
                    throw new IOException(("Illegal fill type: " + (type)));
                    break;
            }
        }
        
        public virtual int GetBitmapId()
        {
            return bitmapId;
        }
        
        public virtual SwfMatrix GetBitmapMatrix()
        {
            return bitmapMatrix;
        }
        
        public virtual Color GetColor()
        {
            return color;
        }
        
        public virtual Gradient GetGradient()
        {
            return gradient;
        }
        
        public virtual SwfMatrix GetGradientMatrix()
        {
            return gradientMatrix;
        }
        
        public virtual short _getType()
        {
            return type;
        }        
    }
}