using System;
using System.Collections.Generic;
using asap.anim.objects;
using asap.graphics;
using asap.resources;
using swiff.com.jswiff;
using swiff.com.jswiff.swfrecords;
using swiff.com.jswiff.swfrecords.tags;
using System.Diagnostics;

namespace asap.anim
{
    public class SwfMovie : ManagedResource
    {
        private int width;
        private int height;
        private int framesCount;
        private int frameRate;

        private SWFFrame[] frames;
        private Dictionary<string, int> labels;

        private SwfLibrary library;        

        public SwfMovie(SWFDocument document)
        {
            this.width = document.GetWidth();
            this.height = document.GetHeight();
            this.framesCount = document.GetFrameCount();
            this.frameRate = document.GetFrameRate();
            frames = new SWFFrame[framesCount];
            library = new SwfLibrary();
            labels = new Dictionary<string, int>();
        }                

        public void AddPartset(SwfPartset partset)
        {
            library.AddPartset(partset);
        }

        public void AddNamedSymbol(string name, int characterId)
        {
            library.AddNamedSymbol(name, characterId);
        }

        public void AddDefinitionTag(DefinitionTag tag)
        {
            library.Add(tag);
        }

        public void AddLabel(string label, int frameIndex)
        {
            labels.Add(label, frameIndex);
        }

        public void SetFrameTags(List<Tag> tags, int frameIndex)
        {
            Debug.Assert(frameIndex >= 0 && frameIndex < frames.Length);
            frames[frameIndex] = SWFFrame.Create(tags);
        }

        public int GetLabelIndex(string label)
        {
            if (labels.ContainsKey(label))
                return labels[label];

            return -1;
        }

        public DefinitionTag GetCharacter(int characterId)
        {
            return library[characterId];
        }

        public SWFFrame[] Frames
        {
            get { return frames; }
        }

        public int FramesCount
        {
            get { return framesCount; }
        }

        public int FrameRate
        {
            get { return frameRate; }
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public CharacterInstance CreateInstance(int characterId)
        {
            DefinitionTag tag = library[characterId];
            switch (tag.GetCode())
            {
                case TagConstants.DEFINE_PACKED_IMAGE:
                {
                    DefinePackedImage packedImage = (DefinePackedImage)tag;
                    int imageId = packedImage.ImageId;
                    GameTexture image = library.GetImage(imageId);
                    return new BitmapInstance(tag.GetCharacterId(), image);
                }

                case TagConstants.DEFINE_SHAPE:
                {
                    DefineShape shape = (DefineShape)tag;
                    ShapeWithStyle shapes = shape.GetShapes();
                    FillStyleArray styles = shapes.GetFillStyles();
                    int stylesCount = styles.GetSize();
                    for (int styleIndex = 1; styleIndex <= stylesCount; ++styleIndex)
                    {
                        FillStyle style = styles.GetStyle(styleIndex);
                        switch (style.Type)
                        {
                            case FillStyle.TYPE_CLIPPED_BITMAP:
                            case FillStyle.TYPE_TILED_BITMAP:
                            case FillStyle.TYPE_NONSMOOTHED_CLIPPED_BITMAP:
                            case FillStyle.TYPE_NONSMOOTHED_TILED_BITMAP:
                                {
                                    int bitmapId = style.GetBitmapId();
                                    CharacterInstance bitmap = CreateInstance(bitmapId);
                                    bitmap.SetCharacterId(characterId); // replace shape with bitmap
                                    return bitmap;
                                }
                        }
                    }
                    throw new NotImplementedException();
                }
                case TagConstants.DEFINE_SHAPE_2:
                {
                    DefineShape2 shape = (DefineShape2) tag;
                    ShapeWithStyle shapes = shape.GetShapes();
                    FillStyleArray styles = shapes.GetFillStyles();
                    int stylesCount = styles.GetSize();
                    for (int styleIndex = 1; styleIndex <= stylesCount; ++styleIndex)
                    {
                        FillStyle style = styles.GetStyle(styleIndex);
                        switch (style.Type)
                        {
                            case FillStyle.TYPE_CLIPPED_BITMAP:
                            case FillStyle.TYPE_TILED_BITMAP:
                            case FillStyle.TYPE_NONSMOOTHED_CLIPPED_BITMAP:
                            case FillStyle.TYPE_NONSMOOTHED_TILED_BITMAP:
                            {
                                int bitmapId = style.GetBitmapId();
                                CharacterInstance bitmap = CreateInstance(bitmapId);
                                bitmap.SetCharacterId(characterId); // replace shape with bitmap
                                return bitmap;
                            }                            
                        }
                    }
                    throw new NotImplementedException();
                }

                case TagConstants.DEFINE_SPRITE:
                {
                    DefineSprite sprite = (DefineSprite)tag;
                    return new SpriteInstance(sprite, this);
                }                

                default:
                    throw new NotImplementedException(tag.ToString());
            }
        }

        public override void Dispose()
        {
            // TODO:
        }
    }
}