using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag imports one or more characters from an SWF file. The imported
     * characters must have been previously exported with an
     * <code>ExportAssets</code> tag.
     * </p>
     * 
     * <p>
     * The character IDs of the imported characters presumably differ from the IDs
     * in the exporting file, therefore the characters chosen for export are
     * identified by (unique) export names. The character IDs are mapped to names
     * within a <code>ExportAssets</code> tag (using <code>ExportMapping</code>
     * instances). After import, these names are mapped back to (different)
     * character IDs within <code>ImportAssets</code> (using
     * <code>ImportMapping</code> instances).
     * </p>
     *
     * @see ExportAssets
     * @since SWF 5
     */
    public class ImportAssets : Tag
    {
        public String url;
        
        public ImportMapping[] importMappings;
        
        /** 
         * Creates a new ImportAssets tag. Supply the URL of the exporting SWF and
         * an array of import mappings (for each imported character one).
         *
         * @param url URL of the source SWF
         * @param importMappings character import mappings
         */
        public ImportAssets(String url ,ImportMapping[] importMappings) 
        {
            code = TagConstants.IMPORT_ASSETS;
            this.url = url;
            this.importMappings = importMappings;
        }
        
        public ImportAssets() 
        {
        }
        
        public virtual void SetImportMappings(ImportMapping[] importMappings)
        {
            this.importMappings = importMappings;
        }
        
        public virtual ImportMapping[] GetImportMappings()
        {
            return importMappings;
        }
        
        public virtual void SetUrl(String url)
        {
            this.url = url;
        }
        
        public virtual String GetUrl()
        {
            return url;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            if ((GetSWFVersion()) < 6) 
            {
                if (IsJapanese()) 
                {
                    inStream.SetShiftJIS(true);
                } 
                else 
                {
                    inStream.SetANSI(true);
                }
            } 
            url = inStream.ReadString();
            int count = inStream.ReadUI16();
            importMappings = new ImportMapping[count];
            for (int i = 0; i < count; i++) 
            {
                int characterId = inStream.ReadUI16();
                String name = inStream.ReadString();
                importMappings[i] = new ImportMapping(name , characterId);
            }
        }
        
        /** 
         * Defines an (immutable) import mapping for a character, containing its
         * export name and the ID the character instance gets after import.
         */
        public class ImportMapping
        {
            private int characterId;
            
            private String name;
            
            /** 
             * Creates a new import mapping. Supply export name of character and ID
             * of imported instance.
             *
             * @param name export name of imported character
             * @param characterId imported instance ID
             */
            public ImportMapping(String name ,int characterId) 
            {
                this.name = name;
                this.characterId = characterId;
            }
            
            public virtual int GetCharacterId()
            {
                return characterId;
            }
            
            public virtual String GetName()
            {
                return name;
            }
        }
    }
}