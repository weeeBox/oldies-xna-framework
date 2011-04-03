using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * @since SWF 8
     */
    public class DefineFontAlignment : Tag
    {
        /** 
         *
         */
        public const byte THIN = 0;
        
        /** 
         *
         */
        public const byte MEDIUM = 1;
        
        /** 
         *
         */
        public const byte THICK = 2;
        
        private int fontId;
        
        private byte thickness;
        
        private AlignmentZone[] alignmentZones;

        public DefineFontAlignment()
        {
        }

        /** 
         * Creates a new DefineFontAlignment instance.
         *
         * @param fontId TODO: Comments
         * @param thickness TODO: Comments
         * @param alignmentZones TODO: Comments
         */
        public DefineFontAlignment(int fontId ,byte thickness ,AlignmentZone[] alignmentZones) 
        {
            this.fontId = fontId;
            this.thickness = thickness;
            this.alignmentZones = alignmentZones;
            code = TagConstants.DEFINE_FONT_ALIGNMENT;
        }        
        
        public virtual void SetAlignmentZones(AlignmentZone[] alignmentZones)
        {
            this.alignmentZones = alignmentZones;
        }
        
        public virtual AlignmentZone[] GetAlignmentZones()
        {
            return alignmentZones;
        }
        
        public virtual void SetFontId(int fontId)
        {
            this.fontId = fontId;
        }
        
        public virtual int GetFontId()
        {
            return fontId;
        }
        
        public virtual void SetThickness(byte thickness)
        {
            this.thickness = thickness;
        }
        
        public virtual byte GetThickness()
        {
            return thickness;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            fontId = inStream.ReadUI16();
            thickness = unchecked((byte)(inStream.ReadUnsignedBits(2)));
            inStream.ReadUnsignedBits(6);
            int glyphCount = (inStream.Available()) / 10;
            alignmentZones = new AlignmentZone[glyphCount];
            for (int i = 0; i < glyphCount; i++) 
            {
                alignmentZones[i] = new AlignmentZone(inStream);
            }
        }
    }
}