using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * Specifies a color transform for a button defined with the
     * <code>DefineButton</code> tag.
     *
     * @since SWF 2
     */
    public class DefineButtonCXform : DefinitionTag
    {
        private CXform colorTransform;
        
        /** 
         * Creates a new DefineButtonCXform instance.
         *
         * @param characterId character ID of the button
         * @param colorTransform color transform
         */
        public DefineButtonCXform(int characterId ,CXform colorTransform) 
        {
            code = TagConstants.DEFINE_BUTTON_C_XFORM;
            this.characterId = characterId;
            this.colorTransform = colorTransform;
        }
        
        public DefineButtonCXform() 
        {
        }
        
        public virtual CXform GetColorTransform()
        {
            return colorTransform;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            colorTransform = new CXform(inStream);
        }
    }
}