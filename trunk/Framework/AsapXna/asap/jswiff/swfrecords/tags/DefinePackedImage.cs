using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    public class DefinePackedImage : DefinitionTag
    {
        private int imageId;
        
        public DefinePackedImage() 
        {
            code = TagConstants.DEFINE_PACKED_IMAGE;
        }        
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream stream = new InputBitStream(data);
            characterId = stream.ReadUI16();
	    imageId = stream.ReadUI16();
        }
        
        public int ImageId
        {
            get { return imageId; }
        }
    }
}