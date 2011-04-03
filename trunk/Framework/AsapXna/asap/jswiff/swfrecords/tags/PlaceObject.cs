using System;

using System.Collections.Generic;


using swiff.com.jswiff.swfrecords.tags.interfaces;
using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;
using System.IO;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag adds a character instance to the display list. When a
     * <code>ShowFrame</code> tag is encountered, the instance is displayed at the
     * specified depth. A transform matrix affects the position, scale and
     * rotation of the character. A color effect can be applied by using an
     * (optional) color transform.
     *
     * @see PlaceObject2
     * @see ShowFrame
     * @since SWF 1
     */
    public class PlaceObject : Tag, IPlaceObject
    {
        private int characterId;
        
        private int depth;
        
        private Matrix matrix;
        
        private CXform colorTransform;
        
        /** 
         * Creates a new PlaceObject tag.
         *
         * @param characterId ID of the character to be placed
         * @param depth placement depth
         * @param matrix transform matrix (for translation, scaling, rotation etc.)
         * @param colorTransform color transform for color effects, optional (use
         * 		  <code>null</code> if not needed)
         */
        public PlaceObject(int characterId ,int depth ,Matrix matrix ,CXform colorTransform) 
        {
            code = TagConstants.PLACE_OBJECT;
            this.characterId = characterId;
            this.depth = depth;
            this.matrix = matrix;
            this.colorTransform = colorTransform;
        }
        
        public PlaceObject() 
        {
        }
        
        public virtual void SetCharacterId(int characterId)
        {
            this.characterId = characterId;
        }
        
        public virtual int GetCharacterId()
        {
            return characterId;
        }
        
        public virtual CXform GetColorTransform()
        {
            return colorTransform;
        }
        
        public virtual void SetDepth(int depth)
        {
            this.depth = depth;
        }
        
        public virtual int GetDepth()
        {
            return depth;
        }
        
        public virtual Matrix GetMatrix()
        {
            return matrix;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            depth = inStream.ReadUI16();
            matrix = new Matrix(inStream);
            try 
            {
                colorTransform = new CXform(inStream);
            }
            catch (IOException) 
            {
            }
        }
    }
}