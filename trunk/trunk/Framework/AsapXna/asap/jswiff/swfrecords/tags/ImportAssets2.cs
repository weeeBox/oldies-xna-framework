using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * TODO: Comments
     */
    public class ImportAssets2 : ImportAssets
    {
        /** 
         * Creates a new ImportAssets2 instance.
         *
         * @param url TODO: Comments
         * @param importMappings TODO: Comments
         */
        public ImportAssets2(String url ,ImportAssets.ImportMapping[] importMappings) 
         : base(url, importMappings)
        {
            code = TagConstants.IMPORT_ASSETS_2;
        }
        
        public ImportAssets2() 
        {
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            url = inStream.ReadString();
            inStream.ReadUI16();
            int count = inStream.ReadUI16();
            importMappings = new ImportAssets.ImportMapping[count];
            for (int i = 0; i < count; i++) 
            {
                int characterId = inStream.ReadUI16();
                String name = inStream.ReadString();
                importMappings[i] = new ImportAssets.ImportMapping(name , characterId);
            }
        }
    }
}