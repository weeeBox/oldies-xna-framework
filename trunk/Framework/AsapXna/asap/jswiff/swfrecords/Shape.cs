using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * This class is used for defining shapes, e.g. by the <code>DefineFont</code>
     * tag in order to define the character glyphs of a font, or within a
     * <code>DefineMorphShape</code> tag. Shapes contain one or more
     * <code>ShapeRecord</code> instances which define style changes and primitives
     * as lines and curves.
     * 
     * @see com.jswiff.swfrecords.tags.DefineFont
     * @see com.jswiff.swfrecords.tags.DefineMorphShape
     */
    public class Shape
    {
        public byte numFillBits;
        
        public byte numLineBits;
        
        public ShapeRecord[] shapeRecords;
        
        /** 
         * Creates a new Shape instance.
         * 
         * @param shapeRecords
         *            an array of one or more shape records
         */
        public Shape(ShapeRecord[] shapeRecords) 
        {
            this.shapeRecords = shapeRecords;
        }
        
        public Shape(InputBitStream stream) /* throws IOException */ 
        {
            Read(stream, false, false);
        }
        
        public Shape() 
        {
        }
        
        public virtual ShapeRecord[] GetShapeRecords()
        {
            return shapeRecords;
        }
        
        public virtual void Read(InputBitStream stream, bool useNewLineStyle, bool hasAlpha) /* throws IOException */
        {
            numFillBits = unchecked((byte)(stream.ReadUnsignedBits(4)));
            numLineBits = unchecked((byte)(stream.ReadUnsignedBits(4)));
            byte currentNumFillBits = numFillBits;
            byte currentNumLineBits = numLineBits;
            List<ShapeRecord>  shapeRecordVector = new List<ShapeRecord> ();
            do 
            {
                int typeFlag = ((int)(stream.ReadUnsignedBits(1)));
                if (typeFlag == 0) 
                {
                    byte flags = unchecked((byte)(stream.ReadUnsignedBits(5)));
                    if (flags == 0) 
                    {
                        break;
                    } 
                    StyleChangeRecord record = new StyleChangeRecord(stream , flags , currentNumFillBits , currentNumLineBits , useNewLineStyle , hasAlpha);
                    currentNumFillBits = record.GetNumFillBits();
                    currentNumLineBits = record.GetNumLineBits();
                    shapeRecordVector.Add(record);
                } 
                else 
                {
                    int straightFlag = ((int)(stream.ReadUnsignedBits(1)));
                    if (straightFlag == 1) 
                    {
                        StraightEdgeRecord record = new StraightEdgeRecord(stream);
                        shapeRecordVector.Add(record);
                    } 
                    else 
                    {
                        CurvedEdgeRecord record = new CurvedEdgeRecord(stream);
                        shapeRecordVector.Add(record);
                    }
                }
            } while (true );
            stream.Align();
            shapeRecords = new ShapeRecord[shapeRecordVector.Count];
            for (int i = 0; i < (shapeRecordVector.Count); ++i) 
            {
                shapeRecords[i] = shapeRecordVector[i];
            }
        }
        
        public virtual void SetNumFillBits(byte numFillBits)
        {
            this.numFillBits = numFillBits;
        }
        
        public virtual byte GetNumFillBits()
        {
            return numFillBits;
        }
        
        public virtual void SetNumLineBits(byte numLineBits)
        {
            this.numLineBits = numLineBits;
        }
        
        public virtual byte GetNumLineBits()
        {
            return numLineBits;
        }
        
        //private void ComputeNumBits()
        //{
        //    List<ShapeRecord>  changeRecords = new List<ShapeRecord> ();
        //    for (int i = 0; i < (shapeRecords.Length); i++) 
        //    {
        //        ShapeRecord record = shapeRecords[i];
        //        if (record is StyleChangeRecord) 
        //        {
        //            changeRecords.Add(shapeRecords[i]);
        //        } 
        //    }
        //    if ((changeRecords.Count) == 0) 
        //    {
        //        return ;
        //    } 
        //    byte fillBits = 0;
        //    byte lineBits = 0;
        //    int groupStartIndex = -1;
        //    for (int i = 0; i < (changeRecords.Count); i++) 
        //    {
        //        StyleChangeRecord record = ((StyleChangeRecord)(changeRecords[i]));
        //        if (record.HasFillStyle0()) 
        //        {
        //            fillBits = unchecked((byte)(Math.Max(fillBits, OutputBitStream.GetUnsignedBitsLength(record.GetFillStyle0()))));
        //        } 
        //        if (record.HasFillStyle1()) 
        //        {
        //            fillBits = unchecked((byte)(Math.Max(fillBits, OutputBitStream.GetUnsignedBitsLength(record.GetFillStyle1()))));
        //        } 
        //        if (record.HasLineStyle()) 
        //        {
        //            lineBits = unchecked((byte)(Math.Max(lineBits, OutputBitStream.GetUnsignedBitsLength(record.GetLineStyle()))));
        //        } 
        //        if (record.HasNewStyles()) 
        //        {
        //            StoreNumBits(groupStartIndex, fillBits, lineBits, changeRecords);
        //            groupStartIndex = i;
        //            fillBits = 0;
        //            lineBits = 0;
        //        } 
        //    }
        //    StoreNumBits(groupStartIndex, fillBits, lineBits, changeRecords);
        //}
        
        private void StoreNumBits(int groupStartIndex, byte fillBits, byte lineBits, List<ShapeRecord>  changeRecords)
        {
            if (groupStartIndex > (-1)) 
            {
                StyleChangeRecord groupStartRecord = ((StyleChangeRecord)(changeRecords[groupStartIndex]));
                groupStartRecord.SetNumFillBits(fillBits);
                groupStartRecord.SetNumLineBits(lineBits);
            } 
            else 
            {
                this.numFillBits = fillBits;
                this.numLineBits = lineBits;
            }
        }
    }
}