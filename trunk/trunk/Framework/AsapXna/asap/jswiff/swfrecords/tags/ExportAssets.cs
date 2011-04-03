using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag makes one or more defined characters available for import by other
     * SWF files. Exported characters can be imported with the
     * <code>ImportAssets</code> tag.
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
     * @see ImportAssets
     * @since SWF 5
     */
    public class ExportAssets : Tag
    {
        private ExportMapping[] exportMappings;
        
        /** 
         * Creates a new ExportAssets instance. Supply an array of export mappings
         * (for each  exported character one).
         *
         * @param exportMappings character export mappings
         */
        public ExportAssets(ExportMapping[] exportMappings) 
        {
            code = TagConstants.EXPORT_ASSETS;
            this.exportMappings = exportMappings;
        }
        
        public ExportAssets() 
        {
        }
        
        public virtual void SetExportMappings(ExportMapping[] exportMappings)
        {
            this.exportMappings = exportMappings;
        }
        
        public virtual ExportMapping[] GetExportMappings()
        {
            return exportMappings;
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
            int count = inStream.ReadUI16();
            exportMappings = new ExportMapping[count];
            for (int i = 0; i < count; i++) 
            {
                exportMappings[i] = new ExportMapping(inStream.ReadUI16() , inStream.ReadString());
            }
        }
        
        /** 
         * Defines an (immutable) export mapping for a character to be exported,
         * containing its ID and its export name.
         */
        public class ExportMapping
        {
            private int characterId;
            
            private String name;
            
            /** 
             * Creates a new export mapping. Supply ID of exported character and
             * export name.
             *
             * @param characterId character ID
             * @param name export name
             */
            public ExportMapping(int characterId ,String name) 
            {
                this.characterId = characterId;
                this.name = name;
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