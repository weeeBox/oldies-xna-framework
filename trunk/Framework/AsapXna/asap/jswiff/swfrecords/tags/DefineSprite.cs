using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using System.Diagnostics;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag defines a sprite character (i.e. a movie inside the main SWF
     * movie). It consists of a character ID, a frame count and several control
     * tags. Character instances referred to by these control tags in the sprite
     * must have been previously defined.
     * </p>
     * 
     * <p>
     * Once defined, the sprite can be displayed using the
     * <code>PlaceObject2</code> tag.
     * </p>
     *
     * @see PlaceObject2
     * @since SWF 3
     */
    public class DefineSprite : DefinitionTag
    {
        private SWFFrame[] frames;

        /** 
         * Creates a new DefineSprite tag. Supply the character ID of the sprite.
         * After tag creation, use <code>addControlTag()</code> to add tags to the
         * sprite's tag list.
         *
         * @param characterId sprite's character ID
         */
        public DefineSprite(int characterId)
        {
            code = TagConstants.DEFINE_SPRITE;
            this.characterId = characterId;
        }

        public DefineSprite()
        {
        }

        public virtual SWFFrame[] GetFrames()
        {
            return frames;
        }

        public virtual int GetFrameCount()
        {
            return frames.Length;
        }

        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            characterId = inStream.ReadUI16();
            int framesCount = inStream.ReadUI16();
            int frameIndex = 0;

            frames = new SWFFrame[framesCount];

            List<Tag> controlTags = new List<Tag>();
            do
            {
                Tag tag = TagReader.ReadTag(inStream);
                int tagCode = tag.GetCode();

                if (tagCode == TagConstants.SHOW_FRAME)
                {
                    frames[frameIndex] = SWFFrame.Create(controlTags);
                    controlTags.Clear();
                    frameIndex++;
                }
                else if (tagCode == TagConstants.END)
                {
                    Debug.Assert(frameIndex == framesCount);
                    Debug.Assert(controlTags.Count == 0);
                    break;
                }
                else
                {
                    controlTags.Add(tag);
                }
            }
            while (true);
        }
    }
}