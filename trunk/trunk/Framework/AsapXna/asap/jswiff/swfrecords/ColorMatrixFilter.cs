using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * TODO: Comments
     */
    public class ColorMatrixFilter : Filter
    {
        private float[] matrix;
        
        /** 
         * Creates a new ColorMatrixFilter instance.
         *
         * @param matrix TODO: Comments
         *
         * @throws IllegalArgumentException if matrix array length != 20
         */
        public ColorMatrixFilter(float[] matrix) 
        {
            if ((matrix.Length) != 20) 
            {
                throw new ArgumentOutOfRangeException("matrix array length must be 20!");
            } 
            this.matrix = matrix;
        }
        
        /** 
         * Creates a new ColorMatrixFilter instance.
         *
         * @param stream TODO: Comments
         *
         * @throws IOException TODO: Comments
         */
        public ColorMatrixFilter(InputBitStream stream) /* throws IOException */ 
        {
            matrix = new float[20];
            for (int i = 0; i < 20; i++) 
            {
                matrix[i] = stream.ReadFloat();
            }
        }
        
        public virtual void SetMatrix(float[] matrix)
        {
            this.matrix = matrix;
        }
        
        public virtual float[] GetMatrix()
        {
            return matrix;
        }        
    }
}