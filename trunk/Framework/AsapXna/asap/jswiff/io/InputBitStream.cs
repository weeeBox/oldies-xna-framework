using System;

using System.Collections.Generic;
using System.IO;



namespace swiff.com.jswiff.io
{
    /** 
     * Implements a bit stream used for reading SWF files.
     */
    public class InputBitStream : IDisposable
    {
        private Stream stream;
        
        private int bitBuffer;
        
        private int bitCursor = 8;
        
        private bool compressed = false;
        
        private long offset;
        
        private bool ansi;
        
        private bool shiftJIS;
        
        /** 
         * Creates a new bit stream instance from a given input stream.
         *
         * @param stream the internal input stream the data is read from.
         */
        public InputBitStream(Stream stream) 
        {
            this.stream = stream;
            offset = 0;
        }
        
        /** 
         * Creates a new bit stream instance. Data is read from a given byte buffer.
         *
         * @param buffer data buffer the bit stream reads from
         */
        public InputBitStream(byte[] buffer) 
         : this(new MemoryStream(buffer))
        {
        }
        
        public virtual void SetANSI(bool ansi)
        {
            this.ansi = ansi;
        }
        
        public virtual bool IsANSI()
        {
            return ansi;
        }
        
        public virtual long GetOffset()
        {
            return offset;
        }
        
        public virtual void SetShiftJIS(bool shiftJIS)
        {
            this.shiftJIS = shiftJIS;
        }
        
        public virtual bool IsShiftJIS()
        {
            return shiftJIS;
        }
        
        public virtual void Align()
        {
            bitCursor = 8;
        }

        public virtual int Available() /* throws IOException */
        {         
            return (int)(stream.Length - stream.Position);
        }

        public virtual void Close() /* throws IOException */
        {
            stream.Close();
        }        

        public void Dispose()
        {
            Close();
        }
        
        public virtual void Move(long delta) /* throws IOException */
        {
            offset = (offset) + delta;
            stream.Position = offset;            
        }
        
        public virtual bool ReadBooleanBit() /* throws IOException */
        {
            return (ReadUnsignedBits(1)) == 1;
        }
        
        public virtual byte[] ReadBytes(int Length) /* throws IOException */
        {
            byte[] result;
            if (Length > 0) 
            {
                result = new byte[Length];
                int totalRead = 0;
                while (totalRead < Length) 
                {
                    int read = stream.Read(result, totalRead, (Length - totalRead));
                    if (read < 0) 
                    {
                        EndReached();
                        return null;
                    } 
                    totalRead += read;
                }
            } 
            else 
            {
                return new byte[0];
            }
            offset += Length;
            Align();
            return result;
        }
        
        public virtual double ReadDouble() /* throws IOException */
        {
            byte[] buffer = ReadBytes(8);            
            return System.BitConverter.ToDouble(buffer, 0);
        }
        
        public virtual double ReadFP16() /* throws IOException */
        {
            short value = ReadSI16();
            return value / 256.0;
        }
        
        public virtual double ReadFP32() /* throws IOException */
        {
            int value = ReadSI32();
            return value / 65536.0;
        }
        
        public virtual double ReadFPBits(int nBits) /* throws IOException */
        {
            long longNumber = ReadSignedBits(nBits);
            return longNumber / 65536.0;
        }
        
        public virtual float ReadFloat() /* throws IOException */
        {
            int value = ReadSI32();
            return BitConverter.ToSingle(BitConverter.GetBytes(value), 0);
        }
        
        public virtual float ReadFloat16() /* throws IOException */
        {
            int bits16 = ReadUI16();
            int sign = (bits16 & 32768) >> 15;
            int exponent16 = (bits16 & 31744) >> 10;
            int mantissa16 = bits16 & 1023;
            int exponent32 = 0;
            if (exponent16 != 0) 
            {
                if (exponent16 == 31) 
                {
                    exponent32 = 255;
                } 
                else 
                {
                    exponent32 = (exponent16 - 15) + 127;
                }
            } 
            int mantissa32 = mantissa16 << 13;
            int bits32 = sign << 31;
            bits32 |= exponent32 << 23;
            bits32 |= mantissa32;
            return BitConverter.ToSingle(BitConverter.GetBytes(bits32), 0);
        }
        
