using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * TODO: Comments
     */
    public class ConvolutionFilter : Filter
    {
        private int matrixRows;
        
        private float[] matrix;
        
        private RGBA color;
        
        private float divisor;
        
        private float bias;
        
        private bool clamp;
        
        private bool preserveAlpha;
        
        /** 
         * Creates a new ConvolutionFilter instance.
         *
         * @param matrix TODO: Comments
         * @param matrixRows TODO: Comments
         */
        public ConvolutionFilter(float[] matrix ,int matrixRows) 
        {
            SetMatrix(matrix, matrixRows);
            InitDefaults();
        }
        
        public ConvolutionFilter(InputBitStream stream) /* throws IOException */ 
        {
            int matrixColumns = stream.ReadUI8();
            matrixRows = stream.ReadUI8();
            divisor = stream.ReadFloat();
            bias = stream.ReadFloat();
            int matrixSize = matrixColumns * (matrixRows);
            matrix = new float[matrixSize];
            for (int i = 0; i < matrixSize; i++) 
            {
                matrix[i] = stream.ReadFloat();
            }
            color = new RGBA(stream);
            stream.ReadUnsignedBits(6);
            clamp = stream.ReadBooleanBit();
            preserveAlpha = stream.ReadBooleanBit();
        }
        
        public virtual void SetBias(float bias)
        {
            this.bias = bias;
        }
        
        public virtual float GetBias()
        {
            return bias;
        }
        
        public virtual void SetClamp(bool clamp)
        {
            this.clamp = clamp;
        }
        
        public virtual bool IsClamp()
        {
            return clamp;
        }
        
        public virtual void SetColor(RGBA color)
        {
            this.color = color;
        }
        
        public virtual RGBA GetColor()
        {
            return color;
        }
        
        public virtual void SetDivisor(float divisor)
        {
            this.divisor = divisor;
        }
        
        public virtual float GetDivisor()
        {
            return divisor;
        }
        
        public virtual void SetMatrix(float[] matrix, int matrixRows)
        {
            if (((matrix.Length) % matrixRows) != 0) 
            {
                throw new ArgumentOutOfRangeException("matrix array length must be a multiple of the matrix width!");
            } 
            this.matrix = matrix;
            this.matrixRows = matrixRows;
        }
        
        public virtual float[] GetMatrix()
        {
            return matrix;
        }
        
        public virtual int GetMatrixRows()
        {
            return matrixRows;
        }
        
        public virtual void SetPreserveAlpha(bool preserveAlpha)
        {
            this.preserveAlpha = preserveAlpha;
        }
        
        public virtual bool IsPreserveAlpha()
        {
            return preserveAlpha;
        }
        
        private void InitDefaults()
        {
            color = RGBA.BLACK;
            divisor = 1;
            preserveAlpha = true;
            clamp = true;
        }
    }
}