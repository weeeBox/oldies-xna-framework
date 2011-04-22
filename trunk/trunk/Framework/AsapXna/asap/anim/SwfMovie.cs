using System;

using System.Collections.Generic;

using swiff.com.jswiff.swfrecords.tags;
using asap.anim.objects;
using asap.graphics;
using swiff.com.jswiff.swfrecords;

namespace asap.anim
{
    public class SwfMovie
    {
        private int framesCount;
        private int frameRate;

        private List<Tag> tags;
        private SwfLibrary library;        

        public SwfMovie(int framesCount, int frameRate)
        {
            this.framesCount = framesCount;
            this.frameRate = frameRate;
            tags = new List<Tag>();
            library = new SwfLibrary();
        }                

        public void AddPartset(SwfPartset partset)
        {
            library.AddPartset(partset);
        }

        public void AddDefinitionTag(DefinitionTag tag)
        {
            library.Add(tag);
        }

        public void AddControlTag(Tag tag)
        {
            tags.Add(tag);
        }

        public DefinitionTag GetCharacter(int characterId)
        {
            return library[characterId];
        }

        public List<Tag> Tags
        {
            get { return tags; }
        }

        public int FramesCount
        {
            get { return framesCount; }
        }

        public int FrameRate
        {
            get { return frameRate; }
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
                    return new BitmapInstance(image);
                }

                case TagConstants.DEFINE_SHAPE:
                {
                    DefineShape shape = (DefineShape) tag;
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
                                 return CreateInstance(bitmapId);
                            }                            
                        }
                    }
                    throw new NotImplementedException();                    
                }

                default:
                    throw new NotImplementedException(tag.ToString());
            }
        }
    }
}