        public virtual short ReadSI16() /* throws IOException */
        {
            return ((short)(ReadUI16()));
        }
        
        public virtual int ReadSI32() /* throws IOException */
        {
            return ((int)(ReadUI32()));
        }
        
        public virtual byte ReadSI8() /* throws IOException */
        {
            return unchecked((byte)(ReadUI8()));
        }
        
        public virtual long ReadSignedBits(int nBits) /* throws IOException */
        {
            long result = ReadUnsignedBits(nBits);
            if ((result & (1L << (nBits - 1))) != 0) 
            {
                result |= (-1L) << nBits;
            } 
            return result;
        }
        
        public virtual String ReadString() /* throws IOException */
        {
            MemoryStream baos = new MemoryStream();
            FillBitBuffer();
            while ((bitBuffer) != 0) 
            {
                baos.WriteByte((byte)bitBuffer);
                FillBitBuffer();
            }
            byte[] buffer = baos.ToArray();
            String encoding;
            if (shiftJIS) 
            {
                encoding = "SJIS";
                throw new NotImplementedException();
            } 
            else if (ansi) 
            {
                encoding = "cp1252";
                throw new NotImplementedException();
            } 
            else 
            {
                encoding = "UTF-8";
            }
            return System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }
        
        public virtual int ReadUI16() /* throws IOException */
        {
            FillBitBuffer();
            int result = bitBuffer;
            FillBitBuffer();
            result |= (bitBuffer) << 8;
            Align();
            return result;
        }
        
        public virtual long ReadUI32() /* throws IOException */
        {
            FillBitBuffer();
            long result = bitBuffer;
            FillBitBuffer();
            result |= (bitBuffer) << 8;
            FillBitBuffer();
            result |= (bitBuffer) << 16;
            FillBitBuffer();
            result |= (bitBuffer) << 24;
            Align();
            return result;
        }
        
        public virtual long ReadEncodedU32() /* throws IOException */
        {
            FillBitBuffer();
            long result = bitBuffer;
            if ((result & 128) == 0) 
            {
                Align();
                return result;
            } 
            FillBitBuffer();
            result = (result & 127) | ((bitBuffer) << 7);
            if ((result & 16384) == 0) 
            {
                Align();
                return result;
            } 
            FillBitBuffer();
            result = (result & 16383) | ((bitBuffer) << 14);
            if ((result & 2097152) == 0) 
            {
                Align();
                return result;
            } 
            FillBitBuffer();
            result = (result & 2097151) | ((bitBuffer) << 21);
            if ((result & 268435456) == 0) 
            {
                Align();
                return result;
            } 
            FillBitBuffer();
            result = (result & 268435455) | ((bitBuffer) << 28);
            Align();
            return result;
        }
        
        public virtual short ReadUI8() /* throws IOException */
        {
            FillBitBuffer();
            short result = ((short)(bitBuffer));
            Align();
            return result;
        }
        
        public virtual long ReadUnsignedBits(int nBits) /* throws IOException */
        {
            if (nBits == 0) 
            {
                return 0;
            } 
            int bitsLeft = nBits;
            long result = 0;
            while (bitsLeft > 0) 
            {
                if ((bitCursor) == 8) 
                {
                    FillBitBuffer();
                } 
                if (((bitBuffer) & (1 << (7 - (bitCursor)))) != 0) 
                {
                    result |= 1L << (bitsLeft - 1);
                } 
                (bitCursor)++;
                bitsLeft--;
            }
            return result;
        }
        
        private void EndReached() /* throws IOException */
        {
            throw new IOException("Input data stream ended unexpectedly!");
        }
        
        private void FillBitBuffer() /* throws IOException */
        {
            bitBuffer = stream.ReadByte();
            (offset)++;
            if ((bitBuffer) < 0) 
            {
                EndReached();
            } 
            bitCursor = 0;
        }
    }
}