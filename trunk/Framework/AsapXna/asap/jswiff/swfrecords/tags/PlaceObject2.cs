using System;

using System.Collections.Generic;


using swiff.com.jswiff.swfrecords.tags.interfaces;
using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * With this tag, an instance of a character can be added to the display list.
     * Besides, unlike <code>PlaceObject</code>, it can modify the attributes of a
     * character that is already on the display list.
     * </p>
     * 
     * <p>
     * The only mandatory attribute is the character depth. A character that is
     * already on the display list can be identified by its depth alone, as there
     * can be only one character at a given depth. If a new character is added to
     * the display list, the character ID is needed.
     * </p>
     * 
     * <p>
     * If the <code>move</code> flag is set, the character at the given depth is
     * removed. If no character ID is set, the removed character is redisplayed at
     * the same depth with new attributes. If the character ID is set, the
     * corresponding character replaces the removed one.
     * </p>
     * 
     * <p>
     * A transform matrix and a color tranform can be specified in order to
     * determine the position, rotation, scale and color of the character to be
     * displayed.
     * </p>
     * 
     * <p>
     * The morph ratio applies to characters defined with
     * <code>DefineMorphShape</code> and specifies how far the morph has progressed.
     * </p>
     * 
     * <p>
     * A (non-zero) clip depth indicates that the character is a clipping character
     * which masks depths up to and including the specified value (e.g. if a
     * character was placed at depth 1 with a clip depth of 4, all depths above 1,
     * up to and including depth 4, will be masked by the shape placed at depth 1;
     * characters placed at depths above 4 will not be masked).
     * </p>
     * 
     * <p>
     * The character instance can be given a name which it can later be referenced
     * by (e.g. within <code>With</code>).
     * </p>
     * 
     * <p>
     * Finally, if the character to be placed is a sprite, one or more event
     * handlers (clip actions) can be defined.
     * </p>
     * 
     * @see PlaceObject
     * @see DefineMorphShape
     * @since SWF 3
     */
    public class PlaceObject2 : Tag, IPlaceObject
    {
        private bool move;
        
        private int depth;
        
        private int characterId;
        
        private Matrix matrix;
        
        private CXformWithAlpha colorTransform;
        
        private int ratio;
        
        private String name;
        
        private int clipDepth;
        
        private ClipActions clipActions;
        
        private bool hasClipActions;
        
        private bool hasClipDepth;
        
        private bool hasName;
        
        private bool hasRatio;
        
        private bool hasColorTransform;
        
        private bool hasMatrix;
        
        private bool hasCharacter;
        
        /** 
         * Creates a new PlaceObject2 tag.
         * 
         * @param depth
         *            depth the character is placed at
         */
        public PlaceObject2(int depth) 
        {
            code = TagConstants.PLACE_OBJECT_2;
            this.depth = depth;
        }
        
        public PlaceObject2() 
        {
        }
        
        public virtual void SetCharacterId(int characterId)
        {
            this.characterId = characterId;
            hasCharacter = true;
        }
        
        public virtual int GetCharacterId()
        {
            return characterId;
        }
        
        public virtual void SetClipActions(ClipActions clipActions)
        {
            this.clipActions = clipActions;
            hasClipActions = clipActions != null;
        }
        
        public virtual ClipActions GetClipActions()
        {
            return clipActions;
        }
        
        public virtual void SetClipDepth(int clipDepth)
        {
            this.clipDepth = clipDepth;
            hasClipDepth = true;
        }
        
        public virtual int GetClipDepth()
        {
            return clipDepth;
        }
        
        public virtual void SetColorTransform(CXformWithAlpha colorTransform)
        {
            this.colorTransform = colorTransform;
            hasColorTransform = colorTransform != null;
        }
        
        public virtual CXformWithAlpha GetColorTransform()
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
        
        public virtual void SetMatrix(Matrix matrix)
        {
            this.matrix = matrix;
            hasMatrix = matrix != null;
        }
        
        public virtual Matrix GetMatrix()
        {
            return matrix;
        }
        
        public virtual void SetMove()
        {
            move = true;
        }
        
        public virtual void SetMove(bool move)
        {
            this.move = move;
        }
        
        public virtual bool IsMove()
        {
            return move;
        }
        
        public virtual void SetName(String name)
        {
            this.name = name;
            hasName = name != null;
        }
        
        public virtual String GetName()
        {
            return name;
        }
        
        public virtual void SetRatio(int ratio)
        {
            if (ratio < 0) 
            {
                this.ratio = 0;
            } 
            else if (ratio > 65535) 
            {
                this.ratio = 65535;
            } 
            else 
            {
                this.ratio = ratio;
            }
            hasRatio = true;
        }
        
        public virtual int GetRatio()
        {
            return ratio;
        }
        
        public virtual bool HasCharacter()
        {
            return hasCharacter;
        }
        
        public virtual bool HasClipActions()
        {
            return hasClipActions;
        }
        
        public virtual bool HasClipDepth()
        {
            return hasClipDepth;
        }
        
        public virtual bool HasColorTransform()
        {
            return hasColorTransform;
        }
        
        public virtual bool HasMatrix()
        {
            return hasMatrix;
        }
        
        public virtual bool HasName()
        {
            return hasName;
        }
        
        public virtual bool HasRatio()
        {
            return hasRatio;
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
            hasClipActions = inStream.ReadBooleanBit();
            hasClipDepth = inStream.ReadBooleanBit();
            hasName = inStream.ReadBooleanBit();
            hasRatio = inStream.ReadBooleanBit();
            hasColorTransform = inStream.ReadBooleanBit();
            hasMatrix = inStream.ReadBooleanBit();
            hasCharacter = inStream.ReadBooleanBit();
            move = inStream.ReadBooleanBit();
            depth = inStream.ReadUI16();
            if (hasCharacter) 
            {
                characterId = inStream.ReadUI16();
            } 
            if (hasMatrix) 
            {
                matrix = new Matrix(inStream);
            } 
            if (hasColorTransform) 
            {
                colorTransform = new CXformWithAlpha(inStream);
            } 
            if (hasRatio) 
            {
                ratio = inStream.ReadUI16();
            } 
            if (hasName) 
            {
                name = inStream.ReadString();
            } 
            if (hasClipDepth) 
            {
                clipDepth = inStream.ReadUI16();
            } 
            if (((GetSWFVersion()) >= 5) && (hasClipActions)) 
            {
                clipActions = new ClipActions(inStream , GetSWFVersion());
            } 
        }
        
        public override String ToString()
        {
            return (((base.ToString()) + ": depth=") + (depth)) + ((characterId) != 0 ? " characterId=" + (characterId) : "");
        }
    }
}