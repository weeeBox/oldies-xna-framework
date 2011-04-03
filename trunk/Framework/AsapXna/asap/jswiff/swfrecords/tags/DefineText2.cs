using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag defines a block of static text, just like <code>DefineText</code>.
     * Unlike the older text definition tag, <code>DefineText2</code> supports
     * transparency, i.e. colors in text records within <code>DefineText2</code>
     * are <code>RGBA</code> instances.
     *
     * @see DefineText
     * @see TextRecord
     * @since SWF 3
     */
    public class DefineText2 : DefinitionTag
    {
        private Rect textBounds;
        
        private Matrix textMatrix;
        
        private TextRecord[] textRecords;
        
        /** 
         * Creates a new DefineText2 tag. Supply text character ID, bounding box
         * and transform matrix. Specify text characters, their style and position
         * in an <code>TextRecord</code> array.
         *
         * @param characterId text character ID
         * @param textBounds bounding box of text
         * @param textMatrix transform matrix for text
         * @param textRecords <code>TextRecord</code> array containing text
         * 		  characters
         */
        public DefineText2(int characterId ,Rect textBounds ,Matrix textMatrix ,TextRecord[] textRecords) 
        {
            code = TagConstants.DEFINE_TEXT_2;
            this.characterId = characterId;
            this.textBounds = textBounds;
            this.textMatrix = textMatrix;
            this.textRecords = textRecords;
        }
        
        public DefineText2() 
        {
        }
        
        public virtual void SetTextBounds(Rect textBounds)
        {
            this.textBounds = textBounds;
        }
        
        public virtual Rect GetTextBounds()
        {
            return textBounds;
        }
        
        public virtual void SetTextMatrix(Matrix textMatrix)
        {
            this.textMatrix = textMatrix;
        }
        
        public virtual Matrix GetTextMatrix()
        {
            return textMatrix;
        }
        
        public virtual void SetTextRecords(TextRecord[] textRecords)
        {
            this.textRecords = textRecords;
        }
        
        public virtual TextRecord[] GetTextRecords()
        {
            return textRecords;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            textBounds = new Rect(inStream);
            textMatrix = new Matrix(inStream);
            short glyphBits = inStream.ReadUI8();
            short advanceBits = inStream.ReadUI8();
            List<TextRecord> records = new List<TextRecord>();
            do 
            {
                if ((data[((int)(inStream.GetOffset()))]) == 0) 
                {
                    break;
                } 
                records.Add(new TextRecord(inStream , glyphBits , advanceBits , true));
            } while (true );
            textRecords = records.ToArray();            
        }
    }
}