using System;

using System.Collections.Generic;
using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * TODO: Comments
     */
    public class PlaceObject3 : Tag
    {
        private bool move;
        
        private int depth;
        
        private int characterId;
        
        private SwfMatrix matrix;
        
        private CXformWithAlpha colorTransform;
        
        private int ratio;
        
        private String name;
        
        private int clipDepth;
        
        private ClipActions clipActions;
        
        private List<Filter> filters;
        
        private short blendMode;
        
        private bool hasClipActions;
        
        private bool hasClipDepth;
        
        private bool hasName;
        
        private bool hasRatio;
        
        private bool hasColorTransform;
        
        private bool hasMatrix;
        
        private bool hasCharacter;
        
        private bool cacheAsBitmap;
        
        private bool hasBlendMode;
        
        private bool hasFilters;
        
        /** 
         * Creates a new PlaceObject3 tag.
         *
         * @param depth depth the character is placed at
         */
        public PlaceObject3(int depth) 
        {
            code = TagConstants.PLACE_OBJECT_3;
            this.depth = depth;
        }
        
        public PlaceObject3() 
        {
        }
        
        public virtual void SetBlendMode(short blendMode)
        {
            this.blendMode = blendMode;
            hasBlendMode = true;
        }
        
        public virtual short GetBlendMode()
        {
            return blendMode;
        }
        
        public virtual void SetCacheAsBitmap(bool cacheAsBitmap)
        {
            this.cacheAsBitmap = cacheAsBitmap;
        }
        
        public virtual bool IsCacheAsBitmap()
        {
            return cacheAsBitmap;
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
        
        public virtual void SetFilters(List<Filter> filters)
        {
            this.filters = filters;
            hasFilters = filters != null;
        }
        
        public virtual List<Filter> GetFilters()
        {
            return filters;
        }
        
        public virtual void SetMatrix(SwfMatrix matrix)
        {
            this.matrix = matrix;
            hasMatrix = matrix != null;
        }
        
        public virtual SwfMatrix GetMatrix()
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
        
        public virtual bool HasBlendMode()
        {
            return hasBlendMode;
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
        
        public virtual bool HasFilters()
        {
            return hasFilters;
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
            hasClipActions = inStream.ReadBooleanBit();
            hasClipDepth = inStream.ReadBooleanBit();
            hasName = inStream.ReadBooleanBit();
            hasRatio = inStream.ReadBooleanBit();
            hasColorTransform = inStream.ReadBooleanBit();
            hasMatrix = inStream.ReadBooleanBit();
            hasCharacter = inStream.ReadBooleanBit();
            move = inStream.ReadBooleanBit();
            inStream.ReadUnsignedBits(5);
            cacheAsBitmap = inStream.ReadBooleanBit();
            hasBlendMode = inStream.ReadBooleanBit();
            hasFilters = inStream.ReadBooleanBit();
            depth = inStream.ReadUI16();
            if (hasCharacter) 
            {
                characterId = inStream.ReadUI16();
            } 
            if (hasMatrix) 
            {
                matrix = new SwfMatrix(inStream);
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
            if (hasFilters) 
            {
                inStream.Align();
                filters = Filter.ReadFilters(inStream);
            } 
            if (hasBlendMode) 
            {
                blendMode = inStream.ReadUI8();
                if ((blendMode) == 0) 
                {
                    blendMode = BlendMode.NORMAL;
                } 
            } 
            if (cacheAsBitmap) 
            {
                inStream.ReadUI8();
            } 
            if (hasClipActions) 
            {
                clipActions = new ClipActions(inStream , GetSWFVersion());
            } 
        }
    }
}