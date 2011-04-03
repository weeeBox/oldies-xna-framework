using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * @since SWF 8
     */
    public class FlashTypeSettings : Tag
    {
        /** 
         *
         */
        public const byte GRID_FIT_NONE = 0;
        
        /** 
         *
         */
        public const byte GRID_FIT_PIXEL = 1;
        
        /** 
         *
         */
        public const byte GRID_FIT_SUBPIXEL = 2;
        
        private int textId;
        
        private bool flashType;
        
        private byte gridFit;
        
        private float thickness;
        
        private float sharpness;
        
        /** 
         * Creates a new FlashTypeSettings instance.
         *
         * @param textId TODO: Comments
         * @param flashType TODO: Comments
         */
        public FlashTypeSettings(int textId ,bool flashType) 
        {
            code = TagConstants.FLASHTYPE_SETTINGS;
            this.textId = textId;
            this.flashType = flashType;
        }
        
        public FlashTypeSettings() 
        {
        }
        
        public virtual void SetFlashType(bool flashType)
        {
            this.flashType = flashType;
        }
        
        public virtual bool IsFlashType()
        {
            return flashType;
        }
        
        public virtual void SetGridFit(byte gridFit)
        {
            this.gridFit = gridFit;
        }
        
        public virtual byte GetGridFit()
        {
            return gridFit;
        }
        
        public virtual void SetSharpness(float sharpness)
        {
            this.sharpness = sharpness;
        }
        
        public virtual double GetSharpness()
        {
            return sharpness;
        }
        
        public virtual void SetTextId(int textId)
        {
            this.textId = textId;
        }
        
        public virtual int GetTextId()
        {
            return textId;
        }
        
        public virtual void SetThickness(float thickness)
        {
            this.thickness = thickness;
        }
        
        public virtual double GetThickness()
        {
            return thickness;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            textId = inStream.ReadUI16();
            flashType = (inStream.ReadUnsignedBits(2)) == 1;
            gridFit = unchecked((byte)(inStream.ReadUnsignedBits(3)));
            inStream.ReadUnsignedBits(3);
            thickness = inStream.ReadFloat();
            sharpness = inStream.ReadFloat();
            inStream.ReadUI8();
        }
    }
}