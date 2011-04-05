using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords
{
    /** 
     * <p>
     * A button record defines a character to be displayed in one or more button
     * states. Each button has four states:
     * 
     * <ul>
     * <li>
     * up: the initial state of the button (e.g. when the movie starts playing)
     * </li>
     * <li>
     * over: active when mouse is moved inside the button area
     * </li>
     * <li>
     * down: active when button is clicked
     * </li>
     * <li>
     * hit: invisible state, defines the area of the button that responds to the
     * mouse
     * </li>
     * </ul>
     * 
     * The state flags indicate which states the character belongs to.
     * </p>
     * 
     * <p>
     * Further, you can specify the depth the character will we displayed at, a
     * transformation matrix and a color transform.
     * </p>
     */
    public class ButtonRecord
    {
        private bool hitState;
        
        private bool downState;
        
        private bool overState;
        
        private bool upState;
        
        private int characterId;
        
        private int placeDepth;
        
        private SwfMatrix placeMatrix;
        
        private CXformWithAlpha colorTransform;
        
        private bool hasBlendMode;
        
        private bool hasFilters;
        
        private List<Filter>  filters;
        
        private short blendMode;
        
        /** 
         * Creates a new ButtonRecord instance.
         *
         * @param characterId ID of the character to be displayed
         * @param placeDepth depth the character will be displayed at
         * @param placeMatrix transformation matrix (for placement)
         * @param upState up state flag
         * @param overState over state flag
         * @param downState down state flag
         * @param hitState hit state flag
         *
         * @throws IllegalArgumentException if no state flag is set
         */
        public ButtonRecord(int characterId ,int placeDepth ,SwfMatrix placeMatrix ,bool upState ,bool overState ,bool downState ,bool hitState) 
        {
            if (!(((upState || overState) || downState) || hitState)) 
            {
                throw new ArgumentOutOfRangeException("At least one of the button state flags must be set!");
            } 
            this.characterId = characterId;
            this.placeDepth = placeDepth;
            this.placeMatrix = placeMatrix;
            this.upState = upState;
            this.overState = overState;
            this.downState = downState;
            this.hitState = hitState;
        }
        
        /** 
         * Reads a ButtonRecord from a bit stream.
         *
         * @param stream source bit stream
         * @param hasColorTransform indicates whether a color transform is present
         *
         * @throws IOException if an I/O error has occured
         */
        public ButtonRecord(InputBitStream stream ,bool hasColorTransform) /* throws IOException */ 
        {
            stream.ReadUnsignedBits(2);
            hasBlendMode = stream.ReadBooleanBit();
            hasFilters = stream.ReadBooleanBit();
            hitState = stream.ReadBooleanBit();
            downState = stream.ReadBooleanBit();
            overState = stream.ReadBooleanBit();
            upState = stream.ReadBooleanBit();
            characterId = stream.ReadUI16();
            placeDepth = stream.ReadUI16();
            placeMatrix = new SwfMatrix(stream);
            if (hasColorTransform) 
            {
                colorTransform = new CXformWithAlpha(stream);
            } 
            if (hasFilters) 
            {
                filters = Filter.ReadFilters(stream);
            } 
            if (hasBlendMode) 
            {
                blendMode = stream.ReadUI8();
                if ((blendMode) == 0) 
                {
                    blendMode = BlendMode.NORMAL;
                } 
            } 
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
        
        public virtual int GetCharacterId()
        {
            return characterId;
        }
        
        public virtual void SetColorTransform(CXformWithAlpha colorTransform)
        {
            this.colorTransform = colorTransform;
        }
        
        public virtual CXformWithAlpha GetColorTransform()
        {
            return colorTransform;
        }
        
        public virtual bool IsDownState()
        {
            return downState;
        }
        
        public virtual void SetFilters(List<Filter>  filters)
        {
            this.filters = filters;
            hasFilters = filters != null;
        }
        
        public virtual List<Filter>  GetFilters()
        {
            return filters;
        }
        
        public virtual bool IsHitState()
        {
            return hitState;
        }
        
        public virtual bool IsOverState()
        {
            return overState;
        }
        
        public virtual int GetPlaceDepth()
        {
            return placeDepth;
        }
        
        public virtual SwfMatrix GetPlaceMatrix()
        {
            return placeMatrix;
        }
        
        public virtual bool IsUpState()
        {
            return upState;
        }
        
        public virtual bool HasBlendMode()
        {
            return hasBlendMode;
        }
        
        public virtual bool HasFilters()
        {
            return hasFilters;
        }        
    }
}