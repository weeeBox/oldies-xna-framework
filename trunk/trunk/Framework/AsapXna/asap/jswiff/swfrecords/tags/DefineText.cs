using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag defines a block of static text. The bounding box of the text as
     * well as a transform matrix can be specified. The actual text characters
     * their style and position is defined in an array of <code>TextRecord</code>
     * instances.
     *
     * @see TextRecord
     * @see DefineText2
     * @since SWF 1
     */
    public class DefineText : DefinitionTag
    {
        private Rect textBounds;
        
        private Matrix textMatrix;
        
        private TextRecord[] textRecords;
        
        /** 
         * Creates a new DefineText tag. Supply text character ID, bounding box and
         * transform matrix. Specify text characters, their style and position in
         * an <code>TextRecord</code> array.
         *
         * @param characterId text character ID
         * @param textBounds bounding box of text
         * @param textMatrix transform matrix for text
         * @param textRecords <code>TextRecord</code> array containing text
         * 		  characters
         */
        public DefineText(int characterId ,Rect textBounds ,Matrix textMatrix ,TextRecord[] textRecords) 
        {
            code = TagConstants.DEFINE_TEXT;
            this.characterId = characterId;
            this.textBounds = textBounds;
            this.textMatrix = textMatrix;
            this.textRecords = textRecords;
        }
        
        public DefineText() 
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
                TextRecord record = new TextRecord(inStream , glyphBits , advanceBits , false);
                records.Add(record);
            } while (true );
            textRecords = records.ToArray();            
        }
    }
}