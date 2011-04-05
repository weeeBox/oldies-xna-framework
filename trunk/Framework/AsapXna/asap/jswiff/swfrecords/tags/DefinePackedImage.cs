using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    public class DefinePackedImage : DefinitionTag
    {
        private int atlasIndex;        
        private int x;        
        private int y;        
        private int width;        
        private int height;        
        
        public DefinePackedImage() 
        {
            code = TagConstants.DEFINE_PACKED_IMAGE;
        }        
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream stream = new InputBitStream(data);
            atlasIndex = stream.ReadUI8();
            x = stream.ReadUI16();
            y = stream.ReadUI16();
            width = stream.ReadUI16();
            height = stream.ReadUI16();
        }        
    }
}